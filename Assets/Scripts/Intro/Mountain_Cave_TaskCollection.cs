using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain_Cave_TaskCollection : CollectTaskInteraction {

	Animator thisAnimator;

	public override void Start(){

		thisAnimator = GetComponent<Animator> ();
	
	
	}
		
	public void OnSpeak(){

		if (PlayerPrefs.GetInt (nameForPlayerPref, 0) == 0) {
			MoveRock ();
			//infoCanvasPrefab.SetActive (true);
			CheckForTaskCompletion ();
		} else {
			MoveRock ();
			//infoCanvasPrefab.SetActive (true);
			infoTextComponent.text = textAfterCompletion;
			return;
		}

	}
	public override void OnTriggerEnter(Collider other){
		onInitialInteractionSelect.Invoke ();


		/*
		if(other.CompareTag("Player")){

			onInitialInteractionSelect.Invoke ();

			if (PlayerPrefs.GetInt (nameForPlayerPref, 0) == 0) {
				infoCanvasPrefab.SetActive (true);
				CheckForTaskCompletion ();
			} else {
				infoCanvasPrefab.SetActive (true);
				infoTextComponent.text = textAfterCompletion;
				return;
			}

		}*/
	}
	public override void OnTriggerStay(Collider other){
		return;
	}
	public override void OnTriggerExit(Collider other){
	
		if(thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("CaveDoorOpen"))
		thisAnimator.SetTrigger ("DoorClosed");
	
	}




	public void MoveRock(){

	
		thisAnimator.SetTrigger ("DoorOpen");
		TriggerInfo ();



	}
}
