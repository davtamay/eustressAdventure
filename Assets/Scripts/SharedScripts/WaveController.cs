using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {
	

	[SerializeField]private GameObject[] firstWaveObjects;
	[SerializeField]private GameObject[] secondWaveObjects;
	[SerializeField]private GameObject[] thirdWaveObjects;

	[SerializeField] float timeUntilSecondWave;
	[SerializeField] float timeUntilThirdWave;

	private GameObject newWave;
	private float timeUntilWaveDisapear;

	private float timer;


	void Awake(){
	
		foreach (GameObject go1 in firstWaveObjects) 
			go1.SetActive (false);


		foreach (GameObject go2 in secondWaveObjects) 
			go2.SetActive (false);
	
		foreach (GameObject go3 in thirdWaveObjects) 
			go3.SetActive (false);
	
	
	}


	void Start(){
		foreach (GameObject go1 in firstWaveObjects) 
			go1.SetActive (true);

		newWave = GameObject.FindWithTag ("NewWave");
		newWave.SetActive (false);
		timeUntilWaveDisapear = newWave.GetComponent<NewWave>().timeUntilDisapear;
		StartCoroutine (OnUpdate ());
	}


	IEnumerator OnUpdate(){
	
		bool TimerOn = true;
		bool isFirstWave = true;
		bool isSecondWave = false;


		while (TimerOn) {
			yield return null;
			timer += Time.deltaTime;
		

		//yield return new WaitUntil (() => timer >= timeUntilSecondWave); 

			if (timer >= timeUntilSecondWave && isFirstWave) {
				foreach (GameObject go1 in firstWaveObjects) 
					go1.SetActive (false);

				newWave.SetActive (true);
				yield return new WaitForSeconds (timeUntilWaveDisapear);

				foreach (GameObject go2 in secondWaveObjects) 
					go2.SetActive (true);

				timer = 0;
				isFirstWave = false;
				isSecondWave = true;
			}	
			
		
			if (timer >= timeUntilThirdWave && isSecondWave) {

				foreach (GameObject go2 in firstWaveObjects) 
					go2.SetActive (false);

				newWave.SetActive (true);
				yield return new WaitForSeconds (timeUntilWaveDisapear);

				foreach (GameObject go3 in secondWaveObjects) 
					go3.SetActive (true);

				timer = 0;
				TimerOn = false;

			}



	}
		
	}
}
