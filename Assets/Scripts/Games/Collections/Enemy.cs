
using UnityEngine;
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


	public IEnumerator State_Idle (){
		
		currentState = EnemyState.Idle;
		canSeePlayer = false;
		yield return new WaitForSeconds (1);
		currentState = EnemyState.Search;
		StartCoroutine (State_Search ());

		}

	public IEnumerator State_Search(){
		

		int tempRotation;
		RaycastHit hit;

		while (currentState == EnemyState.Search) {

			float timeSearch = 0.0f;

			tempRotation = Random.Range (0, 360);

			thisRigidbody.MoveRotation (Quaternion.Euler (0, tempRotation, 0));


			while (timeSearch < 5.0f) {

				//thisRigidbody.MovePosition (new Vector3(0, 0, speed * Time.deltaTime));
				thisTransform.Translate (0, 0, speed * Time.deltaTime);

				float distanceSqrd = (thisTransform.position - playerTransform.position).sqrMagnitude;
			
				if (distanceSqrd < followDistance * followDistance){
					currentState = EnemyState.Chase;
					canSeePlayer = true;
				//	StopAllCoroutines ();	
					StartCoroutine (State_Chase ());
					break;
				}
					

		
				if (Physics.Raycast(thisTransform.position, thisTransform.forward, out hit, 4.0f)){
					break;
				
				}



				timeSearch += Time.deltaTime;
				yield return null;
			}
			timeSearch = 0.0f;
			yield return null;
		}
	}
	public IEnumerator State_Chase(){
		


		while (currentState == EnemyState.Chase) {

			moveDirection = (playerTransform.position - thisTransform.position).normalized;

			moveDirection *= speed * Time.deltaTime;
			moveDirection.y = 0;
			thisTransform.position += moveDirection;

			float distanceSqrd = (thisTransform.position - playerTransform.position).sqrMagnitude;

			if (distanceSqrd > followDistance * followDistance){
				currentState = EnemyState.Idle;
				canSeePlayer = false;
			//	StopAllCoroutines ();	
				StartCoroutine (State_Idle ());
				break;
			}
			if (distanceSqrd < attackDistance * attackDistance){
				currentState = EnemyState.Attack;
				canSeePlayer = true;
			//	StopAllCoroutines ();	
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

		while (currentState == EnemyState.RunAway) {

			moveDirection = (thisTransform.position - playerTransform.position).normalized;




		//	moveDirection *= speed * Time.deltaTime;

		//	thisRigidbody.MoveRotation (Quaternion.Euler (moveDirection));

			//moveDirection.y = 0;

			//test does not work in redirecting
			if(Physics.Raycast (thisTransform.position, thisTransform.forward, out hit, 10)){//,groundLM)) {

				thisRigidbody.MoveRotation (Quaternion.AngleAxis (90, thisTransform.up) * transform.rotation);
			//	changeDirection = thisTransform.eulerAngles + moveDirection /2; 
				thisRigidbody.MovePosition (thisTransform.position + (thisTransform.forward * speed * Time.deltaTime ));
				yield return null;
				continue;
			} 
		

			//thisRigidbody.MoveRotation (Quaternion.Euler (changeDirection));
		//	thisRigidbody.MovePosition (thisTransform.position + (changeDirection * speed * Time.deltaTime ));
			thisRigidbody.MovePosition (thisTransform.position + (thisTransform.forward * speed * Time.deltaTime ));

			changeDirection = Vector3.zero;

			float distanceSqrd = (thisTransform.position - playerTransform.position).sqrMagnitude;
		

			if (distanceSqrd > runAwayDistance * runAwayDistance){
				thisRenderer.material.color = originalColor;
			//	StopAllCoroutines ();	
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
