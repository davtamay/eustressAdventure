using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowMove : MonoBehaviour {

	[SerializeField] float moveSpeed = 3;



	private RandomMoveAnimations randMoveAnimScript;

	private Transform thisTransform;
	//private Animator thisAnimator;

	private int animIdleHash = Animator.StringToHash ("Idle");

	void Awake(){
	
		thisTransform = transform;
		randMoveAnimScript = GetComponent<RandomMoveAnimations> ();

	}
		
	IEnumerator startRanIEnum;
	void OnTriggerEnter(Collider other){


		if (other.CompareTag ("Player")) {
		
			if (startRanIEnum != null)
			StopCoroutine (startRanIEnum);
			randMoveAnimScript.isRandomOn = false;

		
		}

	
	}

	//Coroutine startRandomCoroutine;
	void OnTriggerExit(Collider other){


		if (other.CompareTag ("Player")) {
			
			startRanIEnum = StartRandom ();
			StartCoroutine (startRanIEnum);


		}


	}
	IEnumerator StartRandom(){
	
		yield return new WaitForSeconds (4f);

		randMoveAnimScript.isRandomOn = true;
	}


	float timer = 0;
	Vector3 oldPosition;
	void OnTriggerStay(Collider other){
	
		timer += Time.deltaTime;

		if(other.CompareTag("Player")){


			Vector3 playerRelativePos =(thisTransform.position - other.transform.position).normalized;


			playerRelativePos.y = 0;

			oldPosition = thisTransform.position;

			thisTransform.position += playerRelativePos * moveSpeed * Time.deltaTime;


		/*	if ((thisTransform.position - oldPosition).sqrMagnitude <= 0.005f)
				thisAnimator.SetBool ("Idle", true);
			else
				thisAnimator.SetBool ("Idle", false);*/

		
			if (timer >= 2f) {
				timer = 0;
				randMoveAnimScript.isRandomOn = false;
				if(startRanIEnum != null)
				StopCoroutine (startRanIEnum);
				StartCoroutine (randMoveAnimScript.Turn (2*thisTransform.position - other.transform.position));
			}

		}
	
	
	}
}
