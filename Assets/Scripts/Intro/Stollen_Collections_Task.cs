using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stollen_Collections_Task : CollectTaskInteraction {

	[Header("Task Settings")]
	[SerializeField] private GameObject thiefGO;
	[SerializeField] private string nameOfGONeededForCompletion;

	private Animator thisAnimator;

	public override void Start ()
	{
		thisAnimator = GetComponent<Animator> ();
		
		if (IsTaskIdentified())
			return;

		if (IsTaskCompleted ())
			Destroy (thiefGO);
		
		thiefGO.SetActive (false);

	}
	public void OnSpeak(){

		thisAnimator.SetTrigger ("Talk");

		TriggerInfo ();

		if (IsTaskCompleted())
			return;

		CheckForTaskCompletion ();

		SaveTaskIdentified ();

		thiefGO.SetActive(true);


	}


	public override void CheckForTaskCompletion ()
	{	for(int i = 0; i < PlayerInventory.Instance.playerItemSlotGOList.Count; i++){
		//foreach (GameObject GO in PlayerManager.Instance.playerItemSlotGOList) {
		
			if (string.Equals (nameOfGONeededForCompletion, PlayerInventory.Instance.playerItemSlotGOList[i].name, System.StringComparison.CurrentCultureIgnoreCase))
				SaveTaskCompletion();

			thisAnimator.SetTrigger ("Complete");
			PlayerInventory.Instance.AddItemToSlot (objectToGive);
			//thiefGO.SetActive(true);
			Destroy (thiefGO);
		}
	}
}
