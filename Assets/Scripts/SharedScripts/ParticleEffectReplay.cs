using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ParticleEffectReplay : MonoBehaviour {

	[SerializeField]private ParticleSystem particleSystemToReplay;
	[SerializeField]private bool isPlaySoundWhenEnable;
	//[SerializeField]private AudioType audioType;
	[SerializeField]private string audioGOName;

	void Awake(){
	//	particleSystemToReplay = GetComponent<ParticleSystem> ();
	}

	void OnEnable () {

		if (isPlaySoundWhenEnable) {
			if (!AudioManager.Instance.CheckIfAudioPlaying (AudioManager.AudioReferanceType._AMBIENT, audioGOName))
				AudioManager.Instance.PlayAmbientSoundAndActivate (audioGOName, true, true, 5f, transform);

		}

		particleSystemToReplay.Simulate(particleSystemToReplay.main.duration);
		particleSystemToReplay.Play();
	}
	void OnDisable(){
	
		particleSystemToReplay.Clear();
	
	}
	

}
