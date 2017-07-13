using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectReplay : MonoBehaviour {

	[SerializeField]private ParticleSystem particleSystemToReplay;

	void Awake(){
	//	particleSystemToReplay = GetComponent<ParticleSystem> ();
	}

	void OnEnable () {

		particleSystemToReplay.Simulate(particleSystemToReplay.main.duration);
		particleSystemToReplay.Play();
	}
	void OnDisable(){
	
		particleSystemToReplay.Clear();
	
	}
	

}
