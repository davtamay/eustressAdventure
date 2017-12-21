using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowCollectionTask : CollectTaskInteraction {

	[Header("TaskSettings")]
	[SerializeField]private GameObject[] cowsNeededToCollect;

	//int cowsToCollectForCompletion;

	public void OnSpeak(){
	//	DataManager.Instance.DeletePPDataTaskProgress ();
	//	DataManager.Instance.DeleteHighScoreSlotandPositionData (player.position);

		TriggerInfo ();

		if (PlayerPrefs.GetInt (nameForPlayerPref) != 1)
			CheckForTaskCompletion ();
		else {
		
			foreach (GameObject cows in cowsNeededToCollect)
				cows.transform.position = CowHomeTrigger.thisPosition;
		
		}
	
	
	}
//	public override void OnTriggerEnter (Collider other)
//	{
//		DATA_MANAGER.SaveTaskStatus (task, Task_Status.IDENTIFIED);
//	}
	public override void CheckForTaskCompletion ()
	{
		
		if (CowHomeTrigger.totalCows == cowsNeededToCollect.Length) {
//FIXME commented this out for localization implementation ---			infoTextComponent.text = textAfterCompletion;
			//Debug.Log ("CowCollTrigger : " + CowHomeTrigger.totalCows);
			SaveTaskCompletion();

			if (objectToGive != null) 
				PlayerInventory.Instance.AddItemToSlot (objectToGive);

			UIStressGage.Instance.stress = -180;
		//	PlayerPrefs.SetInt(nameForPlayerPref,1);
		//	PlayerPrefs.Save ();
		//	QuestAssess.Instance.OnUpdate ();
		}else{
			SaveTaskIdentified ();
			//PlayerPrefs.SetInt(nameForPlayerPref,0);
			//PlayerPrefs.Save ();
			//QuestAssess.Instance.OnUpdate ();
		}
	}
//	public override void OnTriggerStay(Collider other){
//	
//		return;
//	
//	}
//	public override void OnTriggerEnter(Collider other){
//
//
//		return;
//	//	if (other.CompareTag ("Player")) {
//
//
//			//transform.LookAt (player, Vector3.up);
//	//	}
//	}
}
