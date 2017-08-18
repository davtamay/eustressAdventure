using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceToFallingObjects : MonoBehaviour {

	private Rigidbody thisRigidbody;
	private Rigidbody[] fracturedObjectsRb;
	private Vector3 lastRigidbodyVelocity;
	//private bool isFractureAvailable = true;
	void Awake(){
	

		thisRigidbody = GetComponent<Rigidbody> ();
		fracturedObjectsRb = GetComponentsInChildren<Rigidbody> ();
		thisRigidbody.constraints = RigidbodyConstraints.FreezeAll;
		for (int i = 0; i < fracturedObjectsRb.Length; i++) {

			fracturedObjectsRb [i].constraints = RigidbodyConstraints.FreezeAll;

		}

	
	}

	void OnCollisionEnter(Collision other){
		
		//thisRigidbody.isKinematic = false;
		if (other.transform.CompareTag ("Player")) {
			thisRigidbody.constraints = RigidbodyConstraints.None;
			for (int i = 0; i < fracturedObjectsRb.Length; i++) {
				fracturedObjectsRb [i].constraints = RigidbodyConstraints.None;
			}
		//	isFractureAvailable = false;
			Destroy (gameObject, 10);

		}
		//if (other.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
		//	Fracture ();
			//Destroy (gameObject, 10);
		//}

	
	}
	/*void FixedUpdate(){

		//lastRigidbodyVelocity = thisRigidbody.velocity;

	}
	void Fracture(){
		if (!isFractureAvailable)
			return;
		
		for (int i = 0; i < fracturedObjectsRb.Length; i++) {
			
			fracturedObjectsRb [i].velocity += lastRigidbodyVelocity;
			fracturedObjectsRb [i].detectCollisions = false;

		}

	}*/
}
