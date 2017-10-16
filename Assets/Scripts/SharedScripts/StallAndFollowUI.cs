using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StallAndFollowUI: MonoBehaviour {

	// Use this for initialization
	//private Transform thisTransform;
	[SerializeField]private Transform objectToRotate;
	[SerializeField]private int angleDistanceUntilRotateBack = 80;
	//private Animator thisAnimator;

	private Transform camTransform;
	private GameObject stressMenu;

	private Vector3 offset;
	private string curSceneName;

	Quaternion rotation;

	bool isInitialLook = false;


	Vector3 oldViewingAngle;
	Vector3 curViewingAngle;

	bool isLerping = false;


	//bool isButtonAvailable = true;


	void Awake(){

		//thisTransform = GetComponent<Transform> ();
		camTransform = Camera.main.transform;
		offset = objectToRotate.position - camTransform.position;

	}

	void OnEnable(){

		isInitialLook = true;

		rotation = Quaternion.Euler (0, camTransform.eulerAngles.y, 0);

		objectToRotate.position =  camTransform.position - (rotation * (offset * -1));

		objectToRotate.LookAt (2 * objectToRotate.position - camTransform.position, camTransform.up);
	}

	void LateUpdate () {



		if (isInitialLook) {
			isInitialLook = false;

			curViewingAngle = camTransform.forward;

		//	oldViewingAngle =  Quaternion.Euler(0,90,0) * camTransform.forward ;
			oldViewingAngle =  camTransform.forward ;

		}else{

			curViewingAngle = camTransform.forward;

			if (Vector3.Angle (oldViewingAngle, curViewingAngle) > angleDistanceUntilRotateBack || isLerping) {

				isLerping = true;

				rotation = Quaternion.Euler (0, camTransform.eulerAngles.y, 0);

				objectToRotate.position = Vector3.Lerp (objectToRotate.position, camTransform.position - (rotation * (offset * -1)), Time.unscaledDeltaTime * 3f);

				objectToRotate.LookAt (2 * objectToRotate.position - camTransform.position, camTransform.up);


				//controlls the smooth step of the rotation;
				if (Vector3.Distance(objectToRotate.position, camTransform.position - (rotation * (offset * -1))) < 0.1f){
					isLerping = false;
					oldViewingAngle = curViewingAngle;
				}
			}
			//Debug.Log (Vector3.Angle (oldViewingAngle, curViewingAngle));
		}


	}
}

