using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFixBug : MonoBehaviour {

	private AudioSource gateAS;
	void Start(){
		//gateAS = AudioManager.Instance.GetAudioSourceReferance (AudioManager.AudioReferanceType._AMBIENT, "Gate");
	
	
	}
	//private bool isFirstTime = false;
	void OnTriggerEnter(){
	
	//	if (!isFirstTime) 
			GetComponent<Rigidbody> ().WakeUp ();

		AudioManager.Instance.PlayAmbientSoundAndActivate ("Gate", true, false, 0f, transform);

	//	}
	}
}
