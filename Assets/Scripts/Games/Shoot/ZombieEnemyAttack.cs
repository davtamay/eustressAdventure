using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemyAttack : MonoBehaviour {
	//private Collider thisCollider;
	private Animator thisAnimator;
	private Transform thisTransform;
	private Transform player;
	[SerializeField] float speed = 20f;
	[SerializeField] float disToAttack = 5f;
	//public AnimationCurve animCurve;
//	private float distanceFromGround = 10f;

	private Vector3 destUp;

	private Vector3 dirToPlayer;

	void Awake(){
		thisTransform = transform;
		player = GameObject.FindWithTag ("Player").transform;
	//	thisCollider = GetComponent<Collider> ();
		thisAnimator = GetComponent<Animator> ();
	}

	void Update(){
	
		float Distance = (player.position - thisTransform.position).sqrMagnitude;

	

	//	if (thisCollider.bounds.Contains (player.position)) {
		if(Distance < disToAttack * disToAttack){
			thisAnimator.SetTrigger ("isAttacking");
		
			return;
		
		} 

	//	Vector3 elevatedPos = transform.position;
	/*	RaycastHit hit;

		if (Physics.Raycast (thisTransform.position, Vector3.down, out hit)) {
		
		//	elevatedPos.y = (hit.point + Vector3.up * distanceFromGround).y;
			destUp = hit.normal;


		}*/
			
			
			dirToPlayer = (player.position - thisTransform.position).normalized;


			thisTransform.position +=  (dirToPlayer * Time.deltaTime* speed);

			
		
			//thisTransform.position = elevatedPos;

		//	thisTransform.up = Vector3.Slerp (thisTransform.up, destUp, Time.deltaTime);
		thisTransform.parent.rotation.SetLookRotation (thisTransform.position, Vector3.up);
		//	LookAt (player.rotation * Quaternion.AngleAxis(180,Vector3.up), Vector3.up);
	
	}
	/*void LateUpdate(){
	//	thisTransform.parent.LookAt (player.position);
	//	thisTransform.up = Vector3.Slerp (thisTransform.up, destUp, Time.deltaTime);
	}*/

	public void AttackPlayer(){
	
	
		PlayerManager.Instance.health = -1;
	
	
	}


}
