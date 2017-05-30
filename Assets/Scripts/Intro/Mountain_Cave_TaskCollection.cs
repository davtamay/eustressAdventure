using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain_Cave_TaskCollection : CollectTaskInteraction {

	Animator thisAnimator;

	public override void Start(){

		thisAnimator = GetComponent<Animator> ();
	
	
	}
	public override void OnTriggerEnter(Collider other){

		if(other.CompareTag("Player")){

			onInteraction.Invoke ();

			if (PlayerPrefs.GetInt (nameForPlayerPref, 0) == 0) {
				infoCanvasPrefab.SetActive (true);
				CheckForTaskCompletion ();
			} else {
				infoCanvasPrefab.SetActive (true);
				infoTextComponent.text = textAfterCompletion;
				return;
			}

		}
	}
	public override void OnTriggerExit(Collider other){
	
		thisAnimator.SetTrigger ("DoorClosed");
	
	}
	//public override void OnTriggerStay(Collider other){


	//}



	public void MoveRock(){

	
		thisAnimator.SetTrigger ("DoorOpen");
		TriggerInfo ();



	}
}
