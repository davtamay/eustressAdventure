using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWaveTransition : MonoBehaviour {

	private Vector3 originalPosition;
	[SerializeField] private float moveSpeed = -100;

	public float timeUntilDisapear = 8;
	private float timer; 

	[SerializeField]private Transform objectToRotateAround;
	[SerializeField]private float fadeSpeed = 6f;

	private SpriteRenderer thisSpriteRenderer;
	private Color alphaColor;

	[SerializeField]bool isEnableAndDisablePause;



	void OnEnable(){

		if (thisSpriteRenderer == null) {
			thisSpriteRenderer = GetComponent<SpriteRenderer> ();
			alphaColor = thisSpriteRenderer.color;
			alphaColor.a = 0;
		}

		if (objectToRotateAround == null)
		objectToRotateAround = Camera.main.transform;
		
		originalPosition = transform.localPosition;

		StartCoroutine (OnStart ());
	}

	IEnumerator OnStart(){
	
		if (isEnableAndDisablePause)
			GameController.Instance.Paused = true;
		
		while (alphaColor.a < 0.9f){//!Mathf.Approximately(alphaColor.a,0.9f)) {

			transform.RotateAround(objectToRotateAround.position , Vector3.up, moveSpeed * Time.unscaledDeltaTime);

			transform.LookAt (objectToRotateAround.position);


			alphaColor.a = Mathf.Lerp (alphaColor.a, 1 , Time.unscaledDeltaTime * fadeSpeed);

			thisSpriteRenderer.color = alphaColor;

			yield return null;


		}

		while (timeUntilDisapear >= timer) {

			timer += Time.unscaledDeltaTime;

			transform.RotateAround(objectToRotateAround.position , Vector3.up, moveSpeed * Time.unscaledDeltaTime);

			transform.LookAt (objectToRotateAround.position);
		
			yield return null;

		}
		timer = 0;

	

		while (alphaColor.a > 0.1f){//!Mathf.Approximately(alphaColor.a,0.1f)) {

			transform.RotateAround(objectToRotateAround.position , Vector3.up, moveSpeed * Time.unscaledDeltaTime);

			transform.LookAt (objectToRotateAround.position);


			alphaColor.a = Mathf.Lerp (alphaColor.a, 0 , Time.unscaledDeltaTime * fadeSpeed);

			thisSpriteRenderer.color = alphaColor;

			yield return null;

	
		}
			
		if (isEnableAndDisablePause)
			GameController.Instance.Paused = false;
			
			gameObject.SetActive (false);
	
	}

	void OnDisable(){
	

		transform.localPosition = originalPosition;

	
	}
}
