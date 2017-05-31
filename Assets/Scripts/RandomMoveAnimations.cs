using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveAnimations : MonoBehaviour {

	Animator thisAnimator;

	[SerializeField] private Vector3 initialPos; 
	[SerializeField] private Vector3 curWayPoint;

	[SerializeField] private float moveForwardSpeed = 3f;

	[SerializeField] private float disUntilWayPointChange;
	[SerializeField] private float distaceToSearch = 10;

	[SerializeField]private bool isFirstTime;

	bool isTurning = false;
	Vector3 oldWayPoint;
	Quaternion rotationToLookTo;

	void Awake(){

		thisAnimator = GetComponent<Animator> ();
		initialPos = transform.position;
	}

	void Start(){

		StartCoroutine (OnUpdate());
	}

	IEnumerator OnUpdate(){

		while (true) {
			yield return null;
			Vector3 dir;

			if (thisAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Walk")) {
			
			
				if (!isFirstTime) {

					curWayPoint = initialPos + Random.insideUnitSphere * distaceToSearch;
					curWayPoint.y = initialPos.y; 

					isFirstTime = true;
				}

				if (Vector3.Distance (transform.position, curWayPoint) < disUntilWayPointChange) {

					//oldWayPoint = curWayPoint;
					curWayPoint = initialPos + Random.insideUnitSphere * distaceToSearch;
					curWayPoint.y = initialPos.y; 

					//isTurning = true;

					yield return StartCoroutine (Turn (curWayPoint));

					//return;
			

				}
	


				dir = (curWayPoint - transform.position).normalized;

				Vector3 movePosition = dir * Time.deltaTime * moveForwardSpeed;
				movePosition.y = 0;

				transform.position += movePosition;

				transform.LookAt (curWayPoint, Vector3.up);

				thisAnimator.SetInteger ("Random", Random.Range (0, 1000));
			
			}
			
		}
		
		}

	IEnumerator Turn(Vector3 toRotation){

		Quaternion targetRotation = Quaternion.LookRotation (toRotation - transform.position);
		float timer = 0;

		while (true) {
			timer += Time.deltaTime;
		
			transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation,3f);
		
			if (Quaternion.Dot (transform.rotation, targetRotation) >= 0.95 || timer > 7) 
				yield break;

			yield return null;
	
		}
	}

	
	
}


