using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookControlMovement : MonoBehaviour {

	public bool isLooking = false;
	[SerializeField] private LayerMask layerMaskMoveControl;


	private Transform thisTransform;
	private Camera mainCamera;

	void Awake(){
		thisTransform = transform;
		mainCamera = Camera.main;
	
	}
	// Use this for initialization
	//void Start () {
		
	//}
	
	// Update is called once per frame

	void Update () {

		RaycastHit hit;
		Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.rotation * Vector3.forward);

		if (Physics.Raycast (ray, out hit, 20, layerMaskMoveControl)) {

			isLooking = true;

			thisTransform.position = hit.point;

		
		} else {
			isLooking = false;
		}
	}
}
