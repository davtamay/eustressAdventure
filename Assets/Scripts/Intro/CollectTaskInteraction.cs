using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTaskInteraction : InteractionBehaviour {

	//[SerializeField]private List<GameObject> gOsToCollect;
	[SerializeField]private Transform collectObjParent;
	[SerializeField]private string textAfterCompletion;
	[SerializeField]private GameObject objectToGive;
	[SerializeField]private string nameForPlayerPref;
	void Start(){

		if (PlayerPrefs.GetInt (nameForPlayerPref, 0) == 1) {
			collectObjParent.gameObject.SetActive (false);

			infoTextComponent.text = textAfterCompletion;
		}
			
	}

	void OnTriggerEnter(Collider other){
	
		if (other.CompareTag ("Player")) {

			if (PlayerPrefs.GetInt (nameForPlayerPref, 0) == 0) {
				infoCanvasPrefab.SetActive (true);
				CheckForTaskCompletion ();
			} else {
				infoCanvasPrefab.SetActive (true);
				return;
			}
		}

	}
	void OnTriggerExit(Collider other){

		if (other.CompareTag ("Player"))
			infoCanvasPrefab.SetActive (false);
	}

	void OnTriggerStay(Collider other){

	
		if (other.CompareTag ("Player"))
			infoCanvasPrefab.transform.LookAt (2* thisTransform.position - player.position);
	
	}

	public void CheckForTaskCompletion(){
	
		int itemsCollected = 0;
		int cCount = collectObjParent.childCount;

		foreach(GameObject gO in PlayerManager.Instance.playerSlotGOList){
			
			for(int i = 0; i < cCount; i++){
				
				if(string.Equals(gO.name, collectObjParent.GetChild(i).name ,System.StringComparison.CurrentCultureIgnoreCase))
					++itemsCollected;
					
				}
			}

	//	Debug.Log("PlayerSlotUSED:" + PlayerManager.Instance.playerSlotGOList.Count + " GO TO Collect: " + gOsToCollect.Count + " ItemsCollected:" + itemsCollected);
		if (cCount == itemsCollected) {
			for (int i = 0; i < cCount; i++) {
				PlayerManager.Instance.RemoveItemFromSlot (collectObjParent.GetChild(i).gameObject);
				infoTextComponent.text = textAfterCompletion;
				if(objectToGive != null)
				PlayerManager.Instance.AddItemToSlot (objectToGive);
			}

			//collectObjParent.gameObject.SetActive (false);
			PlayerPrefs.SetInt(nameForPlayerPref,1);
			return;

		}else
			return;
	
	}

}
