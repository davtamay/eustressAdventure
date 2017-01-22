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

	void Start(){
	
		spawnLoc = new List<Transform> (spawnLocations);
	
	}
	void Update () {
		//Debug.DrawRay (tempCollObj.transform.position + Vector3.down * 3, Vector3.up * 5, Color.black);

		timePass += Time.deltaTime;

		if (spawnTime < timePass) {

			int spawnLocRandom = Random.Range (0, spawnLoc.Count);

				timePass = 0.0f;

			tempCollObj = Instantiate (collectionObjs [Random.Range (0, collectionObjs.Length)], spawnLoc [spawnLocRandom].position, Quaternion.identity) as GameObject;
			RaycastHit[] hits;
			RaycastHit hit;
			hits = Physics.RaycastAll (tempCollObj.transform.position + Vector3.down * 3, Vector3.up, 10, itemLayerMask);
				for (int i = 0; i < hits.Length; i++) {
					
					hit = hits[i];
					if( hit.transform.gameObject.GetInstanceID() != tempCollObj.GetInstanceID())
						Destroy (tempCollObj);
				}
	
		
		


				spawnLoc.RemoveAt (spawnLocRandom);
	
			if (spawnLoc.Count == 0) {

				spawnLoc.AddRange (spawnLocations);
			//	GameObject curEnemy = Instantiate (enemy, spawnLoc [spawnLocRandom].position + Vector3.up * 2, Quaternion.identity) as GameObject;

				EnemyManager.Instance.InitiateEnemy(spawnLoc [spawnLocRandom].position + Vector3.up * 2);
			} 

		}


	}

}
