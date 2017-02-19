using UnityEngine;
using System.Collections;

public class EnemyOrbiter : MonoBehaviour {

	private Transform player;
	private float curDistance;
	private bool isShooting;
	private float timer;

	private Transform thisTransform;

	[SerializeField] private float bulletDelay;
	[SerializeField] private float bulletSpeed;
	[SerializeField] private float speed;
	[SerializeField] private float orbiterSpeed;
	[SerializeField] private float circleRadius;
	[SerializeField] private float distanceUntilCircle;

	void Awake(){
		player = GameObject.FindWithTag ("Player").transform;
		thisTransform = transform;
	}
	


	void Update() {

		bool isOrbit = false;

		curDistance = Vector3.Distance (player.position, thisTransform.position);

	
		if (curDistance < distanceUntilCircle){
		//	isOrbit = true;
		//new 2/13/17
	//	if (isOrbit){

			thisTransform.position = Vector3.Lerp (thisTransform.position, new Vector3 (Mathf.Cos (Time.time) * circleRadius, 0, Mathf.Sin (Time.time) * circleRadius) + player.position, Time.deltaTime);
		//	transform.RotateAround (player.position, transform.up.normalized, Time.deltaTime * orbiterSpeed);


			timer += Time.deltaTime;
			transform.LookAt(player.position);
			if (timer > bulletDelay) {
				EnemyBulletManager.BulletPosition (thisTransform.TransformPoint (Vector3.forward * 1.5f), Quaternion.LookRotation (player.transform.position - transform.position), 0.3f);
				timer = 0;

			}
			return;
		} 
		Vector3 dir = Vector3.Normalize (player.position - thisTransform.position);
		thisTransform.Translate (dir * speed);


	}

	void OnTriggerEnter(Collider other){

		if(other.CompareTag("Bullet")){


			int BulletID = other.transform.GetComponent<BulletControll> ().bulletID;
			if (BulletID == 0) {
				other.gameObject.SetActive (false);
				Destroy (gameObject);
				PlayerManager.Instance.points = 1;
			}

		} else
			return;
	}






	
}
