using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralInteraction : InteractionBehaviour {

	private void OnTriggerEnter(Collider other){
	
		if(other.CompareTag("Player")){

		TriggerInfo ();
		onInteraction.Invoke ();
		}
	}
		





}
