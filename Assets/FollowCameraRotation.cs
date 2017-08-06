using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraRotation : MonoBehaviour {


	
	// Update is called once per frame
	void LateUpdate () {
		transform.eulerAngles = new Vector3 (90, 0, Camera.main.transform.eulerAngles.y);
	}
}
