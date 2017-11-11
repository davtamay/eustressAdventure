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
				AudioManager.Instance.PlayDirectSound("SmallWin");
				PlayerManager.Instance.points = points;
				other.gameObject.SetActive (false);

		} else {
			--livesUntilDisable;
			if (livesUntilDisable < 1) {
				AudioManager.Instance.PlayDirectSound("SmallWin");
				PlayerManager.Instance.points = points;
				other.gameObject.SetActive (false);
				gameObject.SetActive (false);
			}
		}
		}
	}
}
