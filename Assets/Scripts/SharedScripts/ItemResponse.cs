using UnityEngine;
using System.Collections;

public class ItemResponse : MonoBehaviour {

	private GameObject player;
	private PlayerManager playerManager;
	public string itemDescription = string.Empty;


	void Awake(){
		player = GameObject.FindWithTag ("Player");
		playerManager = player.GetComponent<PlayerManager>();
	}

	//private int curEnemy = 0;

	void OnTriggerEnter (Collider other){

		if (other.CompareTag ("Player")) {
			
			if (string.Equals (itemDescription, "Coin", System.StringComparison.CurrentCultureIgnoreCase)) {
				playerManager.points = 1;
				AudioManager.Instance.PlayDirectSound ("Reward", true);
				Destroy (this.gameObject);
			}
			else if (string.Equals (itemDescription, "Armor", System.StringComparison.CurrentCultureIgnoreCase)) {
				playerManager.AddArmor ();
				AudioManager.Instance.PlayDirectSound ("Reward", true);
				Destroy (this.gameObject);
			}
			else if (string.Equals (itemDescription, "Health", System.StringComparison.CurrentCultureIgnoreCase)) {
				playerManager.health += 1;
				AudioManager.Instance.PlayDirectSound ("Reward", true);
				Destroy (this.gameObject);
			}

			else if (string.Equals (itemDescription, "Speed", System.StringComparison.CurrentCultureIgnoreCase)) {
				AudioManager.Instance.PlayDirectSound ("Reward", true);
				Destroy (this.gameObject);
				EnemyManager.Instance.ReduceSpeed ();
			}
	//		else if (string.Equals (itemDescription, "Size", System.StringComparison.CurrentCultureIgnoreCase)) {

		//		Destroy (this.gameObject);
		//		EnemyManager.Instance.ReduceSize ();
		//	}
			else if (string.Equals (itemDescription, "RunAway", System.StringComparison.CurrentCultureIgnoreCase)) {
				AudioManager.Instance.PlayDirectSound ("Reward", true);
				Destroy (this.gameObject);
				EnemyManager.Instance.RunAway ();
			}
			else if (string.Equals (itemDescription, "PlayerSpeed", System.StringComparison.CurrentCultureIgnoreCase)) {
				AudioManager.Instance.PlayDirectSound ("Reward", true);
				StartCoroutine (SetCollectorPlayerSpeed (gameObject));


			}


		
			//SkyWalker and GetThemDown


			if (!playerManager.isInvulnerable) {
				

				if (string.Equals (itemDescription, "Bullet", System.StringComparison.CurrentCultureIgnoreCase)) {
					playerManager.health -= 1;
					AudioManager.Instance.PlayDirectSound ("Collision", true);
					gameObject.SetActive (false);
				}
				if (string.Equals (itemDescription, "CO2", System.StringComparison.CurrentCultureIgnoreCase)) {
					playerManager.health -= 1;
					AudioManager.Instance.PlayDirectSound ("Collision", true);
					Destroy (this.gameObject);
				}
				if (string.Equals (itemDescription, "AirPlane", System.StringComparison.CurrentCultureIgnoreCase)) {
					playerManager.health -= 1;
					AudioManager.Instance.PlayDirectSound ("Collision", true);
					Destroy (this.gameObject);
				}
				if (string.Equals (itemDescription, "Bird", System.StringComparison.CurrentCultureIgnoreCase)) {
					playerManager.health -= 1;
					AudioManager.Instance.PlayDirectSound ("Collision", true);
					Destroy (this.gameObject);
				}
				if (string.Equals (itemDescription, "Jet", System.StringComparison.CurrentCultureIgnoreCase)) {
					playerManager.health -= 1;
					AudioManager.Instance.PlayDirectSound ("Collision", true);
					Destroy (this.gameObject);
				}
				if (string.Equals (itemDescription, "Satelite", System.StringComparison.CurrentCultureIgnoreCase)) {
					playerManager.health -= 1;
					AudioManager.Instance.PlayDirectSound ("Collision", true);
					Destroy (this.gameObject);
				}

			}

		}else if (other.CompareTag ("Bullet")) {

			StartCoroutine(DeactivateObjects(gameObject, other.gameObject));

			Vector3 HitLocation = other.transform.position;
			StartCoroutine( GameController.Instance.HitEffectLocation (HitLocation));
			//int BulletID = other.transform.GetComponent<BulletControll> ().bulletID;
			//if (BulletID == 0) 
		//	if (string.Equals (itemDescription, "Health", System.StringComparison.CurrentCultureIgnoreCase)) {
		//		PlayerManager.Instance.health += 1;

		//	}



			int randomNum = Random.Range(0,10);

			switch (randomNum) {
			case 0:
	//			GameObject PowerUp = PowerUpSpawn.Instance.SpawnPowerUpLocation (HitLocation);

			//	StartCoroutine	(DestroyObjects (14f, PowerUp));


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

	IEnumerator SetCollectorPlayerSpeed(GameObject GOtoDestroy){

		PlayerLookMove coll = player.GetComponent<PlayerLookMove>();
		float CurrentVelocity = coll.velocity;
		coll.velocity = 60;

		//coll.velocity = 10;
		//Does not switch back?

		yield return new WaitForSeconds(4f);
		Debug.Log ("slow player");
	
		coll.velocity = CurrentVelocity;

		Destroy (GOtoDestroy);



	}



	


}