using UnityEngine;
using System.Collections;

public class ObjExitTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other){
	
		Destroy (other.gameObject);
	
	
	}
}
