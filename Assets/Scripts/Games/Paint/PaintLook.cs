using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PaintLook : MonoBehaviour {



	private Camera cam;
	public LayerMask layerMaskDraw;
	public LayerMask layerMaskDrawTool;

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

//	public bool isColorChaged;
	[SerializeField]private Image previewImageColor;




	public int amountToUndo;

	private Vector3 PointInSurface;
	private Vector3 previousCameraLook;
	public Vector3 camDir;

	public Color curColor = Color.black;


	void Start(){

		previousCameraLook = CameraLook ();

		cam = Camera.main;
		//cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

		//focusDial = focusDialCanvas.GetComponentInChildren<Image> (); new
		focusDial = GetComponentInChildren<Image> ();

		previewImageColor.color = curColor;
	}
	void Update () {

		//float fill = focusTime - timer;
		focusDial.fillAmount = 1;

		if (DetectFocus ()) {

			focusDial.enabled = false;

			timer += Time.unscaledDeltaTime;
		
			if (isDialShown){
				focusDial.enabled = true;	
			//bug in 5.40, setting float crashes app in android 
				focusDial.fillAmount = focusTime - timer;//timer / focusTime;
				}

			if (timer > 0.5f && !isDialShown) {
				
				isDialShown = true;
				timer = 0f;
			}
		

		} else {
			timer = 0;
			focusDial.fillAmount = 1;
			focusDial.enabled = false;
			//isDialShown = false;


		}
		if (timer > focusTime) {
			isFocused = true;
			focusDial.fillAmount = 0;
		//	isDialShown = false;
		} else {
			isFocused = false;
		//	isDialShown = false;
		}

		camDir = cam.transform.rotation * Vector3.forward;

		Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);

		RaycastHit hit;
		//In order to start this focus on area for a few seconds
		if (Physics.Raycast (ray, out hit, 20, layerMaskDraw)) {

	

				
			if (camDir == currentLookHit)
				return;
			else
				currentLookHit = camDir;

			PointInSurface = hit.point - (Vector3.forward * 0.001f);
			//	focusDialCanvas.transform.position = PointInSurface; new
			focusDial.transform.position = PointInSurface;

			isMovingSlow = DetectMovingSlow ();
			if (!isMovingSlow) {
				isDialShown = false;
				timer = 0;
			}
			if (!isFocused || !isMovingSlow) {
				//	
				return;
			}
		

			//	if (DetectNotMoving ())
			//		timer = 0;

			//	if (!hit.transform.CompareTag ("Paint"))//!DetectNotMoving())//

			PaintOn (PointInSurface, curColor);

		} else if (Physics.Raycast (ray, out hit, 20, layerMaskDrawTool)) {

			Vector3 PointInSurface = hit.point - (Vector3.forward * 0.001f);
			//focusDialCanvas.transform.position = PointInSurface; new
			focusDial.transform.position = PointInSurface;

			isMovingSlow = DetectMovingSlow ();
			if (!isMovingSlow) {
				isDialShown = false;
				timer = 0;
			}
			if (!isFocused || !isMovingSlow) {
				//isDialShown = false;
				return;
			}

			string typeOfTool = hit.transform.GetComponent<PaintTool> ().ToolType;

			if (string.Equals (typeOfTool, "Undo", System.StringComparison.CurrentCultureIgnoreCase)) {

				timer = 0;

				for (int i = 0; i < amountToUndo; i++) {

					Stack <Transform> sta = PaintManager.GetCurrentStack ();

					Transform despawnPrefab = sta.Pop () as Transform;
					despawnPrefab.gameObject.SetActive (false);

				}
					
			} else if (string.Equals (typeOfTool, "Red", System.StringComparison.CurrentCultureIgnoreCase)) {
//				isColorChaged = !isColorChaged;

//				if (isColorChaged) {
					curColor = Color.red;
					previewImageColor.color = curColor;
					timer = 0;

				
//				} //else {
					//curCollor = Color.black;
					//timer = 0;
				//}
			}else if (string.Equals (typeOfTool, "Blue", System.StringComparison.CurrentCultureIgnoreCase)) {
//				isColorChaged = !isColorChaged;

//				if (isColorChaged) {
					curColor = Color.blue;
					previewImageColor.color = curColor;
					timer = 0;
//				} //else {
				//curCollor = Color.black;
				//timer = 0;
				//}
			}else if (string.Equals (typeOfTool, "Black", System.StringComparison.CurrentCultureIgnoreCase)) {
//				isColorChaged = !isColorChaged;
//
//				if (isColorChaged) {
					curColor = Color.black;
					previewImageColor.color = curColor;
					timer = 0;
//				} //else {
				//curCollor = Color.black;
				//timer = 0;
				//}
			}
		} else
			focusDial.enabled = false;


		
	}


	private void PaintOn (Vector3 pos, Color col){
	
		PaintManager.PaintObject (pos, col);
	
	
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
