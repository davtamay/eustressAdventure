using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour {

	private Animator animator = null;
	private float originalSpeed;

	private int curBoss = 0;
	[SerializeField] private GameObject firstBoss;
	[SerializeField] private GameObject secondBoss;




	void Awake () {
		if (firstBoss != null)
			firstBoss.SetActive (false);
		if (secondBoss != null) {
			secondBoss.SetActive (false);
			//curBoss = 1;
		}
		

		animator = GetComponent<Animator> ();
		originalSpeed = animator.speed;

	}
	
	public void Stop(){

		animator.speed = 0f;
		if (curBoss == 0) {
			firstBoss.SetActive (true);
			curBoss++;
		} else if (curBoss == 1) {
			secondBoss.SetActive (true);
			curBoss++;
		}
	}

	public void Resume(){
	
		animator.speed = originalSpeed;
		PlayerManager.Instance.healthColor.a = 0.0f;


	}





}
