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
	IEnumerator Start () {

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
	
		yield return StartCoroutine(TriggerFinderObjects (0));
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
	Vector3 randomCirleUnits;

	Vector2 camPosWRandom;
	public IEnumerator TriggerFinderObjects (float height){


		foreach (GameObject fO in findTargets) {
			fO.transform.position = new Vector3(fO.transform.position.x, height, fO.transform.position.z);
			fO.SetActive (false);

		}
		


		for (int i = 0; i < findTargets.Length; i++){

			camPosWRandom = (new Vector2 (cam.transform.position.x, cam.transform.position.z) + Random.insideUnitCircle); 
			randomCirleUnits = new Vector3(camPosWRandom.x,0,camPosWRandom.y);

			randomPos = new Vector3 (randomCirleUnits.x , height  , randomCirleUnits.z );


			switch (i) {
			case 3:
				
				//LHANDl
				finderObject = findTargets [i];



				finderObject.transform.position = randomPos;
				yield return StartCoroutine (SeparateAndAddToObjectNormal (finderObject.transform, height, i));
				finderObject.transform.position += Vector3.up * (2f  + Random.Range (-0.5f, 0.5f));
				//height -= 2f;

			//	while (CheckForFinderIntersections (randomPos, cam.transform.position + Vector3.up * height) && (Vector3.Distance(randomPos, findTargets[2].transform.position ) < 8f)) 
			//		randomPos = new Vector3 (randomCirleUnits.x, height, randomCirleUnits.y);

				Debug.Log ("DISTANCE FROM LHANDRANDOM POS TO RHAND : " + Vector3.Distance (finderObject.transform.position, findTargets [2].transform.position) );
				Debug.Log("RandomPos " + finderObject.transform.position  + "findTargets2" + findTargets[2].transform.position );

				break;
			case 2:
				
				//RHAND
				finderObject = findTargets [i];

			//	while (CheckForFinderIntersections (randomPos, cam.transform.position + Vector3.up * height)) 
			///		randomPos = new Vector3 (randomCirleUnits.x, height, randomCirleUnits.y);
				
				finderObject.transform.position = randomPos;
				yield return StartCoroutine (SeparateAndAddToObjectNormal (finderObject.transform, height, i));
				finderObject.transform.position += Vector3.up * (2f + Random.Range (-0.2f, 0.2f)) ;
				break;
			case 1: 

				//LFOOT
				finderObject = findTargets [i];

				//	while (CheckForFinderIntersections (randomPos, cam.transform.position + Vector3.up * height) && Vector3.Distance(randomPos, findTargets[0].transform.position ) > 10f) 
				//		randomPos = new Vector3 (randomCirleUnits.x, height, randomCirleUnits.y);

				finderObject.transform.position = randomPos;
				yield return StartCoroutine (SeparateAndAddToObjectNormal (finderObject.transform, height, i));
				finderObject.transform.position -= Vector3.up * (1.2f  + Random.Range (-0.2f, 0.2f)) ;
				break;

			case 0:
				//RFOOT
				finderObject = findTargets [i];

				finderObject.transform.position = randomPos;
				yield return StartCoroutine (SeparateAndAddToObjectNormal (finderObject.transform, height, i));
				finderObject.transform.position -= Vector3.up * ( 1.2f  + Random.Range (-0.2f, 0.2f));
				break;

			}
			
			finderObject.GetComponent<Renderer> ().material.color = Color.magenta;
			finderObject.GetComponent<Renderer> ().sharedMaterial.SetColor ("_EmissionColor", Color.yellow);//"_EmissionColor",

			finderObject.transform.GetComponent<Collider> ().enabled = true;
		

			CheckForObstacleIntersections (Vector3.up * height, finderObject.transform.position);

		
		
	}

	}
	public void ActivateFinder(){

		foreach (GameObject fO in findTargets)
			fO.SetActive (true);
	
	
	}

	public IEnumerator SeparateAndAddToObjectNormal(Transform finder, float height, int finderID){
	
		//bool isObjectHit = false;
		//finder.rotation = Quaternion.AngleAxis (camTransform.eulerAngles.y, Vector3.up);
		Vector3 curPos = Vector3.up * height;
	//	Vector2 randomCirleUnits;
		RaycastHit hit;


		while (true) {
			yield return null;

			camPosWRandom = (new Vector2 (cam.transform.position.x, cam.transform.position.z) + Random.insideUnitCircle); 
			randomCirleUnits = new Vector3(camPosWRandom.x,0,camPosWRandom.y);
	
			if (Physics.Raycast (curPos, (finder.position - curPos).normalized, out hit, 50f)) {

				if (hit.transform.CompareTag ("Obstacle")) {

				
					if (finderID == 1) {
						if (Vector3.Distance (finder.position, findTargets [0].transform.position) < 1.2f || CheckForFinderIntersections (finder.position, cam.transform.position + Vector3.up * height)) {
					//	if (Vector3.Angle(finder.position, findTargets [0].transform.position) < 30f){ //findVector3.Distance (finder.transform.position, findTargets [0].transform.position) < 2f) {

							finder.position = randomPos = new Vector3 (randomCirleUnits.x, height, randomCirleUnits.z);
							continue;
						}
					} else if (finderID == 3) {
					//	if (Vector3.Angle(finder.transform.position, findTargets [2].transform.position) < 30f){
						if (Vector3.Distance (finder.position, findTargets [2].transform.position) < 1.2f || CheckForFinderIntersections (finder.position, cam.transform.position + Vector3.up * height)) {
							finder.position = randomPos = new Vector3 (randomCirleUnits.x, height, randomCirleUnits.z);
							continue;
						}
					}


				
					//finder.rotation = finder.rotation.SetLookRotation(cam.transform.position, Vector3.up) * finder.rotation;


				//	finder.rotation = (Quaternion.FromToRotation (finder.up, hit.normal)) * finder.rotation;
					finder.position = hit.point;
					finder.LookAt (cam.transform);
					//finder.LookAt (cam.transform, Vector3.up);
					finder.rotation = (Quaternion.FromToRotation (finder.up, hit.normal)) * finder.rotation;
				
				//	finder.LookAt (cam.transform, Vector3.right);
					//finder.rotation.SetLookRotation(new Vector3(cam.transform.position.x,0, 0));
				//	finder.rotation.SetLookRotation(new Vector3(0,cam.transform.position.y, 0));
					//finder.position += finder.up * 0.1f;
					//	finder.position = hit.point - (finder.rotation * (Vector3.one)) ;
					//	isObjectHit = true;

					yield break;
				}
					


			} 


			randomPos = new Vector3 (randomCirleUnits.x , height  , randomCirleUnits.z);

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


		
