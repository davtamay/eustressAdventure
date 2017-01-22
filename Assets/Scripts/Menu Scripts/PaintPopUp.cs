using UnityEngine;
using System.Collections;

public class PaintPopUp : MonoBehaviour {

	private Camera cam;
	public Camera[] cameras;

	public int oldMask;


	void Awake(){
		cam = Camera.main;
		oldMask = cam.cullingMask;

		cameras = cam.GetComponentsInChildren<Camera> ();
	
	
	}
	void OnEnable(){

		//GameController.Instance.Paused = false;
		foreach (Camera c in cameras) {

		//	c.cullingMask = 1 << 10;
			c.cullingMask = 1 << 11 | 1 << 10 | 1 << 5;


		}
		
	}

	//fix when taking off destressor only UI stays in culling mask.. check for isStressMenuActive?
	void OnDisable(){

		foreach (Camera c in cameras) 
			c.cullingMask = oldMask;


		
	}
	

}
