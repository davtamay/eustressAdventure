using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererGuide : MonoBehaviour {

	[SerializeField]private Transform startPos;
	[SerializeField]private Transform endPos;
	private LineRenderer lineRenderer;
	// Use this for initialization
	void Start () {

		lineRenderer = GetComponent<LineRenderer> ();
		//startPos = transform.GetChild (0).position;
		//lineRenderer.SetPosition (0, startPos);
	}
	
	// Update is called once per frame
	void Update () {

		lineRenderer.SetPosition (0, startPos.transform.position);
		lineRenderer.SetPosition (1, endPos.transform.position);
		
	}
}
