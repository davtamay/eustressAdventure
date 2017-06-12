using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowHomeTrigger : MonoBehaviour {

	public static int totalCows = 0;

	void OnTriggerEnter(Collider other){
	
	
		if(other.CompareTag("Cow")){
			//other.enabled = false;
			totalCows++;


			RandomMoveAnimations RMA = other.GetComponent<RandomMoveAnimations> ();

			float yInit = RMA.initialPos.y;
			RMA.initialPos = this.transform.position;
			RMA.initialPos.y = yInit;
			RMA.distaceToSearch = 10f; 
			RMA.isFirstTime = false;
			RMA.isRandomOn = true;


			//other.enabled = false;





		}
	
	
	
	}
	void OnTriggerExit(Collider other){


		if (other.CompareTag ("Cow")) {

			other.enabled = false;
		}
	}
}
