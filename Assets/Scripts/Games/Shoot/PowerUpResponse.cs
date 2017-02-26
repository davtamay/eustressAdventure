using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpResponse : MonoBehaviour {

	public string powerUpType;



	void OnTriggerEnter(Collider other){


		if (other.CompareTag ("Bullet")) {
			int BulletID = other.transform.GetComponent<BulletControll> ().bulletID;

			if (BulletID == 0) {
				StartCoroutine (GameController.Instance.HitEffectLocation (other.transform.position));
				other.gameObject.SetActive (false);

				if (string.Equals (powerUpType, "SpeedShoot", System.StringComparison.CurrentCultureIgnoreCase)) {
					StartCoroutine (SpeedShoot ());

				} else if (string.Equals (powerUpType, "Armor", System.StringComparison.CurrentCultureIgnoreCase)) {
					StartCoroutine (AddArmor ());

				} else if (string.Equals (powerUpType, "SlowDown", System.StringComparison.CurrentCultureIgnoreCase)) {

					StartCoroutine (SlowDown ());
			
				}else if (string.Equals (powerUpType, "Health", System.StringComparison.CurrentCultureIgnoreCase)) {
					PlayerManager.Instance.health += 1;
					Destroy (gameObject);
				}
			}
		
		
		
		} else
			return;

	}
	IEnumerator AddArmor(){

		gameObject.transform.position = new Vector3 (0, -100, 0);
		PlayerManager.Instance.AddArmor ();
		yield return new WaitForSeconds(10); 
		Destroy (gameObject);




	}

	public static float speedTimer = 0f;
	public IEnumerator SpeedShoot(){

		speedTimer += 10f;
		if (speedTimer > 10f) {
			speedTimer = 10f;

		}
		gameObject.transform.position = new Vector3 (0, -100, 0);

		LookShoot.delayShoot = 0.2f;
		while (speedTimer > 0f) {


			speedTimer -= Time.deltaTime;
		
			yield return null;
		}
		//yield return new WaitForSeconds (speedTimer); 
		LookShoot.delayShoot = LookShoot.originalDelayShoot;
	    Destroy (gameObject);
	
	}
	IEnumerator SlowDown(){
	
		gameObject.transform.position = new Vector3 (0, -100, 0);
		Time.timeScale = 0.4f;
		yield return new WaitForSecondsRealtime (10);
		Time.timeScale = 1f;

		Destroy (gameObject);




	}
}
