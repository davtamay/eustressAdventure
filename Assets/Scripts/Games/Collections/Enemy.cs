
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
	
	private LayerMask groundLM;

	public EnemyState currentState = EnemyState.Idle;

	void Awake(){

		groundLM = LayerMask.NameToLayer ("Ground");
		thisTransform = transform;
		thisCollider = GetComponent<Collider> ();
		thisRigidbody = GetComponent<Rigidbody> ();
		thisRenderer = GetComponent<Renderer> ();


	}

	void Start (){
		
		playerTransform = GameObject.FindWithTag ("Player").transform;
		playerManager = GameObject.FindWithTag ("Player").GetComponent <PlayerManager> ();
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
			//thisTransform.Rotate (0, tempRotation, 0);


			while (timeSearch < 5.0f) {

				//thisRigidbody.MovePosition (new Vector3(0, 0, speed * Time.deltaTime));
				thisTransform.Translate (0, 0, speed * Time.deltaTime);

				float distanceSqrd = (thisTransform.position - playerTransform.position).sqrMagnitude;
			
				if (distanceSqrd < followDistance * followDistance){
					currentState = EnemyState.Chase;
					canSeePlayer = true;
					StopAllCoroutines ();	
					StartCoroutine (State_Chase ());
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
				StopAllCoroutines ();	
				StartCoroutine (State_Idle ());
			}
			if (distanceSqrd < attackDistance * attackDistance){
				currentState = EnemyState.Attack;
				canSeePlayer = true;
				StopAllCoroutines ();	
				StartCoroutine (State_Attack ());
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

		while (currentState == EnemyState.RunAway) {

			//test does not work in redirecting
		/*	if(Physics.Raycast (thisTransform.position, thisTransform.forward, out hit, 4, groundLM)) {

				int	tempRotation = Random.Range (0, 360);

				moveDirection = new Vector3(0, tempRotation, 0);
			//	yield return null;

			}else*/
			moveDirection = (thisTransform.position - playerTransform.position).normalized;

			moveDirection *= speed * Time.deltaTime;
			moveDirection.y = 0;
			thisTransform.position += moveDirection;

		

			float distanceSqrd = (thisTransform.position - playerTransform.position).sqrMagnitude;

		

		

			if (distanceSqrd > runAwayDistance * runAwayDistance){
				thisRenderer.material.color = originalColor;
				currentState = EnemyState.Idle;
				canSeePlayer = false;
				StopAllCoroutines ();	
				StartCoroutine (State_Idle ());
			}

			if(thisCollider.bounds.Contains(playerTransform.position))
				gameObject.SetActive(false);



			
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
				StopAllCoroutines ();	
				StartCoroutine (State_Chase ());
		
			}
		}
	}

}
