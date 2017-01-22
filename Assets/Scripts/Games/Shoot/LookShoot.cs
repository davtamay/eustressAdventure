using UnityEngine;
using System.Collections;

public class LookShoot : MonoBehaviour {

	private Camera cam;

	[SerializeField]private float delayShoot;




	IEnumerator Start(){
	
		cam = Camera.main;
	
	
		while (true) {


			yield return new WaitForSeconds (delayShoot);


			BulletManager.BulletPosition (cam.transform.TransformPoint (Vector3.forward * 1.8f), cam.transform.rotation, 1);


		}


	}

}
