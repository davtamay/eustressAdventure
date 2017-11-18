using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopingGameSpawner : MonoBehaviour {

	public static CopingGameSpawner Instance
	{ get { return instance; } }

	private static CopingGameSpawner instance = null;

	private Transform thisTransform;

	[SerializeField] private GameObject spawnedParent;

	[SerializeField] private GameObject[] objectsToSpawn;

	[SerializeField] private float startSpawnSpeed;
	[SerializeField] private float spawnSpeed;

	[SerializeField] private float timeUntilPointSpawn;
	[SerializeField] private GameObject foodPointGO;
	[SerializeField] private Vector2 foodSpawnDeviationFromCenter = new Vector2(120f, 46f);
	[SerializeField] private float timeUntilAddCar;
	[SerializeField] private int carsOnSameTime = 1;

	[SerializeField] private RectTransform mainScreen;

	[SerializeField] private LookControlMovement LookControl;

	[SerializeField] private GameObject gameOverCanvas;
	[SerializeField] private Text GameScoreText;
	private LocalizedText localizationText;

	public bool isGameOver;

	private List<GameObject> tempSessionList;

	List<int> myIndices;

	private int _score;
	public int score{


		get{ return _score;}
		set{
			_score = value;
			GameScoreText.text = string.Empty;
			localizationText.OnUpdate ();
			GameScoreText.text += " " + score;
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

		thisTransform = transform;

		localizationText = GameScoreText.GetComponent<LocalizedText> ();
		localizationText.OnUpdate ();
		spawnLocations = new Transform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
			spawnLocations[i] = transform.GetChild (i);

		spawnSpeed = startSpawnSpeed;
		gameOverCanvas.SetActive (false);


	}
	void OnEnable(){
		tempSessionList = new List<GameObject> ();
	}


	private float timer;
	private float pointTimer;
	private float AddCarTimer;
	private float maintimer;

	private bool isPaused;

	void Update () {

		maintimer += Time.unscaledDeltaTime;

		if (isGameOver ) {
			isGameOver = false;
			isPaused = true;
			timer = 0;
			AddCarTimer = 0;
			pointTimer = 0;
			maintimer = 0;
			carsOnSameTime = 1;

			foreach (GameObject temp in tempSessionList) {
				if(temp != null)
				Destroy (temp);

			}

			gameOverCanvas.SetActive (true);
			StartCoroutine (StartGameAgain ());

		}

		if (LookControl.isLooking && !isPaused) {
			timer += Time.unscaledDeltaTime;
			pointTimer += Time.unscaledDeltaTime;
			AddCarTimer += Time.unscaledDeltaTime;
		}

		if (pointTimer > timeUntilPointSpawn) {
			pointTimer = 0;
			GameObject tempGO = Instantiate (foodPointGO, Vector3.zero, Quaternion.identity);//mainScreen.position.z - 0.0001f), mainScreen.rotation);//Quaternion.identity);
		
			tempGO.transform.SetParent(thisTransform.parent);

			//PROJECT COINS ONTO THE PLAYING PLANE
			tempGO.transform.localPosition = Vector3.ProjectOnPlane (tempGO.transform.position, mainScreen.TransformPoint(Vector3.forward));

			//SET DISPERSION FROM CENTER
			tempGO.transform.localPosition += new Vector3 (Random.Range (-foodSpawnDeviationFromCenter.x, foodSpawnDeviationFromCenter.x), Random.Range (-foodSpawnDeviationFromCenter.y, foodSpawnDeviationFromCenter.y),0);//tempGO.transform.position = new Vector3 (mainScreen.position.x + Random.Range (-0.12f, 0.12f), mainScreen.position.y + Random.Range (-0.085f, 0.085f),0);
			//new Vector3 (mainScreen.position.x + Random.Range (-0.12f, 0.12f), mainScreen.position.y + Random.Range (-0.085f, 0.085f), mainScreen.position.z);tempGO.transform.position = new Vector3 (mainScreen.position.x + Random.Range (-0.12f, 0.12f), mainScreen.position.y + Random.Range (-0.085f, 0.085f),0);

			tempGO.transform.localRotation = Quaternion.identity;


			//tempGO.transform.position (Vector3.ProjectOnPlane())
//			Vector3 tempPos = tempGO.transform.position;
//			tempPos.z = mainScreen.position.z;
//			tempGO.transform.position = tempPos;

			//tempGO
			tempSessionList.Add (tempGO);
		}



		if (timer > spawnSpeed) {

			timer = 0;
		
			StartCoroutine (AddObstacle ());
		
		}

		if (AddCarTimer > timeUntilAddCar) {

			AddCarTimer = 0;

			carsOnSameTime++;

		}

		if (maintimer > timeToSpeedDecrement) {

			spawnSpeed -= speedDecrementPerTime;
			maintimer = 0;
		
		}
			

//				foreach (GameObject temp in tempSessionList) {
//					if(temp != null)
//					{
//						if (temp.name != "Coin")
//							return;
//						
//						Vector3 tempPos = temp.transform.position;
//						tempPos.z = mainScreen.position.z;//thisTransform.position.z;
//						temp.transform.position = tempPos;
//		
//					}
//		
//				}


		
		
	}


	IEnumerator AddObstacle(){

		RandomizeGOToEnable (carsOnSameTime, thisTransform);

		foreach (int cN in myIndices) {
		GameObject tempGO = Instantiate (objectsToSpawn [Random.Range (0, objectsToSpawn.Length)], spawnLocations[cN].position, Quaternion.identity);
		
		tempGO.transform.SetParent (thisTransform.parent);
		tempGO.transform.localRotation = Quaternion.identity;

		tempSessionList.Add (tempGO);

			yield return new WaitForSecondsRealtime (Random.Range (0f, 2f));

	}
	
	
	}


	void OnDisable(){
		isGameOver = false;
		timer = 0;
		AddCarTimer = 0;
		pointTimer = 0;
		maintimer = 0;
		carsOnSameTime = 1;

		foreach (GameObject temp in tempSessionList) {
			if (temp != null)
				Destroy (temp);
		}
		//	LookControl.isLooking = false;
		//gameOverCanvas.SetActive (true);
		//StartCoroutine (StartGameAgain ());
	
	}

	IEnumerator StartGameAgain(){
	
	
		yield return new WaitForSecondsRealtime (5);
		score = 0;
		isPaused = false;
		gameOverCanvas.SetActive (false);
		LookControl.gameObject.SetActive (true);
		spawnSpeed = startSpawnSpeed;
	
	
	}

	//foreach (int go in myIndices) 
	//	sixWaveObject.GetChild(go).gameObject.SetActive (true);

	private void RandomizeGOToEnable(int numToRespawn, Transform gO){


		int ChildCount = gO.childCount;

		if (numToRespawn > ChildCount) {
			Debug.LogWarning ("numToRespawn/GOToRespond can't be bigger than available childCount");
			return;
		}
		//Debug.Log (ChildCount);
		myIndices = new List<int> (ChildCount);

		for (int i = 0; i < numToRespawn; i++) {

			int myIndexPull = Random.Range(0, ChildCount);


			while (myIndices.Contains (myIndexPull)) {

				myIndexPull = Random.Range (0, ChildCount);

			}

			myIndices.Add (myIndexPull);



		}
	}



}
