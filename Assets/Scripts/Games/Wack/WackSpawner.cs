using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WackSpawner : MonoBehaviour {


	private Transform closestBush;

	[SerializeField] private float initPosRandomOffsetMinLimits;
	[SerializeField] private float initPosRandomOffsetMaxLimits;

	[SerializeField] private float speed;
	[SerializeField] private float distanceBushOffset;
	[SerializeField] private float timeUntilOneLessBerry;

	private Transform playerTransform;

	private Transform thisTransform;
	private Animator thisAnimator;
	private Collider thisCollider;
	[SerializeField]private Vector3 initPos;

	//[SerializeField]private bool isStable;


	void Awake(){


		thisTransform = transform;
		//initPos = thisTransform.position;
		thisCollider = GetComponent<Collider> ();
		thisAnimator = GetComponentInChildren<Animator> ();

		playerTransform = GameObject.FindWithTag ("Player").transform;

	
	}
	//10/28/17 changed from onenable to prevent errors
	void Start(){
		//if(!isStable){
		initPos = thisTransform.position;

			
		StopAllCoroutines ();
		closestBush = WackGameManager.Instance.GetClosestBush (thisTransform);
		StartCoroutine (SeekBush());
		SetRandomPos ();

	//	}
	}

	/*public void ChooseAndSeekClosestBush(){

		
		float closestBushDistance = Mathf.Infinity;

		foreach (Transform bs in WackGameManager.Instance.totalBranches) {

			if (Vector3.Distance (thisTransform.position, bs.position) < closestBushDistance) {

				if (!WackGameManager.Instance.BranchHasBerries (bs))
					continue;

				closestBush = bs;
				closestBushDistance = Vector3.Distance (thisTransform.position, bs.position);

			}

		}
		StartCoroutine (SeekBush ());
	
	}*/

	Vector3 dir;
	IEnumerator SeekBush(){


		while(true){

			if (!closestBush)
				closestBush = WackGameManager.Instance.totalBranches [0];
				
			if(thisAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")){
				yield return null;
				continue;
			}
			//Debug.Log (gameObject.name + Vector3.Distance (thisTransform.position, closestBush.position));
			yield return null;
			dir = closestBush.position - thisTransform.position;
			dir.y = 0;
			thisTransform.position += dir.normalized * Time.deltaTime * speed;
			thisTransform.rotation = Quaternion.LookRotation (dir, Vector3.up);
		
			if (Vector3.Distance (thisTransform.position, closestBush.position) < distanceBushOffset) {
				StartCoroutine (EatFruit ());
				thisAnimator.SetTrigger("IsEating");
				break;
			}
		}
	
	}
	private float timer;
	IEnumerator EatFruit(){

		timer = 0;
		while(true){

			yield return null;
			timer += Time.deltaTime;

			if (timer > timeUntilOneLessBerry) {
				
				WackGameManager.Instance.ReduceBerry (closestBush);
				closestBush = WackGameManager.Instance.GetClosestBush (thisTransform);
				StartCoroutine (SeekBush());

				//closestBush = WackGameManager.Instance.GetClosestBush (thisTransform); //ChooseAndSeekClosestBush ();
			//	StartCoroutine(SeekBush());
				break;
			}

		}

	}
	public void SetRandomPos(){

		StopAllCoroutines ();
		float randomX = Random.Range(initPosRandomOffsetMinLimits, initPosRandomOffsetMaxLimits);
		float randomZ = Random.Range(initPosRandomOffsetMinLimits, initPosRandomOffsetMaxLimits);

		Vector3 initTo =  WackGameManager.Instance.centerPos.position + new Vector3 (randomX, 0, randomZ);
		initTo.y = transform.position.y;

		thisTransform.position = initTo;

		StartCoroutine (SeekBush ());

	//	thisTransform.gameObject.SetActive (true);
	//	ghostTransform.gameObject.SetActive (false);


	}

}
