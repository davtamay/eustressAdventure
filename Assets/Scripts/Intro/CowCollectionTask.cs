using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowCollectionTask : CollectTaskInteraction {


	public override void CheckForTaskCompletion ()
	{
		
		if (CowHomeTrigger.totalCows > 0) {
			infoTextComponent.text = textAfterCompletion;
			//Debug.Log ("CowCollTrigger : " + CowHomeTrigger.totalCows);
			PlayerPrefs.SetInt(nameForPlayerPref,1);
			PlayerPrefs.Save ();
			QuestAssess.Instance.OnUpdate ();
		}else{
			PlayerPrefs.SetInt(nameForPlayerPref,0);
			PlayerPrefs.Save ();
			QuestAssess.Instance.OnUpdate ();
		}
	}
	public override void OnTriggerStay(Collider other){
	
		return;
	
	}
	public override void OnTriggerEnter(Collider other){

		return;
	//	if (other.CompareTag ("Player")) {


			//transform.LookAt (player, Vector3.up);
	//	}
	}
}
