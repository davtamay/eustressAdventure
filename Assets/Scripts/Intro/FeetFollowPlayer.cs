using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetFollowPlayer : MonoBehaviour {

	private Transform cam;
	[SerializeField]private float rotationToSpeed = 1;
	private Transform thisTransform;

	Vector3 movePos;



	void Start () {
		
		thisTransform = GetComponent<Transform>();
		cam = Camera.main.transform;

		movePos = new Vector3 (90, cam.eulerAngles.y, 0);

		thisTransform.eulerAngles = movePos;

	}
	Vector3 destUp;
	void Update () {

	//	RaycastHit hit;
	//	if (Physics.Raycast (transform.position, -Vector3.up, out hit, 5,LayerMask.NameToLayer("Ground")))
	//		destUp = hit.normal;

	//		thisRectTransform.
		
		
		
		//movePos.y = cam.eulerAngles.y;

		//float angle = cam.eulerAngles.y;

		//angle = (angle < 1f) ? angle * -1 : angle;
		//angle = (angle > 180) ?//angle - 180 : angle;
		//angle = (angle < -180) ? angle + 360 : angle;


		thisTransform.eulerAngles  = new Vector3 (90, cam.eulerAngles.y, 0);
		//Vector3 firstsrot = new Vector3 (90, cam.eulerAngles.y, 0);
		//movePos;
		//Vector3 secondrot =  Vector3.Slerp (-thisTransform.up, destUp, 5 * Time.deltaTime);
	//	thisTransform.up = Vector3.Slerp (thisTransform.up, destUp, 5 * Time.deltaTime);
	//	thisTransform.eulerAngles = firstsrot + secondrot;
	


		//float LerpToYRotation = Mathf.Lerp (thisRectTransform.eulerAngles.y, angle , Time.deltaTime * rotationToSpeed);
		//thisRectTransform.rotation = Quaternion.FromToRotation (new Vector3 (0,0,thisRectTransform.eulerAngles.y), new Vector3 (0, 0,thisRectTransform.eulerAngles.y));
		//thisRectTransform.rotation = Quaternion.Euler (new Vector3 (90, LerpToYRotation,0));//LerpToYRotation));

			
	}
}
