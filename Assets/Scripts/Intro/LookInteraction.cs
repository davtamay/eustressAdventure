using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class LookInteraction : MonoBehaviour {

	[SerializeField] GameObject imageGO;
	[SerializeField] float lookTime;
	[SerializeField] float timeUntilImageDeactivate = 5f;
	[SerializeField] float lookDistance;

	[SerializeField]UnityEvent onLookClick;

	[SerializeField] bool isItemForSlot;


	private Image image;
	private Collider thisCollider;
	public Collider parentCollider;
	private Camera cam;
	float timer;


	void Awake () {


		cam = Camera.main;
		timer = lookTime;
		thisCollider = GetComponent<Collider> ();

		if(!isItemForSlot)
		parentCollider = transform.parent.GetComponent<Collider> ();

		image = GetComponentInChildren<Image> ();

		imageGO = image.transform.parent.gameObject;
		imageGO.SetActive (false);
		
	}

	private float timeActive;

	IEnumerator EnableAndDisable(){
	//	float timer = 0;
		timeActive = timeUntilImageDeactivate;

		isActive = true;
		imageGO.SetActive (true);

		while (0 < timeActive) {


			yield return null;
			imageGO.transform.LookAt (cam.transform);

			timeActive -= Time.deltaTime;

		}

		isActive = false;
		imageGO.SetActive (false);
	}

	private bool isActive;
	void Update () {

		RaycastHit hit;
		Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);
			

			

		
		
		if (thisCollider.Raycast (ray, out hit, lookDistance)) {

			timeActive = timeUntilImageDeactivate;

			if (isItemForSlot) {

			//	timer = lookTime;

				if (!isActive) {
					StartCoroutine (EnableAndDisable ());
				}
			
			}



			timer -= Time.deltaTime;
			image.fillAmount = timer / lookTime;

			if (0f > timer) {
			
				onLookClick.Invoke ();
				timeActive = 0;

				if (isItemForSlot) 
					PlayerManager.Instance.AddItemToSlot (gameObject);

				timer = lookTime;
				

			}



		
		} else {

			timer = lookTime;
			image.fillAmount = timer / lookTime;
		//	imageGO.SetActive (false);
		
		}

		if(!isItemForSlot)
		if (parentCollider.Raycast (ray, out hit, lookDistance)) {

			timeActive = timeUntilImageDeactivate;
			timer = lookTime;

			if (!isActive) {
				StartCoroutine (EnableAndDisable ());
			}


		}




		}

}
