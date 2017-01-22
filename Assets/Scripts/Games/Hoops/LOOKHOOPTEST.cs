using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LOOKHOOPTEST : MonoBehaviour {

	
	private Camera cam;
	//[SerializeField]private LayerMask layerMaskShoot;

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

		previousCameraLook = CameraLook ();
		cam = Camera.main;
		//focusDial = GetComponentInChildren<Image> ();
		StartCoroutine (OnUpdate ());
	}

	IEnumerator OnUpdate () {

		while (true) {
			camDir = cam.transform.rotation * Vector3.forward;
			PointInSurface = camDir + (Vector3.forward * 1f);
			focusDial.transform.rotation = cam.transform.rotation;
			isMovingSlow = DetectMovingSlow ();

			focusDial.fillAmount = 1;
			yield return null;

			if (DetectFocus ()) {

				focusDial.enabled = false;

				timer += Time.deltaTime;
		
				if (timer > 0.5f && !isDialShown) {

					isDialShown = true;
					timer = 0f;
					continue;
					//return;
				}

				if (isDialShown) {
					focusDial.enabled = true;	
					//test
					focusDial.transform.position = PointInSurface;
					focusDial.transform.rotation = cam.transform.rotation;
					//bug in 5.40, setting float crashes app in android 
					focusDial.fillAmount = focusTime - timer;//timer / focusTime;
				}

			} else {
				isDialShown = false;
				timer = 0;
				focusDial.fillAmount = 1;
				focusDial.enabled = false;


			}


			if (!isMovingSlow) {
				isDialShown = false;
				timer = 0;
			}

			//if (!isFocused || !isMovingSlow) 
			//	return;

			if (timer > focusTime) {
				BallOn (PointInSurface);

				isFocused = true;
				//focusDial.fillAmount = 0;
				timer = 0;
				focusDial.fillAmount = 1;
				focusDial.enabled = false;

			} else {
				focusDial.enabled = false;
				isFocused = false;

			}

	


		}

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
