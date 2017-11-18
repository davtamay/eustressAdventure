using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuArmFollow : MonoBehaviour {

	// Use this for initialization
	private Transform thisTransform;
	private Animator thisAnimator;

	private Transform camTransform;
	private GameObject stressMenu;

	private Vector3 offset;
	private string curSceneName;

	void Awake(){
		
		thisTransform = transform;




	}
	void Start(){


		camTransform = Camera.main.transform;
		thisAnimator = GetComponentInChildren<Animator> ();
		offset = thisTransform.position - Camera.main.transform.position;

		stressMenu = GameObject.FindWithTag ("StressMenu");

		if(SceneController.Instance != null)
		curSceneName = SceneController.Instance.GetCurrentSceneName ();
		
		thisTransform.GetChild(0).gameObject.SetActive(false);

		//if use close Menu makes closing sound in the beginign.
		CloseMenu (false);
		//EventManager.Instance.AddListener (EVENT_TYPE.SCENE_LOADED, OnEvent);
	

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
		EventManager.Instance.PostNotification (EVENT_TYPE.STRESSMENU_CLOSED, this,null);

		if(hasSound)
		AudioManager.Instance.PlayDirectSound ("TakeOutMenu");
	
		if(thisAnimator.isActiveAndEnabled)
		thisAnimator.SetBool ("Close", true);

		GameController.Instance.Paused = false;
		stressMenu.SetActive (false);
		isClosedClick = true;


	}


	Vector3 oldViewingAngle;
	Vector3 curViewingAngle;

	bool isLerping = false;

	bool isButtonAvailable = true;

	public void TriggerMenu(){
	
		if( isButtonAvailable) {
			isButtonAvailable = false;

			AudioManager.Instance.PlayDirectSound ("TakeOutMenu");

			if (isInitialLook) {

				thisAnimator.SetBool ("Close", false);
				return;
			}

			curViewingAngle = camTransform.rotation * Vector3.forward;

			thisTransform.GetChild(0).gameObject.SetActive(true);
			oldViewingAngle =  Quaternion.Euler(0,90,0) * camTransform.forward ;
			isInitialLook = true;

			//if (curSceneName != "intro")
				GameController.Instance.Paused = true;

		}
	
	}


	void LateUpdate () {


		if (!thisAnimator.gameObject.activeInHierarchy)
			return;


		if (isInitialLook) {


			if (thisAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Idle")) {

				EventManager.Instance.PostNotification (EVENT_TYPE.STRESSMENU_OPENED, this, null);
				stressMenu.SetActive (true);
				isInitialLook = false;

				//ALLOW FOR INITIAL SET UP OF MENU TO PLAYERS FACE
				isLerping = true;
			
			
			}
		
		} else if (isClosedClick) {

		
			if (thisAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Start")) {

				isClosedClick = false;

				thisAnimator.SetBool ("Close", false);

				thisTransform.GetChild(0).gameObject.SetActive(false);

				isButtonAvailable = true;


				//

			}

		
		}

		if (GameController.Instance.IsMenuActive) {


			curViewingAngle = camTransform.forward;

			if (Vector3.Angle (oldViewingAngle, curViewingAngle) > 80 || isLerping) {

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
		}
			
		
	}
		
}
