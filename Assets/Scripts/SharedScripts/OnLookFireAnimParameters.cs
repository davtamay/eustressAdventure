using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class OnLookFireAnimParameters : MonoBehaviour {

	[SerializeField]private Animator animatorToTrigger;
	[SerializeField]private string nameOfBool;


	private Collider thisCollider;
	private Transform camTransform;

	void Start () {
		
		thisCollider = GetComponent<Collider> ();
		camTransform = Camera.main.transform;
	}

	void Update () {
	
		RaycastHit hit;
		Ray ray = new Ray (camTransform.position, camTransform.rotation * Vector3.forward);

		if (thisCollider.Raycast (ray, out hit, 50f)) 
			animatorToTrigger.SetBool (nameOfBool, true);
		else
			animatorToTrigger.SetBool (nameOfBool, false);
		
		

}

}