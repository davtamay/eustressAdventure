using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeAnimation : MonoBehaviour {

	[SerializeField]string animParamName;

	void OnTriggerEnter(){

		Animator animator = GetComponentInChildren<Animator> ();
		animator.SetTrigger (animParamName);
	
	}
}
