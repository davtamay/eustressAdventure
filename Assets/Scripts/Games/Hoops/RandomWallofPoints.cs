using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWallofPoints : MonoBehaviour {

	//private float wallHeight;
	//private float wallWidth;

	void OnEnable(){

	//	wallHeight = transform.localScale.y/2;
	//	wallWidth = transform.localScale.x/2;

	
		foreach (Transform po in transform) {
		
			//float poHeight = po.localPosition.y + wallHeight;
			//float poWidth = po.localPosition.x + wallWidth;
			float randomHeight = Random.Range (-0.4f, 0.4f);
			float randomWidth = Random.Range (-0.4f, 0.4f);
			po.localPosition = new Vector3 (randomWidth, randomHeight, po.localPosition.z);
			//Debug.Log (po.name);
		
		}
	
	
	}
}
