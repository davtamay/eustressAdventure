using UnityEngine;
using System.Collections;

public class ButtonPopUp : MonoBehaviour {
	
	private RectTransform myRectTransform;
	[SerializeField] GameObject dButton;

	public bool isFacingDown = false;
	public bool isMovingDown = false;

	public bool isMenuShowing = false;

	public Camera[] cameras;
	private Camera cam;

	public float sweepRate = 100.0f;
	private float previousCameraAngle;

	private bool clicker;




	void Awake(){
		
		cam = Camera.main;

		cameras = cam.GetComponentsInChildren<Camera> ();
		previousCameraAngle = CameraAngleFromGround ();

		myRectTransform = GetComponent<RectTransform> ();

		dButton = myRectTransform.GetChild (1).gameObject;
		dButton.SetActive (false);


	

	}

	void OnEnable(){
		MagnetSensor.OnCardboardTrigger += OpenMenuClicker;
	
	
	}
	void Start(){

		StartCoroutine (UpdateGame());
	}


	//void Update(){
	IEnumerator UpdateGame(){
		


		while(true){

			yield return null;

			if (Input.GetMouseButton(0)) {
			
				isMenuShowing = true;
				GameController.Instance.SetMenuActive ();
				GameController.Instance.Paused = true;
				foreach (Camera c in cameras) {

					c.cullingMask = 1 << 5;

				}

				myRectTransform.localPosition = cam.transform.rotation * Vector3.forward * 3; 
				myRectTransform.localRotation = Quaternion.LookRotation (myRectTransform.position - cam.transform.position);
				dButton.SetActive (true);
			
			
			
			}

#if (!UNITY_ANDROID)
		

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

				myRectTransform.localPosition = cam.transform.rotation * Vector3.forward * 3; 
				myRectTransform.localRotation = Quaternion.LookRotation (myRectTransform.position - cam.transform.position);
				dButton.SetActive (true);
		
		}
#endif
			}

	}


	void OpenMenuClicker(){

		isMenuShowing = true;
		GameController.Instance.SetMenuActive ();
		GameController.Instance.Paused = true;
		foreach (Camera c in cameras) {

			c.cullingMask = 1 << 5;

		}

	//	myRectTransform.localPosition =  cam.transform.rotation * (( stressMStartingPos - Vector3.forward *5) + Vector3.up *5);
		myRectTransform.localRotation = Quaternion.LookRotation (myRectTransform.position - cam.transform.position);
	}


	private void OnDisable(){

		MagnetSensor.OnCardboardTrigger -= OpenMenuClicker;


	}
	public void HideDButton(){
		isMenuShowing = false;
		dButton.SetActive (false);
		//layermask to everything
		foreach (Camera c in cameras) 
			c.cullingMask = -1;

	
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
