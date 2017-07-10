using UnityEngine;
using System.Collections;

public class OrientationAdjustment : MonoBehaviour {



	private RectTransform myRectTransform;
	//private GameObject dButton;

	[SerializeField] float destressOffset = 1;
	//public bool isFacingDown = false;
	//public bool isMovingDown = false;

	public bool isMenuShowing = false;

	//private Camera cam;

	//public float sweepRate = 100.0f;
	private float previousCameraAngle;

	Vector3 originalDirection = Vector3.zero;
	Vector3 currentDirection;

	[SerializeField] private bool isOrientationChangeToFront = true;
	[SerializeField] private bool isOrientationChangeToLastView = false;
	[SerializeField] private bool isMenuFirstTime = false;
	public Transform camTransform;
	public Transform camParent;


	void Awake(){

		camTransform = Camera.main.transform;//cam.transform.parent;
		camParent = camTransform.parent;//camHead.transform.parent;


		previousCameraAngle = CameraAngleFromGround ();

		myRectTransform = GetComponent<RectTransform> ();

		//dButton = myRectTransform.GetChild (1).gameObject;
		//dButton.SetActive (false);




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
				if (isOrientationChangeToFront)
					currentDirection = camTransform.localEulerAngles;

				if(isOrientationChangeToLastView)
				if (isMenuFirstTime == false){

					isMenuFirstTime = true;
					currentDirection = camTransform.localEulerAngles;


					if(isOrientationChangeToLastView){
						if (Mathf.Sign (originalDirection.y) == 1)
							currentDirection.y -= originalDirection.y;
						else
							currentDirection.y += originalDirection.y;

					}

				}

			


				isMenuShowing = true;
				GameController.Instance.SetMenuActive ();
				GameController.Instance.Paused = true;
				GameController.Instance.MakeOnlyUIVisible();

				myRectTransform.localPosition = camTransform.rotation * Vector3.forward * destressOffset; 
				myRectTransform.localRotation = Quaternion.LookRotation (myRectTransform.position - camTransform.position);
				//dButton.SetActive (true);



			}
			#endif

			#if (UNITY_ANDROID)
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
		if (isOrientationChangeToFront) 
			currentDirection = camTransform.localEulerAngles;


		if (isOrientationChangeToLastView) {
			if (isMenuFirstTime == false) {

				isMenuFirstTime = true;
				currentDirection = camTransform.localEulerAngles;


				if (isOrientationChangeToLastView) {
					if (Mathf.Sign (originalDirection.y) == 1)
						currentDirection.y -= originalDirection.y;
					else
						currentDirection.y += originalDirection.y;

				}

			}
		}

		isMenuShowing = true;

		GameController.Instance.SetMenuActive ();
		GameController.Instance.Paused = true;
		GameController.Instance.MakeOnlyUIVisible();

		myRectTransform.localPosition = camTransform.rotation * Vector3.forward * 3; 
		myRectTransform.localRotation = Quaternion.LookRotation (myRectTransform.position -  camTransform.position);
		//dButton.SetActive (true);
	}


	private void OnDisable(){

		MagnetSensor.OnCardboardTrigger -= OpenMenuClicker;


	}
	public void ShowGame(){

		if (isOrientationChangeToFront) {
			//float angle = Mathf.DeltaAngle (originalDirection.y, currentDirection.y);
			//originalDirection.y = angle;
			originalDirection.y = currentDirection.y;

			float deviation = currentDirection.y;
			if (Mathf.Sign (deviation) == 1)
				originalDirection *= -1;


			camParent.localEulerAngles = originalDirection;
			currentDirection = Vector3.zero;
			originalDirection = Vector3.zero;
		
		}
		if (isOrientationChangeToLastView) {

			originalDirection.y = currentDirection.y;

			currentDirection.y = camTransform.localEulerAngles.y;

			originalDirection.y -= currentDirection.y;
		

			camParent.localEulerAngles = originalDirection;

			currentDirection = Vector3.zero;
		
			isMenuFirstTime = false;
		}

		isMenuShowing = false;
		//dButton.SetActive (false);

		GameController.Instance.MakeEverythingVisible ();
	




	}
	private float CameraAngleFromGround(){

		return Vector3.Angle (Vector3.down, camTransform.rotation * Vector3.forward); 
	}
	private bool DetectFacingDown(){

		return (CameraAngleFromGround () < 50.0f);
	}
	private bool DetectMovingDown(){

		float angle = CameraAngleFromGround ();
		float deltaAngle = previousCameraAngle - angle;
		float rate = deltaAngle / Time.deltaTime;
		previousCameraAngle = angle;
		return false;
		//return (rate >= sweepRate);

	}


}
