using UnityEngine;
using System.Collections;


public class GTDEnemyAim : MonoBehaviour {

	public GameObject bullet;
	public float bulletDelay;
	public float bulletSpeed;
	public GameObject playerTarget;
	public bool isDirectShoot;




	void OnEnable(){
		StartCoroutine (OnUpdate());

	}

	IEnumerator OnUpdate(){

		while (true) {

			yield return new WaitForSeconds(bulletDelay);	


			if (!isDirectShoot) 
				BulletManager.BulletPosition (transform.TransformPoint (Vector3.forward * 1.5f), Quaternion.LookRotation (playerTarget.transform.position - transform.position),0.3f,1 );
			else
				BulletManager.BulletPosition (transform.TransformPoint (Vector3.forward * 1f), transform.rotation, 0.3f,1);
		}
	} 

	


	void OnTriggerEnter(Collider other){
	
		if (other.transform.GetComponent<BulletControll>().bulletID == 0) {
			other.gameObject.SetActive (false);
			Destroy (gameObject);
			PlayerManager.Instance.points = 1;
		}
	}
}
