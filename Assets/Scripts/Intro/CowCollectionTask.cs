﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowCollectionTask : CollectTaskInteraction {


	public override void CheckForTaskCompletion ()
	{
		if (CowHomeTrigger.totalCows > 0) {
			infoTextComponent.text = textAfterCompletion;
			//Debug.Log ("CowCollTrigger : " + CowHomeTrigger.totalCows);
			PlayerPrefs.SetInt(nameForPlayerPref,1);
		}
	}
	public override void OnTriggerStay(Collider other){
	
		return;
	
	}
	public override void OnTriggerEnter(Collider other){

		if (other.CompareTag ("Player")) {

			//transform.LookAt (player, Vector3.up);
		}
	}
}
