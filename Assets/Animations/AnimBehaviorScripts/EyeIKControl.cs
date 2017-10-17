using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeIKControl : MonoBehaviour {

	private Animator thisAnimator;

	public bool ikActive = false;
	public Transform lookObj = null;
	// Use this for initialization

	void Start () {
		thisAnimator = GetComponent<Animator> ();
	}
	
	void OnAnimatorIK(){
	
		if (thisAnimator) {
		
		
			if (ikActive) {
			
			
				thisAnimator.SetLookAtWeight (1);
				thisAnimator.SetLookAtPosition (lookObj.position);
				//thisAnimator.SetIKRotation(
				//thisAnimator.SetIKRotation(S
					
			
			} else {
			
				thisAnimator.SetLookAtWeight (0);
			
			}
		
		
		}
	
	
	}
}
