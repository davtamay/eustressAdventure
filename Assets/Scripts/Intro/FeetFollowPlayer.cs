using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetFollowPlayer : MonoBehaviour {

	private Transform cam;
	[SerializeField]private float rotationToSpeed = 1;
	private RectTransform thisRectTransform;




	void Start () {
		
		thisRectTransform = GetComponent<RectTransform>();
		cam = Camera.main.transform;

	}
	void Update () {

		Vector3 movePos = new Vector3 (90, cam.eulerAngles.y, 0);

		//float angle = cam.eulerAngles.y;

		//angle = (angle < 1f) ? angle * -1 : angle;
		//angle = (angle > 180) ?//angle - 180 : angle;
		//angle = (angle < -180) ? angle + 360 : angle;


		thisRectTransform.eulerAngles = movePos;

	


		//float LerpToYRotation = Mathf.Lerp (thisRectTransform.eulerAngles.y, angle , Time.deltaTime * rotationToSpeed);
		//thisRectTransform.rotation = Quaternion.FromToRotation (new Vector3 (0,0,thisRectTransform.eulerAngles.y), new Vector3 (0, 0,thisRectTransform.eulerAngles.y));
		//thisRectTransform.rotation = Quaternion.Euler (new Vector3 (90, LerpToYRotation,0));//LerpToYRotation));

			
	}
}
