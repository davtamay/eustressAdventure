using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {

	[SerializeField] float dissableTime;

	void OnEnable(){
	
		Invoke ("DisableBall", dissableTime);
	
	
	}

	void DisableBall(){

		gameObject.SetActive (false);
	
	
	}
}
