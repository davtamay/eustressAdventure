using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowHomeTrigger : MonoBehaviour {

	public static int totalCows = 0;
	public static Vector3 thisPosition;

	void Awake(){
	
	
		thisPosition = transform.position;
	}

	void OnTriggerEnter(Collider other){
	
		Debug.Log (totalCows);
		if(other.CompareTag("Cow")){

			if (other.isTrigger)
				Destroy (other);//.enabled = false;
			//other.enabled = false;

			//++totalCows;



			RandomMoveAnimations RMA = other.GetComponent<RandomMoveAnimations> ();

			float yInit = RMA.initialPos.y;
			RMA.initialPos = this.transform.position;
			RMA.initialPos.y = yInit;
			RMA.distaceToSearch = 10f; 
			RMA.isFirstTime = false;
			RMA.isRandomOn = true;

			totalCows += 1;

			//other.attachedRigidbody.isKinematic = true;


			Destroy (other.attachedRigidbody);



		}
	
	
	
	}
	void OnTriggerExit(Collider other){


		if (other.CompareTag ("Cow")) {

		//	other.enabled = false;
		}
	}
}
