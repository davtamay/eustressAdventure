using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnCollections : MonoBehaviour {

	public GameObject[] collectionObjs;

	public Transform[] spawnLocations;

	public float spawnTime;

	private float timePass;

	public List <Transform> spawnLoc;

	public GameObject enemy;

	private GameObject tempCollObj;

	public LayerMask itemLayerMask;

	private GameObject newWave;
	[SerializeField] private float timeUntilNewWave;

	private bool isFirstWave = true;
	[SerializeField] private int amountOfEnemies1;
	private bool isSecondWave = false;
	[SerializeField] private int amountOfEnemies2;
	//private bool isThirdWave = false;
	[SerializeField] private int amountOfEnemies3;


	void Start(){
	
		spawnLoc = new List<Transform> (spawnLocations);

		StartCoroutine (OnUpdate ());
	
	}
	int spawnLocRandom;
	IEnumerator OnUpdate () {
		//Debug.DrawRay (tempCollObj.transform.position + Vector3.down * 3, Vector3.up * 5, Color.black);

		while(true){
			yield return null;
		timePass += Time.deltaTime;

		if (spawnTime < timePass) {

			spawnLocRandom = Random.Range (0, spawnLoc.Count);
				if(spawnLoc [spawnLocRandom].childCount == 0){

					timePass = 0.0f;
					tempCollObj = Instantiate (collectionObjs [Random.Range (0, collectionObjs.Length)], spawnLoc [spawnLocRandom].position, Quaternion.identity) as GameObject; 
					tempCollObj.transform.parent = spawnLoc [spawnLocRandom];


				}
		

	
//			if (isFirstWave) {
//		
//				if (EnemyManager.Instance.activeEnemies.Count == 0) {
//						StartCoroutine (WaveManager.Instance.NewWaveIntermission());
//						isFirstWave = false;
//						isSecondWave = true;
//						for (int i = 0; i < amountOfEnemies1; i++){
//							
//							EnemyManager.Instance.InitiateEnemy(spawnLoc [spawnLocRandom].position + Vector3.up * 4);
//							yield return null;
//						}
//				}
//
//			
//			}
//			if (isSecondWave) {
//
//					if (EnemyManager.Instance.activeEnemies.Count == 0) {
//						StartCoroutine (WaveManager.Instance.NewWaveIntermission());
//						isSecondWave = false;
//					//	isThirdWave = true;
//						for (int i = 0; i < amountOfEnemies2; i++){
//							
//							EnemyManager.Instance.InitiateEnemy(spawnLoc [spawnLocRandom].position + Vector3.up * 4);
//							yield return null;
//						}
//					}
//
//
//				}
			/*
				spawnLoc.RemoveAt (spawnLocRandom);
	
			if (spawnLoc.Count == 0) {

				spawnLoc.AddRange (spawnLocations);
			//	GameObject curEnemy = Instantiate (enemy, spawnLoc [spawnLocRandom].position + Vector3.up * 2, Quaternion.identity) as GameObject;

				EnemyManager.Instance.InitiateEnemy(spawnLoc [spawnLocRandom].position + Vector3.up * 2);*/

		}
		}
	}

//	public void SpawnEnemy()
//	{
//		spawnLocRandom = Random.Range (0, spawnLoc.Count);
//		EnemyManager.Instance.InitiateEnemy(spawnLoc [spawnLocRandom].position + Vector3.up * 4);
//
//
//	}




}
