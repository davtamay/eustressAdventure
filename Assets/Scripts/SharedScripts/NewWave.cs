using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWave : MonoBehaviour {

	private Vector3 originalPosition;
	[SerializeField] private float moveSpeed;

	public float timeUntilDisapear;
	private float timer; 



	void OnEnable(){
		originalPosition = transform.position;
		StartCoroutine (OnUpdate ());
	}

	IEnumerator OnUpdate(){
	
		while (timeUntilDisapear >= timer) {

			timer += Time.deltaTime;

			transform.Translate(Vector3.right * (Time.unscaledDeltaTime * moveSpeed)); 

			yield return null;

		}
		timer = 0;
		gameObject.SetActive (false);
	
	
	
	}

	void OnDisable(){
	
		transform.position = originalPosition;
	
	}
}
