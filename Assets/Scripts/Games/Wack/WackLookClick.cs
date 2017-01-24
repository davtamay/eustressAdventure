using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WackLookClick : MonoBehaviour {

	private Camera cam;

	public float speedDifficulty;
	public float timer;

	private int isIdleHash = Animator.StringToHash("IsIdle"); 
	private int isPopupHash = Animator.StringToHash("IsPopup");
	private int	isDeadHash = Animator.StringToHash ("IsDead");

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

				if (Physics.Raycast (ray, out hit, 500)) {

					if (hit.transform.CompareTag ("Ground")) {
						yield return null;
					}


					if (hit.transform.CompareTag ("Wacked")) {

						Debug.Log ("is Wacked");

						yield return StartCoroutine (HitMole (hit.transform.gameObject));
						StartCoroutine (TurnOffMole (hit.transform.gameObject));
				
						yield return new WaitForSeconds (0.5f);



					}
				} else {
					yield return null;
				} 


				timer += Time.deltaTime;

				if (timer > speedDifficulty) {

				//	Debug.Log ("timer is called");

					timer = 0;


					int randomMole = Random.Range (0, WackGameManager.Instance.activeMoles.Count);
						

				while (currentMole.GetInstanceID () == WackGameManager.Instance.activeMoles [randomMole].GetInstanceID ()) {
		//			Debug.Log (randomMole);
					randomMole = Random.Range (0, WackGameManager.Instance.activeMoles.Count);
					//currentMole = WackGameManager.Instance.activeMoles [randomMole];
				//	yield return null;
				}
					

					currentMole = WackGameManager.Instance.activeMoles [randomMole];

					yield return StartCoroutine (TurnOnMole (WackGameManager.Instance.activeMoles[randomMole]));

					StartCoroutine (TurnOffMole (WackGameManager.Instance.activeMoles[randomMole]));

					yield return null;


				}
			yield return null;

			}
		
	}
		
	//make this turn off all for a bit
	public void TurnOffAllMoles(float timeOff){

		Debug.Log ("Turn off is being called");
		StopAllCoroutines ();

		foreach (GameObject mole in WackGameManager.Instance.activeMoles) {

			Animator an = mole.GetComponent<Animator> ();

			an.SetBool (isPopupHash, false);
			an.SetBool (isIdleHash, true);
		}
		StartCoroutine (TurnOffAll(timeOff));
		

	
	}

	public IEnumerator TurnOffAll(float offTime){
	

		yield return new WaitForSeconds (offTime);
			StartCoroutine (UpdateGame());
			
			
		}


	IEnumerator TurnOnMole(GameObject mole){

		mole.GetComponent<Collider> ().enabled = true;

		Animator an = mole.GetComponent<Animator> ();

		an.SetBool (isDeadHash, false);
		an.SetBool (isIdleHash, false);
		an.SetBool(isPopupHash, true);

		yield return null;


	
	}
	IEnumerator TurnOffMole(GameObject mole){


		Animator an = mole.GetComponent<Animator> ();

		an.SetBool (isDeadHash, false);
		an.SetBool (isIdleHash, true);
		an.SetBool (isPopupHash, false);


		yield return null;



	}

	IEnumerator HitMole (GameObject mole){

		mole.GetComponent<Collider> ().enabled = false;
		
		Animator an = mole.GetComponent<Animator> ();

		an.SetBool (isIdleHash, false);
		an.SetBool (isPopupHash, false);
		an.SetBool (isDeadHash, true);


		string moleType = mole.GetComponent<MoleType> ().moleDescription;

		if(string.Equals ("Mole", moleType, System.StringComparison.CurrentCultureIgnoreCase))
			PlayerManager.Instance.points = 1;
		else if (string.Equals ("Cloud", moleType, System.StringComparison.CurrentCultureIgnoreCase))
			PlayerManager.Instance.points = 3;
		else if (string.Equals ("Mole", moleType, System.StringComparison.CurrentCultureIgnoreCase))
			PlayerManager.Instance.points = 5;

	
		yield return null;


	}


		
	}

