﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameSelection : MonoBehaviour {

	private ParticleSystem.MainModule particleSys;


	[SerializeField] private string gameSceneName;
	[SerializeField] private float timeUntilSceneChange;

	//Not working with ParticleSystem.MainModule

	//[SerializeField] private Color stepOutColor;
	//[SerializeField] private Color stepInColor;



	void Awake(){
	
		particleSys = GetComponent <ParticleSystem> ().main;
		particleSys.startColor = new ParticleSystem.MinMaxGradient (Color.green);
	}


	void OnTriggerEnter (Collider other){
	
	
		if (other.CompareTag ("Player")) {

		
			particleSys.startColor = new ParticleSystem.MinMaxGradient(Color.blue);


				
		}
	
	
	}

	private float timer = 0;

	void OnTriggerStay(Collider other){

		if (other.CompareTag ("Player")) {
		
			timer += Time.deltaTime;

			if (timer > timeUntilSceneChange) {
			
				SceneController.Instance.Load (gameSceneName);
				timer = 0;
			
			
			}

		
		}


	}

	void OnTriggerExit(Collider other){
	

		if (other.CompareTag ("Player")) {

			timer = 0;
			particleSys.startColor = new ParticleSystem.MinMaxGradient(Color.green);
		}
	

	
	}





}