using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowMove : MonoBehaviour {

	[SerializeField] float moveSpeed = 3;



	private RandomMoveAnimations randMoveAnimScript;

	private Transform thisTransform;
	private Animator thisAnimator;

	private int animIdleHash = Animator.StringToHash ("Idle");

	void Awake(){
	
		thisTransform = transform;
		randMoveAnimScript = GetComponent<RandomMoveAnimations> ();

	}

	void Start(){

		thisAnimator = randMoveAnimScript.thisAnimator;
	
	}

	void OnTriggerEnter(Collider other){


		if (other.CompareTag ("Player")) {
		
			randMoveAnimScript.isRandomOn = false;

		
		}

	
	}
	void OnTriggerExit(Collider other){


		if (other.CompareTag ("Player")) {
			StartCoroutine (StartRandom ());


		}


	}
	IEnumerator StartRandom(){
	
		yield return new WaitForSeconds (0.1f);
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
				StartCoroutine (randMoveAnimScript.Turn (2*thisTransform.position - other.transform.position));
			}

		}
	
	
	}
}
