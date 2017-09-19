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


	public int currentMoleIndex;

	public GameObject deadCloudActionGO;


	public GameObject currentMole = null;

	void Start () {
		cam = Camera.main;
		StartCoroutine (UpdateGame ());
	}




	IEnumerator UpdateGame(){
		
		yield return new WaitForEndOfFrame();
		currentMole = WackGameManager.Instance.activeMoles [0];
	
			while (true) {

				Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);

				RaycastHit hit;

				if (Physics.SphereCast (ray, 0.2f, out hit, 500, 1 << 13)) {

					if (hit.transform.CompareTag ("Ground")) {
						yield return null;
					}
					
					if (hit.transform.CompareTag ("Wacked")) {

						Debug.Log ("is Wacked");

						//hit.collider.enabled = false;

						GameObject temp = Instantiate(deadCloudActionGO, hit.point, Quaternion.LookRotation(cam.transform.position - hit.point, Vector3.up));
						Destroy(temp, 5);

						AudioSource tempAS = AudioManager.Instance.GetAudioSourceReferance (AudioManager.AudioReferanceType._DIRECT, "Pop");
						tempAS.transform.position = hit.point;
						tempAS.Play ();

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

				

					
			

				currentMoleIndex = GetComparedRandomMoleIndex (currentMole);

				while(WackGameManager.Instance.activeMoles [currentMoleIndex].GetComponentInChildren<Animator> ().GetCurrentAnimatorStateInfo (0).IsTag ("PopUp")) {
					currentMole = WackGameManager.Instance.activeMoles [Random.Range (0, WackGameManager.Instance.activeMoles.Count)];
					currentMoleIndex = GetComparedRandomMoleIndex (currentMole);
					yield return null;
				}
					currentMole = WackGameManager.Instance.activeMoles [currentMoleIndex];
					

					yield return StartCoroutine (TurnOnMole (currentMole));
					timer = 0;

					//yield return null;


				}
			yield return null;

			}
		
	}
		
	//make this turn off all for a bit
	public void TurnOffAllMoles(){
		
		Debug.Log ("Turn off is being called");
		StopAllCoroutines ();

		foreach (GameObject mole in WackGameManager.Instance.activeMoles) {

			Animator an = mole.GetComponentInChildren<Animator> ();
			mole.GetComponentInChildren<CapsuleCollider> ().enabled = false;

		//	an.SetTrigger (isIdleHash);
			an.SetBool (isPopupHash, false);
	
		}
		float timeOff = GameController.Instance.GetNewWaveTime;
		StartCoroutine (TurnOffAll(timeOff));
		

	
	}

	public IEnumerator TurnOffAll(float offTime){
		

		yield return new WaitForSeconds (offTime);
			StartCoroutine (UpdateGame());

		yield return new WaitForEndOfFrame ();
		foreach (GameObject mole in WackGameManager.Instance.activeMoles) {
			Animator an = mole.GetComponentInChildren<Animator> ();
			mole.GetComponentInChildren<CapsuleCollider> ().enabled = false;

			//an.SetTrigger (isIdleHash);
			an.SetBool (isPopupHash, false);
		
		}
			
		}


	IEnumerator TurnOnMole(GameObject mole){
		//mole.GetComponent<Collider> ().enabled = true;
		mole.GetComponentInChildren<CapsuleCollider> ().enabled = true;

		Animator an = mole.GetComponentInChildren<Animator> ();

		//an.SetTrigger (isIdleHash);
		an.SetBool(isPopupHash, true);


		yield return null;


	
	}
	IEnumerator TurnOffMole(GameObject mole){


		Animator an = mole.GetComponentInChildren<Animator> ();
		mole.GetComponentInChildren<CapsuleCollider> ().enabled = false;

		an.SetBool (isPopupHash, false);
		//an.SetTrigger (isIdleHash);


		yield return null;



	}

	IEnumerator HitMole (GameObject mole){


		mole.GetComponentInChildren<CapsuleCollider> ().enabled = false;
		Animator an = mole.GetComponentInChildren<Animator> ();

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
	/// <summary>
	/// Gets the index of the compared random mole.
	/// </summary>
	/// <returns>To avoid calling same mole again (has to have atleast two moles in active list or editor will crash!).</returns>
	/// <param name="gO">currentMoleActive.</param>
	private int GetComparedRandomMoleIndex(GameObject gO){
	
		int randomMole = Random.Range (0, WackGameManager.Instance.activeMoles.Count);

		while (gO.GetInstanceID () == WackGameManager.Instance.activeMoles [randomMole].GetInstanceID ()) {
		

			randomMole = Random.Range (0, WackGameManager.Instance.activeMoles.Count);
		

		}
		return randomMole;

	}


		
	}

