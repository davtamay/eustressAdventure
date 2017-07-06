using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFixBug : MonoBehaviour {

	//private bool isFirstTime = false;
	void OnTriggerEnter(){
	
	//	if (!isFirstTime) {
			GetComponent<Rigidbody> ().WakeUp ();
	//		isFirstTime = true;
	//	}
	}
}
