using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTransform : MonoBehaviour {

	Transform thisTransform;
	[SerializeField] private float speed;

	[Header("Move With Sin Settings")]
	[SerializeField] private bool isMoveWithSin;
	[SerializeField] private Vector3 positionRange;


	[Header("Move With Inside Random Sphere Settings")]
	[SerializeField] private bool isMoveWithSphereRandom;
	[SerializeField] private float radiusMagnitude;
	[SerializeField] private float distanceUntilRandomChange;
	[SerializeField] private bool xConstraint;
	[SerializeField] private bool yConstraint;
	[SerializeField] private bool zConstraint;

	[Header("Position Settings")]
	[SerializeField] private bool isMovePosition;
	[SerializeField] Vector3 moveMagnitude;

	[Header("Rotate Settings")]
	[SerializeField] private bool isRotate;
	[SerializeField] Vector3 rotateMagnitude;

	private Vector3 originalPosition;
	private Vector3 curRandomPos;

	// Use this for initialization
	void Start () {
		
		thisTransform = transform;
		originalPosition = thisTransform.position;
	}
	
	// Update is called once per frame
	private bool isFirstSearch = false;
	void Update () {
		
		if(isMoveWithSin)
		thisTransform.position =  new Vector3 ( thisTransform.position.x + positionRange.x *  Mathf.Sin (Time.time * speed),thisTransform.position.y + positionRange.y *  Mathf.Sin (Time.time * speed), thisTransform.position.z + positionRange.z *  Mathf.Sin (Time.time * speed));

		if (isMoveWithSphereRandom) {



			if (Vector3.Distance (curRandomPos, thisTransform.position) < distanceUntilRandomChange || !isFirstSearch) {
			
				isFirstSearch = true;

				curRandomPos = originalPosition + Random.insideUnitSphere * radiusMagnitude;

				if (xConstraint)
					curRandomPos.x = thisTransform.position.x;
				if (yConstraint)
					curRandomPos.y = thisTransform.position.y;
				if (zConstraint)
					curRandomPos.z = thisTransform.position.z;
			


			
			
	
			}
		
			thisTransform.position += (curRandomPos - thisTransform.position).normalized * Time.deltaTime * speed ;

		
		
		
		}
		if (isRotate) 
			thisTransform.Rotate (rotateMagnitude * Time.deltaTime, Space.Self);

		if (isMovePosition)
			thisTransform.Translate (moveMagnitude * Time.deltaTime, Space.Self);
		

			
	}
}
