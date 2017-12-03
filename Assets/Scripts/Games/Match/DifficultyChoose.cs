using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DifficultyChoose : MonoBehaviour {

	private Camera cam;
	private Image buttonFill;
	private Collider col;
	private float countDown;
	private CardSpawner cardSpawner;
	[SerializeField]private float timeToSelect;
	[SerializeField]private bool is16CardsButton;
	[SerializeField]private bool is32CardsButton;
	[SerializeField]private bool is48CardsButton;


	void Awake(){
		cardSpawner = GameObject.FindWithTag ("GameController").GetComponent<CardSpawner> ();
		cam = Camera.main;
		buttonFill = GetComponent<Image> ();
		col = GetComponent<Collider> ();
	
	}
	void Update(){


		Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);


		RaycastHit hit;


		if (col.Raycast (ray, out hit, 400)) {

			if (hit.transform.gameObject.CompareTag ("Button")) {
				//USE SCRIPTABLE CHECKER
				//if (!GameController.Instance.IsMenuActive) {



					StartCoroutine ("ButtonLook");


			//	} 

			}

		} else {

		//	ExecuteEvents.Execute<IPointerExitHandler> (currentButton, data, ExecuteEvents.pointerExitHandler);
			countDown = timeToSelect;
			buttonFill.fillAmount = 1.0f;

		}

	}	

	IEnumerator ButtonLook(){

		// highlight

		//ExecuteEvents.Execute<IPointerEnterHandler> (hitButton, data, ExecuteEvents.pointerEnterHandler);
		countDown -= Time.deltaTime;

		buttonFill.fillAmount = countDown / timeToSelect;


		if (countDown < 0.0f) {
			if (is16CardsButton) {
				cardSpawner.ChangeWave (Difficulty.easy);
			}
			if (is32CardsButton) {
				cardSpawner.ChangeWave (Difficulty.medium);
			}
			if (is48CardsButton) {
				cardSpawner.ChangeWave (Difficulty.hard);

			}
			countDown = timeToSelect;
			yield break;
		
		}
		yield return null;

}

}

