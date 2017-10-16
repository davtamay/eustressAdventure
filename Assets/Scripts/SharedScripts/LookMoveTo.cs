using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookMoveTo : MonoBehaviour {

	private Transform camTrans;

	//[SerializeField]private Transform ImageToProject;



//	[SerializeField]private float distanceToBreakFocus;
	[SerializeField]private LayerMask walkToLM;
	private Vector3 previousVectorHit;


	//HOOPS
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

	void Awake(){ 

		camTrans = Camera.main.transform;
		previousCameraLook = CameraLook ();

		focusDial = GetComponentInChildren<Image> ();
			
	}
	void Update () {


		//if (GameController.Instance.IsStartMenuActive)
		//	return;

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



		camDir = camTrans.rotation * Vector3.forward;

		Ray ray = new Ray (camTrans.position, camTrans.rotation * Vector3.forward);

		RaycastHit hit;
		//In order to start this focus on area for a few seconds
		if (Physics.Raycast (ray, out hit, 10, walkToLM)) {


			PointInSurface = hit.point + (Vector3.up * 0.1f);

			focusDial.transform.position = PointInSurface;
			//focusDial.transform.rotation = camTrans.rotation;

			isMovingSlow = DetectMovingSlow ();
			if (!isMovingSlow) {
				isDialShown = false;
				timer = 0;
			}

			if (!isFocused || !isMovingSlow) 
				return;

			//focusDial.transform.position = PointInSurface;
			//BallOn (PointInSurface);
			MoveToLocation(PointInSurface);
			Debug.Log("MoveTO!!!!!!!");

			//isFocused = false;
			timer = 0;
			focusDial.fillAmount = 1;
			focusDial.enabled = false;


		} else
			focusDial.enabled = false;
		
	/*	
	 *RaycastHit hit;

		if (Physics.Raycast (camTrans.position, camTrans.rotation * Vector3.forward,out hit, 50f)) {
		
			//isFocused = true;


			if (Vector3.Distance (previousVectorHit, hit.point) > distanceToBreakFocus) {

				ImageToProject.position = hit.point;
				return;
			} else {
				isFocused = true;
			
			
			}

			previousVectorHit = hit.point;


			Debug.Log ("IS STAYING FOCUSED");
		

		
		}
		*/
			
	}
	private void MoveToLocation(Vector3 pos){

		GameObject player = GameObject.FindWithTag ("Player");
		pos.y = player.transform.position.y;
		player.transform.position = pos;

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
