using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAnimParamOffAndOn : MonoBehaviour {

	[SerializeField]string nameOfParameter;

	private Animator thisAnimator;


	void Awake(){

		thisAnimator = transform.parent.GetComponentInParent<Animator> (); 

	}

	void OnEnable(){

		thisAnimator.SetBool (nameOfParameter, true);


	}

	void OnDisable(){

		thisAnimator.SetBool (nameOfParameter, false);

	
	}

}
