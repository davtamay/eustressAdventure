using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLookAt : MonoBehaviour {

	[SerializeField] private Transform lookingTransform;
	private Transform thisTransform;

	[SerializeField] private bool isEyeBall;
	
	void Awake(){
		thisTransform = transform;
	}
	void LateUpdate () {
		if (isEyeBall) {
			Debug.DrawRay (thisTransform.position, -thisTransform.right);
			//thisTransform.LookAt (2 * (thisTransform.position - lookingTransform.position) -thisTransform.right, thisTransform.TransformDirection(Vector3.up));
			//Quaternion rotate =	Quaternion.FromToRotation(-thisTransform.right,(2 * (thisTransform.position - lookingTransform.position)));
			//Quaternion.AngleAxis(Vector3.SignedAngle(thisTransform
		//	thisTransform.rotation = rotate
			thisTransform.LookAt (lookingTransform.position, thisTransform.TransformDirection(Vector3.forward) );
		//	thisTransform.LookAt (2 * (thisTransform.position - lookingTransform.position));
		}else
		thisTransform.LookAt (2 * (thisTransform.position - lookingTransform.position), Vector3.up );


			
	}

}
