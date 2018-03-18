using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain_TicketTaskCollection : CollectTaskInteraction {

	[SerializeField] private string nameOfItemNeeded;
	[SerializeField] private Rigidbody gOToChangeIsKinematic;


	public override void Start ()
	{
		//PlayerPrefs.DeleteAll ();
		if (PlayerPrefs.GetInt (nameForPlayerPref) == 1) {
			gOToChangeIsKinematic.isKinematic = false;
			for (int i = 0; i < PlayerInventory.Instance.playerItemSlotGOList.Count; i++) {
				//foreach(GameObject gO in PlayerManager.Instance.playerItemSlotGOList){
				Debug.Log ("ITEMSLOTGOLIST" + i + PlayerInventory.Instance.playerItemSlotGOList [i]);
				if (string.Equals (PlayerInventory.Instance.playerItemSlotGOList [i].name, nameOfItemNeeded, System.StringComparison.CurrentCultureIgnoreCase))
					PlayerInventory.Instance.RemoveItemFromSlot (PlayerInventory.Instance.playerItemSlotGOList [i]);
			}
		}
	}
	
	public override void OnTriggerEnter (Collider other)
	{
		//CheckForTaskCompletion ();
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
		if (PlayerPrefs.GetInt (nameForPlayerPref) == 1)
			return;
		for(int i = 0; i < PlayerInventory.Instance.playerItemSlotGOList.Count; i++){
		//foreach(GameObject gO in PlayerManager.Instance.playerItemSlotGOList){
			//Debug.Log (gO.name);
			if (string.Equals (PlayerInventory.Instance.playerItemSlotGOList[i].name, nameOfItemNeeded, System.StringComparison.CurrentCultureIgnoreCase)) {

				gOToChangeIsKinematic.isKinematic = false;


				PlayerInventory.Instance.RemoveItemFromSlot (PlayerInventory.Instance.playerItemSlotGOList[i]);

				SaveTaskCompletion();
				//Debug.Log ("NAMEOFOBJECTTOBEERASED" + gO.name);




				//DataManager.Instance.SaveItemList (PlayerManager.Instance.playerSlotGOList);
				//Debug.Log ("LISTAFTERTHEFACT:" + PlayerManager.Instance.playerSlotGOList);
			}


		}
	}

	/*public void SaveTaskCompletion(){

		PlayerPrefs.SetInt (nameForPlayerPref, 1);
		PlayerPrefs.Save ();
		QuestAssess.Instance.OnUpdate ();

	}*/

	/*public void SetUpQuestUpdatePP(){

		if (PlayerPrefs.HasKey (nameForPlayerPref) == false) {
			PlayerPrefs.SetInt (nameForPlayerPref, 0);
			PlayerPrefs.Save ();
			QuestAssess.Instance.OnUpdate ();
		}

	}*/
}
