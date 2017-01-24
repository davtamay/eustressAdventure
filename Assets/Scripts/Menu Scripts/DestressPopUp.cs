using UnityEngine;
using System.Collections;

public class DestressPopUp : MonoBehaviour {

	private RectTransform myRectTransform;
	private GameObject dButton;

	[SerializeField] float destressOffset = 1;
	public bool isFacingDown = false;
	public bool isMovingDown = false;

	public bool isMenuShowing = false;

	private Camera cam;

	public float sweepRate = 100.0f;
	private float previousCameraAngle;






	void Awake(){

		cam = Camera.main;

		//cameras = cam.GetComponentsInChildren<Camera> ();
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

			#if(UNITY_EDITOR)
			if (Input.GetMouseButton(0)) {

				isMenuShowing = true;
				GameController.Instance.SetMenuActive ();
				GameController.Instance.Paused = true;
				GameController.Instance.MakeOnlyUIVisible();

				myRectTransform.localPosition = cam.transform.rotation * Vector3.forward * destressOffset; 
				myRectTransform.localRotation = Quaternion.LookRotation (myRectTransform.position - cam.transform.position);
				dButton.SetActive (true);



			}
			#endif

			#if (!UNITY_ANDROID)
			/*		

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
                GameController.Instance.MakeOnlyUIVisible();

				myRectTransform.localPosition = cam.transform.rotation * Vector3.forward * 3; 
				myRectTransform.localRotation = Quaternion.LookRotation (myRectTransform.position - cam.transform.position);
				dButton.SetActive (true);
		
		}*/
			#endif
		}

	}


	void OpenMenuClicker(){
		Handheld.Vibrate ();
		isMenuShowing = true;
		GameController.Instance.SetMenuActive ();
		GameController.Instance.Paused = true;
		GameController.Instance.MakeOnlyUIVisible();

		myRectTransform.localPosition = cam.transform.rotation * Vector3.forward * 3; 
		myRectTransform.localRotation = Quaternion.LookRotation (myRectTransform.position - cam.transform.position);
		dButton.SetActive (true);
	}


	private void OnDisable(){

		MagnetSensor.OnCardboardTrigger -= OpenMenuClicker;


	}
	public void HideDestress(){
		isMenuShowing = false;
		dButton.SetActive (false);
		//layermask to everything
		GameController.Instance.MakeEverythingVisible ();
		//foreach (Camera c in cameras) 
		//	c.cullingMask = -1;


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
