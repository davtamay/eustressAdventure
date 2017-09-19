using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrollz : MonoBehaviour {

	[SerializeField] private float scrollSpeed = 20;
	[SerializeField] private float amountOfSecondsUntilRestart = 10;

	private Transform thisTransform;
	private Vector3 initialPos;

	void Awake(){
		thisTransform = transform;
		initialPos = thisTransform.position;
	}
	private float timer; 
	void Update () {

		timer += Time.deltaTime;

		Vector3 pos = transform.position;
		Vector3 localVectorUp = transform.TransformDirection (0, 1, 0);

		pos += localVectorUp * scrollSpeed * Time.deltaTime;
		transform.position = pos;

		if (timer > amountOfSecondsUntilRestart) {
			thisTransform.position = initialPos;
			timer = 0;
		}


	}
}
