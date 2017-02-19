using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour {

	private Animator animator = null;
	private float originalSpeed;
	[SerializeField] private GameObject firstBoss;




	void Awake () {
		firstBoss.SetActive (false);
		animator = GetComponent<Animator> ();
		originalSpeed = animator.speed;

	}
	
	public void Stop(){

		animator.speed = 0f;
		if (firstBoss != null)
			firstBoss.SetActive (true);
	}

	public void Resume(){
	
		animator.speed = originalSpeed;
		PlayerManager.Instance.healthColor.a = 0.0f;


	}





}
