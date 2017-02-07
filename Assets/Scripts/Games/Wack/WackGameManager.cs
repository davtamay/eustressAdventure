using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

using System.Linq;


public class WackGameManager : MonoBehaviour {

	public Difficulty difficulty = Difficulty.easy;

	public GameObject easyWack;
	public GameObject mediumWack;
	public GameObject hardWack;

	public List<int> myIndices;


	public GameObject[] totalMoles;
	public List<GameObject> activeMoles; 

	private float timePassed;

	public int amountOfMolesToEnable = 3;

	public float mediumLevelTime;
	public float hardLevelTime;


	private bool isEasy;
	private bool isMedium;

	public Text coinText;
	WackLookClick wackLookClick;

	private bool isDone;

	public static WackGameManager Instance
	{ get { return instance; } }

	private static WackGameManager instance = null;

	void Awake()
	{

		if (instance) {
			Debug.LogError ("Two instances of singleton (WackGameManager)");

			return;
		}
		instance = this; 


	}


	void Start(){
	
		wackLookClick = GetComponent<WackLookClick> ();
	
		SetDifficulty (Difficulty.easy);
		isEasy = true;

		GameController.Instance.TimeToAdd(ref isDone, mediumLevelTime);
		StartCoroutine (UpdateGame());
	}
		
	IEnumerator UpdateGame(){
	
		while (true) {
			if (!GameController.Instance.Paused) { 
				timePassed += Time.deltaTime;

				if (timePassed > mediumLevelTime && isEasy) {
					wackLookClick.TurnOffAllMoles(GameController.Instance.GetNewWaveTime);

				yield return StartCoroutine (GameController.Instance.NewWave ());
					GameController.Instance.TimeToAdd(ref isDone, hardLevelTime);

					SetDifficulty (Difficulty.medium);
					isMedium = true;
					isEasy = false;
				}
				if (timePassed > hardLevelTime && isMedium) {
					wackLookClick.TurnOffAllMoles(GameController.Instance.GetNewWaveTime);

					yield return StartCoroutine (GameController.Instance.NewWave ());

					//warningSign.SetActive (false);
					SetDifficulty (Difficulty.hard);
					isMedium = false;
				}
	
			
			}
			yield return null;
		}
	}



		

	void SetDifficulty(Difficulty selectedDifficulty){

		int DivideByDifficulty= totalMoles.Length / 3;

	




		switch (selectedDifficulty) {

		case Difficulty.easy:

			easyWack.SetActive (true);
			mediumWack.SetActive (false);
			hardWack.SetActive (false);

		

			for (int i = amountOfMolesToEnable; i < DivideByDifficulty; i++) {

				int myIndexPull = Random.Range(0, DivideByDifficulty);


					while (myIndices.Contains (myIndexPull)) {
						
						myIndexPull = Random.Range (0, DivideByDifficulty);
						
					}

					myIndices.Add (myIndexPull);
			
					

				}
		

				foreach (int ran in myIndices)
				totalMoles [ran].transform.parent.gameObject.SetActive (false);



			AddMolesToActiveList ();

			break;

			case Difficulty.medium:
			
			easyWack.SetActive(true);
			mediumWack.SetActive(true);
			hardWack.SetActive(false);

			for (int i = amountOfMolesToEnable; i < DivideByDifficulty; i++) {

				int myIndexPull = Random.Range(DivideByDifficulty, DivideByDifficulty *2 );


				while (myIndices.Contains (myIndexPull)) {

					myIndexPull = Random.Range (DivideByDifficulty, DivideByDifficulty *2);

				}

				myIndices.Add (myIndexPull);



			}


			foreach (int ran in myIndices)
				totalMoles [ran].transform.parent.gameObject.SetActive (false);

			AddMolesToActiveList ();

			break;

			case Difficulty.hard:

			easyWack.SetActive(true);
			mediumWack.SetActive(true);
			hardWack.SetActive(true);


			for (int i = amountOfMolesToEnable; i < DivideByDifficulty; i++) {

				int myIndexPull = Random.Range(DivideByDifficulty *2, DivideByDifficulty *3 );


				while (myIndices.Contains (myIndexPull)) {

					myIndexPull = Random.Range (DivideByDifficulty*2, DivideByDifficulty *3);

				}

				myIndices.Add (myIndexPull);



			}


			foreach (int ran in myIndices)
				totalMoles [ran].transform.parent.gameObject.SetActive (false);

			AddMolesToActiveList ();

			break;


		}
	}
	//CheckifMolesAreActiveInHierarcy (AKA scene - taking into account parent active)

	void AddMolesToActiveList(){
		
		for (int i = 0; i < totalMoles.Length; i++) {

			if (totalMoles [i].activeInHierarchy) 
				activeMoles.Add (totalMoles [i]);
	
		}
	}
}