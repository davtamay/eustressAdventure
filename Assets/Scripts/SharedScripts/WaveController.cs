using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class WaveController : MonoBehaviour {



	private Transform thisTransform;
	[SerializeField]UnityEvent onGameStart;

	[SerializeField]UnityEvent onWaveShowStart;
	[SerializeField]UnityEvent onWaveShowEnd;
	[SerializeField]UnityEvent onWaveChange;

	[SerializeField]private Transform firstWaveObject;
	[SerializeField]private Transform secondWaveObject;
	[SerializeField]private Transform thirdWaveObject;
	[SerializeField]private Transform fourthWaveObject;
	[SerializeField]private Transform fifthWaveObject;
	[SerializeField]private Transform sixWaveObject;

	[SerializeField]private bool isRemainAfterWave;

	[SerializeField] int GOToRespondFirstWave;
	[SerializeField]UnityEvent onFirstWaveStart;
	[SerializeField] int GOToRespondSecondWave;
	[SerializeField] float timeUntilSecondWave;
	[SerializeField]UnityEvent onSecondWaveStart;
	[SerializeField] int GOToRespondThirdWave;
	[SerializeField] float timeUntilThirdWave;
	[SerializeField]UnityEvent onThirdWaveStart;
	[SerializeField] int GOToRespondFourthWave;
	[SerializeField] float timeUntilFourthWave;
	[SerializeField]UnityEvent onFourthWaveStart;
	[SerializeField] int GOToRespondFifthWave;
	[SerializeField] float timeUntilFifthWave;
	[SerializeField]UnityEvent onFifthWaveStart;
	[SerializeField] int GOToRespondSixthWave;
	[SerializeField] float timeUntilSixthWave;
	[SerializeField]UnityEvent onWaveFinished;


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
		onGameStart.Invoke();
	//	onWaveChange.Invoke ();

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

		onFirstWaveStart.Invoke ();

		while (TimerOn) {
			yield return null;
		

			GameController.Instance.TimeToAdd (ref isDone);

		//yield return new WaitUntil (() => timer >= timeUntilSecondWave); 

			if (isDone && isFirstWave) {
				isDone = false;


				if (!isRemainAfterWave)
				firstWaveObject.gameObject.SetActive (false);
				
				onWaveShowStart.Invoke ();
				yield return StartCoroutine (GameController.Instance.NewWave ());
				onWaveShowEnd.Invoke ();

				GameController.Instance.TimeToAdd (ref isDone, timeUntilThirdWave);

				RandomizeGOToEnable (GOToRespondSecondWave, secondWaveObject);

				foreach (int go2 in myIndices) 
					secondWaveObject.GetChild (go2).gameObject.SetActive (true);

				onWaveChange.Invoke ();
				onSecondWaveStart.Invoke ();
			//	myIndices.Clear();

				isFirstWave = false;
				isSecondWave = true;

			}	
			
		
			if (isDone && isSecondWave) {
				isDone = false;

				if (!isRemainAfterWave)
				secondWaveObject.gameObject.SetActive (false);

				onWaveShowStart.Invoke ();
				yield return StartCoroutine (GameController.Instance.NewWave ());
				onWaveShowEnd.Invoke ();

				GameController.Instance.TimeToAdd (ref isDone, timeUntilFourthWave);


				RandomizeGOToEnable (GOToRespondThirdWave, thirdWaveObject);

				foreach (int go3 in myIndices) 
					thirdWaveObject.GetChild(go3).gameObject.SetActive (true);




			//	myIndices.Clear();
				onWaveChange.Invoke ();
				onThirdWaveStart.Invoke ();
			//	timer = 0;
				isSecondWave = false;
				isThirdWave = true;

			


			}
			if (isDone && isThirdWave) {
				isDone = false;

				if (!isRemainAfterWave)
				thirdWaveObject.gameObject.SetActive (false);

				onWaveShowStart.Invoke ();
				yield return StartCoroutine (GameController.Instance.NewWave ());
				onWaveShowEnd.Invoke ();

				GameController.Instance.TimeToAdd (ref isDone, timeUntilFifthWave);

				RandomizeGOToEnable (GOToRespondFourthWave, fourthWaveObject);

				foreach (int go4 in myIndices) 
					fourthWaveObject.GetChild(go4).gameObject.SetActive (true);

			//	myIndices.Clear();
				onWaveChange.Invoke ();
				onFourthWaveStart.Invoke ();
			//	timer = 0;
				isThirdWave = false;
				isFourthWave = true;




			}
			if (isDone && isFourthWave) {

				if (!isRemainAfterWave)
				fourthWaveObject.gameObject.SetActive (false);

				onWaveShowStart.Invoke ();
				yield return StartCoroutine (GameController.Instance.NewWave ());
				onWaveShowEnd.Invoke ();

				GameController.Instance.TimeToAdd (ref isDone, timeUntilSixthWave);
				isDone = false;

				RandomizeGOToEnable (GOToRespondFifthWave, fifthWaveObject);

				foreach (int go5 in myIndices) 
					fifthWaveObject.GetChild(go5).gameObject.SetActive (true);

				//myIndices.Clear();
				onWaveChange.Invoke ();
				onFifthWaveStart.Invoke ();
		
				isFourthWave = false;
				isFifthWave = true;



			}
			if (isDone && isFifthWave) {
				isDone = false;
			

				if (!isRemainAfterWave)
				fifthWaveObject.gameObject.SetActive (false);

				onWaveShowStart.Invoke ();
				yield return StartCoroutine (GameController.Instance.NewWave ());
				onWaveShowEnd.Invoke ();

				RandomizeGOToEnable (GOToRespondSixthWave, sixWaveObject);

				foreach (int go6 in myIndices) 
					sixWaveObject.GetChild(go6).gameObject.SetActive (true);

			//	myIndices.Clear();
				onWaveChange.Invoke ();
				onWaveFinished.Invoke ();


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
