using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceBerry_AnimBehavior : StateMachineBehaviour {

	//public int popupTimesForBerryReduction;
	//public int curPopUpTimes;
	public bool isFirstTime;
	public Vector3 initPos;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		if (!isFirstTime) {
			isFirstTime = true;
			initPos = animator.transform.position;
		
		}
			
		if (stateInfo.IsName ("Idle")) {
		
			animator.transform.position = initPos;
		
		}
			
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	/*override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	
		animator.SetBool("IsPopup", false);

			++curPopUpTimes;

		if (curPopUpTimes == popupTimesForBerryReduction) {
			WackGameManager.Instance.ReduceBerry (animator.transform);
			curPopUpTimes = 0;
		}
	}*/

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
