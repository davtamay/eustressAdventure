using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnRandomizedPowerUps : MonoBehaviour {

	[SerializeField] private float powerUpSpawnTime;
	[SerializeField] private float powerUpDeviationHorizontal = 1f;
	[SerializeField] private float powerUpDeviationVertical = 1f ;
	[SerializeField] private float powerUpRadiusOffset;

	[SerializeField]private GameObject[] powerUps;

	IEnumerator Start () {

		float timer = 0f;

		while (true) {
			yield return null;
			timer += Time.deltaTime;

			if (powerUpSpawnTime < timer) {
				Vector3 randomSphereUnits = Random.insideUnitSphere.normalized;
				int tempRandom = Random.Range (0, powerUps.Length);
				GameObject tempGO = Instantiate (powerUps [tempRandom], transform.position + new Vector3(powerUpRadiusOffset + randomSphereUnits.x * powerUpDeviationHorizontal, powerUpRadiusOffset + randomSphereUnits.y * powerUpDeviationVertical, powerUpRadiusOffset +randomSphereUnits.z * powerUpDeviationHorizontal),Quaternion.Euler(Random.insideUnitSphere));  //Random.insideUnitSphere * powerUpSpawnRadius, Quaternion.identity);
				//GameObject tempGO = Instantiate (powerUps [tempRandom], transform.position + Random.insideUnitSphere * powerUpSpawnRadius, Quaternion.identity);
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
