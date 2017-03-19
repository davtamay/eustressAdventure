using UnityEngine;
using System.Collections;

public class CollectorLookWalk : MonoBehaviour {

	public LayerMask layerMask;
	public float jumpHeight;
	public float jumpFromGroundDis;
	public float jumpSpeed;
	public float velocity = 0.7f;
	public float gravity = 8;
	private CharacterController controller;
	private Vector3 moveDirection;
	private bool isGoingDown = true;
	private bool isGoingUp;

	private Transform thisTransform;
	private float originalYPos;

	public float minMoveAngleFromUp = 89.0f;
	public float maxMoveAngleFromUp = 180.0f;
	public float minJumpAngleFromUp = 0.0f;
	public float maxJumpAngleFromUp = 70.0f;

	public bool isCharInGround;

	void Start () {
		controller = GetComponent<CharacterController> ();
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
		
			} 

		}else { 

		

				moveDirection.y -= gravity * Time.deltaTime;

		}

		if (isGoingUp) {


			moveDirection.y += (jumpSpeed + gravity) * Time.deltaTime;


			
			if (thisTransform.position.y > originalYPos + jumpHeight) {


				isGoingDown = true;
				isGoingUp = false;
			}

		}
			

			if (minMoveAngleFromUp < CameraAngleFromUp() && CameraAngleFromUp() < maxMoveAngleFromUp) {

			moveDirection.x = 0;
			moveDirection.z = 0;

			} 
		moveDirection.x *= velocity;
		moveDirection.z *= velocity;
		controller.Move (moveDirection);
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



	private bool isCharGrounded(){

		RaycastHit hit;

		if (Physics.Raycast (transform.position, -transform.up, out hit, jumpFromGroundDis, layerMask)){
			return true;

		}else {return false;}
	}
	private float CameraAngleFromUp(){
		return Vector3.Angle (Vector3.up, Camera.main.transform.rotation * Vector3.forward);}
}
