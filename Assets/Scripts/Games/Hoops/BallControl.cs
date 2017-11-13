using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {

	[SerializeField] float dissableTime;
	[SerializeField] float volumeRatio;
	[SerializeField] float pitchRatio;


	void OnEnable(){
	
		Invoke ("DisableBall", dissableTime);
	
	
	}
	void OnCollisionEnter(Collision collision){

		if(collision.relativeVelocity.magnitude >2){

			var magnitude = collision.relativeVelocity.magnitude;
			var volume = Mathf.Clamp01 (magnitude / volumeRatio);
			var pitch = magnitude / pitchRatio;
			var tempAS = new AudioSource();

//			BallManager.Instance.aSHitQueue.c
//			for (int i = 0; i < 3; i++) {
//				tempAS = AudioManager.Instance.GetAudioSourceReferance (AudioManager.AudioReferanceType._DIRECT, "Fall"+i);
//
//				if (!tempAS.isPlaying)
//					break;
//			}

			tempAS.volume = volume;
			tempAS.pitch = pitch;
			tempAS.transform.position = collision.contacts [0].point;
			tempAS.Play ();

		}




	}

	void DisableBall(){

		gameObject.SetActive (false);
	
	
	}
}
