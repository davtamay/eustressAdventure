using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WackLookClick : MonoBehaviour {

	private Camera cam;

	public static float speedDifficulty = 1f;
	private float timer;

	private int isIdleHash = Animator.StringToHash("IsIdle"); 
	private int isPopupHash = Animator.StringToHash("IsPopup");
	private int	isDeadHash = Animator.StringToHash ("IsDead");

	//[SerializeField] private List<GameObject> curPopUpMoles = new List<GameObject>();

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

				Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);

				RaycastHit hit;

				if (Physics.SphereCast (ray, 0.2f, out hit, 500)) {

					if (hit.transform.CompareTag ("Ground")) {
						yield return null;
					}


					if (hit.transform.CompareTag ("Wacked")) {

						Debug.Log ("is Wacked");

						yield return StartCoroutine (HitMole (hit.transform.gameObject));
						



					}
					
					if (hit.transform.CompareTag ("PowerUp")) {

					hit.transform.GetComponent<PowerUpResponse> ().Response();
					//hit.transform.GetComponent<Collider> ().enabled = false;

					}


				} else {
					yield return null;
				} 


				timer += Time.deltaTime;

				if (timer > speedDifficulty) {

			

					timer = 0;
			

					currentMoleIndex = GetComparedRandomMoleIndex (currentMole);
					currentMole = WackGameManager.Instance.activeMoles [currentMoleIndex];
					

					yield return StartCoroutine (TurnOnMole (currentMole));


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
		
			an.SetTrigger (isIdleHash);
			an.SetBool (isPopupHash, false);
	;
		}
		float timeOff = GameController.Instance.GetNewWaveTime;
		StartCoroutine (TurnOffAll(timeOff));
		

	
	}

	public IEnumerator TurnOffAll(float offTime){
		

		yield return new WaitForSeconds (offTime);
			StartCoroutine (UpdateGame());

		yield return new WaitForEndOfFrame ();
		foreach (GameObject mole in WackGameManager.Instance.activeMoles) {
			Animator an = mole.GetComponent<Animator> ();

			an.SetTrigger (isIdleHash);
		
		
		}
			
		}


	IEnumerator TurnOnMole(GameObject mole){

		mole.GetComponent<Collider> ().enabled = true;

		Animator an = mole.GetComponent<Animator> ();

		an.SetTrigger (isIdleHash);
		an.SetBool(isPopupHash, true);


		yield return null;


	
	}
	IEnumerator TurnOffMole(GameObject mole){


		Animator an = mole.GetComponent<Animator> ();

		an.SetBool (isPopupHash, false);
		an.SetTrigger (isIdleHash);


		yield return null;



	}

	IEnumerator HitMole (GameObject mole){

		mole.GetComponent<Collider> ().enabled = false;
		
		Animator an = mole.GetComponent<Animator> ();

		an.SetTrigger (isDeadHash);
		an.SetBool (isPopupHash, false);
	



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
		

			randomMole = Random.Range (0, WackGameManager.Instance.activeMoles.Count);
		

		}
		return randomMole;

	}


		
	}

