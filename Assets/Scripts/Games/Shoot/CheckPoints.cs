using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckPoints : MonoBehaviour {

	[SerializeField]private string checkPointName;

	[SerializeField]private int secondsUntilInactive = 15;

	[SerializeField] int numToNotRespawn;

	private GameObject TEMPGO;

	private List<int> myIndices;




	void OnTriggerEnter(Collider other){

		if (other.transform.CompareTag("Player")){
			if (string.Equals ("1a", checkPointName, System.StringComparison.CurrentCultureIgnoreCase)) {
			//	StartCoroutine (GameController.Instance.NewWave ());
				StartCoroutine (spawnWave ());

			}
			else if (string.Equals ("1b", checkPointName, System.StringComparison.CurrentCultureIgnoreCase))
				StartCoroutine (spawnWave ());
			else if (string.Equals ("1c", checkPointName, System.StringComparison.CurrentCultureIgnoreCase))
				StartCoroutine (spawnWave ());
			else if (string.Equals ("1d", checkPointName, System.StringComparison.CurrentCultureIgnoreCase))
				StartCoroutine (spawnWave ());
			else if (string.Equals ("2a", checkPointName, System.StringComparison.CurrentCultureIgnoreCase)) {

				StartCoroutine (spawnWave ()); 

			}
			else if (string.Equals ("2b", checkPointName, System.StringComparison.CurrentCultureIgnoreCase))
				StartCoroutine (spawnWave ()); 
			else if (string.Equals ("2c", checkPointName, System.StringComparison.CurrentCultureIgnoreCase))
				StartCoroutine (spawnWave ()); 
			else if (string.Equals ("2d", checkPointName, System.StringComparison.CurrentCultureIgnoreCase))
				StartCoroutine (spawnWave ());
			else if (string.Equals ("3a", checkPointName, System.StringComparison.CurrentCultureIgnoreCase))
				StartCoroutine (spawnWave ()); 
			else if (string.Equals ("3b", checkPointName, System.StringComparison.CurrentCultureIgnoreCase))
				StartCoroutine (spawnWave ()); 
			else if (string.Equals ("3c", checkPointName, System.StringComparison.CurrentCultureIgnoreCase))
				StartCoroutine (spawnWave ()); 
			else if (string.Equals ("3d", checkPointName, System.StringComparison.CurrentCultureIgnoreCase))
				StartCoroutine (spawnWave ());

		
		

		}
		

	}
	IEnumerator spawnWave(){
	
		TEMPGO = transform.GetChild (0).gameObject;

			TEMPGO.SetActive (true);

		int ChildCount = TEMPGO.transform.childCount;


		myIndices = new List<int> (ChildCount);


		for (int i = 0; i < numToNotRespawn; i++) {

			int myIndexPull = Random.Range(0, ChildCount);


			while (myIndices.Contains (myIndexPull)) {

				myIndexPull = Random.Range (0, ChildCount);

			}

			myIndices.Add (myIndexPull);



		}


		foreach (int ran in myIndices)
			TEMPGO.transform.GetChild (ran).gameObject.SetActive (false);

			
		yield return new WaitForSeconds (secondsUntilInactive);

			transform.GetChild (0).gameObject.SetActive (false);

	
	}
}

