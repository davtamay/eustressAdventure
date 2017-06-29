using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DogInteraction : InteractionBehaviour{

	private Coroutine FollowPlayer;

	[SerializeField]private float walkingSpeed;
	[SerializeField]private float maxDistance;
	[SerializeField]private float slowRadius;
	[SerializeField][Range(0f,1f)]private float chanceOfJump;

	private Animator thisAnimator;
	private int idleAnimHash = Animator.StringToHash("Idle");
	private int walkAnimHash = Animator.StringToHash("Walk");
	private SkinnedMeshRenderer thisRenderer;

	private static DogInteraction curDogInteraction = null;
	//private static GameObject curDogInteraction;

	void Awake(){

		base.Awake ();
		thisAnimator = GetComponent<Animator> ();
		thisRenderer = GetComponentInChildren<SkinnedMeshRenderer> ();
	
	}




	public void OnStart(){
	


		if (!curDogInteraction) {
			curDogInteraction = this;//.gameObject;
	//		curDogInteraction.transform.GetChild(0).GetComponent<LookInteraction> ().enabled = false;
		}else{
			
	//		curDogInteraction.transform.GetChild(0).GetComponent<LookInteraction> ().enabled = true;
			OnChange ();
	//		curDogInteraction.transform.GetChild(0).GetComponent<LookInteraction> ().enabled = false;


		}

			
		onInitialInteractionSelect.Invoke ();


		FollowPlayer = StartCoroutine (Follow());
	
	}

	private void OnTriggerExit(Collider other){

		if (other.CompareTag ("Player")) {
		
			GetComponent<RandomMoveAnimations> ().isRandomOn = true;
			thisAnimator.SetBool(idleAnimHash,false);
			thisAnimator.SetBool(walkAnimHash,true);
		}

	}


	float timer = 0f;
	float timeUntilRespond = 3;
	private void OnTriggerStay(Collider other){
	
		if (other.CompareTag ("Player")) {
			timer += Time.deltaTime;



			if (timer > timeUntilRespond) {
				
				thisTransform.rotation = Quaternion.RotateTowards (thisTransform.rotation, Quaternion.LookRotation (Camera.main.transform.position - thisTransform.position), 1f);

				Vector3 dir = (player.position - thisTransform.position).normalized;
				if(Vector3.Dot(transform.forward, dir) >= 0.99f || timer > 10)
				timer = 0;

				thisAnimator.SetBool(walkAnimHash,false);
				thisAnimator.SetBool(idleAnimHash,true);
				GetComponent<RandomMoveAnimations> ().isRandomOn = false;

			
			
			}
				

		}
	
	}

	private void OnChange(){
	
		if (curDogInteraction) {

			//StopCoroutine (curDogInteraction.transform.parent.GetComponent<DogInteraction>().FollowPlayer);
			StopCoroutine (curDogInteraction.transform.GetComponent<DogInteraction>().FollowPlayer);
			curDogInteraction = this;//.gameObject;

			//check for bugs especially for switching multiple pets
		//	if (Equals (curDogInteraction, this))
			//StopCoroutine (FollowPlayer);

		}
	
	}

/*	public void Stop(){
	
		StopCoroutine ("FollowPlayer");
	}*/


	private IEnumerator Follow(){

		while (true) {

			thisTransform.LookAt (player);
			Vector3 playerDistance = player.position - thisTransform.position;
			float distance = playerDistance.magnitude;
			Vector3 playerDirection = playerDistance.normalized;

			if (thisRenderer.isVisible) {
				if (Random.Range (0f, 100f) < chanceOfJump) 
					thisAnimator.SetTrigger ("Jump");
				


			}


			Vector3 velocity = Vector3.zero;

			if (distance > maxDistance) {
			
				if (distance > slowRadius)
					velocity = playerDirection * walkingSpeed * Time.deltaTime;
				else
					velocity = playerDirection *walkingSpeed * Time.deltaTime * distance/ slowRadius;
				velocity.y = 0;
				thisTransform.position += velocity;


			}

			if (velocity.magnitude > 0.01f) {
				thisAnimator.SetBool ("Walk",true);
				thisAnimator.SetBool ("Idle",false);
			} else {
				thisAnimator.SetBool("Idle",true);
				thisAnimator.SetBool ("Walk",false);
			}

			yield return null;
		
		

		}


	}


}
