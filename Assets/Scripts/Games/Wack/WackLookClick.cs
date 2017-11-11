using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WackLookClick : MonoBehaviour {

	private Camera cam;

	public float speedDifficulty = 1f;
	private float timer;

	//private int isIdleHash = Animator.StringToHash("IsIdle"); 
	private int isPopUpHash = Animator.StringToHash("IsPopUp");
	private int	isDeadHash = Animator.StringToHash ("IsDead");
	private int	isEatingHash = Animator.StringToHash ("IsEating");


	public int currentMoleIndex;

	public GameObject deadCloudActionGO;


	public GameObject currentMole = null;
	public bool isAllowPopUps = true;

	void Start () {
		cam = Camera.main;
		StartCoroutine (UpdateLookRaycast ());

		StartCoroutine (TurnOnController ());
	}




	IEnumerator UpdateLookRaycast(){
		
		yield return new WaitForEndOfFrame();
		currentMole = WackGameManager.Instance.activeMoles [0];
	
			while (true) {

			if (GameController.Instance.IsMenuActive ) {
				yield return null;
					continue;
			
			}
				
				Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);

				RaycastHit hit;

			if (Physics.SphereCast (ray, 0.1f, out hit, 500, 1 << 13) ){
				
					if (hit.collider.CompareTag ("Wacked")) {

					Debug.Log (hit.collider.gameObject.name);
			
						GameObject temp = Instantiate(deadCloudActionGO, hit.point, Quaternion.LookRotation(cam.transform.position - hit.point, Vector3.up));
						Destroy(temp, 5);

						AudioSource tempAS = AudioManager.Instance.GetAudioSourceReferance (AudioManager.AudioReferanceType._DIRECT, "Pop");
						tempAS.transform.position = hit.point;
						tempAS.Play ();

						//hit.collider.enabled = false;

						yield return StartCoroutine (TurnOffMole (hit.transform.gameObject, true));
						

					}
					
				/*	if (hit.transform.CompareTag ("PowerUp")) {

					hit.transform.GetComponent<PowerUpResponse> ().Response();
					//hit.transform.GetComponent<Collider> ().enabled = false;

					}*/

				} 
			yield return null;
			}
	}
		
	public IEnumerator TurnOnController(){

		while(true){
			
			yield return null;

			if (GameController.Instance.IsMenuActive || GameController.Instance.IsStartMenuActive) 
				continue;

			if (!isAllowPopUps) 
				continue;
			


			timer += Time.deltaTime;
			//yield return new WaitForSeconds (speedDifficulty);
			if (timer > speedDifficulty) {

				currentMoleIndex = GetComparedRandomMoleIndex (currentMole);
				//while(WackGameManager.Instance.activeMoles [currentMoleIndex].GetComponentInChildren<Animator> ().GetBool(isPopUpHash)){
					while(!WackGameManager.Instance.activeMoles [currentMoleIndex].GetComponentInChildren<Animator> ().GetCurrentAnimatorStateInfo (0).IsName("Idle")){
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
	public void TurnOffAllMolesForWaveChange(){

		isAllowPopUps = false;

		foreach (GameObject mole in WackGameManager.Instance.activeMoles) 
			StartCoroutine (TurnOffMole (mole));
		
		float timeOff = GameController.Instance.GetNewWaveTime;
		StartCoroutine (TurnOnAllowPopUps (timeOff));//TurnOffAll(timeOff));

	}
	IEnumerator TurnOnAllowPopUps(float tOff){
		yield return new WaitForSeconds (tOff);
		isAllowPopUps = true;

	}

	public IEnumerator TurnOffAll(float offTime){
		
		isAllowPopUps = false;


		foreach (GameObject mole in WackGameManager.Instance.activeMoles) {
			StartCoroutine (TurnOffMole (mole));
		}
	
		//yield return null;
		yield return new WaitForSeconds (offTime);

			isAllowPopUps = true;
			
		}

	IEnumerator TurnOffMole (GameObject mole, bool hit = false){
		
		mole.GetComponentInChildren<CapsuleCollider> ().enabled = false;

		Animator an = mole.GetComponentInChildren<Animator> ();

		an.SetTrigger (isDeadHash);

		an.ResetTrigger (isPopUpHash);

		if(an.HasParameter (isEatingHash))
		an.ResetTrigger (isEatingHash);




		if(hit)
			PlayerManager.Instance.points = mole.GetComponent<WackHitPoints> ().hitPoints;


		yield return null;
	
		//mole.GetComponent<WackSpawner> ().StopAllCoroutines ();
	}
	IEnumerator TurnOnMole(GameObject mole){

		mole.GetComponentInChildren<CapsuleCollider> ().enabled = true;

		Animator an = mole.GetComponentInChildren<Animator> ();

		if(an.HasParameter (isEatingHash))
			an.ResetTrigger (isEatingHash);
		

		an.ResetTrigger (isDeadHash);
		an.SetTrigger (isPopUpHash);


		if (mole.GetComponent<WackSpawner> () != null)
		mole.GetComponent<WackSpawner> ().SetRandomPos ();
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

