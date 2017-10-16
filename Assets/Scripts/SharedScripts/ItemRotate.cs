using UnityEngine;
using System.Collections;

public class ItemRotate : MonoBehaviour {

	private Transform myTransform;
	public Vector3 initRotation;
	public Vector3 turnSpeed;

	[SerializeField]private bool isUseUnscaledTime;


	void Start(){

		myTransform = transform;

		myTransform.localEulerAngles = initRotation;

		if (isUseUnscaledTime)
			StartCoroutine (UnscaledRotate ());
		else
			StartCoroutine (scaledRotate ());
	}

	IEnumerator UnscaledRotate(){

		while (true) {
			if (!this.gameObject.activeInHierarchy)
				StopAllCoroutines ();

			myTransform.Rotate (turnSpeed * Time.deltaTime);

			yield return null;
		}

	}
	IEnumerator scaledRotate(){

		while (true) {
			if (!this.gameObject.activeInHierarchy)
			
				myTransform.Rotate (turnSpeed * Time.unscaledDeltaTime);
				StopAllCoroutines ();
		
			yield return null;
		}
	}
}
