using UnityEngine;
using System.Collections;

public class ObjectResponse : MonoBehaviour {

	private GameObject player;

	//private PlayerManager playerManager;
	public string itemDescription = string.Empty;
	[Tooltip("If Clicking isObstacle you can leave itemDescription Empty")]
	[SerializeField]private bool isObstacle = false;

	[SerializeField]private int healthEffect;



	void Awake(){
		player = GameObject.FindWithTag ("Player");

	}

	void OnTriggerEnter (Collider other){

		if (other.CompareTag ("Player")) {

			if (!isObstacle) {
				if (string.Equals (itemDescription, "Coin", System.StringComparison.CurrentCultureIgnoreCase)) {
					PlayerManager.Instance.points = 1;
					Destroy (this.gameObject);
				} else if (string.Equals (itemDescription, "Armor", System.StringComparison.CurrentCultureIgnoreCase)) {
					PlayerManager.Instance.AddArmor ();
					AudioManager.Instance.PlayInterfaceSound ("Reward");
					Destroy (this.gameObject);
				} else if (string.Equals (itemDescription, "Health", System.StringComparison.CurrentCultureIgnoreCase)) {
					PlayerManager.Instance.health += 1;
					AudioManager.Instance.PlayInterfaceSound ("Reward");
					Destroy (this.gameObject);
				} else if (string.Equals (itemDescription, "Speed", System.StringComparison.CurrentCultureIgnoreCase)) {
					AudioManager.Instance.PlayInterfaceSound ("Reward");
					Destroy (this.gameObject);
					EnemyManager.Instance.ReduceSpeed ();
				}
	//		else if (string.Equals (itemDescription, "Size", System.StringComparison.CurrentCultureIgnoreCase)) {
		//		Destroy (this.gameObject);
		//		EnemyManager.Instance.ReduceSize ();
		//	}
			else if (string.Equals (itemDescription, "RunAway", System.StringComparison.CurrentCultureIgnoreCase)) {
					AudioManager.Instance.PlayInterfaceSound ("Reward");
					Destroy (this.gameObject);
					EnemyManager.Instance.RunAway ();
				} else if (string.Equals (itemDescription, "PlayerSpeed", System.StringComparison.CurrentCultureIgnoreCase)) {
					AudioManager.Instance.PlayInterfaceSound ("Reward");
					StartCoroutine (SetCollectorPlayerSpeed (gameObject));


				}
				return;
			}
			//SkyWalker and GetThemDown

			if (!PlayerManager.Instance.isInvulnerable && isObstacle) {
				

			//	if (string.Equals (itemDescription, "Obstacle", System.StringComparison.CurrentCultureIgnoreCase)) {
				
					PlayerManager.Instance.health -= healthEffect;
					AudioManager.Instance.PlayDirectSound ("Collision", true);
					Vector3 HitLocation = transform.position;
					StartCoroutine (GameController.Instance.HitEffectLocation (HitLocation));
					Destroy (gameObject);
				//}



			} else {
				
				Vector3 HitLocation = transform.position;
				StartCoroutine( GameController.Instance.HitEffectLocation (HitLocation));
				Destroy (gameObject);
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
		//	playerManager = player.GetComponent<PlayerManager>();

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