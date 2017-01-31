using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWave : MonoBehaviour {

	private Vector3 originalPosition;
	[SerializeField] private float moveSpeed;

	public float timeUntilDisapear;
	private float timer; 

	private Transform player;
	

	void OnEnable(){

		player = Camera.main.transform;
		originalPosition = transform.position;

		StartCoroutine (OnUpdate ());
	}

	IEnumerator OnUpdate(){
	
		while (timeUntilDisapear >= timer) {

			timer += Time.deltaTime;

			transform.RotateAround(player.position , Vector3.up, moveSpeed * Time.deltaTime);

			transform.LookAt (player.position);
		
			yield return null;

		}
		timer = 0;
		gameObject.SetActive (false);
	
	
	
	}

	void OnDisable(){
	
		transform.position = originalPosition;
	
	}
}
