using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinderSpawner : MonoBehaviour {


	//Find differenet objects each round?

	public Transform findTargetParent;
	public GameObject[] findTargets;



	//public int numberOfObstacles;

	[SerializeField] private float distanceAgregate = 5.5f;
	//[SerializeField] private float finderDistanceAgregate = 5.5f;

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
			fO.GetComponent<Renderer> ().sharedMaterial.SetColor ("_EmissionColor", Color.yellow);
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
	
		StartCoroutine(TriggerFinderObjects (0));
		ActivateFinder ();
				


	}	

	public void Update(){

		GameObject[] finderObj = GameObject.FindGameObjectsWithTag ("FinderObject");

		foreach (GameObject fO in finderObj) {
			
			Debug.DrawRay (fO.transform.position, cam.transform.position - fO.transform.position, Color.blue);
		}
	}

	GameObject finderObject;
	Vector3 randomPos;

	public IEnumerator TriggerFinderObjects (float height){


		foreach (GameObject fO in findTargets)
			fO.SetActive (false);
		


		for (int i = 0; i < findTargets.Length; i++){

			Vector2 camPosWRandom = (new Vector2 (cam.transform.position.x, cam.transform.position.y) + Random.insideUnitCircle.normalized); 
			Vector3 randomCirleUnits = new Vector3(camPosWRandom.x,0,camPosWRandom.y);

			randomPos = new Vector3 (randomCirleUnits.x , height  , randomCirleUnits.y );

			while(CheckForFinderIntersections(randomPos,cam.transform.position + Vector3.up * height))
				randomPos = new Vector3 (randomCirleUnits.x , height  , randomCirleUnits.y );
				//randomPos = new Vector3 (cam.transform.position.x + randomCirleUnits.x , height , cam.transform.position.z + randomCirleUnits.y );

		
			switch (i) {
			case 3:
				//LHAND
				finderObject = findTargets [i];
				finderObject.transform.position = randomPos;
				yield return StartCoroutine(AddToObjectNormal (finderObject.transform, height));
				finderObject.transform.position += Vector3.up * 2f;
				//height -= 2f;
				break;
			case 2:
				//RHAND
				finderObject = findTargets [i];
				finderObject.transform.position = randomPos;
				yield return StartCoroutine(AddToObjectNormal (finderObject.transform, height));
				finderObject.transform.position += Vector3.up * 2f;
				break;
			case 1: 
				//LFOOT
				finderObject = findTargets [i];
				finderObject.transform.position = randomPos;
				yield return StartCoroutine(AddToObjectNormal (finderObject.transform, height));
				finderObject.transform.position -= Vector3.up * 1.2f;
				break;
			case 0:
				//RFOOT
				finderObject = findTargets [i];
				finderObject.transform.position = randomPos;
				yield return StartCoroutine (AddToObjectNormal (finderObject.transform, height));
				finderObject.transform.position -= Vector3.up * 1.2f;
				break;

			}

			finderObject.GetComponent<Renderer> ().material.color = Color.magenta;
			finderObject.GetComponent<Renderer> ().sharedMaterial.SetColor ("_EmissionColor", Color.yellow);//"_EmissionColor",

			finderObject.transform.GetComponent<Collider> ().enabled = true;
		

			CheckForObstacleIntersections (Vector3.up * height, finderObject.transform.position);

			//yield return null;
		
	}
		//foreach (GameObject fO in findTargets)
			//fO.SetActive (true);
	}
	public void ActivateFinder(){

		foreach (GameObject fO in findTargets)
			fO.SetActive (true);
	
	
	}

	public IEnumerator AddToObjectNormal(Transform finder, float height){
	
		//bool isObjectHit = false;
		//finder.rotation = Quaternion.AngleAxis (camTransform.eulerAngles.y, Vector3.up);
		Vector3 curPos = Vector3.up * height;
		Vector2 randomCirleUnits;
		RaycastHit hit;

		while (true) {
			if (Physics.Raycast (curPos, (finder.position - curPos).normalized, out hit, 50f)) {

				if (hit.transform.CompareTag ("Obstacle")) {


					finder.rotation = (Quaternion.FromToRotation (finder.up, hit.normal)) * finder.rotation;
					finder.position = hit.point;
					//finder.position += finder.up * 0.1f;
					//	finder.position = hit.point - (finder.rotation * (Vector3.one)) ;
					//	isObjectHit = true;

					yield break;
				}
					


			} 
			Vector2 camPosWRandom = (new Vector2 (cam.transform.position.x, cam.transform.position.y) + Random.insideUnitCircle.normalized); 
			Vector3 randomCirleUnit = new Vector3(camPosWRandom.x,0,camPosWRandom.y);

			randomPos = new Vector3 (randomCirleUnit.x , height  , randomCirleUnit.y );

				//randomCirleUnits = Random.insideUnitCircle.normalized;
			//randomPos = new Vector3 (cam.transform.position.x + randomCirleUnits.x, curPos.y, cam.transform.position.z + randomCirleUnits.y);
				finder.position = randomPos;

				yield return null;


		}


	//	thisTransform.rotation *= Quaternion.Euler (Vector3.right* 90);
	
	
	
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
			
		Vector2 randomCirleUnits = Random.insideUnitCircle.normalized;
		for (int i = 0; i < amount; i++) {

			randomCirleUnits = Random.insideUnitCircle.normalized;

			Transform spawnedPrefab = queue.Dequeue ();

			spawnedPrefab.position = new Vector3 (cam.transform.position.x + randomCirleUnits.x * distanceAgregate, heightUpOffset + (height + Random.Range (-1f, 1f) * heightDeviationFromPlayer), cam.transform.position.z + randomCirleUnits.y * distanceAgregate);
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


		
