using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpResponse : MonoBehaviour {

	public string powerUpType;

	void OnTriggerEnter(Collider other){

		if (other.transform.GetComponent<BulletControll> ().bulletID == 0) {

			if (string.Equals (powerUpType, "SpeedShoot", System.StringComparison.CurrentCultureIgnoreCase)) {
				other.gameObject.SetActive (false);
				StartCoroutine (SpeedShoot ());
			} else if (string.Equals (powerUpType, "Armor", System.StringComparison.CurrentCultureIgnoreCase)) {
				PlayerManager.Instance.AddArmor ();
				Destroy (gameObject);
			}


		
		} 


	}

	IEnumerator SpeedShoot(){
		Debug.Log ("Bullet Speed Increased");
		gameObject.transform.position = new Vector3 (0, -100, 0);
		float curDelayShoot = LookShoot.delayShoot;
		LookShoot.delayShoot = 0.2f;
		yield return new WaitForSeconds(10); 
		Destroy (gameObject);
		LookShoot.delayShoot = curDelayShoot;

	
	
	}
}
