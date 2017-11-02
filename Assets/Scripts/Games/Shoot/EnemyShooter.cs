using UnityEngine;
using System.Collections;


public class EnemyShooter : MonoBehaviour {

	//public GameObject bullet;
	public float bulletDelay;
	[SerializeField]private float magnitudeOffsetDelay;
	public float bulletSpeed;
	public GameObject playerTarget;
	public bool isDirectShoot;




	void OnEnable(){
		playerTarget = Camera.main.gameObject;//GameObject.FindWithTag ("Player");
		StartCoroutine (OnUpdate());

	}

	IEnumerator OnUpdate(){

		while (true) {
			
			yield return new WaitForSeconds(bulletDelay + Random.Range(-magnitudeOffsetDelay, magnitudeOffsetDelay));	


			if (isDirectShoot) 
				EnemyBulletManager.BulletPosition (transform.TransformPoint (Vector3.forward * 1f),transform.rotation, bulletSpeed);
			else
				EnemyBulletManager.BulletPosition (transform.TransformPoint (Vector3.forward * 1.5f), Quaternion.LookRotation (playerTarget.transform.position - transform.position),bulletSpeed);

				
		}
	} 

	


	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Bullet")) {
			int BulletID = other.transform.GetComponent<BulletControll> ().bulletID;
			if (BulletID == 0) {
				GameController.Instance.HitEffectLocation (other.transform.position);
				other.gameObject.SetActive (false);
				Destroy (gameObject);
				PlayerManager.Instance.points = 1;
			}
		}
	}
}
