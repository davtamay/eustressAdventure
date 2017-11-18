using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

	[SerializeField]private int amountOfBallObjects;
	[SerializeField]private GameObject ballPrefab;
	private GameObject[] ballPrefabList;
	private Queue <Transform> queue = new Queue<Transform>();


	private Queue <Transform> audioQueue = new Queue<Transform>();
	[SerializeField]private float ballForce; 


	public static BallManager Instance
	{ get { return instance; } }

	private static  BallManager instance = null;


	void Awake()
	{

		if (instance) {
			Debug.Log ("There are two BallManagers");
			return;
		}
		instance = this; 

	}

	public Queue<AudioSource> audioSourceHitQueue;
	IEnumerator Start () {

		audioSourceHitQueue = AudioManager.Instance.CreateTempAudioSourcePoolQueue (AudioManager.AudioReferanceType._AMBIENT, "BallHit", 3);


		ballPrefabList = new GameObject [amountOfBallObjects];

		for (int i = 0; i < amountOfBallObjects; i++) {
			ballPrefabList [i] = Instantiate (ballPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			Transform prefabTrans = ballPrefabList [i].GetComponent<Transform> ();
		
			prefabTrans.parent = transform;
			queue.Enqueue (prefabTrans);

			ballPrefabList [i].SetActive (false);

			yield return null;
		}
	}

	public Transform BallObject(Vector3 pos, Vector3 dir){


		Transform spawnedPrefab = Instance.queue.Dequeue ();

		spawnedPrefab.gameObject.SetActive (true);
		spawnedPrefab.position = pos; 

		Rigidbody spawnedRigidbody = spawnedPrefab.GetComponent<Rigidbody> ();
		//remove velocity from used object to prevent objects from having velocity at start
		spawnedRigidbody.velocity = Vector3.zero;
		//Debug.Log (spawnedRigidbody.velocity);
		spawnedRigidbody.AddForce (dir * ballForce, ForceMode.Impulse);

		Instance.queue.Enqueue (spawnedPrefab);

		return spawnedPrefab;


	}


}
