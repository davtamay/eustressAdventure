using UnityEngine;
using System.Collections;

public class ItemRotate : MonoBehaviour {

	private Transform myTransform;
	public Vector3 initRotation;
	public Vector3 turnSpeed;

	[SerializeField]private bool isUseUnscaledTime;


	void Awake(){

		myTransform = transform;

		myTransform.localEulerAngles = initRotation;

	
	}

	void OnEnable(){

		if (isUseUnscaledTime)
			StartCoroutine (UnscaledRotate ());
		else
			StartCoroutine (scaledRotate ());


	}
	void OnDisable(){

		StopAllCoroutines ();

	}

	IEnumerator scaledRotate(){

		while (true) {

			myTransform.Rotate (turnSpeed * Time.deltaTime);

			yield return null;
		}

	}
	IEnumerator UnscaledRotate(){

		while (true) {
			
				myTransform.Rotate (turnSpeed * Time.unscaledDeltaTime);

		
			yield return new WaitForSecondsRealtime(0.01f);
		}
	}
}
