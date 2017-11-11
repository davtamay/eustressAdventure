using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WackSpawner : MonoBehaviour {


	private Transform closestBush;

	[SerializeField] private Vector2 initXZPosOffsetMinLimits;
	[SerializeField] private Vector2 initXZPosOffsetMaxLimits;

	//[SerializeField] private float initPosRandomOffsetMinLimits;
	//[SerializeField] private float initPosRandomOffsetMaxLimits;

	[SerializeField] private float speed;
	[SerializeField] private float distanceBushOffset;
	[SerializeField] private float timeUntilOneLessBerry;

	private Transform playerTransform;

	private Transform rootMoveTransform;
	private Animator thisAnimator;
	//private Collider thisCollider;
	[SerializeField]private Vector3 initPos;
	[SerializeField]private bool isUseParentRootTransform;

	[Tooltip("For FLying Creatures that use the y coordinate system")]
	[SerializeField]private bool isAllowYVector;
	[SerializeField]private float YInitStart;


	//[SerializeField]private bool isStable;


	void Awake(){

		if(isUseParentRootTransform)
			rootMoveTransform = transform.parent;
		else
			rootMoveTransform = transform;
		//initPos = thisTransform.position;
		//thisCollider = GetComponent<Collider> ();
		thisAnimator = GetComponentInChildren<Animator> ();

		playerTransform = GameObject.FindWithTag ("Player").transform;

	
	}
	//10/28/17 changed from onenable to prevent errors
	void Start(){
		//if(!isStable){
		initPos = rootMoveTransform.position;

			
	//	StopAllCoroutines ();
		//closestBush = WackGameManager.Instance.GetClosestBush (thisTransform);
		//StartCoroutine (SeekBush());
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
				
		//	if (thisAnimator.GetBool ("IsDead"))
			//	StopAllCoroutines ();


			if(thisAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")){
				yield return null;
				continue;
			}

		//	if (thisAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Dead"))
		//		break;

			yield return null;
			dir = closestBush.position - rootMoveTransform.position;
		
			if(!isAllowYVector)
			dir.y = 0;

				rootMoveTransform.position += dir.normalized * Time.deltaTime * speed;
				rootMoveTransform.rotation = Quaternion.LookRotation (dir, Vector3.up);
				if (Vector3.Distance (rootMoveTransform.position, closestBush.position) < distanceBushOffset) {
					StartCoroutine (EatFruit ());
					thisAnimator.SetTrigger("IsEating");
					break;
				}

			

		}
	
	}
	private float timer;
	IEnumerator EatFruit(){

		Transform oldBush;
		timer = 0;


	//	if (thisAnimator.GetBool ("IsDead"))
	//		StopAllCoroutines ();


		while(true){

			yield return null;
			timer += Time.deltaTime;

			if (timer > timeUntilOneLessBerry) {
				
				WackGameManager.Instance.ReduceBerry (closestBush);
			//	oldBush = closestBush;
				closestBush = WackGameManager.Instance.GetClosestBush (rootMoveTransform);

			/*	if (oldBush.GetInstanceID() != closestBush.GetInstanceID())
				if(thisAnimator.HasParameter ("IsWalk"))
					thisAnimator.SetTrigger("IsWalk");*/
		
				StartCoroutine (SeekBush());
				break;
			}

		}

	}
	public void SetRandomPos(){

		//thisCollider.enabled = true;

		StopAllCoroutines ();

		float randomX = Random.Range(initXZPosOffsetMinLimits.x, initXZPosOffsetMaxLimits.x);
		float randomZ = Random.Range(initXZPosOffsetMinLimits.y, initXZPosOffsetMaxLimits.y);

		//float randomX = Random.Range(initPosRandomOffsetMinLimits, initPosRandomOffsetMaxLimits);
		//float randomZ = Random.Range(initPosRandomOffsetMinLimits, initPosRandomOffsetMaxLimits);
		Vector3 initTo = Vector3.zero;
		if (!isAllowYVector) {
			initTo = WackGameManager.Instance.centerPos.position + new Vector3 (randomX, 0, randomZ);
			initTo.y = transform.position.y;
		} else {
			initTo = WackGameManager.Instance.centerPos.position + new Vector3 (randomX, 0, randomZ);
			initTo.y = YInitStart;
		
		}

		rootMoveTransform.position = initTo;


		StartCoroutine (SeekBush ());

	}

}
