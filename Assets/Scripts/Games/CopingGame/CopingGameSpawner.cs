using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopingGameSpawner : MonoBehaviour {

	public static CopingGameSpawner Instance
	{ get { return instance; } }

	private static CopingGameSpawner instance = null;

	[SerializeField] private GameObject[] objectsToSpawn;

	[SerializeField] private float startSpawnSpeed;
	[SerializeField] private float spawnSpeed;


	[SerializeField] private LookControlMovement LookControl;



	public bool isGameOver;

	[SerializeField] private GameObject gameOverCanvas;
	[SerializeField] private Text GameScoreText;

	private int _score;
	public int score{


		get{ return _score;}
		set{
			_score = value;
			GameScoreText.text = "Score: " + score;
		}

	}


	[Header("WaveSettings")]
	[SerializeField] private float speedDecrementPerTime;
	[SerializeField] private float timeToSpeedDecrement;

	[SerializeField]private Transform[] spawnLocations;

	void Awake(){
		
		if (instance) {
			Debug.Log ("There are two CopingGameSpawners");
			return;
		}
		instance = this; 
	
	}
	void Start () {
		spawnLocations = new Transform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
			spawnLocations[i] = transform.GetChild (i);

		spawnSpeed = startSpawnSpeed;
		gameOverCanvas.SetActive (false);
	}
	
	private float timer;
	private float maintimer;

	private bool isPaused;
	void Update () {

		maintimer += Time.unscaledDeltaTime;

		if (isGameOver) {
			isGameOver = false;
			isPaused = true;
			timer = 0;
			maintimer = 0;

		//	LookControl.isLooking = false;
			gameOverCanvas.SetActive (true);
			StartCoroutine (StartGameAgain ());

		}

		if(LookControl.isLooking && !isPaused)
		timer += Time.unscaledDeltaTime;



		if (timer > spawnSpeed) {

			timer = 0;
		
		
			GameObject tempGO = Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Length)], spawnLocations [Random.Range(0, spawnLocations.Length)].position, transform.rotation);
			tempGO.transform.SetParent(transform.parent);
		
			if (spawnSpeed <= 3) {
				tempGO = Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Length)], spawnLocations [Random.Range(0, spawnLocations.Length)].position, transform.rotation);
				tempGO.transform.SetParent(transform.parent);
			
			}

		
		}

		if (maintimer > timeToSpeedDecrement) {

			spawnSpeed -= speedDecrementPerTime;
			maintimer = 0;
		
		}
			


		
		
	}

	IEnumerator StartGameAgain(){
	
	
		yield return new WaitForSecondsRealtime (5);
		score = 0;
		isPaused = false;
		gameOverCanvas.SetActive (false);
		LookControl.gameObject.SetActive (true);
		spawnSpeed = startSpawnSpeed;
	
	
	}



}
