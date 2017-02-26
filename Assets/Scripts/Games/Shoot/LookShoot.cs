using UnityEngine;
using System.Collections;

public class LookShoot : MonoBehaviour {

	private Camera cam;

	public static float delayShoot = 0.8f;
	public static float originalDelayShoot;




	IEnumerator Start(){
	
		originalDelayShoot = delayShoot;
		cam = Camera.main;
	
	
		while (true) {


			yield return new WaitForSeconds (delayShoot);


			BulletManager.BulletPosition (cam.transform.TransformPoint (Vector3.forward * 1.8f), cam.transform.rotation, 1);


		}

	

	}
	//void OnCollisionEnter(Collision other){

	//	if (other.transform.GetComponent<BulletControll> ().bulletID == 1) {
	//		other.gameObject.SetActive (false);
	//		PlayerManager.Instance.points = -1;

	//	}

	
	
//	}

}
