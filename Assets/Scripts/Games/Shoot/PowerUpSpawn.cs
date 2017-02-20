using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour {

	[SerializeField]private GameObject[] PowerUps;
	[SerializeField] float timeUntilPowerUp = 2;
	private Transform playerTransform;

	IEnumerator Start(){

		playerTransform = GameObject.FindWithTag ("Player").transform;

		int RandomPowerUp;

		while (true) {
		


			yield return new WaitForSeconds (timeUntilPowerUp);
			RandomPowerUp = Random.Range (0, PowerUps.Length - 1);

			Instantiate (PowerUps [RandomPowerUp], playerTransform.position + (30 * Random.insideUnitSphere + Vector3.forward * 70), Quaternion.identity);
		


		}
	
	
	
	
	}
}
