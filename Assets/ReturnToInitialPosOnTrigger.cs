using System.Collections;

using UnityEngine;
[RequireComponent(typeof (Collider))]
public class ReturnToInitialPosOnTrigger : MonoBehaviour {

	private Vector3 initialPosition;
	private string tagCheckTriggerName;

	void OnEnable(){

		initialPosition = transform.position;

	}

	void OnTriggerEnter(Collider other){

		if(other.CompareTag(tagCheckTriggerName)){

			transform.position = initialPosition;


		}


	}
	

}
