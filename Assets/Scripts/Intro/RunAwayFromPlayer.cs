using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayFromPlayer : MonoBehaviour {


	[SerializeField] Transform wayPointParent;
	[SerializeField] float speed;
	[SerializeField] GameObject objectToDropOnCollision;

	public bool isStandingBy;
	private bool isGODroped;

	private Vector3[] wayPoints;
	private Transform player;
	private Transform thisTransform;
	private Animator thisAnimator;
	private Vector3 curentTarget;


	void Start(){

		thisTransform = transform;
		thisAnimator = GetComponent<Animator> ();
		player = GameObject.FindWithTag ("Player").transform;
		objectToDropOnCollision.SetActive (false);

		wayPoints = new Vector3[wayPointParent.childCount];
		int i = 0;
		foreach (Transform wP in wayPointParent.transform) {
			
			wayPoints [i] = wP.position;
			wayPoints [i].y = thisTransform.position.y;

			i++;

		
		}
	
		isStandingBy = true;
	}

	void OnCollisionEnter(Collision other){
	
		if (other.transform.CompareTag ("Player") && !isGODroped) {
			objectToDropOnCollision.transform.parent = null;
			objectToDropOnCollision.transform.position = thisTransform.position;
			objectToDropOnCollision.SetActive (true);
			isGODroped = true;

		}
	
	
	
	
	}
	void OnTriggerStay(){



	//	isStandingBy = false;
	//	curentTarget = GetFurthestWayPointFromPlayer(200f);
	//	thisTransform.position= GetFurthestWayPointFromPlayer();


	}

	void OnTriggerEnter(Collider other){

		if (other.transform.CompareTag ("Player")) {
			isStandingBy = false;
			curentTarget = GetFurthestWayPointFromPlayer(200f);
			thisAnimator.SetBool ("IsRunning", true);
		}


	}
	void OnTriggerExit(Collider other){

		if (other.transform.CompareTag ("Player")){
		
			thisAnimator.SetBool ("IsRunning", false);
			isStandingBy = true;
			Debug.Log ("THISISEXXXITTT");
		}
	}

	void LateUpdate(){


		if (!isStandingBy) {

			thisTransform.position += (curentTarget - thisTransform.position).normalized * speed * Time.deltaTime;
			thisTransform.LookAt (curentTarget);

			if(Physics.BoxCast(thisTransform.position, Vector3.one, thisTransform.forward,transform.rotation, 5))
				curentTarget = GetFurthestWayPointFromPlayer (200f);//curentTarget = wayPoints [Random.Range (0, wayPoints.Length)];

			if (Vector3.Distance (curentTarget, thisTransform.position) < 15f || Vector3.Dot (thisTransform.forward, player.position - thisTransform.position) > 0.50f) {
				curentTarget = GetFurthestWayPointFromPlayer (200f);




				if (Vector3.Distance (curentTarget, GetFurthestWayPointFromPlayer (200f)) < 2f  )
					curentTarget = wayPoints [Random.Range (0, wayPoints.Length)];


			}
		}	
	}


	public Vector3 GetFurthestWayPointFromPlayer(float radiusPermitance = 40f){


		Vector3 furtherWayPoint = wayPoints[0];
		float furthestDistance = 0;


		foreach (Vector3 wp in wayPoints) {


			float playerDistance = Vector3.Distance (player.position, wp);
			float thisDistance = Vector3.Distance (thisTransform.position, wp);

			if(Vector3.Distance(thisTransform.position, wp) > radiusPermitance)
				continue;
			
			if (playerDistance > thisDistance){

				if (playerDistance > furthestDistance) {

					furthestDistance = playerDistance;

					furtherWayPoint = wp;
					//closestWayPoint = wayPoints[0];

				}
			}

		}

		return furtherWayPoint;

	}
	public Vector3 ClosestWayPointFromPlayer(float radiusPermitance){



		Vector3 closestWayPoint = wayPoints[0];
		float closestDistance = Mathf.Infinity;


		foreach (Vector3 wp in wayPoints) {

			float playerDistance = Vector3.Distance (player.position, wp);
			float thisDistance = Vector3.Distance (thisTransform.position, wp);

			if(Vector3.Distance(thisTransform.position, wp) > radiusPermitance)
				continue;

			if (playerDistance >= thisDistance){

				if (thisDistance <= closestDistance) {

					closestDistance = thisDistance;

					closestWayPoint = wp;
					//closestWayPoint = wayPoints[0];

				}
			}

		}

		return closestWayPoint;





	}

}
