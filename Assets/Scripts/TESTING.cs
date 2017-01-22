using UnityEngine;
using System.Collections;

public class TESTING : MonoBehaviour,IGvrGazePointer {
	float radiusy;
	float radiusx;

	// Use this for initialization
	void OnEnable () {
		
		GazeInputModule.gazePointer = this;
	}
	
	// Update is called once per frame
	void OnDisable () {
		GazeInputModule.gazePointer = null;
	}

	public void OnGazeEnabled(){
		Debug.Log ("This Button Enabled");

	}

	public void OnGazeDisabled(){
		Debug.Log ("This Button Disabled");
	
	}

	public void OnGazeStart(Camera cam, GameObject go, Vector3 vec, bool boo){
		Debug.Log ("Gaze started");

	
	}
	public void OnGazeStay(Camera cam, GameObject go, Vector3 vec, bool boo){
		Debug.Log ("Gaze Stay");


	}
	public void OnGazeExit(Camera cam, GameObject go){
		Debug.Log ("Gaze Exit");


	}
	public void OnGazeTriggerStart(Camera cam){
		Debug.Log ("Gaze Trigger Start");
	
	}
	public void OnGazeTriggerEnd(Camera cam){
		Debug.Log ("Gaze Trigger End");


	}

	public void GetPointerRadius(out float x, out float y){
	//	radiusx = x;
	//	radiusy = y; 
		x = 0;
		y = 0;
	}
	void Selected(){
		
		Debug.Log ("This Button");
	}
}
