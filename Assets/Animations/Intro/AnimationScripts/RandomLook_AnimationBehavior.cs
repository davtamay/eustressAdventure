using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLook_AnimationBehavior : StateMachineBehaviour {

	[SerializeField] private Vector3 initialPos; 
	[SerializeField] private Vector3 curWayPoint;

	[SerializeField] private float moveForwardSpeed = 3f;

	[SerializeField] private float disUntilWayPointChange;
	[SerializeField] private float distaceToSearch = 10;


	[SerializeField]private bool isFirstTime;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	
		if (!isFirstTime) {
		
			initialPos = animator.transform.position;
			isFirstTime = true;
		}


	//	if (curWayPoint == Vector3.zero) {
			curWayPoint = initialPos + Random.insideUnitSphere * distaceToSearch;
			curWayPoint.y = initialPos.y; 
			animator.transform.LookAt (curWayPoint,Vector3.up);

	//	}
		
		

	}
//	bool isTurning = false;
//	Vector3 oldWayPoint;
	Quaternion rotationToLookTo;
	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {


	/*	do {

			animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, rotationToLookTo , 0.5f * Time.deltaTime);
				//Quaternion.RotateTowards (animator.transform.rotation, rotationToLookTo , 0.5f);
			if (Quaternion.Angle(animator.transform.rotation, rotationToLookTo) <= 10)
				isTurning = false;

			return;

		} while (isTurning);*/

		if (Vector3.Distance(animator.transform.position, curWayPoint) < disUntilWayPointChange ){

		//	oldWayPoint = curWayPoint;
			curWayPoint = initialPos + Random.insideUnitSphere * distaceToSearch;
			curWayPoint.y = initialPos.y; 

			//rotationToLookTo = Quaternion.LookRotation (curWayPoint);
			//Quaternion.FromToRotation (oldWayPoint, curWayPoint);
			animator.transform.LookAt (curWayPoint);
			//isTurning = true;

		}
	//	animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, rotationToLookTo , 0.5f * Time.deltaTime);
	//	Debug.Log (isTurning);
		
		

		Vector3 dir = (curWayPoint - animator.transform.position).normalized;

		Vector3 movePosition = dir * Time.deltaTime * moveForwardSpeed;
		movePosition.y = 0;

		animator.transform.position += movePosition;

		//if(animator.GetInteger("RandomLook") != null)
		//animator.SetInteger ("RandomLook", Random.Range (0, 400));
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		curWayPoint = initialPos + Random.insideUnitSphere * 10;
		curWayPoint.y = initialPos.y; 
		animator.transform.LookAt (curWayPoint);
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
