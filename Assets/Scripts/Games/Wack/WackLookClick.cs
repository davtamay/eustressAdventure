using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WackLookClick : MonoBehaviour {

	private Camera cam;

	public static float speedDifficulty = 1f;
	private float timer;

	//private int isIdleHash = Animator.StringToHash("IsIdle"); 
	private int isPopupHash = Animator.StringToHash("IsPopUp");
	private int	isDeadHash = Animator.StringToHash ("IsDead");


	public int currentMoleIndex;

	public GameObject deadCloudActionGO;


	public GameObject currentMole = null;

	void Start () {
		cam = Camera.main;
		StartCoroutine (UpdateLookRaycast ());

		StartCoroutine (TurnOnController ());
	}




	IEnumerator UpdateLookRaycast(){
		
		yield return new WaitForEndOfFrame();
		currentMole = WackGameManager.Instance.activeMoles [0];
	
			while (true) {

				Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);

				RaycastHit hit;

			//	if(Physics.Raycast(ray,out hit, 600f)){
			if (Physics.SphereCast (ray, 0.1f, out hit, 500, 1 << 13) ){

				Debug.Log ("1ST CALLS BEFORE COMPARETAG");
				Debug.Log (hit.collider.gameObject.name);
					if (hit.collider.CompareTag ("Wacked")) {

					Debug.Log (hit.collider.gameObject.name);
				
					Debug.Log ("2ND CALLS BEFORE COMPARETAG");
					//	hit.collider.enabled = false;

						GameObject temp = Instantiate(deadCloudActionGO, hit.point, Quaternion.LookRotation(cam.transform.position - hit.point, Vector3.up));
						Destroy(temp, 5);

						AudioSource tempAS = AudioManager.Instance.GetAudioSourceReferance (AudioManager.AudioReferanceType._DIRECT, "Pop");
						tempAS.transform.position = hit.point;
						tempAS.Play ();

					//	yield return 
						StartCoroutine (HitMole (hit.transform.gameObject));
						

					}
					
				/*	if (hit.transform.CompareTag ("PowerUp")) {

					hit.transform.GetComponent<PowerUpResponse> ().Response();
					//hit.transform.GetComponent<Collider> ().enabled = false;

					}*/


				} 


			yield return null;

			}
		
	}
		
	private bool isAllowPopUps = true;
	public IEnumerator TurnOnController(){

		while(true ){

			if (!isAllowPopUps) {
				yield return null;
				continue;
			
			}

			timer += Time.deltaTime;

			if (timer > speedDifficulty) {

				currentMoleIndex = GetComparedRandomMoleIndex (currentMole);
				while(WackGameManager.Instance.activeMoles [currentMoleIndex].GetComponentInChildren<Animator> ().GetBool(isPopupHash)){
					//while(WackGameManager.Instance.activeMoles [currentMoleIndex].GetComponentInChildren<Animator> ().GetCurrentAnimatorStateInfo (0).IsName("Idle")){
					//while(WackGameManager.Instance.activeMoles [currentMoleIndex].GetComponentInChildren<Animator> ().GetCurrentAnimatorStateInfo (0).IsTag ("PopUp")) {
					currentMole = WackGameManager.Instance.activeMoles [Random.Range (0, WackGameManager.Instance.activeMoles.Count)];
					currentMoleIndex = GetComparedRandomMoleIndex (currentMole);
					yield return null;
				}
				currentMole = WackGameManager.Instance.activeMoles [currentMoleIndex];


				yield return StartCoroutine (TurnOnMole (currentMole));
				timer = 0;

				yield return null;


			}
	
		}
	
	
	}
	//make this turn off all for a bit
	public void TurnOffAllMoles(){

		isAllowPopUps = false;

		Debug.Log ("Turn off is being called");

		Animator an = null;;
		foreach (GameObject mole in WackGameManager.Instance.activeMoles) {

			an = mole.GetComponentInChildren<Animator> ();
			mole.GetComponentInChildren<CapsuleCollider> ().enabled = false;
			an.ResetTrigger (isPopupHash);
			an.SetTrigger (isDeadHash);
			//an.SetTrigger (isPopupHash);
	
		}

		float timeOff = GameController.Instance.GetNewWaveTime;
		StartCoroutine (TurnOffAll(timeOff));

	
	}

	public IEnumerator TurnOffAll(float offTime){
		
	
		yield return new WaitForSeconds (offTime);
			StartCoroutine (UpdateLookRaycast());

		yield return new WaitForEndOfFrame ();
		foreach (GameObject mole in WackGameManager.Instance.activeMoles) {
			Animator an = mole.GetComponentInChildren<Animator> ();
			mole.GetComponentInChildren<CapsuleCollider> ().enabled = false;

			an.ResetTrigger (isPopupHash);
			an.SetTrigger (isDeadHash);
		//	an.SetBool (isPopupHash, false);
		
		}
			isAllowPopUps = true;
			
		}


	IEnumerator TurnOnMole(GameObject mole){
		
		mole.GetComponentInChildren<CapsuleCollider> ().enabled = true;

		Animator an = mole.GetComponentInChildren<Animator> ();


		//an.SetBool(isPopupHash, true);
		an.SetTrigger (isPopupHash);

		yield return null;


	
	}
	IEnumerator TurnOffMole(GameObject mole){


		Animator an = mole.GetComponentInChildren<Animator> ();
		mole.GetComponentInChildren<CapsuleCollider> ().enabled = false;
	//	an.SetTrigger(isDeadHash);
		//an.SetBool (isPopupHash, false);



		yield return null;



	}

	IEnumerator HitMole (GameObject mole){


		mole.GetComponentInChildren<CapsuleCollider> ().enabled = false;
		Animator an = mole.GetComponentInChildren<Animator> ();

		an.SetTrigger (isDeadHash);
	

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

