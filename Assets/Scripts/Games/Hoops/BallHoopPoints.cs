using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHoopPoints : MonoBehaviour {

	[SerializeField] private int points;
	[SerializeField] private bool isPersist;



	void OnTriggerEnter(Collider other){
		if (isPersist) {
			PlayerManager.Instance.points = points;
			other.gameObject.SetActive (false);
		} else {
			PlayerManager.Instance.points = points;
			other.gameObject.SetActive (false);
			gameObject.SetActive (false);
		}
	}
}
