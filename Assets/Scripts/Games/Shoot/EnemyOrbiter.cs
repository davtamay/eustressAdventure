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
	[SerializeField] private float upDistance;
	//[SerializeField] private bool isHelicopter;
	[SerializeField] private float zOffset;
	[SerializeField] private float xOffset;
	[SerializeField] private bool isHelicopter;



	void Awake(){
		player = GameObject.FindWithTag ("Player").transform;
		thisTransform = transform;
	}
	
	public float turningDirection = 0f;


	void LateUpdate() {


		curDistance = Vector3.Distance (player.position, thisTransform.position);

	
		if (curDistance < distanceUntilCircle){
	

			if (isHelicopter) {

			/*	if (Physics.SphereCast (thisTransform.position,50f, thisTransform.right, out hit, 7f))
					turningDirection -= 0.005f;

				if (Physics.SphereCast (thisTransform.position,50f, thisTransform.right * -1, out hit, 7f))
					turningDirection += 0.005f;*/


			/*	if (Physics.SphereCast (thisTransform.position, 8f, thisTransform.rotation * Vector3.forward, out hit, 6f))
					turningDirection += 0.1f;
				else
					turningDirection = 0f;
			//		thisTransform.position += thisTransform.rotation * Vector3.forward;*/
					
				thisTransform.position = Vector3.Lerp (thisTransform.position, new Vector3 (Mathf.Cos (Time.time ) * circleRadius + xOffset, upDistance, Mathf.Sin (Time.time ) * circleRadius + zOffset) + player.position, Time.deltaTime * orbiterSpeed);

		//		thisTransform.position += Vector3.forward * turningDirection;
				//if (Physics.SphereCast (thisTransform.position, 20f, thisTransform.ba, out hit, 7f))
			//		thisTransform.localPosition += new Vector3 (0, 0, 10);//thisTransform.forward;
			}else
			thisTransform.position = Vector3.Lerp (thisTransform.position, new Vector3 (Mathf.Cos (Time.time) * circleRadius + xOffset, upDistance, Mathf.Sin (Time.time) * circleRadius + zOffset) + player.position , Time.deltaTime * orbiterSpeed);
		


			timer += Time.deltaTime;



			transform.LookAt(player.position);


			if (timer > bulletDelay) {
			
				if (isHelicopter)
					EnemyBulletManager.BulletPosition (thisTransform.TransformPoint (Vector3.forward * 1.5f),  transform.rotation , 3f);//Quaternion.LookRotation ((player.position - thisTransform.position) + player.forward * zOffset ), 0.3f);
						else
							EnemyBulletManager.BulletPosition (thisTransform.TransformPoint (Vector3.forward * 1.5f), transform.rotation, bulletSpeed);// Quaternion.LookRotation ((player.position - thisTransform.position) + player.forward * zOffset), 0.3f);
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
				GameController.Instance.HitEffectLocation (other.transform.position);
				other.gameObject.SetActive (false);
				Destroy (gameObject);
				PlayerManager.Instance.points = 1;
			}

		} else
			return;
	}






	
}
