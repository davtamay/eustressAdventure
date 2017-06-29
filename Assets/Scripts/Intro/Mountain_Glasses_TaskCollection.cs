using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain_Glasses_TaskCollection : CollectTaskInteraction {

	private Animator thisAnimator;

	// Use this for initialization
	public override void Start () {

		thisAnimator = GetComponent<Animator> ();
		thisAnimator.SetBool ("Idle", false);
		thisAnimator.SetBool ("Walk", true);

		if (PlayerPrefs.GetInt (nameForPlayerPref) == 1) {
			collectObjParent.gameObject.SetActive (false);
			thisAnimator.SetBool ("IsNotLooking", true);

			infoTextComponent.text = textAfterCompletion;
		}else
			PlayerPrefs.SetInt(nameForPlayerPref,0);
		
	}
	
	public override void OnTriggerEnter(Collider other){

		if (other.CompareTag ("Player")) {

			thisAnimator.SetBool ("Idle", true);
			thisAnimator.SetBool ("Walk", false);
			transform.LookAt (player, Vector3.up);
			/*
			if (PlayerPrefs.GetInt (nameForPlayerPref, 0) == 0) {
				infoCanvasPrefab.SetActive (true);
				CheckForTaskCompletion ();
			} else {
				infoCanvasPrefab.SetActive (true);
				return;
			}*/
		}

	}
	public override void OnTriggerExit(Collider other){

		if (other.CompareTag ("Player")) {

			thisAnimator.SetBool ("Idle", false);
			thisAnimator.SetBool ("Walk", true);

		//	infoCanvasPrefab.SetActive (false);

		}
	}

	public override void OnTriggerStay(Collider other){


		if (other.CompareTag ("Player")) {
		//	infoCanvasPrefab.transform.LookAt (2 * thisTransform.position - player.position);
			thisTransform.rotation = Quaternion.RotateTowards(thisTransform.rotation, Quaternion.LookRotation(Camera.main.transform.position - thisTransform.position),3f);
		
			thisTransform.eulerAngles = new Vector3 (0, thisTransform.eulerAngles.y, 0);
			//thisTransform.transform.LookAt (player.position);
			//thisTransform.rotation = Quaternion.RotateTowards (thisTransform.rotation, Quaternion.LookRotation (player.position), 5f);
		}

	}
	public override void CheckForTaskCompletion(){

		if (PlayerPrefs.GetInt (nameForPlayerPref, 0) == 1)
			return;

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

			thisAnimator.SetBool ("IsNotLooking", true);
			//collectObjParent.gameObject.SetActive (false);
			PlayerPrefs.SetInt(nameForPlayerPref,1);
			return;

		}else
			return;

	}
}
