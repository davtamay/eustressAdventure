using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHoopPoints : MonoBehaviour {

	[SerializeField] private int points;
	[SerializeField] private bool isPersist;

	[SerializeField] private int livesUntilDisable = 1;



	void OnTriggerEnter(Collider other){

		if(other.CompareTag("Bullet")){
		if (isPersist) {
			
				PlayerManager.Instance.points = points;
				other.gameObject.SetActive (false);

		} else {
			--livesUntilDisable;
			if (livesUntilDisable < 1) {
				PlayerManager.Instance.points = points;
				other.gameObject.SetActive (false);
				gameObject.SetActive (false);
			}
		}
		}
	}
}
