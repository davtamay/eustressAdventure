
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public enum EnemyState {
	Idle = 11111, Search = 22222 , RunAway = 33333, Chase = 44444, Attack = 55555

}

public class Enemy : MonoBehaviour {

	Transform playerTransform;
	public float speed;
	public float deltaTimeMoveTowards;
	public float followDistance;
	public float attackDistance;
	public float runAwayDistance;
	public float attackPush;

	private bool canSeePlayer;
	private Color originalColor;
	private float originalSpeed;
	private float originalAngularSpeed;
	private float originalStopingDistance;
	private float originalAcceleration;

	Renderer thisRenderer;
	Collider thisCollider;
	Rigidbody thisRigidbody;
	private NavMeshAgent thisAgent;
	Transform thisTransform;

	PlayerManager playerManager;

	private Vector3 moveDirection; 

	public LayerMask groundLM;

	public EnemyState currentState = EnemyState.Idle;


	void Awake(){


		thisTransform = transform;
		thisCollider = GetComponent<Collider> ();
		thisRigidbody = GetComponent<Rigidbody> ();
		thisRenderer = GetComponent<Renderer> ();



	}



	void OnEnable (){

		groundLM = 1 << 8;//LayerMask.NameToLayer ("Ground");
		playerTransform = GameObject.FindWithTag ("Player").transform;
		playerManager = playerTransform.GetComponent <PlayerManager> ();
		originalColor = thisRenderer.material.color;

		StartCoroutine (State_Idle());
	}

	#region Enemy_Idle
	public IEnumerator State_Idle (){
		



		currentState = EnemyState.Idle;
		canSeePlayer = false;
		yield return new WaitForSeconds (1);
		currentState = EnemyState.Search;

		if (thisAgent == null) {
			thisAgent = GetComponent <NavMeshAgent> ();
			thisAgent.speed = 15f;
			originalSpeed = thisAgent.speed;
			thisAgent.stoppingDistance = 6;
			originalStopingDistance = thisAgent.stoppingDistance;
			originalAngularSpeed = thisAgent.angularSpeed;
			originalAcceleration = thisAgent.acceleration;
			thisAgent.baseOffset = 4.5f;

		} else {
		
			thisAgent.speed = originalSpeed;
			thisAgent.angularSpeed = originalAngularSpeed;
			thisAgent.stoppingDistance = originalStopingDistance;
		
		
		}

		StartCoroutine (State_Search ());

		}
	#endregion
	#region Enemy_Search
	public IEnumerator State_Search(){
		
		int tempWayPoint;
		int tempRotation;
		RaycastHit hit;
		//temp

		while (currentState == EnemyState.Search) {
			yield return null;

			tempWayPoint = Random.Range (0, EnemyManager.Instance.wayPoints.Length);
	

		

				if(thisAgent.remainingDistance < 3f)
				thisAgent.SetDestination (EnemyManager.Instance.wayPoints [tempWayPoint]);
		//	if (thisAgent.pathPending) 

			


				float distanceSqrd = (thisTransform.position - playerTransform.position).sqrMagnitude;
			
				if (distanceSqrd < followDistance * followDistance){
					currentState = EnemyState.Chase;
					canSeePlayer = true;

					StartCoroutine (State_Chase ());
					break;
				}
					


			//	if (Physics.Raycast(thisTransform.position, thisTransform.forward, out hit, 4.0f))
		



		
				yield return null;
			}
	
			yield return null;
	
	}

	#endregion
	#region Enemy_Chase
	public IEnumerator State_Chase(){
		


		while (currentState == EnemyState.Chase) {

			moveDirection = (playerTransform.position - thisTransform.position).normalized;

			moveDirection *= speed * Time.deltaTime;
			moveDirection.y = 0;
		//	thisTransform.position += moveDirection;

			//thisAgent.Move (moveDirection);


			thisAgent.SetDestination (playerTransform.position);
			thisAgent.stoppingDistance = 4.5f;
	

			float distanceSqrd = (thisTransform.position - playerTransform.position).sqrMagnitude;

			if (distanceSqrd > followDistance * followDistance){
				currentState = EnemyState.Idle;
				canSeePlayer = false;
			//	StopAllCoroutines ();	
				thisAgent.stoppingDistance = 0;
				StartCoroutine (State_Idle ());
				break;
			}
			if (distanceSqrd < attackDistance * attackDistance){
				currentState = EnemyState.Attack;
				canSeePlayer = true;
			//	StopAllCoroutines ();	
				thisAgent.stoppingDistance = 6;
				StartCoroutine (State_Attack ());
				break;
			}
		
			yield return null;
		}
	
	}

	#endregion
	//FIXME - Enemy becomes stuck and heads forwad check - CookBook AI
	#region Enemy_RunAway
	public void EnemyRunAway(){

		StartCoroutine (State_RunAway());
	}
	public IEnumerator State_RunAway(){



		thisRenderer.material.color = Color.blue;
		RaycastHit hit;
		Vector3 changeDirection = Vector3.zero;
	
		thisAgent.speed = 20f;
		thisAgent.angularSpeed = 20f;
		thisAgent.acceleration = 18f;
		//test
		thisAgent.stoppingDistance = 0f;
		thisAgent.autoBraking = false;


		thisAgent.isStopped = true;
		thisAgent.SetDestination (EnemyManager.Instance.GetFurthestWayPointFromPlayer (thisTransform, 120f));
		yield return new WaitUntil (() => thisAgent.pathPending == false);
		thisAgent.isStopped = false;

		while (currentState == EnemyState.RunAway) {

			float distanceSqrd = (thisTransform.position - playerTransform.position).sqrMagnitude;

	
			if (thisAgent.remainingDistance < 15f){// || thisAgent.path.status == NavMeshPathStatus.PathInvalid || thisAgent.path.status == NavMeshPathStatus.PathPartial) {
			

				thisAgent.SetDestination (EnemyManager.Instance.GetFurthestWayPointFromPlayer (thisTransform, 120f));

			}

			


			if (distanceSqrd > runAwayDistance * runAwayDistance){
				thisRenderer.material.color = originalColor;
				StartCoroutine (State_Idle ());
				yield break;
			}

			if (thisCollider.bounds.Contains (playerTransform.position)) {

				thisRenderer.material.color = originalColor;
			//	StopAllCoroutines ();	
				StartCoroutine (State_Idle ());
				EnemyManager.Instance.activeEnemies.Remove (this.gameObject);
				gameObject.SetActive (false);

				EnemyManager.Instance.SetCurrentEnemies ();

				yield break;

			}



			
			yield return null;
		}
			
	
	
	
	
	
	
	}
	#endregion
	#region Enemy_Attack
	public IEnumerator State_Attack(){
	

		while (currentState == EnemyState.Attack) {

			if (thisCollider.bounds.Contains (playerTransform.position)) {
				moveDirection = (playerTransform.position - thisTransform.position).normalized;

				thisRigidbody.AddForce (moveDirection * -attackPush, ForceMode.Impulse);

				//thisTransform.position = thisTransform.position + (moveDirection * -40);
				if(!playerManager.isInvulnerable)
				playerManager.health -= 1;

				yield return new WaitForSeconds (3);
			} else {
				currentState = EnemyState.Chase;
				canSeePlayer = true;
			//	StopAllCoroutines ();	
				StartCoroutine (State_Chase ());
				break;
		
			}
		}
	}
	#endregion

}
