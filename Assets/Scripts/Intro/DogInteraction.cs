using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DogInteraction : InteractionBehaviour{

	[Header("Dog Settings")]
	private Coroutine FollowTarget;

	[SerializeField]private float walkingSpeed;
	[SerializeField]private float maxDistance;
	[SerializeField]private float slowRadius;
	[SerializeField]private float dogSpeedRotation = 1.5f;
	[SerializeField][Range(0f,1f)]private float chanceOfJump;

	public Transform dogTransform;

	private Animator thisAnimator;
	private int idleAnimHash = Animator.StringToHash("Idle");
	private int walkAnimHash = Animator.StringToHash("Walk");
	private SkinnedMeshRenderer thisRenderer;

	private bool isFollowing = false;

	public static DogInteraction curDogInteraction = null;
	//private static GameObject curDogInteraction;
	public Transform target;

	[Header("Task Settings")]
	[SerializeField] private string nameOfGONeeded = "Bone";
	[SerializeField] private GameObject objectToActivateWhenHad;



	public override void Awake(){
		
		base.Awake ();

		target = player;
		dogTransform = thisTransform;
		thisAnimator = GetComponent<Animator> ();
		thisRenderer = GetComponentInChildren<SkinnedMeshRenderer> ();
	
	}




	public void OnStart(){


		if (!curDogInteraction) {
			curDogInteraction = this;//.gameObject;
	
		}else{

			if (isFollowing) {
				StopCoroutine (curDogInteraction.transform.GetComponent<DogInteraction>().FollowTarget);
				curDogInteraction = null;
				isFollowing = false;
				return;
			}
			OnChange ();

		}
			
		onInitialInteraction.Invoke ();

		FollowTarget = StartCoroutine (Follow());
	
	}
	private void OnChange(){

		if (curDogInteraction) {
			LookInteraction tempLI = curDogInteraction.transform.Find ("ActionSelect").GetComponent<LookInteraction> (); 
			//tempLI.image.sprite = tempLI.originalSprite;
			tempLI.ChangeSprite();

			curDogInteraction.isFollowing = false;

			StopCoroutine (curDogInteraction.transform.GetComponent<DogInteraction>().FollowTarget);
			curDogInteraction = this;//.gameObject;

			//check for bugs especially for switching multiple pets
			//	if (Equals (curDogInteraction, this))
			//StopCoroutine (FollowPlayer);

		}

	}
	private void OnTriggerEnter(Collider other){


		if (other.CompareTag ("Player")) {

			foreach(GameObject gO in PlayerManager.Instance.playerItemSlotGOList){



				if (string.Equals (gO.name, nameOfGONeeded, System.StringComparison.CurrentCultureIgnoreCase))
					//onActionSelect.Invoke (); 
					objectToActivateWhenHad.SetActive(true);

				
			}

		}


	}
	private void OnTriggerExit(Collider other){

		if (!isFollowing)
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
				
				thisTransform.rotation = Quaternion.RotateTowards (thisTransform.rotation, Quaternion.LookRotation (target.position - thisTransform.position), dogSpeedRotation);



				thisAnimator.SetBool(walkAnimHash,false);
				thisAnimator.SetBool(idleAnimHash,true);
				GetComponent<RandomMoveAnimations> ().isRandomOn = false;

			}
			if (Vector3.Dot (thisTransform.forward, targetDirection) >= 0.99f || timer > 2) {
				timer = 0;
				GoToTarget ();
			}

				

		}
	
	}






	public IEnumerator Follow(){

		isFollowing = true;

		while (true) {

			GoToTarget ();

			yield return null;

		}


	}

	Vector3 targetDistance;
	float distance; 
	Vector3 targetDirection; 

	public void GoToTarget(){

		targetDistance = target.position - thisTransform.position;
		distance = targetDistance.magnitude;
		targetDirection = targetDistance.normalized;

		if (thisRenderer.isVisible) {
			if (Random.Range (0f, 100f) < chanceOfJump) {
				thisAnimator.SetTrigger ("Jump");
				if (!AudioManager.Instance.CheckIfAudioPlaying (AudioManager.AudioReferanceType._AMBIENT, "Bark"))
					AudioManager.Instance.PlayAmbientSoundAndActivate ("Bark", true, true, 5f, thisTransform);
			}


		}


		Vector3 velocity = Vector3.zero;

		if (distance > maxDistance) {

			if (distance > slowRadius)
				velocity = targetDirection * walkingSpeed * Time.deltaTime;
			else
				velocity = targetDirection *walkingSpeed * Time.deltaTime * distance/ slowRadius;
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

		thisTransform.LookAt (target); 

	}


}
