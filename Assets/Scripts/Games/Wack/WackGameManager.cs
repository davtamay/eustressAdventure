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
	public GameObject warningSign;

	private bool isEasy;
	private bool isMedium;

	public Text coinText;
	WackLookClick wackLookClick;

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
	
		warningSign.SetActive (false);
		SetDifficulty (Difficulty.easy);
		isEasy = true;

		StartCoroutine (UpdateGame());
	}
		
	IEnumerator UpdateGame(){
	
		while (true) {
			if (!GameController.Instance.Paused) { 
				timePassed += Time.deltaTime;

				if (timePassed > mediumLevelTime && isEasy) {
					warningSign.SetActive (true);
				
					wackLookClick.TurnOffAllMoles(2);
					yield return new WaitForSeconds (2);
					warningSign.SetActive (false);
				
					SetDifficulty (Difficulty.medium);
					isMedium = true;
					isEasy = false;
				}
				if (timePassed > hardLevelTime && isMedium) {
					warningSign.SetActive (true);
					wackLookClick.TurnOffAllMoles(2);
					yield return new WaitForSeconds (2);
					warningSign.SetActive (false);
					SetDifficulty (Difficulty.hard);
					isMedium = false;
				}
	
			
			}
			yield return null;
		}
	}


	private int _points;
	public int points{
		get{return _points;}

		set{coinText.text = ":";
			_points += value;
			coinText.text += _points.ToString();  }

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