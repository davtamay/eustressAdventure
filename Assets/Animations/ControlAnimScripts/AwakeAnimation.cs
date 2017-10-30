using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeAnimation : MonoBehaviour {

	[SerializeField]string animParamName;
	[SerializeField]private bool isSubmitParameterWhenOnTrigger = true;

	private Animator animator;
	void Awake(){

		animator = GetComponentInChildren<Animator> ();

		if(!isSubmitParameterWhenOnTrigger)
			animator.SetTrigger (animParamName);

	}

	void OnTriggerEnter(){

		if (isSubmitParameterWhenOnTrigger) {
			
			animator.SetTrigger (animParamName);
		}
	}
}
