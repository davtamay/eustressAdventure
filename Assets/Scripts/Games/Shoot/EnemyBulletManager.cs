using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour {

	public int amountOfBullets;
	public GameObject bulletPrefab;
	public GameObject[] bulletPrefabList;
	public Queue <Transform> queue = new Queue<Transform>();


	public static EnemyBulletManager Instance
	{ get { return instance; } }

	private static EnemyBulletManager instance = null;


	void Awake()
	{

		if (instance) {
			Debug.Log ("There are two EnemyBulletManager");
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
	//ID : Player = 0, Enemy = 1;
	public static Transform BulletPosition(Vector3 pos, Quaternion dir, float speed){

		Transform spawnedPrefab = Instance.queue.Dequeue ();
		while (spawnedPrefab == null)
			spawnedPrefab = Instance.queue.Dequeue ();

		spawnedPrefab.gameObject.SetActive (true);

		BulletControll bulletControll = spawnedPrefab.GetComponent<BulletControll> ();
		bulletControll.bulletSpeed = speed;
		bulletControll.bulletID = 1;


		spawnedPrefab.rotation = dir;
		spawnedPrefab.position = pos; 


		Instance.queue.Enqueue (spawnedPrefab);

		return spawnedPrefab;


	}
}
