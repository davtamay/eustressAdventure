using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopeGameObstacles : MonoBehaviour {

	//[SerializeField]private float speed;
	[SerializeField]private Vector3 moveLocal;
	[SerializeField]private Vector3 moveGlobal;


	[SerializeField]private bool isDeleteWhenReachY;
	[SerializeField]private float yToReachToDestroy;
	//[SerializeField]private Transform objectYPos;

	[SerializeField]private bool isPoints;
	[SerializeField]private int points;


	public Transform target;
	[SerializeField] private float distanceFromTargetForTrigger;

	private Transform thisTransform;




	// Use this for initialization
	void Start () {

		thisTransform = transform;
		//transform.localRotation = Quaternion.identity;
		target = transform.parent.GetChild (0);
	}
	
	// Update is called once per frame
	void Update () {

		thisTransform.localPosition += moveLocal;
		thisTransform.position += moveGlobal;

	//	Debug.Log (Vector3.Distance (target.position, thisTransform.position));
		if (Vector3.Distance (target.position, thisTransform.position) < distanceFromTargetForTrigger) {
			

			if (isPoints)
				CopingGameSpawner.Instance.score += points;
			else {
				target.gameObject.SetActive (false);
				CopingGameSpawner.Instance.isGameOver = true;


			}
			
			Debug.Log ("I GOT HIT");
			Destroy (gameObject);

		}

		if(isDeleteWhenReachY)
		if (yToReachToDestroy > thisTransform.localPosition.y)
			Destroy (gameObject);
			
			
		
	}
}
