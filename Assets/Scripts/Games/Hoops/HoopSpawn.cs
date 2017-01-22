using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopSpawn : MonoBehaviour {

	private Animator animator;
	[SerializeField]bool isCrate;


	// Use this for initialization
	void OnEnable () {

		animator = GetComponent<Animator> ();

		if(isCrate)
		animator.Play("Move",-1,Random.Range (0.0f,1.0f));
		else
		animator.Play("ZombieMove",-1,Random.Range (0.0f,1.0f));
		
	}

}
