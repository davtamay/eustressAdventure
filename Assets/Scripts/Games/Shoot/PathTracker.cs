using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof (LineRenderer))]
[RequireComponent(typeof (TrailRenderer))]
public class PathTracker : MonoBehaviour {

	//private LineRenderer lineRenderer;
	//TrailRenderer trailRenderer;
	private int lengthOfLineRenderer = 1000;
	//[SerializeField]private float widthMultiply = 0.2f;


	void Start(){
	
		//lineRenderer = GetComponent<LineRenderer> ();
		//lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		//lineRenderer.widthMultiplier = widthMultiply;
		//lineRenderer.numPositions = lengthOfLineRenderer;

	
	}
	//void OnDrawGizmos(){
	//	float timer = 0;
	//	lineRenderer = GetComponent<LineRenderer> ();
		//trailRenderer = GetComponent<TrailRenderer> ();
	//	StartCoroutine (LineDelay ());
	/*	for (int i =0; i <lengthOfLineRenderer; i++) {

			timer += Time.deltaTime;

			if (timer > 1f) {
				lineRenderer.SetPosition (i, transform.position + Vector3.back);
				timer = 0;
			}
			
			}*/

		


//	}

	IEnumerator LineDelay(){
		while (true) {
			for (int i =0; i <lengthOfLineRenderer; i++) {

				yield return new WaitForSeconds (2f);
				//lineRenderer.SetPosition (i, transform.forward);

			}
			yield return null;

		}
	}
}
