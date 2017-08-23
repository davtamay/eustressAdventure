using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinderSpawner : MonoBehaviour {


	//Find differenet objects each round?

	public Transform findTargetParent;
	public GameObject[] findTargets;



	//public int numberOfObstacles;

	[SerializeField] private float distanceAgregate = 5.5f;
	[SerializeField] private float finderDistanceAgregate = 5.5f;

	[SerializeField] private float heightDeviationFromPlayer = 2f;
	[SerializeField] private float heightUpOffset = 2f;


	//[SerializeField] private float animationHeight = 10;


	public float radiusOfFinderOpening = 0.2f;

	private GameObject cam;

	[SerializeField] private Transform obstacleManager;
	public int amountOfObjects;
	public GameObject[] obstacles;
	//private int obstacleTypes = 0;
	//public GameObject obstaclePrefab;
	private GameObject[] obstacleList;
	public Queue <Transform> queue = new Queue<Transform>();
	//public static Stack <Transform> stack = new Stack<Transform> ();





	void Awake(){
	
		findTargets = new GameObject[findTargetParent.childCount];
		int e = 0;
		foreach (Transform fO in findTargetParent) {

			findTargets [e] = fO.gameObject;

			e++;
		}
	
	}
	void Start () {

		cam = Camera.main.gameObject;

		obstacleList = new GameObject [amountOfObjects];
		//int obstacletypes = obstacles.Length;

		for (int i = 0; i < amountOfObjects; i++) {

			int randomNum = Random.Range (0, obstacles.Length);

			obstacleList [i] = Instantiate (obstacles[randomNum], Vector3.zero, Quaternion.identity) as GameObject;
			Transform prefabTrans = obstacleList [i].GetComponent<Transform> ();
			prefabTrans.tag = "Obstacle";
			prefabTrans.parent = obstacleManager;
			queue.Enqueue (prefabTrans);

			obstacleList [i].SetActive (false);

			//yield return null;
		}
		for (int i = 0; i < findTargets.Length; i++){
			
			switch (i) {
			case 3:
				finderObject = findTargets [i];
				finderObject.tag = "FinderObject";
				finderObject.name = "L_Hand";
				break;
			case 2:
				finderObject = findTargets [i];
				finderObject.tag = "FinderObject";
				finderObject.name = "R_Hand";
				break;
			case 1: 
				finderObject = findTargets [i];
				finderObject.tag = "FinderObject";
				finderObject.name = "L_Foot";
				break;
			case 0:
				finderObject = findTargets [i];
				finderObject.tag = "FinderObject";
				finderObject.name = "R_Foot";
				break;

			}

		}
		TriggerObjects (600,cam.transform.position.y);

	/*	int obstacletypes = obstacles.Length;

		for (int i = 0; i < numberOfObstacles; i++) {
		
			int randomNum = Random.Range (0, obstacletypes);
			//GameObject obstacle = Instantiate (obstacles[randomNum], (Random.onUnitSphere + Vector3.up * 0.5f) * distanceAgregate , Quaternion.identity) as GameObject;
			GameObject obstacle = Instantiate (obstacles[randomNum],  new Vector3(cam.transform.position.x + Random.insideUnitCircle.x * distanceAgregate, cam.transform.position.y + Random.Range(-1f,1f) * heightDeviationFromPlayer , cam.transform.position.z + Random.insideUnitCircle.y * distanceAgregate) , Quaternion.identity) as GameObject;
			obstacle.transform.parent = obstacleManager.transform;


				obstacle.tag = "Obstacle";
		
		}

		distanceAgregate++;
			
*/
	
		TriggerFinderObjects (0);
				


	}	

	public void Update(){

		GameObject[] finderObj = GameObject.FindGameObjectsWithTag ("FinderObject");

		foreach (GameObject fO in finderObj) {
			
			Debug.DrawRay (fO.transform.position, cam.transform.position - fO.transform.position, Color.blue);
		}
	}

	GameObject finderObject;
	Vector3 randomPos;

	public void TriggerFinderObjects (float height){


		foreach (GameObject fO in findTargets)
			fO.SetActive (false);
		


		for (int i = 0; i < findTargets.Length; i++){

			Vector2 randomCirleUnits = Random.insideUnitCircle.normalized;
			//randomCirleUnits.normalized;

			randomPos = new Vector3 (cam.transform.position.x + randomCirleUnits.x  * finderDistanceAgregate, height  , cam.transform.position.z + randomCirleUnits.y  * finderDistanceAgregate);

			while(CheckForFinderIntersections(randomPos,cam.transform.position + Vector3.up * height))
				randomPos = new Vector3 (cam.transform.position.x + randomCirleUnits.x  * finderDistanceAgregate, height , cam.transform.position.z + randomCirleUnits.y  * finderDistanceAgregate);

		
			switch (i) {
			case 3:
				finderObject = findTargets [i];
				finderObject.transform.position = randomPos;

				break;
			case 2:
				finderObject = findTargets [i];
				finderObject.transform.position = randomPos;

				break;
			case 1: 
				finderObject = findTargets [i];
				finderObject.transform.position = randomPos;
				break;
			case 0:
				finderObject = findTargets [i];
				finderObject.transform.position = randomPos;
				break;

			}

			finderObject.GetComponent<Renderer> ().material.color = Color.magenta;
			finderObject.GetComponent<Renderer> ().material.SetColor ("EmissionColor", Color.yellow);//"_EmissionColor",

			finderObject.transform.GetComponent<Collider> ().enabled = true;

		//	finderObject.SetActive)(
			//Rigidbody rb = finderObject.AddComponent<Rigidbody>();
			//rb.constraints = RigidbodyConstraints.FreezeAll;



			CheckForObstacleIntersections (Vector3.up * height, finderObject.transform.position);

		
	}
		foreach (GameObject fO in findTargets)
			fO.SetActive (true);
	}

	public void CheckForObstacleIntersections (Vector3 from, Vector3 to){
	
		float distance = (from - to).magnitude;
			RaycastHit[] hits;

		hits = Physics.SphereCastAll (from, radiusOfFinderOpening,  to - from, distance);


			for (int h = 0; h < hits.Length; h++){

				RaycastHit hit = hits [h];


				if(hit.transform.CompareTag("Obstacle"))
					hit.transform.gameObject.SetActive(false);

			}
	
		}
	public bool CheckForFinderIntersections (Vector3 from, Vector3 to){

		RaycastHit[] hits;

		Vector3 direction = to - from;

		hits = Physics.RaycastAll (from, direction.normalized, direction.magnitude);


		for (int h = 0; h < hits.Length; h++){

			RaycastHit hit = hits [h];


			if (hit.transform.CompareTag ("FinderObject"))
				return true;

		}
		return false;
	}

		public int GetAmountOfFindTargets (){


		return findTargets.Length;
	
	
		}
	public void TriggerObjects (int amount, float height){
			
		for (int i = 0; i < amount; i++) {

			Transform spawnedPrefab = queue.Dequeue ();

			spawnedPrefab.position = new Vector3 (cam.transform.position.x + Random.insideUnitCircle.x * distanceAgregate, heightUpOffset + (height + Random.Range (-1f, 1f) * heightDeviationFromPlayer), cam.transform.position.z + Random.insideUnitCircle.y * distanceAgregate);
			spawnedPrefab.gameObject.SetActive (true);


			queue.Enqueue (spawnedPrefab);

			//return spawnedPrefab;

		}
					/*

		for (int i = 0; i < amount; i++) {

			int randomNum = Random.Range (0, obstacleTypes);
			GameObject obstacle = Instantiate (obstacles[randomNum],  new Vector3(cam.transform.position.x + Random.insideUnitCircle.x * distanceAgregate, cam.transform.position.y + Random.Range(-1f,1f) * heightDeviationFromPlayer , cam.transform.position.z + Random.insideUnitCircle.y * distanceAgregate) , Quaternion.identity) as GameObject;
			//GameObject obstacle =  Instantiate (obstacles[randomNum], new Vector3 (cam.transform.position.x + Random.insideUnitCircle.x  * distanceAgregate, height * heightDeviationFromPlayer , cam.transform.position.z + Random.insideUnitCircle.x  * distanceAgregate, Quaternion.identity));
			//GameObject obstacle = Instantiate (obstacles[randomNum], (Random.onUnitSphere + Vector3.up * 0.7f) * distanceAgregate , Quaternion.identity) as GameObject;
			obstacle.transform.parent = obstacleManager.transform;

			obstacle.tag = "Obstacle";


		}*/
		//distanceAgregate++;

}
}


		
