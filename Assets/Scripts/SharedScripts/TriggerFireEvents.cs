using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerFireEvents : MonoBehaviour {

	[SerializeField]private UnityEvent onTriggerEnter;
	[SerializeField]private UnityEvent onTriggerExit;

	void OnTriggerEnter(Collider other){

		if(other.CompareTag("Player")){

			onTriggerEnter.Invoke();

		}
			
	}
	void OnTriggerExit(Collider other){

		if(other.CompareTag("Player")){

			onTriggerExit.Invoke();

		}
			
	}
}
