using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain_Rock_Collect : CollectTaskInteraction {

	[SerializeField] private string nameOfItemNeeded;
	[SerializeField] private GameObject gOImagetoUnlockAndActivate;


	public override void Start ()
	{
		//PlayerPrefs.DeleteAll ();
		if (PlayerPrefs.GetInt (nameForPlayerPref) == 1)
			Destroy (transform.parent.gameObject);
	}
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
		
		foreach(GameObject gO in PlayerInventory.Instance.playerItemSlotGOList){
			//Debug.Log (gO.name);
			if (string.Equals (gO.name, nameOfItemNeeded, System.StringComparison.CurrentCultureIgnoreCase)) 
				gOImagetoUnlockAndActivate.SetActive (true);	


		}
	}

	/*public void SavePlayerPreference(){

		PlayerPrefs.SetInt (nameForPlayerPref, 1);
		PlayerPrefs.Save ();
		QuestAssess.Instance.OnUpdate ();
	
	}

	public void SetUpQuestUpdatePP(){

		if (PlayerPrefs.HasKey (nameForPlayerPref) == false) {
			PlayerPrefs.SetInt (nameForPlayerPref, 0);
			PlayerPrefs.Save ();
			QuestAssess.Instance.OnUpdate ();
		}
	
	}*/
}
