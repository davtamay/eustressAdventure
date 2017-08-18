using UnityEngine;
using System.Collections;
//using TagFrenzy;
public class SkyJumperLookMovement : MonoBehaviour {

	private Camera cam;
	//private GameObject player;

	float horizontalDisToMove;
	float upDisToJump;

	CharacterController charControll;

	private float previouCameraAngle;
	public float speed = 0.1f;
	private Vector3 movement;

	public float jumpHeight;
	public float maxHeight;
	public float gravity;

	private bool isGoingDown;

	public float jumpFromGroundDis;

	public float smoothJump;

	public float minHorizontal;
	public float maxHorizontal;



//	public bool isStayParentPos;
//	public Transform parentTrans;

	//public float accDeltaTime;
	//public float acceleration;






	void Start(){
		cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>() as Camera;
	//	player = GameObject.FindWithTag ("Player");
		charControll = GetComponent <CharacterController> ();

	//	if(isStayParentPos)
	//		parentTrans = GetComponentInParent<Transform> ();
	




	}

	void FixedUpdate () {

		if (speed > 0) {

			if (70.0f > CameraAngleFromRight () && CameraAngleFromRight () > 0.0f) {

				movement.x += speed;
				Debug.Log ("this is movement right");


			} else if (180.0f > CameraAngleFromRight () && CameraAngleFromRight () > 110.0f) {

				movement.x += -(speed);
				Debug.Log ("this is movement left");
		
			} //else if (110.0f > CameraAngleFromRight () && CameraAngleFromRight () > 70.0f)
			//movement.x = 0.0f;

		
			if (70.0f > CameraAngleFromUp () && CameraAngleFromUp () > 0.0f) {

				if (transform.position.y < maxHeight)
					movement.y += speed;//jumpHeight * smoothJump;
			} else if (100.0f < CameraAngleFromUp () && CameraAngleFromUp () < 170.0f) {

				if (transform.position.y > 0)
					movement.y -= speed;//jumpHeight * smoothJump;

			}
				

		


	

		
			//	}

			//charControll. = Mathf.Clamp (charControll.center.y, 0, maxHeight);


			/*	if (isCharGrounded()) {

				if (70.0f > CameraAngleFromUp() && CameraAngleFromUp() >0.0f) {
					if (!GameController.Instance.IsMenuActive) {
					movement.y += jumpHeight * smoothJump;
						isGoingDown = false;
					}
				} 

				}else { 


					if (transform.position.y < maxHeight && !isGoingDown) {
					movement.y += jumpHeight * smoothJump;
					
					
					} else {isGoingDown = true;	}
					
					if (isGoingDown) {
					movement.y += gravity;
					}
				
			
				}*/

			//if (isStayParentPos) {
		

			//		transform.position = new Vector3(transform.position.x, transform.position.y, parentTrans.position.z);
			//transform.position = parentTrans.position;
			//	Debug.Log (parentTrans.position);
			//}



			movement *= Time.deltaTime;


			charControll.Move (movement);

		}
		}

	private float CameraAngleFromRight(){
		return Vector3.Angle (Vector3.right, cam.transform.rotation * Vector3.forward);}
	
	private float CameraAngleFromUp(){
		return Vector3.Angle (Vector3.up, cam.transform.rotation * Vector3.forward);}


	private bool isCharGrounded(){

				RaycastHit hit;

				if (Physics.Raycast (transform.position, -transform.up, out hit, jumpFromGroundDis)){
					return true;

				}else {return false;}

			}
}

	