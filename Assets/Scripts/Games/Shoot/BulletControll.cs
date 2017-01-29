using UnityEngine;
using System.Collections;

public class BulletControll : MonoBehaviour {

	public float bulletSpeed;
	public int bulletID;
	[SerializeField]private float timeUntilOnDisabled;




	void OnEnable(){

		StartCoroutine (DestoyBullet());
	



	}

	void FixedUpdate(){
	//	myRigidBody.AddRelativeForce (0, 0, bulletSpeed,ForceMode.Impulse);
		transform.Translate (0, 0, bulletSpeed);
	}

	IEnumerator DestoyBullet(){


		
		yield return new WaitForSeconds (timeUntilOnDisabled);
		gameObject.SetActive (false);


	
	}
}
