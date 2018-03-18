using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain_Cave_TaskCollection : CollectTaskInteraction {

	private Animator thisAnimator;

	[SerializeField] private int wackScoreToBeat;
	[SerializeField] private GameObject[] GOToActivateOnCompletion;

//	[Header("References")]
//	[SerializeField]private DataManager DATA_MANAGER;

	public override void Start(){

		thisAnimator = GetComponent<Animator> ();
	
	
	}
		
	public void OnSpeak(){

		if (PlayerPrefs.GetInt (nameForPlayerPref, 0) == 0) {
			MoveRock ();
			//infoCanvasPrefab.SetActive (true);
			CheckForTaskCompletion ();
		} else {
			MoveRock ();

			foreach (GameObject gO in GOToActivateOnCompletion)
				gO.SetActive (true);
			//infoCanvasPrefab.SetActive (true);
//FIXME commented this out for localization implementation ---			infoTextComponent.text = textAfterCompletion;
			return;
		}

	}
	public override void OnTriggerEnter(Collider other){
		onInitialInteraction.Invoke ();


		/*
		if(other.CompareTag("Player")){

			onInitialInteractionSelect.Invoke ();

			if (PlayerPrefs.GetInt (nameForPlayerPref, 0) == 0) {
				infoCanvasPrefab.SetActive (true);
				CheckForTaskCompletion ();
			} else {
				infoCanvasPrefab.SetActive (true);
				infoTextComponent.text = textAfterCompletion;
				return;
			}

		}*/
	}
	public override void OnTriggerStay(Collider other){
		return;
	}
	public override void OnTriggerExit(Collider other){
	
		if(thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("CaveDoorOpen"))
		thisAnimator.SetTrigger ("DoorClosed");
	
	}

	public override void CheckForTaskCompletion ()
	{

		SaveTaskIdentified ();

		if (DATA_MANAGER.LoadScore () > wackScoreToBeat) {
			SaveTaskCompletion ();

			foreach (GameObject gO in GOToActivateOnCompletion)
				gO.SetActive (true);
		
		}
			
	}



	public void MoveRock(){

	
		thisAnimator.SetTrigger ("DoorOpen");
		TriggerInfo ();



	}
}
