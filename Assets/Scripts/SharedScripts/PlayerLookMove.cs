using UnityEngine;
using System.Collections;

public class PlayerLookMove : MonoBehaviour {

	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private LayerMask bounceLayer;
	[SerializeField] private float jumpHeight;
	[SerializeField]private bool isSuperJumpAvailable;
	[SerializeField]private float superJumpHeightAdd;
	[SerializeField]private float superJumpSpeedAdd;
	[SerializeField] private float jumpFromGroundDis;
	[SerializeField] private float jumpSpeed;
	public float velocity = 0.7f;
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

	[SerializeField] private bool isSlotsPresent;
	private GameObject UISlots;
	private bool isStayedLookingDown;

	private bool isCharInGround;

	Vector3 destUp = Vector3.zero;
	[SerializeField]private float angleSpeed = 5;
	private ControllerColliderHit _contact;

	void Awake(){
		
		controller = GetComponent<CharacterController> ();

		if (isSlotsPresent)
			UISlots = GameObject.FindWithTag ("UISlot");
		
	}

	void Start () {
		//controller = GetComponent<CharacterController> ();
		thisTransform = transform;
		originalYPos = thisTransform.position.y;


	}

	
	// Update is called once per frame
	void Update () {

		isCharInGround = isCharGrounded();

		moveDirection = Camera.main.transform.forward;
		moveDirection *= Time.deltaTime;



		if (isCharGrounded() ) {

		

			if (maxJumpAngleFromUp > CameraAngleFromUp() && CameraAngleFromUp() > minJumpAngleFromUp && isGoingDown){

				originalYPos = thisTransform.position.y;
				isGoingDown = false;
				isGoingUp = true;
				//new
				isStayedLookingDown = false;
		
			} 

		}else { 

		

				moveDirection.y -= gravity * Time.deltaTime;

		}

		if (isGoingUp) {

			if (!isSuperJumpAvailable) {
				moveDirection.y += (jumpSpeed + gravity) * Time.deltaTime;


			
				if (thisTransform.position.y > originalYPos + jumpHeight) {


					isGoingDown = true;
					isGoingUp = false;
				}


			} else {


				moveDirection.y += (jumpSpeed +SuperJumpSpeed+ gravity) * Time.deltaTime;

				SuperJumpSpeed = 0;

				if (thisTransform.position.y > originalYPos + jumpHeight + SuperJump) {
					
					SuperJump = 0;

					isGoingDown = true;
					isGoingUp = false;
				}
			
			
			
			
			}

		}
			

			if (minMoveAngleFromUp < CameraAngleFromUp() && CameraAngleFromUp() < maxMoveAngleFromUp) {

			moveDirection.x = 0;
			moveDirection.z = 0;



			if (isSlotsPresent) {
				if (!UISlots.activeInHierarchy && !isStayedLookingDown)
					StartCoroutine (PlayerManager.Instance.ShowUISlots());

			}
			isStayedLookingDown = true;

			} 
	/*	if (controller.isGrounded) {

			if (Vector3.Dot (moveDirection, _contact.normal) < 0)
				moveDirection = _contact.normal;
			else
				moveDirection += _contact.normal;

		}*/
		
		moveDirection.x *= velocity;
		moveDirection.z *= velocity;
		controller.Move (moveDirection);

		thisTransform.up = Vector3.Slerp (thisTransform.up, destUp, angleSpeed * Time.deltaTime);
	}

	IEnumerator JumpUp(){
	
		//moveDirection = Vector3.zero;

		while (true) {


			yield return null;

			//moveDirection.y
		
			moveDirection.y += jumpSpeed * Time.deltaTime;

			controller.Move (moveDirection);

			if (transform.position.y > jumpHeight) {
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
			if (Physics.Raycast (transform.position, -Vector3.up, out hit, jumpFromGroundDis, groundLayer)) {
				destUp = hit.normal;
				return true;
			
			}else
				return false;

		} else {
		
			if (Physics.Raycast (transform.position, -Vector3.up, out hit, jumpFromGroundDis, groundLayer))
				destUp = hit.normal;
				return true;

			if (Physics.Raycast (transform.position, -Vector3.up, out hit, jumpFromGroundDis, bounceLayer)){
				SuperJump = superJumpHeightAdd;
				SuperJumpSpeed = superJumpSpeedAdd;
				destUp = hit.normal;
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
