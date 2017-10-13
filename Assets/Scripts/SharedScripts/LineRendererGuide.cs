using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererGuide : MonoBehaviour {

	public Transform startPos;
	public Transform endPos;
	private LineRenderer lineRenderer;
	// Use this for initialization
	void Awake () {

		endPos = transform.parent;
		lineRenderer = GetComponent<LineRenderer> ();
		//startPos = transform.GetChild (0).position;
		//lineRenderer.SetPosition (0, startPos);
	}
	
	// Update is called once per frame
	void Update () {

		if(startPos != null)
		lineRenderer.SetPosition (0, startPos.position);
		lineRenderer.SetPosition (1, endPos.position);
		
	}
}
