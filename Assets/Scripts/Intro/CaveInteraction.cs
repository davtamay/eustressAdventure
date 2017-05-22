using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveInteraction : InteractionBehaviour {


	private void OnTriggerEnter(Collider other){

		if(other.CompareTag("Player")){


			onInteraction.Invoke ();
		}
	}

	Animator thisAnimator;

	public void MoveRock(){

		thisAnimator = GetComponent<Animator> ();
		thisAnimator.SetTrigger ("DoorOpen");
		TriggerInfo ();
		thisAnimator.SetTrigger ("DoorClosed");


	}
}
