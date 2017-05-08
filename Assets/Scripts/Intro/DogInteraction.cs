using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DogInteraction : InteractionBehavior{

	private Coroutine FollowPlayer;

	[SerializeField]private float walkingSpeed;
	[SerializeField]private float maxDistanceOffset;
	[SerializeField]private float minDistanceOffset;
	[SerializeField][Range(0f,1f)]private float chanceOfJump;
	private Animator thisAnimator;
	private SkinnedMeshRenderer thisRenderer;



	public void OnTriggerEnter(){

		thisAnimator = GetComponent<Animator> ();
		thisRenderer = GetComponentInChildren<SkinnedMeshRenderer> ();

		ReachedProximity ();

		onInteraction.Invoke ();

		FollowPlayer = StartCoroutine (Follow());
		
	
	
	}

	private IEnumerator Follow(){



		while (true) {

			thisTransform.LookAt (player);
			Vector3 playerDistance = player.position - thisTransform.position;
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

			if (playerDistance.magnitude > maxDistanceOffset) {
			
				velocity = playerDirection * walkingSpeed * Time.deltaTime;
				velocity.y = 0;
				thisTransform.position += velocity;


			}

			if (velocity.magnitude > 0.1f)
				thisAnimator.SetTrigger ("Walk");
			else
				thisAnimator.SetTrigger ("Idle");




			yield return null;
		
		

		}


	}


}
