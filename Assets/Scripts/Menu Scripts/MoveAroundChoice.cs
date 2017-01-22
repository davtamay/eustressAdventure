using UnityEngine;
using System.Collections;

public class MoveAroundChoice : MonoBehaviour {

	public float xSpeed = 0.3f, xAmplitude = 200.0f, ySpeed = 0.3f, yAmplitude = 200.00f;

	public float offset = 0.5f;

	public RectTransform rect;

	void Start () {
	
		rect = GetComponent <RectTransform> ();
	
	}



	void Update () {


		rect.localPosition = new Vector3 (xAmplitude * Mathf.Cos (offset + Time.time * xSpeed), 
			yAmplitude * Mathf.Sin(offset + Time.time * ySpeed) , 0.0f);




	


	}
}
