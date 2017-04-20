using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOrDisableTrigger : MonoBehaviour {

	public GameObject[] targets;
	public bool isEnabler;
	public bool isDisabler;

	void OnTriggerEnter(Collider other){
	
		if (other.CompareTag ("Player")) {
		
			if(isEnabler){

				foreach (GameObject gO in targets) 
					gO.SetActive (true);

			}else if(isDisabler){

				foreach (GameObject gO in targets) 
					gO.SetActive (false);


			}
		
		
		}

	
	
	}
}
