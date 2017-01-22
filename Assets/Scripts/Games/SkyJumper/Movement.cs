using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float speed;

	public bool isChar;
	public bool isStanding;

	public float forwardMovSpeed;
	public float horizonMovSpeed = 0.5f;
	public float jumpHeight =10.0f;
	public float jumpFromGroundDis;


	private CharacterController charControl;

	private Vector3 movement;



	void Start(){
		if (isChar)
			charControl = GetComponent<CharacterController> ();

	}



	
	// Update is called once per frame
	void FixedUpdate () {
		if (!isChar) {

			transform.localPosition += (Vector3.forward * speed) * Time.deltaTime;

		} else {

			float x = 0.0f;
			float y = 0.0f;
			float z = 0.0f;

			if (!isStanding) {

				z += horizonMovSpeed * Time.deltaTime;
			} else {
				z = 0.0f;
			}
			x += Input.GetAxis ("Horizontal") * horizonMovSpeed; 
		
			if (isCharGrounded()) {

				if (Input.GetButtonDown ("Submit")) {

					y += jumpHeight;

				
				} 
			
			
			}else {

					y += Physics.gravity.y * Time.deltaTime;

			}

			movement = new Vector3 (x, y, z);
		
			charControl.Move (movement);
		}
}

private bool isCharGrounded(){

		RaycastHit hit;

		if (Physics.Raycast (transform.position, -transform.up, out hit, jumpFromGroundDis)){
			return true;

		}else {return false;
		
		}
	
	}
}