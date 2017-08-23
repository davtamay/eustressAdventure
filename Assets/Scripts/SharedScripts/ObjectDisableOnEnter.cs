using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObjectDisableOnEnter : MonoBehaviour {

	[SerializeField] string tagName;

	void OnTriggerEnter(Collider other){
	
		if(other.CompareTag(tagName))
		other.gameObject.SetActive (false);
	
	
	
	}
}
