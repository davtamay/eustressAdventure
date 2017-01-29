using UnityEngine;
using System.Collections;

public class FinderSpawner : MonoBehaviour {


	//Find differenet objects each round?
	public GameObject[] findTargets;

	public GameObject[] obstacles;
	private int obstacleTypes;

	public int numberOfObstacles;
	[SerializeField] private GameObject obstacleManager;


	public float radiusOfFinderOpening = 0.2f;

	private GameObject player;

	void Start () {

		player = GameObject.FindWithTag ("Player");


		int obstacletypes = obstacles.Length;

		for (int i = 0; i < numberOfObstacles; i++) {
		
			int randomNum = Random.Range (0, obstacletypes);
			GameObject obstacle = Instantiate (obstacles[randomNum], (Random.onUnitSphere + Vector3.up * 0.5f) * 5.5f , Quaternion.identity) as GameObject;
			obstacle.transform.parent = obstacleManager.transform;

			//obstacle.GetComponent<Collider>().bounds.Contains (obstacle)

				obstacle.tag = "Obstacle";
		
		}


			

	
		TriggerFinderObjects ();
				


	}	

	public void Update(){

		GameObject[] finderObj = GameObject.FindGameObjectsWithTag ("FinderObject");

		foreach (GameObject fO in finderObj) {
			
			Debug.DrawRay (fO.transform.position, player.transform.position - fO.transform.position, Color.blue);
		}
	}

	public void TriggerFinderObjects (){


		GameObject[] finderObj = GameObject.FindGameObjectsWithTag("FinderObject");

		foreach (GameObject fO in finderObj) {
	
			Destroy(fO);
		}


		for (int i = 0; i < findTargets.Length; i++){

			GameObject finderObject = Instantiate (findTargets[i], (Random.onUnitSphere + Vector3.up * 0.7f) * 6 , Quaternion.identity) as GameObject;


			Rigidbody rb = finderObject.AddComponent<Rigidbody>();
			rb.constraints = RigidbodyConstraints.FreezeAll;
			finderObject.tag = "FinderObject";

			RaycastHit[] hits;

			hits = Physics.SphereCastAll (finderObject.transform.position, radiusOfFinderOpening, player.transform.position - finderObject.transform.position, 10.0f);


			for (int h = 0; h < hits.Length; h++){

				RaycastHit hit = hits [h];

				if(hit.transform.CompareTag("Obstacle"))
					Destroy(hit.transform.gameObject);
			}
	}

	}
		public int GetAmountOfFindTargets (){


		return findTargets.Length;
	
	
		}
		public void TriggerMoreObjects (int amount, float distance){
			

		for (int i = 0; i < amount; i++) {

			int randomNum = Random.Range (0, obstacleTypes);
			GameObject obstacle = Instantiate (obstacles[randomNum], (Random.onUnitSphere + Vector3.up * 0.7f) * distance , Quaternion.identity) as GameObject;
			obstacle.transform.parent = obstacleManager.transform;

			obstacle.tag = "Obstacle";


		}

}
}


		
