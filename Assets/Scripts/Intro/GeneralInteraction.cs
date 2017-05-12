using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralInteraction : InteractionBehavior {

	private void OnTriggerEnter(Collider other){
	
		if(other.CompareTag("Player")){

		ReachedProximity ();
		infoCanvasPrefab.transform.rotation = Quaternion.identity;//Quaternion.AngleAxis (0, Vector3.up);
		onInteraction.Invoke ();
		}
	}
}
