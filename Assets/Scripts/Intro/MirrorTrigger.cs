using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorTrigger : MonoBehaviour {

	ReflectionProbe reflectionProbe;

	void Awake(){
	
		reflectionProbe = GetComponentInChildren<ReflectionProbe> ();
		reflectionProbe.RenderProbe ();
	}

	void OnTriggerStay(){
	
		reflectionProbe.RenderProbe ();
	}
}
