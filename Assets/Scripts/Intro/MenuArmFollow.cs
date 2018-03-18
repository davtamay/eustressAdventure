using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuArmFollow : MonoBehaviour {

	// Use this for initialization
	private Transform thisTransform;
	[SerializeField]private Animator armAnimator;

	private Transform camTransform;
	private GameObject stressMenu;
	private GameObject directMenu;

	private Vector3 offset;
	private string curSceneName;

	//last item in hierarchy (tablet case "Cube")
	private GameObject tablet;

	[SerializeField]private BoolVariable isStressMenuDisplayed;

	void Awake(){
		
		thisTransform = transform;

	}
	IEnumerator Start(){
		isStressMenuDisplayed.SetValue (false);

		camTransform = Camera.main.transform;
		//armAnimator = GetComponentInChildren<Animator> (());
		offset = thisTransform.position - Camera.main.transform.position;

		stressMenu = GameObject.FindWithTag ("StressMenu");
		directMenu = GameObject.FindWithTag ("DirectMenu");

		if(SceneController.Instance != null)
		curSceneName = SceneController.Instance.GetCurrentSceneName ();

		tablet = thisTransform.FindDeepChild ("Cube").gameObject;
		//if use close Menu makes closing sound in the beginign.


		CloseMenu (false);
		yield return null;

		//yield return StartCoroutine (TriggerWithoutMenuWait ());
	//	StartCoroutine (menu);
		//CloseMenu (false);
	//	TriggerWithoutMenu ();
		//CloseWithNoMenu ();
	//	yield return null;
		//EventManager.Instance.AddListener (EVENT_TYPE.SCENE_LOADED, OnEvent);
		//armAnimator.SetBool ("Close", true);
	

	}
	

	/*bool isMagnetTriggered;

	void MagnetTrigger(){
	
		Handheld.Vibrate ();
		isMagnetTriggered = true;
	}*/

	Quaternion rotation;

	bool isInitialLook = false;

	//this is checked off by button look click (BackToGame Button)
	bool isClosedClick = false;

	public void CloseMenu(bool hasSound = true){

		directMenu.SetActive (false);
		isStressMenuDisplayed.SetValue (false);
		//EventManager.Instance.PostNotification (EVENT_TYPE.STRESSMENU_CLOSED, this,null);


		if(hasSound)
		AudioManager.Instance.PlayDirectSound ("TakeOutMenu");
	
		if(armAnimator.isActiveAndEnabled)
		armAnimator.SetBool ("Close", true);

		GameController.Instance.Paused = false;
		stressMenu.SetActive (false);
		isClosedClick = true;


		//new


	}

	public void CloseWithNoMenu(){
		
		if(armAnimator.isActiveAndEnabled)
			armAnimator.SetBool ("Close", true);

		directMenu.SetActive (false);
			isClosedClick = true;

		//thisTransform.GetChild(0).gameObject.SetActive(false);

	}



	Vector3 oldViewingAngle;
	Vector3 curViewingAngle;

	bool isLerping = false;

	bool isButtonAvailable = true;


	private bool hasMenu;
	public void TriggerMenu(){
	//new
		GameController.Instance.Paused = true;
		CloseWithNoMenu ();
		StartCoroutine (TriggerMenuWait ());
//
//		if( isButtonAvailable) {
//			isButtonAvailable = false;
//
//			AudioManager.Instance.PlayDirectSound ("TakeOutMenu");
//
//			if (isInitialLook) {
//
//				armAnimator.SetBool ("Close", false);
//				return;
//			}
//
//			curViewingAngle = camTransform.rotation * Vector3.forward;
//
//			thisTransform.GetChild(0).gameObject.SetActive(true);
//
//			tablet.SetActive(true);
//
//			oldViewingAngle =  Quaternion.Euler(0,90,0) * camTransform.forward ;
//			isInitialLook = true;
//			hasMenu = true;
//			//if (curSceneName != "intro")
//				GameController.Instance.Paused = true;
//
//		}
//	
	}
	IEnumerator TriggerMenuWait(){



		while (!armAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle"))
			yield return null;
		


		if( isButtonAvailable) {
			isButtonAvailable = false;

			AudioManager.Instance.PlayDirectSound ("TakeOutMenu");

			if (isInitialLook) {

				armAnimator.SetBool ("Close", false);
				yield break;
			}

			curViewingAngle = camTransform.rotation * Vector3.forward;

			thisTransform.GetChild(0).gameObject.SetActive(true);
			directMenu.SetActive (false);

			tablet.SetActive(true);

			oldViewingAngle =  Quaternion.Euler(0,90,0) * camTransform.forward ;
			isInitialLook = true;
			hasMenu = true;
			//if (curSceneName != "intro")


		}


	}
	public void TriggerWithoutMenu(){

		//if (!armAnimator.isActiveAndEnabled)
		//	return;

		StartCoroutine (TriggerWithoutMenuWait ());
			


			curViewingAngle = camTransform.rotation * Vector3.forward;

			thisTransform.GetChild(0).gameObject.SetActive(true);
			
			
		directMenu.SetActive (false);
			tablet.SetActive(false);




			oldViewingAngle =  Quaternion.Euler(0,90,0) * camTransform.forward ;

			hasMenu = false;




	}
	IEnumerator TriggerWithoutMenuWait(){

		while (!armAnimator.isActiveAndEnabled)
			yield return null;

		armAnimator.SetBool ("Close", false);

		while (!armAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Idle"))
			yield return null;

		directMenu.SetActive (true);




	}


	void LateUpdate () {


		if (!armAnimator.gameObject.activeInHierarchy)
			return;


		if (isInitialLook) {


			if (armAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Idle")) {

				if (hasMenu) {
					isStressMenuDisplayed.SetValue (true);
				//	EventManager.Instance.PostNotification (EVENT_TYPE.STRESSMENU_OPENED, this, null);
					stressMenu.SetActive (true);

				}


				isInitialLook = false;

				//ALLOW FOR INITIAL SET UP OF MENU TO PLAYERS FACE
				isLerping = true;
			
			
			}
		
		} else if (isClosedClick) {

		
			if (armAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Start")) {

				isClosedClick = false;

				armAnimator.SetBool ("Close", false);

				thisTransform.GetChild(0).gameObject.SetActive(false);


				isButtonAvailable = true;
				//new
				TriggerWithoutMenu ();


				//

			}

		
		}

	//	if (GameController.Instance.IsMenuActive) {


			curViewingAngle = camTransform.forward;

			if (Vector3.Angle (oldViewingAngle, curViewingAngle) > 45 || isLerping) {

				isLerping = true;

				rotation = Quaternion.Euler (0, camTransform.eulerAngles.y, 0);

				thisTransform.position = Vector3.Lerp (thisTransform.position, camTransform.position - (rotation * (offset * -1)), Time.unscaledDeltaTime * 3f);

				thisTransform.LookAt (2 * thisTransform.position - camTransform.position, Vector3.up);
			


					if (Vector3.Distance(thisTransform.position, camTransform.position - (rotation * (offset * -1))) < 0.05f){
					isLerping = false;
					oldViewingAngle = curViewingAngle;
					}
			}
			//Debug.Log (Vector3.Distance(thisTransform.position, camTransform.position - (rotation * (offset * -1))));
	//	}
			
		
	}
		
}
