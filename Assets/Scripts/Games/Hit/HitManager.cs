using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitManager : MonoBehaviour {

	private static HitManager instance;
	public static HitManager Instance {
		get{ return instance; }
	}

	//public List<GameObject> gONotInGround;

	void Awake(){
	
		if (instance != null) {
			Debug.LogError ("There is two instances off CoinManager");
			return;
		} else {
			instance = this;
		}
	
		//gONotInGround = new List<GameObject> ();
	
	}
		

	public void UpdateCoinText(int amountOfPoints){

		PlayerManager.Instance.points = amountOfPoints; 

	}



}
