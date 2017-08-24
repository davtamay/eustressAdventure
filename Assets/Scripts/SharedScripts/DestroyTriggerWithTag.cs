using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTriggerWithTag : MonoBehaviour {
	[SerializeField] private bool useTag = false;
	[SerializeField] private string tagName; 

	public void OnTriggerEnter(Collider other){
	
		Debug.Log ("IWORK");
		if (useTag) {
			if (other.CompareTag (tagName))
				Destroy (other.gameObject);
		}else
			Destroy(other.gameObject);
	
	
	
	}
	void OnCollisionEnter(Collision other){
	
		Debug.Log ("COLLISIONWORK");
	
	}

}
