using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLookAt : MonoBehaviour {

	[SerializeField] private Transform lookingTransform;
	private Transform thisTransform;
	
	void Awake(){
		thisTransform = transform;
	}
	void Update () {

		thisTransform.LookAt (2 * (thisTransform.position - lookingTransform.position), Vector3.up );
		
	}

}
