using UnityEngine;
using System.Collections;

public class CollectorLookWalk : MonoBehaviour {

	public LayerMask layerMask;
	public float jumpHeight;
	public float jumpFromGroundDis;
	public float velocity = 0.7f;
	public float gravity = 8;
	private CharacterController controller;
	Vector3 moveDirection;

	void Start () {
		controller = GetComponent<CharacterController> ();

	}

	
	// Update is called once per frame
	void Update () {

		moveDirection = Camera.main.transform.forward;
		moveDirection *= Time.deltaTime;

		if (isCharGrounded()) {

			if (70.0f > CameraAngleFromUp() && CameraAngleFromUp() >0.0f) {
				if(!GameController.Instance.IsMenuActive)
				moveDirection.y += jumpHeight;

			} 

		}else { 

			moveDirection.y -= gravity * Time.deltaTime;
		}
			

			if (100.0f < CameraAngleFromUp() && CameraAngleFromUp() < 180.0f) {

			moveDirection.x = 0;
			moveDirection.z = 0;

			} 
		moveDirection *= velocity;
		controller.Move (moveDirection);
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
