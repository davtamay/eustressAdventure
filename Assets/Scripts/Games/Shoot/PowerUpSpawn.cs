using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class PowerUpSpawn : MonoBehaviour {

	[SerializeField]private GameObject[] PowerUps;
	[SerializeField]private Vector3 spawnOffset = Vector3.back * 5f;
	[SerializeField]private float timeUntilDestroy = 12f;



	public static PowerUpSpawn Instance
	{ get { return instance; } }

	private static PowerUpSpawn instance = null;


	void Awake()
	{

		if (instance) {
			Debug.Log ("There is two PowerUpSpawn Instances");
			return;
		}
		instance = this; 
	}

	public GameObject SpawnPowerUpLocation(Vector3 pos){
	
	
		int RandomPowerUp = Random.Range (0, PowerUps.Length);
		GameObject gO = Instantiate (PowerUps [RandomPowerUp], pos , Quaternion.identity);
		gO.transform.position += spawnOffset;
		StartCoroutine (DestroyObjects (timeUntilDestroy, gO));

		//Invoke ("Destroy(gO)", 12f);
		return gO;
	}

	IEnumerator DestroyObjects(float time, params GameObject[] gO){

		while (time > 0f) {

			time -= Time.deltaTime;
			yield return null;
		
		}


			//yield return new WaitForSeconds(time);
		if (gO == null)

			foreach (GameObject go in gO)
				go.SetActive (false);

	}
		/*int RandomPowerUp;

		while (true) {
		


			yield return new WaitForSeconds (timeUntilPowerUp);
			RandomPowerUp = Random.Range (0, PowerUps.Length - 1);

			Instantiate (PowerUps [RandomPowerUp], playerTransform.position + (30 * Random.insideUnitSphere + Vector3.forward * 70), Quaternion.identity);
		


		}
	
	
	
	
	}*/
}
