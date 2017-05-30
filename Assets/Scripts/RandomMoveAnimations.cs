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

	void Update(){
		
		Vector3 dir;

		if (thisAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Walk")) {
			
			
			if (!isFirstTime) {

				curWayPoint = initialPos + Random.insideUnitSphere * distaceToSearch;
				curWayPoint.y = initialPos.y; 

				isFirstTime = true;
			}

			if (Vector3.Distance(transform.position, curWayPoint) < disUntilWayPointChange ){

				oldWayPoint = curWayPoint;
				curWayPoint = initialPos + Random.insideUnitSphere * distaceToSearch;
				curWayPoint.y = initialPos.y; 

				isTurning = true;


				return;
			

			}
	


			dir = (curWayPoint - transform.position).normalized;

			Vector3 movePosition = dir * Time.deltaTime * moveForwardSpeed;
			movePosition.y = 0;

			transform.position += movePosition;

			if (isTurning) {

				while(true) {


					rotationToLookTo = Quaternion.LookRotation (curWayPoint);


					//if(Quaternion.Angle(transform.rotation, rotationToLookTo) <= 0.2f)
					//float diff = Mathf.LerpAngle(transform.rotation.eulerAngles.y, rotationToLookTo.eulerAngles.y, 2f * Time.deltaTime);
					//transform.rotation = Quaternion.AngleAxis (diff, Vector3.up);

					//transform.eulerAngles = Vector3.Lerp (transform.rotation.eulerAngles, rotationToLookTo.eulerAngles, 2f * Time.deltaTime);
					//transform.eulerAngles = Vector3.Lerp (transform.rotation.eulerAngles, rotationToLookTo.eulerAngles, 2f * Time.deltaTime);
					transform.rotation = Quaternion.RotateTowards (transform.rotation, rotationToLookTo , 5f);//Quaternion.Lerp(transform.rotation, rotationToLookTo , 2f * Time.deltaTime);
					//Quaternion.RotateTowards (animator.transform.rotation, rotationToLookTo , 0.5f);
					if (Mathf.Abs (transform.rotation.eulerAngles.y - rotationToLookTo.eulerAngles.y) <= 1) {
						isTurning = false;
						break;
					}
					//	Debug.Log(Vector3.Dot(transform.forward.normalized, dir) );


					return;
				}
			
			
			}else
			transform.LookAt (curWayPoint,Vector3.up);

			thisAnimator.SetInteger ("RandomLook", Random.Range (0, 1000));
			
		}
			
		
		
		}

	
	
}


