using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class CheckAndInstantiate : MonoBehaviour {

	private Collider thisCollider;
	[SerializeField] private GameObject prefab;
	[SerializeField] private float instantiateBasedOnClearCheckTime;
	[SerializeField]private Vector3 initialPos;
	void Start () {
		initialPos = transform.position;
		thisCollider = GetComponent<Collider> ();
	}
	
	private float timer;
	void OnTriggerStay(Collider other){
		

		if (other.CompareTag ("Obstacle")) {
			timer = 0;
			return;
		} else {

			timer += Time.deltaTime;

			if (timer > instantiateBasedOnClearCheckTime) {
				timer = 0;
				GameObject	tempGO = Instantiate (prefab, initialPos, Quaternion.identity);
				tempGO.GetComponent<Rigidbody> ().velocity = Vector3.zero;
				tempGO.transform.parent = transform;
			}
			}

			

	}
	void OnDisable(){

		foreach (Transform c in transform)
			Destroy (c.gameObject);


	}
}
