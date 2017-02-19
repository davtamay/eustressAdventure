using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpResponse : MonoBehaviour {

	public string powerUpType;

	void OnTriggerEnter(Collider other){


		if (other.CompareTag ("Bullet")) {
			int BulletID = other.transform.GetComponent<BulletControll> ().bulletID;

			if (BulletID == 0) {
				other.gameObject.SetActive (false);

				if (string.Equals (powerUpType, "SpeedShoot", System.StringComparison.CurrentCultureIgnoreCase)) {
					StartCoroutine (SpeedShoot ());

				} else if (string.Equals (powerUpType, "Armor", System.StringComparison.CurrentCultureIgnoreCase)) {
					PlayerManager.Instance.AddArmor ();
					Destroy (gameObject);

				} else if (string.Equals (powerUpType, "SlowDown", System.StringComparison.CurrentCultureIgnoreCase)) {

					StartCoroutine (SlowDown ());
			
			
				}
			}
		
		
		
	}

	}

	IEnumerator SpeedShoot(){

		gameObject.transform.position = new Vector3 (0, -100, 0);
		float curDelayShoot = LookShoot.delayShoot;
		LookShoot.delayShoot = 0.2f;
		yield return new WaitForSeconds(10); 
		Destroy (gameObject);
		LookShoot.delayShoot = curDelayShoot;

	
	
	}
	IEnumerator SlowDown(){
	
		gameObject.transform.position = new Vector3 (0, -100, 0);
		Time.timeScale = 0.4f;
		yield return new WaitForSecondsRealtime (10);
		Time.timeScale = 1f;

		Destroy (gameObject);




	}
}
