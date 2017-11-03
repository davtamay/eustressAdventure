using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnRandomizedPowerUps : MonoBehaviour {

	[SerializeField] private float powerUpSpawnTime;
	[SerializeField] private float powerUpSpawnRadius;

	[SerializeField]private GameObject[] powerUps;

	IEnumerator Start () {

		float timer = 0f;

		while (true) {
			yield return null;
			timer += Time.deltaTime;

			if (powerUpSpawnTime < timer) {
			
				int tempRandom = Random.Range (0, powerUps.Length);

				GameObject tempGO = Instantiate (powerUps [tempRandom], transform.position + Random.insideUnitSphere * powerUpSpawnRadius, Quaternion.identity);
				tempGO.transform.parent = transform;
					timer = 0f;
			
			
			}
		
		
		
		}

		
	}

	void OnDisable(){
	
		foreach (Transform c in transform)
			Destroy (c.gameObject);
	
	
	}
	


}
