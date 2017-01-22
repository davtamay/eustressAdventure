using UnityEngine;
using System.Collections;

public class ButtonPopUp : MonoBehaviour {

	public RectTransform myRectTransform;
	public RectTransform stressMenuRectTransform;

	private Vector3 stressMStartingPos;

	private Vector3 startPosition;

	public bool isFacingDown = false;
	public bool isMovingDown = false;

	public bool isMenuShowing = false;

	public Camera[] cameras;
	private Camera cam;

	public float sweepRate = 100.0f;
	private float previousCameraAngle;




	void Awake(){
		
		cam = Camera.main;

		cameras = cam.GetComponentsInChildren<Camera> ();




		stressMenuRectTransform = GameObject.FindWithTag ("StressMenu").GetComponent<RectTransform>()as RectTransform;
		stressMStartingPos = stressMenuRectTransform.localPosition;


		myRectTransform = GetComponent<RectTransform> ();

		 

		previousCameraAngle = CameraAngleFromGround ();
	

	}
	void Start(){
		myRectTransform.position =  (Vector3.down * 100);

		StartCoroutine (UpdateGame());
	}


	//void Update(){
	IEnumerator UpdateGame(){
		


		while(true){
			//myRectTransform.localPosition = cam.transform.rotation * (Vector3.back * 10);
			yield return null;


		isFacingDown = DetectFacingDown ();
		isMovingDown = DetectMovingDown ();

		if (!isMovingDown) {

			if (isMenuShowing)
				continue;
			
				HideDButton (); 


			} else if(isMovingDown && !isMenuShowing && isFacingDown) {

				isMenuShowing = true;
				GameController.Instance.SetMenuActive ();
				GameController.Instance.Paused = true;

				//test to stop GO bloking world UI
				foreach (Camera c in cameras) {

					c.cullingMask = 1 << 5;
				
				}
				myRectTransform.localPosition = cam.transform.rotation * (Vector3.forward*2.5f);


				myRectTransform.rotation = Quaternion.LookRotation (myRectTransform.position - cam.transform.position);
				stressMenuRectTransform.localPosition =  cam.transform.rotation * (( stressMStartingPos - Vector3.forward *5) + Vector3.up *5);
				stressMenuRectTransform.localRotation = Quaternion.LookRotation (stressMenuRectTransform.position - cam.transform.position);
		
		}

	}
	}

	public void HideDButton(){
		isMenuShowing = false;

		//layermask to everything
		foreach (Camera c in cameras) 
			c.cullingMask = -1;
		
		myRectTransform.position =  (Vector3.down * 100);
		//myRectTransform.position = cam.transform.rotation * (Vector3.back *10);
	
	}
	private float CameraAngleFromGround(){

		return Vector3.Angle (Vector3.down, cam.transform.rotation * Vector3.forward); 
	}
	private bool DetectFacingDown(){

		return (CameraAngleFromGround () < 50.0f);
	}
	private bool DetectMovingDown(){
		
		float angle = CameraAngleFromGround ();
		float deltaAngle = previousCameraAngle - angle;
		float rate = deltaAngle / Time.deltaTime;
		previousCameraAngle = angle;
		return (rate >= sweepRate);
	
	}
}
