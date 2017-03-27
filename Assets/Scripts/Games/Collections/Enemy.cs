
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
			
	/*	Vector3 sourcePostion = thisTransform.position;//The position you want to place your agent
		NavMeshHit closestHit;
		if( NavMesh.SamplePosition(  sourcePostion, out closestHit, 500, 1 ) ){
			transform.position = closestHit.position;
			gameObject.AddComponent<NavMeshAgent>();
			//TODO
		}
		else
			Debug.LogError("Could not find position on NavMesh!");
*/		//thisAgent = GetComponent <UnityEngine.AI.NavMeshAgent> ();

		StartCoroutine (State_Idle());
	}


	public IEnumerator State_Idle (){


		currentState = EnemyState.Idle;
		canSeePlayer = false;
		yield return new WaitForSeconds (1);
		currentState = EnemyState.Search;

		if (thisAgent == null) {
			thisAgent = GetComponent <NavMeshAgent> ();
			thisAgent.speed = 15f;
			thisAgent.stoppingDistance = 0;
			thisAgent.baseOffset = 4.5f;
		}

		StartCoroutine (State_Search ());

		}

	public IEnumerator State_Search(){
		
		int tempWayPoint;
		int tempRotation;
		RaycastHit hit;

		while (currentState == EnemyState.Search) {

			//float timeSearch = 0.0f;

			tempWayPoint = Random.Range (0, EnemyManager.Instance.wayPoints.Length);
	

		//	while (timeSearch < 5.0f) {

				if(thisAgent.remainingDistance < 5f)
				thisAgent.SetDestination (EnemyManager.Instance.wayPoints [tempWayPoint]);
			//	if (thisAgent.pathPending)

				//test 3/20/2017
				//thisTransform.Translate (0, 0, speed * Time.deltaTime);

				float distanceSqrd = (thisTransform.position - playerTransform.position).sqrMagnitude;
			
				if (distanceSqrd < followDistance * followDistance){
					currentState = EnemyState.Chase;
					canSeePlayer = true;
				//	StopAllCoroutines ();	
					StartCoroutine (State_Chase ());
					break;
				}
					

		
				if (Physics.Raycast(thisTransform.position, thisTransform.forward, out hit, 4.0f))
			//		break;
				
			//	}



			//	timeSearch += Time.deltaTime;
				yield return null;
			}
		//	timeSearch = 0.0f;
			yield return null;
		//}
	}
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
				thisAgent.stoppingDistance = 0;
				StartCoroutine (State_Attack ());
				break;
			}
		
			yield return null;
		}
	
	}
	public void EnemyRunAway(){

		StartCoroutine (State_RunAway());
	}
	public IEnumerator State_RunAway(){



		thisRenderer.material.color = Color.blue;
		RaycastHit hit;
		Vector3 changeDirection = Vector3.zero;
	
		thisAgent.speed = 20f;

		while (currentState == EnemyState.RunAway) {

			float distanceSqrd = (thisTransform.position - playerTransform.position).sqrMagnitude;

		/*	if (Vector3.Dot (playerTransform.forward, thisTransform.forward) > 0f) {
			
				int tempWayPoint = Random.Range (0, EnemyManager.Instance.wayPoints.Length);
				thisAgent.SetDestination (EnemyManager.Instance.wayPoints [tempWayPoint]);
				yield return new WaitUntil (() => thisAgent.pathPending == false);

			}*/

			if (thisAgent.remainingDistance < 6f || thisAgent.path.status == NavMeshPathStatus.PathInvalid || thisAgent.path.status == NavMeshPathStatus.PathPartial) {
			

				thisAgent.SetDestination (EnemyManager.Instance.furthestWayPointFromPlayer (thisTransform));

				if (Vector3.Dot (playerTransform.forward, thisTransform.forward) > 0f) {

					int tempWayPoint = Random.Range (0, EnemyManager.Instance.wayPoints.Length);
					thisAgent.SetDestination (EnemyManager.Instance.wayPoints [tempWayPoint]);
					yield return new WaitUntil (() => thisAgent.pathPending == false);
				}

				yield return new WaitUntil (() => thisAgent.pathPending == false);
			}

			


			if (distanceSqrd > runAwayDistance * runAwayDistance){
				thisRenderer.material.color = originalColor;
			//	StopAllCoroutines ();	
				thisAgent.speed = 15f;
				StartCoroutine (State_Idle ());
				yield break;
			}

			if (thisCollider.bounds.Contains (playerTransform.position)) {

				thisRenderer.material.color = originalColor;
			//	StopAllCoroutines ();	
				StartCoroutine (State_Idle ());
				EnemyManager.Instance.activeEnemies.Remove (this.gameObject);
				gameObject.SetActive (false);
				yield break;

			}



			
			yield return null;
		}
			
	
	
	
	
	
	
	}
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

}
