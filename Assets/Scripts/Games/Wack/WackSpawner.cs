using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WackSpawner : MonoBehaviour {

	[SerializeField] private Transform[] bushLocations;
	private Transform closestBush;

	[SerializeField] private float speed;
	[SerializeField] private float distanceBushOffset;
	[SerializeField] private float timeUntilOneLessBerry;

	private Transform playerTransform;

	private Transform thisTransform;
	//private Transform ghostTransform;
	private Animator thisAnimator;
	private Collider thisCollider;
	private Vector3 initPos;


	void Awake(){


		thisTransform = transform;
		initPos = thisTransform.position;
		thisCollider = GetComponent<Collider> ();
		thisAnimator = GetComponent<Animator> ();

		playerTransform = GameObject.FindWithTag ("Player").transform;
	//	ghostTransform = thisTransform.GetChild (0);
	
	}
	void OnEnable(){
	
		float closestBushDistance = Mathf.Infinity;
		
		foreach (Transform bs in bushLocations) {
		
			if (Vector3.Distance (thisTransform.position, bs.position) < closestBushDistance) {
			
				closestBush = bs;
				closestBushDistance = Vector3.Distance (thisTransform.position, bs.position);
			
			}
	
		
		}
		StartCoroutine (SeekBush ());
	
	
	}
	//bool isSeekingBush;
	Vector3 dir;
	IEnumerator SeekBush(){

		while(true){

			if(thisAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")){
				yield return null;
				continue;

			}

			yield return null;
			dir = closestBush.position - thisTransform.position;
			dir.y = 0;
			thisTransform.position += dir.normalized * Time.deltaTime * speed;
			thisTransform.rotation = Quaternion.LookRotation (dir, Vector3.up);
		//	ghostTransform.rotation = Quaternion.LookRotation (playerTransform.position - ghostTransform.position, Vector3.up);

			if (Vector3.Distance (thisTransform.position, closestBush.position) < distanceBushOffset) {
				StartCoroutine (EatFruit ());
				thisAnimator.SetTrigger("IsEating");
				break;
			}
		}
	
	}
	private float timer;
	IEnumerator EatFruit(){

		while(true){

			yield return null;
			timer += Time.deltaTime;

			if (timer > timeUntilOneLessBerry) {
			
				WackGameManager.Instance.ReduceBerry ();
				//Debug.Log ("ONE LESS BERRY");
				timer = 0;
			}

		}

	}
	public void BackToInitialPosition(){

	//	ghostTransform.GetComponent<Animator> ().SetTrigger ("IsDead");

	//	thisAnimator.Play ("Idle");
		StopAllCoroutines ();
		thisTransform.position = initPos;
	//	thisCollider.enabled = true;
		StartCoroutine (SeekBush ());

	//	thisTransform.gameObject.SetActive (true);
	//	ghostTransform.gameObject.SetActive (false);


	}

}
