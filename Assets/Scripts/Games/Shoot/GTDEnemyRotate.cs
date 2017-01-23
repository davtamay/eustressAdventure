using UnityEngine;
using System.Collections;

public class GTDEnemyRotate : MonoBehaviour {

	private Transform player;
	private float curDistance;
	private bool isShooting;
	private float timer;

	[SerializeField] private float bulletDelay;
	[SerializeField] private float bulletSpeed;
	[SerializeField] private float speed;
	[SerializeField] private float circleRadius;
	[SerializeField] private float distanceUntilCircle;

	void Start () {
		player = GameObject.FindWithTag ("Player").transform;
	

	}


	void Update() {



		curDistance = Vector3.Distance (player.position, transform.position);

		if (curDistance < distanceUntilCircle) {
			transform.position = Vector3.Lerp (transform.position, new Vector3 (Mathf.Cos (Time.time) * circleRadius, 0, Mathf.Sin (Time.time) * circleRadius) + player.position, Time.deltaTime);

			timer += Time.deltaTime;
			transform.LookAt(player.position);
			if (timer > bulletDelay) {
				BulletManager.BulletPosition (transform.TransformPoint (Vector3.forward * 1.5f), Quaternion.LookRotation (player.transform.position - transform.position), 0.3f,1);
				timer = 0;

			}
			return;
		} 
		Vector3 dir = Vector3.Normalize (player.position - transform.position);
		transform.Translate (dir * speed);

	}

	void OnTriggerEnter(Collider other){

		if (other.transform.GetComponent<BulletControll>().bulletID == 0)  {
			other.gameObject.SetActive (false);
			Destroy (gameObject);
			PlayerManager.Instance.points = 1;
		}
	}






	
}
