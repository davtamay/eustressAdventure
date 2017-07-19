using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain_Injuredtask : CollectTaskInteraction {


	//[SerializeField] private string nameOfItemNeeded;
	//[SerializeField] private GameObject gOImagetoUnlockAndActivate;
	[SerializeField] int scoreNeededForTask;
	[Header("Movement For Head Settings")]
	[SerializeField] Transform headBone;
	[SerializeField]float weight = 1f;
	[SerializeField]float dampTime;
	[SerializeField]float maxAngle;
	[SerializeField]Vector3 additionalRotation;
	[SerializeField] bool isLooking = false;
	private Animator thisAnimator;

	public override void Awake ()
	{
		thisAnimator = GetComponent<Animator> ();

		base.Awake();
		//PlayerPrefs.DeleteAll ();
		//if (PlayerPrefs.GetInt (nameForPlayerPref) == 1)

	}

	public override void OnTriggerEnter (Collider other)
	{
		/*thisAnimator.SetBool("IsLooking", true);
		isLooking = true;
		CheckForTaskCompletion ();*/
		return;
	}
	public override void OnTriggerExit (Collider other)
	{
		//thisAnimator.SetBool("IsLooking", false);
		//isLooking = false;
		return;
	}
	public override void OnTriggerStay (Collider other)
	{
		
		return;
	}
		

	private Vector3 lookDirection;
	private Vector3 finalLookVector;
	private Quaternion rotation;

	void LateUpdate(){
			

		
		if (weight <= 0f)
			return;

			//Mathf.Lerp (weight, 0f, Time.deltaTime);

			Vector3 dampVelocity = Vector3.zero;

			lookDirection = Vector3.SmoothDamp (lookDirection, player.position - headBone.position, ref dampVelocity, dampTime);



		if (Vector3.Angle (lookDirection, transform.forward) > maxAngle) {
			finalLookVector = Vector3.RotateTowards (thisTransform.forward, lookDirection, Mathf.Deg2Rad * maxAngle, 0.5f);
			thisAnimator.SetBool ("IsLooking", false);
			//thisAnimator.CrossFade ("LookRaiseArm", Time.deltaTime );
			isLooking = false;
		} else {
			finalLookVector = lookDirection;
			thisAnimator.SetBool("IsLooking", true);
			isLooking = true;
		}

		if (!isLooking)
			return;

			rotation = Quaternion.LookRotation (finalLookVector) * Quaternion.Euler (additionalRotation);
			headBone.rotation = Quaternion.Lerp (headBone.rotation, rotation, weight);



	}

	public override void CheckForTaskCompletion ()
	{
		if (PlayerPrefs.GetInt (nameForPlayerPref) == 1)
			return;

			SaveTaskIdentified ();
		//PlayerPrefs.SetInt (nameForPlayerPref, 0);

		if (DataManager.Instance.LoadSkyWalkerScore () <= scoreNeededForTask) {
			SaveTaskCompletion ();
		}
		/*foreach(GameObject gO in PlayerManager.Instance.playerSlotGOList){
			Debug.Log (gO.name);
			if (string.Equals (gO.name, nameOfItemNeeded, System.StringComparison.CurrentCultureIgnoreCase)) 
				gOImagetoUnlockAndActivate.SetActive (true);	*/


	//	}
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


