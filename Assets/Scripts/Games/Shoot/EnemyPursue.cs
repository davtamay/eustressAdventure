using UnityEngine;
using System.Collections;

public class EnemyPursue : MonoBehaviour {


	private Transform player;
	private Transform thisTransform;

	[SerializeField]private float speed;
	[SerializeField]private bool isPursueEnemy;

	void Awake(){
		player = GameObject.FindWithTag ("Player").transform;
		thisTransform = transform;
	
	}


	

	void Update () {


		if (isPursueEnemy) {
			Vector3 dir = (player.position - transform.position).normalized;
			thisTransform.position += dir * speed * Time.deltaTime;
			thisTransform.LookAt (thisTransform.position);

		}
	}
}
