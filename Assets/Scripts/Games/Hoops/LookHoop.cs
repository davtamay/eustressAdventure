using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LookHoop : MonoBehaviour {

	

	private Camera cam;
	[SerializeField]private LayerMask layerMaskShoot;

	public float notMovingSweepRate;
	public Vector3 currentLookHit;

	public float focusSweepRate;
	public float slowSweepRate;

	//public Canvas focusDialCanvas; cur
	public Image focusDial;
	public bool isDialShown;
	private float timer;
	public float focusTime;

	public bool isFocused;
	public bool isMovingSlow;

	

	private Vector3 PointInSurface;
	private Vector3 previousCameraLook;
	public Vector3 camDir;




	void Start(){

		//AudioManager.Instance.cr
		previousCameraLook = CameraLook ();
		cam = Camera.main;
		focusDial = GetComponentInChildren<Image> ();
	}

	void Update () {

	//	if (GameController.Instance.IsStartMenuActive)
	//		return;

		/*if (GameController.Instance.GetWaveActiveState ()) {
			focusDial.enabled = false;
			return;
		
		}*/

		focusDial.fillAmount = 1;

		if (DetectFocus ()) {

			focusDial.enabled = false;

			timer += Time.deltaTime;

			if (isDialShown){
				focusDial.enabled = true;	
				//bug in 5.40, setting float crashes app in android 
				focusDial.fillAmount = focusTime - timer;//timer / focusTime;
			}

			if (timer > 0.5f && !isDialShown) {

				isDialShown = true;
				timer = 0f;
				return;
			}


		} else {
			timer = 0;
			focusDial.fillAmount = 1;
			focusDial.enabled = false;


		}
		if (timer > focusTime) {
			isFocused = true;
			focusDial.fillAmount = 0;
		
		} else 
			isFocused = false;

		

		camDir = cam.transform.rotation * Vector3.forward;

		Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);

		RaycastHit hit;
		//In order to start this focus on area for a few seconds
		if (Physics.Raycast (ray, out hit, 20, layerMaskShoot)) {
		

			PointInSurface = hit.point - (Vector3.forward * 0.001f);

			focusDial.transform.position = PointInSurface;
			focusDial.transform.rotation = cam.transform.rotation;

			isMovingSlow = DetectMovingSlow ();
			if (!isMovingSlow) {
				isDialShown = false;
				timer = 0;
			}

			if (!isFocused || !isMovingSlow) 
				return;
			
			BallOn (PointInSurface);


			//isFocused = false;
			timer = 0;
			focusDial.fillAmount = 1;
			focusDial.enabled = false;


		} else
			focusDial.enabled = false;



	}


	private void BallOn (Vector3 pos){

		Vector3 dir = (cam.transform.rotation * Vector3.forward).normalized;
		BallManager.Instance.BallObject (pos, dir);


	}

	private Vector3 CameraLook(){

		return Vector3.Normalize(camDir);

	}

	private bool DetectMovingSlow(){

		Vector3 lookingDirection = CameraLook ();
		Vector3 deltaLookingDirection = previousCameraLook - lookingDirection;
		float rate = (deltaLookingDirection / Time.unscaledDeltaTime).magnitude;
		previousCameraLook = lookingDirection;
		return (rate <= slowSweepRate);

	}
	private bool DetectFocus(){

		Vector3 lookingDirection = CameraLook ();
		Vector3 deltaLookingDirection = previousCameraLook - lookingDirection;
		float rate = (deltaLookingDirection / Time.unscaledDeltaTime).magnitude;
		previousCameraLook = lookingDirection;
		return (rate <= focusSweepRate);

	}
	private bool DetectNotMoving(){

		Vector3 lookingDirection = CameraLook ();
		Vector3 deltaLookingDirection = previousCameraLook - lookingDirection;
		float rate = (deltaLookingDirection / Time.unscaledDeltaTime).magnitude;
		previousCameraLook = lookingDirection;
		return (rate <= notMovingSweepRate);

	}

}
