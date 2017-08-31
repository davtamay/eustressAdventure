using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowMove : MonoBehaviour {

	[SerializeField] float moveSpeed = 3;
	[SerializeField] float timeUntilRestartSearch = 2f;



	private RandomMoveAnimations randMoveAnimScript;

	private Transform thisTransform;
	private Rigidbody thisRigidBody;
	private Animator thisAnimator;

	private int randAnimHash = Animator.StringToHash("Random");
	private int animIdleHash = Animator.StringToHash ("Idle");

	void Awake(){
	
		thisTransform = transform;
		//thisRigidBody = GetComponent<Rigidbody> ();
		thisAnimator = GetComponent<Animator>();
		randMoveAnimScript = GetComponent<RandomMoveAnimations> ();


	}
		
	IEnumerator startRanIEnum;
	void OnTriggerEnter(Collider other){


		if (other.CompareTag ("Player")) {

			//thisAnimator.SetBool (animIdleHash, false);
			//CrossFade("Walk",4f);
			StopAllCoroutines ();
			randMoveAnimScript.isRandomOn = false;
		}
		/*	if (startRanIEnum != null)
			StopCoroutine (startRanIEnum);


		
		}*/

	
	}

	//Coroutine startRandomCoroutine;
	void OnTriggerExit(Collider other){


		if (other.CompareTag ("Player")) {
			thisAnimator.SetBool (animIdleHash, true);
			//thisAnimator.CrossFade(animIdleHash, 3f);
			startRanIEnum = StartRandom ();
			StartCoroutine (startRanIEnum);


		}


	}
	IEnumerator StartRandom(){
	
		yield return new WaitForSeconds (timeUntilRestartSearch);
		thisAnimator.SetBool (animIdleHash, false);
		randMoveAnimScript.isRandomOn = true;
	}


	float timer = 0;
	Vector3 oldPosition;
	//To stop cow from going thru fence?
	/*void OnColliderStay(Collision coll){
	
		thisTransform.position += 
	}*/
//	Vector3 velocity;
	void OnTriggerStay(Collider other){
	
		timer += Time.deltaTime;

		if(other.CompareTag("Player")){

			thisAnimator.SetInteger (randAnimHash, Random.Range (0, randMoveAnimScript.randomAnimationParameterChance));
			thisAnimator.SetBool (animIdleHash, false);

			Vector3 playerRelativePos =(thisTransform.position - other.transform.position).normalized;


			playerRelativePos.y = 0;

			thisTransform.position += playerRelativePos * moveSpeed * Time.deltaTime;
	
		
			if (timer >= 0.7f) {
				timer = 0;
				randMoveAnimScript.isRandomOn = false;
				StopAllCoroutines();
				StartCoroutine (randMoveAnimScript.Turn (2*thisTransform.position - other.transform.position));
			}




		}
	
	
	}
}
