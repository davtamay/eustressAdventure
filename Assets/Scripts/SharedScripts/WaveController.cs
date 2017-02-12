using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {


	private Transform thisTransform;

	[SerializeField]private Transform firstWaveObject;
	[SerializeField]private Transform secondWaveObject;
	[SerializeField]private Transform thirdWaveObject;
	[SerializeField]private Transform fourthWaveObject;
	[SerializeField]private Transform fifthWaveObject;
	[SerializeField]private Transform sixWaveObject;

	[SerializeField] int GOToRespondFirstWave;
	[SerializeField] int GOToRespondSecondWave;
	[SerializeField] float timeUntilSecondWave;
	[SerializeField] int GOToRespondThirdWave;
	[SerializeField] float timeUntilThirdWave;
	[SerializeField] int GOToRespondFourthWave;
	[SerializeField] float timeUntilFourthWave;
	[SerializeField] int GOToRespondFifthWave;
	[SerializeField] float timeUntilFifthWave;
	[SerializeField] int GOToRespondSixthWave;
	[SerializeField] float timeUntilSixthWave;


	private List<int> myIndices;

	private float timer;
	private bool isDone;


	void Awake(){
		thisTransform = transform;

		firstWaveObject = thisTransform.GetChild(0);
		secondWaveObject = thisTransform.GetChild(1);
		thirdWaveObject = thisTransform.GetChild(2);
		fourthWaveObject = thisTransform.GetChild(3);
		fifthWaveObject = thisTransform.GetChild(4);
		sixWaveObject = thisTransform.GetChild(5);
	
		foreach (Transform gO1 in firstWaveObject)
			gO1.gameObject.SetActive (false);

		foreach (Transform gO2 in secondWaveObject)
			gO2.gameObject.SetActive (false);

		foreach (Transform gO3 in thirdWaveObject)
			gO3.gameObject.SetActive (false);

		foreach (Transform gO4 in fourthWaveObject)
			gO4.gameObject.SetActive (false);

		foreach (Transform gO5 in fifthWaveObject)
			gO5.gameObject.SetActive (false);

		foreach (Transform gO6 in sixWaveObject)
			gO6.gameObject.SetActive (false);


	}


	void Start(){
		GameController.Instance.TimeToAdd (ref isDone, timeUntilSecondWave);

		RandomizeGOToEnable (GOToRespondFirstWave, firstWaveObject);
		
		foreach (int go1 in myIndices) 
			firstWaveObject.GetChild (go1).gameObject.SetActive (true);
		
		//myIndices.Clear();

		StartCoroutine (OnUpdate ());
	}


	IEnumerator OnUpdate(){
	
		bool TimerOn = true;
		bool isFirstWave = true;
		bool isSecondWave = false;
		bool isThirdWave = false;
		bool isFourthWave = false;
		bool isFifthWave = false;
		bool isSixWave = false;


		while (TimerOn) {
			yield return null;
		

			GameController.Instance.TimeToAdd (ref isDone);

		//yield return new WaitUntil (() => timer >= timeUntilSecondWave); 

			if (isDone && isFirstWave) {
				isDone = false;
	
				firstWaveObject.gameObject.SetActive (false);

				yield return StartCoroutine (GameController.Instance.NewWave ());

				GameController.Instance.TimeToAdd (ref isDone, timeUntilThirdWave);

				RandomizeGOToEnable (GOToRespondSecondWave, secondWaveObject);

				foreach (int go2 in myIndices) 
					secondWaveObject.GetChild (go2).gameObject.SetActive (true);

			//	myIndices.Clear();

				isFirstWave = false;
				isSecondWave = true;

			}	
			
		
			if (isDone && isSecondWave) {
				isDone = false;

				secondWaveObject.gameObject.SetActive (false);


				yield return StartCoroutine (GameController.Instance.NewWave ());

				GameController.Instance.TimeToAdd (ref isDone, timeUntilFourthWave);


				RandomizeGOToEnable (GOToRespondThirdWave, thirdWaveObject);

				foreach (int go3 in myIndices) 
					thirdWaveObject.GetChild(go3).gameObject.SetActive (true);




			//	myIndices.Clear();

			//	timer = 0;
				isSecondWave = false;
				isThirdWave = true;

			


			}
			if (isDone && isThirdWave) {
				isDone = false;

				thirdWaveObject.gameObject.SetActive (false);

				yield return StartCoroutine (GameController.Instance.NewWave ());

				GameController.Instance.TimeToAdd (ref isDone, timeUntilFifthWave);

				RandomizeGOToEnable (GOToRespondFourthWave, fourthWaveObject);

				foreach (int go4 in myIndices) 
					fourthWaveObject.GetChild(go4).gameObject.SetActive (true);

			//	myIndices.Clear();

			//	timer = 0;
				isThirdWave = false;
				isFourthWave = true;




			}
			if (isDone && isFourthWave) {

				fourthWaveObject.gameObject.SetActive (false);

				yield return StartCoroutine (GameController.Instance.NewWave ());

				GameController.Instance.TimeToAdd (ref isDone, timeUntilSixthWave);
				isDone = false;

				RandomizeGOToEnable (GOToRespondFifthWave, fifthWaveObject);

				foreach (int go5 in myIndices) 
					fifthWaveObject.GetChild(go5).gameObject.SetActive (true);

				//myIndices.Clear();

		
				isFourthWave = false;
				isFifthWave = true;



			}
			if (isDone && isFifthWave) {
				isDone = false;

				fifthWaveObject.gameObject.SetActive (false);

				yield return StartCoroutine (GameController.Instance.NewWave ());

				RandomizeGOToEnable (GOToRespondSixthWave, sixWaveObject);

				foreach (int go6 in myIndices) 
					sixWaveObject.GetChild(go6).gameObject.SetActive (true);

			//	myIndices.Clear();


				isFifthWave = false;
				isSixWave = true;

					TimerOn = false;


			}



	}
		
	}

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
