using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stollen_Collections_Task : CollectTaskInteraction {

	[Header("Task Settings")]
	[SerializeField] private GameObject thiefGO;
	[SerializeField] private string nameOfGONeededForCompletion;

	public override void Start ()
	{
		
		if (IsTaskIdentified())
			return;
		
		thiefGO.SetActive (false);

	}
	public void OnSpeak(){

		TriggerInfo ();

		if (IsTaskCompleted())
			return;


		SaveTaskIdentified ();

		thiefGO.SetActive(true);


	}


	public override void CheckForTaskCompletion ()
	{
		foreach (GameObject GO in PlayerManager.Instance.playerItemSlotGOList) {
		
			if (string.Equals (nameOfGONeededForCompletion, GO.name, System.StringComparison.CurrentCultureIgnoreCase))
				SaveTaskCompletion();

			PlayerManager.Instance.AddItemToSlot (objectToGive);
		}
	}
}
