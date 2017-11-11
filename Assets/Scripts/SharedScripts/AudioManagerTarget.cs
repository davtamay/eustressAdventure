using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerTarget : MonoBehaviour {

	[SerializeField] private AudioManager.AudioReferanceType currentAudioReferance;
	[SerializeField] private string nameOfTrack = string.Empty;
//	[SerializeField] private bool isSoundRandomPitchAndVolume = false;
//	[SerializeField] private bool isReleaseFromMemory = false;
	[SerializeField] private float releaseAfterSeconds = 0f;
	[SerializeField] private Transform transformOfAudio = null;

	void Start(){
	
		if (transformOfAudio == null)
			transformOfAudio = transform;

		AudioSource tempAS;

		switch (currentAudioReferance) {

		case AudioManager.AudioReferanceType._AMBIENT:
			//AudioManager.Instance.PlayAmbientSoundAndActivate (nameOfTrack, isSoundRandomPitchAndVolume, isReleaseFromMemory, releaseAfterSeconds, transformOfAudio);
			//if use upstairs method it produces a scratching noice on begining... because being se onawwake
			tempAS = AudioManager.Instance.GetAudioSourceReferance (AudioManager.AudioReferanceType._AMBIENT, nameOfTrack);
			tempAS.Play ();

			break;

		case AudioManager.AudioReferanceType._DIRECT:

			break;
		case AudioManager.AudioReferanceType._INTERFACE:

			break;

		case AudioManager.AudioReferanceType._MUSIC:

			break;


		}
	
	}




}
