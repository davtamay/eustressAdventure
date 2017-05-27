using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLook_AnimationBehavior : StateMachineBehaviour {

	[SerializeField] private Vector3 initialPos; 
	[SerializeField] private Vector3 curWayPoint;
	[SerializeField] private float disUntilWayPointChange;

	[SerializeField]private bool isFirstTime;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		if (!isFirstTime) {
		
			initialPos = animator.transform.position;
			isFirstTime = true;
		}

		if (curWayPoint == Vector3.zero) {
			curWayPoint = initialPos + Random.insideUnitSphere * 10;
			curWayPoint.y = initialPos.y; 
			animator.transform.LookAt (curWayPoint,Vector3.up);

		}
		
		

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		if (Vector3.Distance(animator.transform.position, curWayPoint) < disUntilWayPointChange){

			curWayPoint = initialPos + Random.insideUnitSphere * 10;
			curWayPoint.y = initialPos.y; 
			animator.transform.LookAt (curWayPoint);

		}

		Vector3 dir = (curWayPoint - animator.transform.position);

		Vector3 movePosition = dir * Time.deltaTime * 0.5f;
		movePosition.y = 0;

		animator.transform.position += movePosition;


		animator.SetInteger ("RandomLook", Random.Range (0, 500));
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
