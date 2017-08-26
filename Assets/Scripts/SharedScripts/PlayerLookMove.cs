using UnityEngine;
using System.Collections;

public class PlayerLookMove : MonoBehaviour {

	//public int testOBJECTASSESSIBILITY;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private LayerMask bounceLayer;
	public float velocity = 0.7f;

	[SerializeField] private float jumpHeight;
	[SerializeField] private float jumpFromGroundDis;
	[SerializeField] private float jumpSpeed;
	[SerializeField] private float jumpRechargeTime;

	[SerializeField]private bool isSuperJumpAvailable;
	[SerializeField]private float superJumpHeightAdd;
	[SerializeField]private float superJumpSpeedAdd;
	[SerializeField] private float gravity = 8;



	private CharacterController controller;
	private Vector3 moveDirection;
	private bool isGoingDown = true;
	private bool isGoingUp;

	private Transform thisTransform;
	private float originalYPos;

	[SerializeField] private float minMoveAngleFromUp = 89.0f;
	[SerializeField] private float maxMoveAngleFromUp = 180.0f;
	[SerializeField] private float minJumpAngleFromUp = 0.0f;
	[SerializeField] private float maxJumpAngleFromUp = 70.0f;

	[SerializeField] private float magnitudeOfStressFromFalling;




	private GameObject UISlots;


	private bool isCharInGround;

	//[SerializeField]private float angleSpeed = 5;
	private ControllerColliderHit _contact;




	void Awake(){
		
		controller = GetComponent<CharacterController> ();

		//if (isSlotsPresent)
		//	UISlots = GameObject.FindWithTag ("UISlot");
		
	}

	float rechargeTimer;
	void Start () {
		//controller = GetComponent<CharacterController> ();
		thisTransform = transform;
		originalYPos = thisTransform.position.y;


	}

	
	// Update is called once per frame
	float timer;
	float amountOfFall;


	bool isInitialFalling = false;


	void Update () {



		isCharInGround = isCharGrounded();

		moveDirection = Camera.main.transform.forward.normalized;
		moveDirection *= Time.deltaTime;

//		Debug.Log ("AMOUNT OFF STRESS FROM FALL" + amountOfFall + "ischaronGroud" + isCharInGround);


		if (isCharInGround ) {

			if (isInitialFalling) {
				AudioManager.Instance.PlayDirectSound ("Fall",true);
				isInitialFalling = false;

			}
			rechargeTimer -= Time.deltaTime;

		//	UIStressGage.Instance.stress = amountOfFall * magnitudeOfStressFromFalling;
		//	amountOfFall = 0f;

		
			if (rechargeTimer < 0)
			if (maxJumpAngleFromUp > CameraAngleFromUp() && CameraAngleFromUp() > minJumpAngleFromUp && isGoingDown){

				originalYPos = thisTransform.position.y;
				isGoingDown = false;
				isGoingUp = true;

				isInitialFalling = true;
				rechargeTimer = jumpRechargeTime;
				//new
				//isStayedLookingDown = false;
		
			} 

		}
			
		if (isGoingUp) {


			if (!isSuperJumpAvailable) {
				moveDirection.y += (jumpSpeed + gravity) * Time.deltaTime;


			
				if (thisTransform.position.y > originalYPos + jumpHeight) {


					isGoingDown = true;
					isGoingUp = false;
				}


			} else {


				moveDirection.y += (jumpSpeed + SuperJumpSpeed + gravity) * Time.deltaTime;

				SuperJumpSpeed = 0;

				if (thisTransform.position.y > originalYPos + jumpHeight + SuperJump) {
					
					SuperJump = 0;

					isGoingDown = true;
					isGoingUp = false;
				}
			
			
			
			
			}

		} else {
			
			moveDirection.y -= gravity * Time.deltaTime;
			amountOfFall += gravity * Time.deltaTime;
		}

			if (minMoveAngleFromUp < CameraAngleFromUp() && CameraAngleFromUp() < maxMoveAngleFromUp) {

			moveDirection.x = 0;
			moveDirection.z = 0;



		

			} 
	/*	if (controller.isGrounded) {

			if (Vector3.Dot (moveDirection, _contact.normal) < 0)
				moveDirection = _contact.normal;
			else
				moveDirection += _contact.normal;

		}*/
		
		moveDirection.x *= velocity;
		moveDirection.z *= velocity;
		if (controller.isGrounded && controller.velocity.magnitude > 2f && !AudioManager.Instance.CheckIfAudioPlaying (AudioType._DIRECT, "Steps"))
			AudioManager.Instance.PlayDirectSound ("Steps", true); //StartCoroutine (Step ());
		controller.Move (moveDirection);

	
	}
	//IEnumerator Step(){

	//	yield return new WaitForSeconds (1);
	//	SoundController.Instance.PlayDirectSound ("Step");
	//}


	IEnumerator JumpUp(){
	
		//moveDirection = Vector3.zero;

		while (true) {


			yield return null;

			//moveDirection.y
		
			moveDirection.y += jumpSpeed * Time.deltaTime;

			controller.Move (moveDirection);

			if (thisTransform.position.y > jumpHeight) {
				isGoingDown = true;
				yield break;
			}
		}



	
	}

	void OnDisable(){

		if (!gameObject.activeInHierarchy)
			return;
		
		StartCoroutine (FallDown ());
	
	
	}

	IEnumerator FallDown(){

		yield return new WaitForEndOfFrame();

		if (!controller.enabled)
			yield break;
			
		
		while (true) {
			
			yield return null;
		
			
			moveDirection.x = 0;
			moveDirection.y = 0;

			moveDirection.y -= gravity * Time.deltaTime;

			controller.Move (moveDirection);

	
			if (isCharGrounded ())
				yield break;
		}
	
	
	}

	private float SuperJump;
	private float SuperJumpSpeed;

	private bool isCharGrounded(){





		RaycastHit hit;



		if (!isSuperJumpAvailable) {
			if (Physics.Raycast (thisTransform.position, -Vector3.up, out hit, jumpFromGroundDis, groundLayer)) {
			//	destUp = hit.normal;
				return true;
			
			}else
				return false;

		} else {
		
		/*	if (Physics.Raycast (transform.position, -Vector3.up, out hit, jumpFromGroundDis, groundLayer))
				destUp = hit.normal;
				return true;*/

			if (Physics.Raycast (thisTransform.position, -Vector3.up, out hit, jumpFromGroundDis, bounceLayer)){
				SuperJump = superJumpHeightAdd;
				SuperJumpSpeed = superJumpSpeedAdd;
		//		destUp = hit.normal;
				return true;
		
			}
					
				return false;

		

		
		}



	}
	/*void OnControllerColliderHit(ControllerColliderHit hit){
		_contact = hit;
	
	}*/
	private float CameraAngleFromUp(){
		return Vector3.Angle (Vector3.up, Camera.main.transform.rotation * Vector3.forward);}



}
