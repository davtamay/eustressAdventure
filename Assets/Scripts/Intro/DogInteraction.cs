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
	private SkinnedMeshRenderer thisRenderer;

	private static DogInteraction curDogInteraction = null;
	//private static GameObject curDogInteraction;






	public void OnStart(){
	
		thisAnimator = GetComponent<Animator> ();
		thisRenderer = GetComponentInChildren<SkinnedMeshRenderer> ();

		if (!curDogInteraction) {
			curDogInteraction = this;//.gameObject;
			curDogInteraction.transform.GetChild(0).GetComponent<LookInteraction> ().enabled = false;
		}else{
			

			curDogInteraction.transform.GetChild(0).GetComponent<LookInteraction> ().enabled = true;
			OnChange ();
			curDogInteraction.transform.GetChild(0).GetComponent<LookInteraction> ().enabled = false;


		}

			


		//TriggerInfo ();

		onInitialInteractionSelect.Invoke ();


		FollowPlayer = StartCoroutine (Follow());
	
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
				if (Random.Range(0f,100f) < chanceOfJump)
					thisAnimator.SetTrigger ("Jump");


			}


			//float distanceOffset = 0f;
			//Mathf.Clamp (distanceOffset, minDistanceOffset, maxDistanceOffset);
			//distanceOffset = Random.Range 
			//float distanceOffset = Mathf.Clamp(max
			Vector3 velocity = Vector3.zero;

			if (distance > maxDistance) {
			
				if (distance > slowRadius)
					velocity = playerDirection * walkingSpeed * Time.deltaTime;
				else
					velocity = playerDirection *walkingSpeed * Time.deltaTime * distance/ slowRadius;
				velocity.y = 0;
				thisTransform.position += velocity;


			}

			if (velocity.magnitude > 0.01f)
				thisAnimator.SetTrigger ("Walk");
			else
				thisAnimator.SetTrigger ("Idle");


			yield return null;
		
		

		}


	}


}
