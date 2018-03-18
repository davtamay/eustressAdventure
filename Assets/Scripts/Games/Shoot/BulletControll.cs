using UnityEngine;
using System.Collections;

public class BulletControll : MonoBehaviour {

	public float bulletSpeed;
	public int bulletID;
	[SerializeField]private float timeUntilOnDisabled;
	private Rigidbody myRigidBody;



	void OnEnable(){

		StartCoroutine (DestoyBullet());
		myRigidBody = GetComponent<Rigidbody> ();
		myRigidBody.velocity = Vector3.zero;



	}

	void FixedUpdate(){
	//	myRigidBody.AddRelativeForce (0, 0, bulletSpeed,ForceMode.Impulse);
		transform.Translate (Vector3.forward * bulletSpeed);
	//	transform.position += transform.forward.normalized * bulletSpeed;

	//	transform.Translate (0, 0, bulletSpeed);
	//	transform.position += transform.forward.normalized * bulletSpeed;//new Vector3 (0, 0, bulletSpeed);
	}

	IEnumerator DestoyBullet(){


		
		yield return new WaitForSeconds (timeUntilOnDisabled);
		gameObject.SetActive (false);


	
	}
}
