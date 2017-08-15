using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveAnimations : MonoBehaviour {

	private Animator thisAnimator;
	private Transform thisTransform;

	private int animRandHash = Animator.StringToHash("Random");

	public bool isRandomOn = true;

	public Vector3 initialPos; 
	[SerializeField] private Vector3 curWayPoint;

	[SerializeField] private float moveForwardSpeed = 3f;
	[SerializeField] private float rotationSpeed = 2.5f;

	[SerializeField] private float disUntilWayPointChange;
	public float distaceToSearch = 10;

	public int perFrameChanceOfRandom = 1000;

	public bool isFirstTime;

//	bool isTurning = false;
	Vector3 oldWayPoint;
	Quaternion rotationToLookTo;

	void Awake(){

		thisTransform = transform;
		thisAnimator = GetComponent<Animator> ();
		initialPos = transform.position;

		//animRandHash = thisAnimator.StringToHash("Random");
	}

	public void Start(){

		//onUpdateIEnum = OnUpdate();
			
	//	onUpdateCoroutine = 
		StartCoroutine (OnUpdate());
	}
	Vector3 dir;
	IEnumerator OnUpdate(){

		while (true) {
			yield return null;


			if (isRandomOn) {
				


				if (thisAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Walk")) {
			
			
					if (!isFirstTime) {
						
						curWayPoint = initialPos + Random.insideUnitSphere * distaceToSearch;
						curWayPoint.y = initialPos.y; 
						/*
						while (!(Vector3.Dot (transform.forward, dir) > 0.20f)) {

							curWayPoint = initialPos + Random.insideUnitSphere * distaceToSearch;
							curWayPoint.y = initialPos.y; 
							dir = (curWayPoint - transform.position).normalized;
						}*/

						isFirstTime = true;
					}

					//RaycastHit hit;
					if (Physics.Raycast (thisTransform.position, thisTransform.forward, 5)) {

						curWayPoint = initialPos + Random.insideUnitSphere * distaceToSearch;
						curWayPoint.y = initialPos.y; 
					
					}

					if (Vector3.Distance (thisTransform.position, curWayPoint) < disUntilWayPointChange) {



						curWayPoint = initialPos + Random.insideUnitSphere * distaceToSearch;
						curWayPoint.y = initialPos.y; 


						dir = (curWayPoint - transform.position).normalized;

						while (!(Vector3.Dot (transform.forward, dir) > 0.15f)) {

							curWayPoint = initialPos + Random.insideUnitSphere * distaceToSearch;
							curWayPoint.y = initialPos.y; 
							dir = (curWayPoint - thisTransform.position).normalized;
						}

					

						yield return StartCoroutine (Turn (curWayPoint));
			

					}
	

					dir = (curWayPoint - thisTransform.position).normalized;


					Vector3 movePosition = dir * Time.deltaTime * moveForwardSpeed;
					movePosition.y = 0;

					thisTransform.position += movePosition;

					thisTransform.LookAt (curWayPoint, Vector3.up);

					thisAnimator.SetInteger (animRandHash, Random.Range (0, perFrameChanceOfRandom));
			
				}
			}
		}
		
		}

	public IEnumerator Turn(Vector3 toRotation){

		Quaternion targetRotation = Quaternion.LookRotation (toRotation - thisTransform.position);
		float timer = 0;

		while (true) {
			timer += Time.deltaTime;
		
			thisTransform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation, rotationSpeed);

			thisTransform.eulerAngles = new Vector3 (0, thisTransform.eulerAngles.y);
				
			if(Vector3.Dot(thisTransform.forward, dir) >= 0.99f || timer > 7)
				yield break;

		/*	transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation,3f);
		
			if (Quaternion.Dot (transform.rotation, targetRotation) >= 0.99 || timer > 7) 
				yield break;*/

			yield return null;
	
		}
	}

	
	
}


