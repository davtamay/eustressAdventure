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
				Destroy (this.gameObject);
			}
			else if (string.Equals (itemDescription, "Armor", System.StringComparison.CurrentCultureIgnoreCase)) {
				playerManager.AddArmor ();
				Destroy (this.gameObject);
			}
			else if (string.Equals (itemDescription, "Health", System.StringComparison.CurrentCultureIgnoreCase)) {
				playerManager.health += 1;
				Destroy (this.gameObject);
			}

			else if (string.Equals (itemDescription, "Speed", System.StringComparison.CurrentCultureIgnoreCase)) {

				Destroy (this.gameObject);
				EnemyManager.Instance.ReduceSpeed ();
			}
			else if (string.Equals (itemDescription, "Size", System.StringComparison.CurrentCultureIgnoreCase)) {

		//		Destroy (this.gameObject);
		//		EnemyManager.Instance.ReduceSize ();
			}
			else if (string.Equals (itemDescription, "RunAway", System.StringComparison.CurrentCultureIgnoreCase)) {

				Destroy (this.gameObject);
				EnemyManager.Instance.RunAway ();
			}
			else if (string.Equals (itemDescription, "PlayerSpeed", System.StringComparison.CurrentCultureIgnoreCase)) {

				StartCoroutine (SetCollectorPlayerSpeed (gameObject));


			}

		
			//SkyWalker and GetThemDown


			if (!playerManager.isInvulnerable) {

				if (string.Equals (itemDescription, "Bullet", System.StringComparison.CurrentCultureIgnoreCase)) {
					playerManager.health -= 1;
					gameObject.SetActive (false);
				}
				if (string.Equals (itemDescription, "CO2", System.StringComparison.CurrentCultureIgnoreCase)) {
					playerManager.health -= 1;
					Destroy (this.gameObject);
				}
				if (string.Equals (itemDescription, "AirPlane", System.StringComparison.CurrentCultureIgnoreCase)) {
					playerManager.health -= 1;
					Destroy (this.gameObject);
				}
				if (string.Equals (itemDescription, "Bird", System.StringComparison.CurrentCultureIgnoreCase)) {
					playerManager.health -= 1;
					Destroy (this.gameObject);
				}
				if (string.Equals (itemDescription, "Jet", System.StringComparison.CurrentCultureIgnoreCase)) {
					playerManager.health -= 1;
					Destroy (this.gameObject);
				}
				if (string.Equals (itemDescription, "Satelite", System.StringComparison.CurrentCultureIgnoreCase)) {
					playerManager.health -= 1;
					Destroy (this.gameObject);
				}
			}

		}else if (other.CompareTag ("Bullet")) {
		
			Destroy (this.gameObject);
		
		
		
		}
	}
		
	IEnumerator SetCollectorPlayerSpeed(GameObject GOtoDestroy){

		CollectorLookWalk coll = player.GetComponent<CollectorLookWalk>();
		float CurrentVelocity = coll.velocity;
		coll.velocity = 60;

		//coll.velocity = 10;
		//Does not switch back?

		yield return new WaitForSeconds(4f);
		Debug.Log ("slow player");
	
		coll.velocity = CurrentVelocity;

		Destroy (GOtoDestroy);



	}





		/*void OnCollisionEnter (Collision collision){ 

		//other.transform.GetComponent <Collider> ().enabled = false;
		Debug.Log ("i triggered");

		if (collision.transform.CompareTag ("Item")) {

			Destroy (collision.gameObject);
			/*foreach (ContactPoint contact in collision.contacts) {

				contact.otherCollider.gameObject;
				Destroy (contact.thisCollider.gameObject);
			}
			//if (this.transform.gameObject.InstanceID() > other.transform.InstanceID())

	
		}
		return;
	
		}*/




}