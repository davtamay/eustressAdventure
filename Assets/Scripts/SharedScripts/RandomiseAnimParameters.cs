using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomiseAnimParameters : MonoBehaviour {

	private Animator thisAnimator;
	private int animRandHash = Animator.StringToHash("Random");
	[SerializeField]private int perFrameChanceOfRandom = 100;


	void Start () {

		thisAnimator = GetComponent<Animator> ();
		
	}
	

	void Update () {

		thisAnimator.SetInteger (animRandHash, Random.Range (0, perFrameChanceOfRandom));
	}
}
