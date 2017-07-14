using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DogInteraction : InteractionBehaviour{

	private Coroutine FollowPlayer;

	[SerializeField]private float walkingSpeed;
	[SerializeField]private float maxDistance;
	[SerializeField]private float slowRadius;
	[SerializeField]private float dogSpeedRotation = 1.5f;
	[SerializeField][Range(0f,1f)]private float chanceOfJump;

	private Animator thisAnimator;
	private int idleAnimHash = Animator.StringToHash("Idle");
	private int walkAnimHash = Animator.StringToHash("Walk");
	private SkinnedMeshRenderer thisRenderer;

	private bool isFollowing = false;

	private static DogInteraction curDogInteraction = null;
	//private static GameObject curDogInteraction;

	public override void Awake(){
		//PlayerPrefs.DeleteAll ();
		base.Awake ();
		thisAnimator = GetComponent<Animator> ();
		thisRenderer = GetComponentInChildren<SkinnedMeshRenderer> ();
	
	}




	public void OnStart(){


		if (!curDogInteraction) {
			curDogInteraction = this;//.gameObject;
	
		}else{

			if (isFollowing) {
				StopCoroutine (curDogInteraction.transform.GetComponent<DogInteraction>().FollowPlayer);
				curDogInteraction = null;
				isFollowing = false;
				return;
			}
			OnChange ();

		}
			
		onInitialInteractionSelect.Invoke ();

		FollowPlayer = StartCoroutine (Follow());
	
	}
	private void OnChange(){

		if (curDogInteraction) {
			LookInteraction tempLI = curDogInteraction.transform.FindChild ("ActionSelect").GetComponent<LookInteraction> (); 
			//tempLI.image.sprite = tempLI.originalSprite;
			tempLI.ChangeSprite();

			curDogInteraction.isFollowing = false;

			StopCoroutine (curDogInteraction.transform.GetComponent<DogInteraction>().FollowPlayer);
			curDogInteraction = this;//.gameObject;

			//check for bugs especially for switching multiple pets
			//	if (Equals (curDogInteraction, this))
			//StopCoroutine (FollowPlayer);

		}

	}

	private void OnTriggerExit(Collider other){

		if (other.CompareTag ("Player")) {
			timer = 0;
			GetComponent<RandomMoveAnimations> ().isRandomOn = true;
			thisAnimator.SetBool(idleAnimHash,false);
			thisAnimator.SetBool(walkAnimHash,true);
		}

	}


	float timer = 0f;
	float timeUntilRespond = 0.5f;
	private void OnTriggerStay(Collider other){
	
		if (other.CompareTag ("Player")) {
			timer += Time.deltaTime;



			if (timer > timeUntilRespond) {
				
				thisTransform.rotation = Quaternion.RotateTowards (thisTransform.rotation, Quaternion.LookRotation (Camera.main.transform.position - thisTransform.position), dogSpeedRotation);
				//thisTransform.eulerAngles = new Vector3 (0, thisTransform.eulerAngles.y);


				thisAnimator.SetBool(walkAnimHash,false);
				thisAnimator.SetBool(idleAnimHash,true);
				GetComponent<RandomMoveAnimations> ().isRandomOn = false;

			}
			if (Vector3.Dot (thisTransform.forward, playerDirection) >= 0.99f || timer > 2) {
				timer = 0;
				GoToPlayer ();
			}

				

		}
	
	}






	private IEnumerator Follow(){

		isFollowing = true;

		while (true) {

			GoToPlayer ();

			yield return null;

		}


	}

	Vector3 playerDistance;
	float distance; 
	Vector3 playerDirection; 

	public void GoToPlayer(){

		playerDistance = player.position - thisTransform.position;
		distance = playerDistance.magnitude;
		playerDirection = playerDistance.normalized;

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



	}


}
