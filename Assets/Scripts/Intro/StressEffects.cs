using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class StressEffects : MonoBehaviour {
	[SerializeField] float magnitudeOfWalkEffect;

	PostProcessingProfile pPV;

	private Transform player;
	PlayerLookMove playerMoveScript;

	private float normalizedStress;
	private float originalVelocity;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player").transform;
		playerMoveScript = player.GetComponent<PlayerLookMove> ();
		originalVelocity = playerMoveScript.velocity;
		//PlayerLookMove.

		pPV =  GetComponent<PostProcessingBehaviour>().profile;


	
	}
	

	void LateUpdate () {
		//Debug.Log ("PlayerVeloc" + playerMoveScript.velocity);
		normalizedStress = UIStressGage.Instance.stress / 180;
		playerMoveScript.velocity = originalVelocity - normalizedStress * originalVelocity    ;//magnitudeOfWalkEffect; 


		var intensity = pPV.vignette.settings;

		intensity.intensity = normalizedStress;
		pPV.vignette.settings = intensity;


	
	//	Debug.Log ((UIStressGage.Instance.stress / 180));
		
	}

}
