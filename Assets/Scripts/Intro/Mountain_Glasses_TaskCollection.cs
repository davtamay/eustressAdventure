using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Mountain_Glasses_TaskCollection : CollectTaskInteraction {

	private Animator thisAnimator;
	private SkinnedMeshRenderer thisSkinMeshRend;

	[Header("Task Settings")]
	[SerializeField]protected Transform collectObjParent;
	[SerializeField]private GameObject glasses;

	public override void Start () {
		
		infoTextComponent.text = "BYE,BYE" + textAfterCompletion;
		thisAnimator = GetComponent<Animator> ();
		thisSkinMeshRend = GetComponentInChildren<SkinnedMeshRenderer> ();
		thisAnimator.SetBool ("Idle", false);
		thisAnimator.SetBool ("Walk", true);

		if (PlayerPrefs.GetInt (nameForPlayerPref) == 1) {
			thisParticleSystem.startColor = new ParticleSystem.MinMaxGradient (Color.green);
			infoTextComponent.text = textAfterCompletion;
			glasses.SetActive (true);
			collectObjParent.gameObject.SetActive (false);
			thisAnimator.SetBool ("IsNotLooking", true);
			infoTextComponent.text = textAfterCompletion;
			thisSkinMeshRend.SetBlendShapeWeight (0, 20);
			thisSkinMeshRend.SetBlendShapeWeight (1, 90);
			thisSkinMeshRend.SetBlendShapeWeight (3, 0);
		} //else {
			
		
	//	}
	}
	
	public override void OnTriggerEnter(Collider other){

		if (other.CompareTag ("Player")) {
			thisSkinMeshRend.SetBlendShapeWeight (1, 70);
			thisAnimator.SetBool ("Idle", true);
			thisAnimator.SetBool ("Walk", false);
			//transform.LookAt (player, Vector3.up);
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
			thisSkinMeshRend.SetBlendShapeWeight (1, 0);
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

		if (PlayerPrefs.GetInt (nameForPlayerPref) == 1)
			return;

		SaveTaskIdentified ();

		int itemsCollected = 0;
		int cCount = collectObjParent.childCount;

		foreach(GameObject gO in PlayerManager.Instance.playerItemSlotGOList){

			for(int i = 0; i < cCount; i++){

				if(string.Equals(gO.name, collectObjParent.GetChild(i).name ,System.StringComparison.CurrentCultureIgnoreCase))
					++itemsCollected;

			}
		}

		if (cCount == itemsCollected) {

			SaveTaskCompletion ();

			infoTextComponent.text = textAfterCompletion;


			for (int i = 0; i < cCount; i++) {
				PlayerManager.Instance.RemoveItemFromSlot (collectObjParent.GetChild(i).gameObject);
			}

			if (objectToGive != null) 
				PlayerManager.Instance.AddItemToSlot (objectToGive);

			UIStressGage.Instance.stress = -180;

			thisAnimator.SetBool ("IsNotLooking", true);
			thisSkinMeshRend.SetBlendShapeWeight (0, 20);
			thisSkinMeshRend.SetBlendShapeWeight (1, 50);
			thisSkinMeshRend.SetBlendShapeWeight (3, 0);
			//infoBackGround = Color.green;


			return;

		}else
			return;

	}
}
