using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WackLookClick : MonoBehaviour {

	private Camera cam;

	public float speedDifficulty;
	public float timer;

	private int isIdleHash = Animator.StringToHash("IsIdle"); 
	private int isPopupHash = Animator.StringToHash("IsPopup");
	private int	isDeadHash = Animator.StringToHash ("IsDead");

	[SerializeField] private List<GameObject> curPopUpMoles = new List<GameObject>();

	public int currentMoleIndex;


	public GameObject currentMole = null;

	void Start () {
		cam = Camera.main;
	//	currentMole = WackGameManager.Instance.activeMoles [0];
		StartCoroutine (UpdateGame ());
	}




	IEnumerator UpdateGame(){
		
		yield return new WaitForEndOfFrame();
		currentMole = WackGameManager.Instance.activeMoles [0];
	
			while (true) {

			//if (WackGameManager.Instance.activeMoles.Count == 0)
				//continue;
				
				Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);

				RaycastHit hit;

				if (Physics.SphereCast (ray, 0.2f, out hit, 500)) {

					if (hit.transform.CompareTag ("Ground")) {
						yield return null;
					}


					if (hit.transform.CompareTag ("Wacked")) {

						Debug.Log ("is Wacked");

						yield return StartCoroutine (HitMole (hit.transform.gameObject));
					//	StartCoroutine (TurnOffMole (hit.transform.gameObject));
				
						



					}
				} else {
					yield return null;
				} 


				timer += Time.deltaTime;

				if (timer > speedDifficulty) {

				//	Debug.Log ("timer is called");

					timer = 0;
				/*

					int randomMole = Random.Range (0, WackGameManager.Instance.activeMoles.Count);
						


				while (currentMole.GetInstanceID () == WackGameManager.Instance.activeMoles [randomMole].GetInstanceID ()) {
		//			Debug.Log (randomMole);
					!currentMole.GetComponent<MoleType> ().isIdle;
					randomMole = Random.Range (0, WackGameManager.Instance.activeMoles.Count);
					//currentMole = WackGameManager.Instance.activeMoles [randomMole];
					yield return null;
				}
					*/

					currentMoleIndex = GetComparedRandomMoleIndex (currentMole);
					currentMole = WackGameManager.Instance.activeMoles [currentMoleIndex];
					

		/*			while (!currentMole.GetComponent<MoleType>().isIdle) {
					
					currentMoleIndex = GetComparedRandomMoleIndex (currentMole);
					currentMole = WackGameManager.Instance.activeMoles [currentMoleIndex];
					yield return null;

					}*/
			//	print (currentMoleIndex);
				//	currentMole.GetComponent<MoleType> ().isIdle = false;

					yield return StartCoroutine (TurnOnMole (currentMole));

				//	StartCoroutine (TurnOffMole (currentMole));

					//currentMole.GetComponent<MoleType> ().isIdle = true;

					yield return null;


				}
			yield return null;

			}
		
	}
		
	//make this turn off all for a bit
	public void TurnOffAllMoles(){

		Debug.Log ("Turn off is being called");
		StopAllCoroutines ();

		foreach (GameObject mole in WackGameManager.Instance.activeMoles) {

			Animator an = mole.GetComponent<Animator> ();
		//	an.speed = 10f;
			an.SetTrigger (isIdleHash);
			an.SetBool (isPopupHash, false);
		//	an.SetBool (isIdleHash, true);
		//	an.speed = 1f;
			//an.SetTrigger ("IsTuenedOff");
		//	an.SetTrigger (isIdleHash);
		//	an.SetTrigger ("IsDead");
			//an.SetBool (isDeadHash, true);
		}
		float timeOff = GameController.Instance.GetNewWaveTime;
		StartCoroutine (TurnOffAll(timeOff));
		

	
	}

	public IEnumerator TurnOffAll(float offTime){
		//float Timer = offTime;

	//	while(Timer > 0){

		//	Timer -= Time.deltaTime;
		//	yield return null;



		

		yield return new WaitForSeconds (offTime);
			StartCoroutine (UpdateGame());

		yield return new WaitForEndOfFrame ();
		foreach (GameObject mole in WackGameManager.Instance.activeMoles) {
			Animator an = mole.GetComponent<Animator> ();

			an.SetTrigger (isIdleHash);
		
		
		}
		//StartCoroutine (UpdateGame());
	//	}
			
		}


	IEnumerator TurnOnMole(GameObject mole){

		mole.GetComponent<Collider> ().enabled = true;

		Animator an = mole.GetComponent<Animator> ();

		//an.SetBool (isDeadHash, false);
		//an.SetBool (isIdleHash, true);

		an.SetTrigger (isIdleHash);
		an.SetBool(isPopupHash, true);


		yield return null;
		//an.SetBool(isPopupHash, false);

	
	}
	IEnumerator TurnOffMole(GameObject mole){


		Animator an = mole.GetComponent<Animator> ();

	//	an.SetBool (isDeadHash, false);
	//	an.SetBool (isIdleHash, true);

		an.SetBool (isPopupHash, false);
		an.SetTrigger (isIdleHash);

		//yield return new WaitUntil (() => an.IsInTransition (0));
		//mole.GetComponent<MoleType> ().isIdle = true;
		yield return null;



	}

	IEnumerator HitMole (GameObject mole){

		mole.GetComponent<Collider> ().enabled = false;
		
		Animator an = mole.GetComponent<Animator> ();

		an.SetTrigger (isDeadHash);
		//an.SetBool (isIdleHash, false);
		an.SetBool (isPopupHash, false);
		//an.SetBool (isDeadHash, true);



		string moleType = mole.GetComponent<MoleType> ().moleDescription;

		if(string.Equals ("Mole", moleType, System.StringComparison.CurrentCultureIgnoreCase))
			PlayerManager.Instance.points = 1;
		else if (string.Equals ("Cloud", moleType, System.StringComparison.CurrentCultureIgnoreCase))
			PlayerManager.Instance.points = 3;
		else if (string.Equals ("Mole", moleType, System.StringComparison.CurrentCultureIgnoreCase))
			PlayerManager.Instance.points = 5;

	
		yield return null;


	}
	private int GetComparedRandomMoleIndex(GameObject gO){
	
		int randomMole = Random.Range (0, WackGameManager.Instance.activeMoles.Count);

		while (gO.GetInstanceID () == WackGameManager.Instance.activeMoles [randomMole].GetInstanceID ()) {
		
			//!currentMole.GetComponent<MoleType> ().isIdle;
			randomMole = Random.Range (0, WackGameManager.Instance.activeMoles.Count);
		

		}
		return randomMole;

	}


		
	}

