using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObjectResponse : MonoBehaviour {

	private GameObject player;
	private PlayerManager playerManager;
	[SerializeField]private string objectDescription = string.Empty;
	[SerializeField]private int health = 1;


	void Awake(){
		player = GameObject.FindWithTag ("Player");
		playerManager = player.GetComponent<PlayerManager>();
	}
	
	void OnTriggerEnter (Collider other){
	
		if (other.CompareTag ("Player")) {

			if (string.Equals (objectDescription, "Bullet", System.StringComparison.CurrentCultureIgnoreCase)) {
				playerManager.health -= 1;
				gameObject.SetActive (false);
			}
		}

		if (other.CompareTag ("Bullet")) {

			int BulletID = other.transform.GetComponent<BulletControll> ().bulletID;
			if (BulletID == 1)
				return;
			

				--health;

			if(health == 0)
			StartCoroutine(DeactivateObjects(gameObject, other.gameObject));
			else
			StartCoroutine(DeactivateObjects(other.gameObject));

			Vector3 HitLocation = other.transform.position;
			StartCoroutine( GameController.Instance.HitEffectLocation (HitLocation));
			//int BulletID = other.transform.GetComponent<BulletControll> ().bulletID;//
			//if (BulletID == 0) //
			if (string.Equals (objectDescription, "Health", System.StringComparison.CurrentCultureIgnoreCase)) {
				PlayerManager.Instance.health += 1;

			}



			int randomNum = Random.Range(0,10);

			switch (randomNum) {
				case 0:
				GameObject PowerUp = PowerUpSpawn.Instance.SpawnPowerUpLocation (HitLocation);
				StartCoroutine	(DestroyObjects (12f, PowerUp));
				break;

				default:
				break;

			}

		}
	}

	IEnumerator DestroyObjects(float time, params GameObject[] gO){

		yield return new WaitForSeconds(time);
		if (gO == null)

			foreach (GameObject go in gO)
				go.SetActive (false);

	}
	IEnumerator DeactivateObjects(params GameObject[] gO){
		//	yield return new WaitForSeconds(0.2f);
		yield return null;//new WaitForEndOfFrame ();
		foreach (GameObject go in gO)
			go.SetActive (false);

	}
	
}



