using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {
	

	[SerializeField]private GameObject[] firstWaveObjects;
	[SerializeField]private GameObject[] secondWaveObjects;
	[SerializeField]private GameObject[] thirdWaveObjects;
	[SerializeField]private GameObject[] fourthWaveObjects;
	[SerializeField]private GameObject[] fifthWaveObjects;
	[SerializeField]private GameObject[] sixWaveObjects;

	[SerializeField] float timeUntilSecondWave;
	[SerializeField] float timeUntilThirdWave;
	[SerializeField] float timeUntilFourthWave;
	[SerializeField] float timeUntilFifthWave;
	[SerializeField] float timeUntilSixthWave;


	private float timer;
	private bool isDone;


	void Awake(){
	
		foreach (GameObject go1 in firstWaveObjects) 
			go1.SetActive (false);


		foreach (GameObject go2 in secondWaveObjects) 
			go2.SetActive (false);
	
		foreach (GameObject go3 in thirdWaveObjects) 
			go3.SetActive (false);

		foreach (GameObject go4 in fourthWaveObjects) 
			go4.SetActive (false);


		foreach (GameObject go5 in fifthWaveObjects) 
			go5.SetActive (false);

		foreach (GameObject go6 in sixWaveObjects) 
			go6.SetActive (false);

	
	
	}


	void Start(){
		GameController.Instance.TimeToAdd (ref isDone, timeUntilSecondWave);

		foreach (GameObject go1 in firstWaveObjects) 
			go1.SetActive (true);


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
				foreach (GameObject go1 in firstWaveObjects) 
					go1.SetActive (false);

				yield return StartCoroutine (GameController.Instance.NewWave ());

				GameController.Instance.TimeToAdd (ref isDone, timeUntilThirdWave);

				foreach (GameObject go2 in secondWaveObjects) 
					go2.SetActive (true);


				isFirstWave = false;
				isSecondWave = true;

			}	
			
		
			if (isDone && isSecondWave) {

				foreach (GameObject go2 in secondWaveObjects) 
					go2.SetActive (false);

				yield return StartCoroutine (GameController.Instance.NewWave ());

				GameController.Instance.TimeToAdd (ref isDone, timeUntilFourthWave);

				foreach (GameObject go3 in thirdWaveObjects) 
					go3.SetActive (true);

			//	timer = 0;
				isSecondWave = false;
				isThirdWave = true;

			


			}
			if (isDone && isThirdWave) {

				foreach (GameObject go3 in thirdWaveObjects) 
					go3.SetActive (false);

				yield return StartCoroutine (GameController.Instance.NewWave ());

				GameController.Instance.TimeToAdd (ref isDone, timeUntilFifthWave);

				foreach (GameObject go4 in fourthWaveObjects) 
					go4.SetActive (true);

			//	timer = 0;
				isThirdWave = false;
				isFourthWave = true;




			}
			if (isDone && isFourthWave) {

				foreach (GameObject go4 in fourthWaveObjects) 
					go4.SetActive (false);

				yield return StartCoroutine (GameController.Instance.NewWave ());

				GameController.Instance.TimeToAdd (ref isDone, timeUntilSixthWave);

				foreach (GameObject go5 in fifthWaveObjects) 
					go5.SetActive (true);

		
				isFourthWave = false;
				isFifthWave = true;



			}
			if (isDone && isFifthWave) {

				foreach (GameObject go5 in fifthWaveObjects) 
					go5.SetActive (false);

				yield return StartCoroutine (GameController.Instance.NewWave ());

				foreach (GameObject go6 in sixWaveObjects) 
					go6.SetActive (true);


				isFifthWave = false;
				isSixWave = true;

					TimerOn = false;


			}



	}
		
	}
}
