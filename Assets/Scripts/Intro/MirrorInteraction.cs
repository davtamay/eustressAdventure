using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorInteraction : InteractionBehavior {

	private void OnTriggerEnter(){
	
		ReachedProximity ();
		infoCanvasPrefab.transform.rotation = Quaternion.identity;//Quaternion.AngleAxis (0, Vector3.up);
		onInteraction.Invoke ();
	}
}
