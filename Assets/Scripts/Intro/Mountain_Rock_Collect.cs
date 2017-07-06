using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain_Rock_Collect : CollectTaskInteraction {

	[SerializeField] private string nameOfItemNeeded;
	[SerializeField] private GameObject gOImagetoUnlockAndActivate;


	public override void OnTriggerEnter (Collider other)
	{
		CheckForTaskCompletion ();
		return;
	}
	public override void OnTriggerExit (Collider other)
	{
		return;
	}
	public override void OnTriggerStay (Collider other)
	{
		return;
	}
	public override void CheckForTaskCompletion ()
	{
		foreach(GameObject gO in PlayerManager.Instance.playerSlotGOList){
			Debug.Log (gO.name);
			if (string.Equals (gO.name, nameOfItemNeeded, System.StringComparison.CurrentCultureIgnoreCase))
				gOImagetoUnlockAndActivate.SetActive (true);
				


		}
	}
}
