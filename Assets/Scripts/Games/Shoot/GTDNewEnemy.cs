using UnityEngine;
using System.Collections;

public class GTDNewEnemy : MonoBehaviour {


	private Transform player;

	[SerializeField]private float speed;
	[SerializeField]private bool isPurseEnemy;



	void Start () {
		player = GameObject.FindWithTag ("Player").transform;

	}
	

	void Update () {
		
		if (isPurseEnemy) {
			Vector3 dir = Vector3.Normalize (player.position - transform.position);
			transform.Translate (dir * speed);
		}
	}
}
