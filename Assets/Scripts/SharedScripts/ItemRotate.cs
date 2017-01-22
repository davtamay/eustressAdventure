using UnityEngine;
using System.Collections;

public class ItemRotate : MonoBehaviour {

	private Transform myTransform;
	public Vector3 initRotation;
	public Vector3 turnSpeed;



	void Start(){

		myTransform = transform;

		myTransform.localEulerAngles = initRotation;

	
	}

	void FixedUpdate () {


		myTransform.Rotate (turnSpeed);


	}
}
