using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinTrigger : MonoBehaviour {

	public int pointsForCollider;
	//public Text coinText;
	//public int amountOfCoins;
	//private float timer;
	//public float timeForAirCoin;

	void OnTriggerEnter(Collider other){
		Debug.Log(other.gameObject.name);

		CoinManager.Instance.UpdateCoinText (pointsForCollider);
	
	}




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
