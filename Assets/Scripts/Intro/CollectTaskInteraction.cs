using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTaskInteraction : InteractionBehaviour {


	[SerializeField]protected Transform collectObjParent;
	[TextArea(0,15)][SerializeField]protected string textAfterCompletion;
	[SerializeField]protected GameObject objectToGive;
	[SerializeField]protected string nameForPlayerPref;

	public virtual void Start(){


		if (PlayerPrefs.GetInt (nameForPlayerPref) == 1) {
			collectObjParent.gameObject.SetActive (false);

			infoTextComponent.text = textAfterCompletion;
		}//else if( PlayerPrefs.GetInt (nameForPlayerPref) == 0)
		//	PlayerPrefs.SetInt(nameForPlayerPref,0);
		//else if(PlayerPrefs.HasKey(!nameForPlayerPref)
			
	}

	public virtual void OnTriggerEnter(Collider other){
	
		if (other.CompareTag ("Player")) {

			if (PlayerPrefs.GetInt (nameForPlayerPref, 0) == 0) {
				CheckForTaskCompletion ();
			} else 
				return;
			
		}

	}
	public virtual void OnTriggerExit(Collider other){

		if (other.CompareTag ("Player")) {

			infoCanvasPrefab.SetActive (false);

		}
	}

	public virtual void OnTriggerStay(Collider other){

	
		if (other.CompareTag ("Player")) {
			infoCanvasPrefab.transform.LookAt (2 * thisTransform.position - player.position);
			//thisTransform.transform.LookAt (player.position);
			//thisTransform.rotation = Quaternion.RotateTowards (thisTransform.rotation, Quaternion.LookRotation (player.position), 5f);
		}
	
	}

	public virtual void CheckForTaskCompletion(){
	
		int itemsCollected = 0;
		int cCount = collectObjParent.childCount;

		foreach(GameObject gO in PlayerManager.Instance.playerItemSlotGOList){
			
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
				
			collectObjParent.gameObject.SetActive (false);
			PlayerPrefs.SetInt(nameForPlayerPref,1);
			return;

		}else
			return;
	
	}
	public void SaveTaskIdentified(){

		if (PlayerPrefs.HasKey (nameForPlayerPref) == false) {
			PlayerPrefs.SetInt (nameForPlayerPref, 0);
			PlayerPrefs.Save ();
			QuestAssess.Instance.OnUpdate ();
		}

	}
	public void SaveTaskCompletion(){

		PlayerPrefs.SetInt (nameForPlayerPref, 1);
		PlayerPrefs.Save ();
		QuestAssess.Instance.OnUpdate ();

	}

}
