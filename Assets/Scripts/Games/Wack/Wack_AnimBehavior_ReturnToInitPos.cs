using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wack_AnimBehavior_ReturnToInitPos : StateMachineBehaviour {

	[SerializeField] private float initPosRandomOffsetMinLimits;
	[SerializeField] private float initPosRandomOffsetMaxLimits;

	[SerializeField] private bool isAppearOnClosestBranchWithBerries;

	[SerializeField] private float timeUntilOneLessBerry;

	public bool isFirstTime;
	public Vector3 initPos;

	public Transform closestBush;

	public float timer;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (stateInfo.IsName ("PopUp"))
			timer = 0;

		if (!isFirstTime) {
			isFirstTime = true;
			initPos = animator.transform.parent.position;
		
		}
			
		if (stateInfo.IsName ("Idle")) {

			float randomX = Random.Range(initPosRandomOffsetMinLimits, initPosRandomOffsetMaxLimits);
			float randomZ = Random.Range(initPosRandomOffsetMinLimits, initPosRandomOffsetMaxLimits);

			Vector3 initTo;
			closestBush = null;
			if (isAppearOnClosestBranchWithBerries) {
				//closestBush = WackGameManager.Instance.GetClosestBush (animator.transform.parent);
				float closestBushDistance = Mathf.Infinity;

				foreach (Transform bs in WackGameManager.Instance.totalBranches) {

					if (Vector3.Distance (animator.transform.parent.position, bs.position) < closestBushDistance) {

						if (!WackGameManager.Instance.BranchHasBerries (bs))
							continue;

						closestBush = bs;
						closestBushDistance = Vector3.Distance (animator.transform.parent.position, bs.position);

					}

				}
				if (closestBush != null)
					initTo = closestBush.position + new Vector3 (randomX, 0, randomZ);
				else
					initTo = WackGameManager.Instance.totalBranches [0].position;
			}else
				initTo =  WackGameManager.Instance.centerPos.position + new Vector3 (randomX, 0, randomZ);




			initTo.y = animator.transform.parent.position.y;

			animator.transform.parent.position = initTo;

		
		}
			
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (stateInfo.IsName ("PopUp")) {
			
				timer += Time.deltaTime;

		//	if(true){
				if (timer > timeUntilOneLessBerry) {
					closestBush = WackGameManager.Instance.GetClosestBush (animator.transform);
					WackGameManager.Instance.ReduceBerry (closestBush);
					
					timer = 0;
				}

			}
	
	}

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
