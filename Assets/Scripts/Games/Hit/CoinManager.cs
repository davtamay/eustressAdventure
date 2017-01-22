using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour {

	private static CoinManager instance;
	public static CoinManager Instance {
		get{ return instance; }
	}

	void Awake(){
	
		if (instance != null) {
			Debug.LogError ("There is two instances off CoinManager");
			return;
		} else {
			instance = this;
		}
	
	}

	public void UpdateCoinText(int amountOfPoints){

		PlayerManager.Instance.points = amountOfPoints; 

	}



}
