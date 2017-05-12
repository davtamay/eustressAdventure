using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class LookInteraction : MonoBehaviour {

	[SerializeField]private string objectName;
	[SerializeField] GameObject imageGO;
	[SerializeField] Vector3 imageOffset;
	[SerializeField] float lookTime;
	[SerializeField] float lookDistance;
	[SerializeField]UnityEvent onLookClick;


	private Image image;
	private Collider thisCollider;
	private Camera cam;
	float timer;

	void Awake () {


		cam = Camera.main;
		timer = lookTime;
		thisCollider = GetComponent<Collider> ();


		image = GetComponentInChildren<Image> ();

		imageGO = image.transform.parent.gameObject;
		imageGO.SetActive (false);
		
	}
	

	void Update () {

		RaycastHit hit;
		Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);
			
		if (thisCollider.Raycast (ray, out hit, lookDistance)) {

			if(!imageGO.activeInHierarchy)
			imageGO.SetActive (true);

			imageGO.transform.LookAt (cam.transform);

			timer -= Time.deltaTime;
			image.fillAmount = timer / lookTime;

			if (0f > timer) {
			
				onLookClick.Invoke ();
			}



		
		} else {

			timer = lookTime;
			imageGO.SetActive (false);
		
		}




		}

}
