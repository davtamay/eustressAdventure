using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HitTrigger : MonoBehaviour {

	public int pointsForCollider;

	[SerializeField]private bool isGround;

	public GameObject triggerGO = null;

	[SerializeField]private HitTrigger[] hitTriggers;

	void Start(){

		if(isGround)
		hitTriggers = GetComponentsInChildren<HitTrigger> ();

	}
	void OnTriggerEnter(Collider other){

		if (isGround) {
		//	if(HitManager.Instance.gONotInGround.Contains (other.gameObject))
		//	HitManager.Instance.gONotInGround.Remove (other.gameObject);
			//foreach(Transform t in transform)

			foreach (HitTrigger h in hitTriggers)
				if (h.triggerGO == other.gameObject)
					h.triggerGO = null;
				

		}

		if (triggerGO == null || triggerGO.GetInstanceID () != other.gameObject.GetInstanceID()) {
			HitManager.Instance.UpdateCoinText (pointsForCollider);
			triggerGO = other.gameObject;
		}
	
	}

	/*void OnTriggerExit(Collider other){

		if(isGround)
			HitManager.Instance.gONotInGround.Add (other.gameObject);

	}*/






	//void OnTriggerStay(Collider other){
		
//	if(gameObject.GetComponent <Collider>() == 
	/*	timer += Time.deltaTime;

		if (timer > timeForAirCoin) {
			UpdateCoinText ();
			timer = 0;
		}*/

	//}

	//void UpdateCoinText(){

	//	amountOfCoins += 1;

	//	coinText.text = ":" + amountOfCoins;
	
	//}
}
