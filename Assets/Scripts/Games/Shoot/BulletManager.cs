using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {
	public int amountOfBullets;
	public GameObject bulletPrefab;
	public GameObject[] bulletPrefabList;
	public Queue <Transform> queue = new Queue<Transform>();


	public static BulletManager Instance
	{ get { return instance; } }

	private static BulletManager instance = null;


	void Awake()
	{

		if (instance) {
			Debug.Log ("There are two BulletManager");
			return;
		}
		instance = this; 

	}


	IEnumerator Start () {

		bulletPrefabList = new GameObject [amountOfBullets];

		for (int i = 0; i < amountOfBullets; i++) {
			bulletPrefabList [i] = Instantiate (bulletPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			Transform prefabTrans = bulletPrefabList [i].GetComponent<Transform> ();
		
			prefabTrans.parent = transform;
			queue.Enqueue (prefabTrans);

			bulletPrefabList [i].SetActive (false);

			yield return null;
		}
	}

	public static Transform BulletPosition(Vector3 pos, Quaternion dir, float speed){

		Transform spawnedPrefab = Instance.queue.Dequeue ();
	

		spawnedPrefab.gameObject.SetActive (true);

		spawnedPrefab.GetComponent<BulletControll> ().bulletSpeed = speed;
		spawnedPrefab.rotation = dir;
		spawnedPrefab.position = pos; 
		 

		Instance.queue.Enqueue (spawnedPrefab);

		return spawnedPrefab;


	}


}
