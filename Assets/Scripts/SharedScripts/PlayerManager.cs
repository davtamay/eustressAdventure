using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;


public class PlayerManager : MonoBehaviour {


	private Transform thisTransform;
	private GameObject UISlots;
	private int curSlot;

	public Color hurtColor;
	public Color addHealthColor;
	public Color healthColor;
	[SerializeField] private bool isUseAdditionalHealthIndicator;
	[SerializeField] private Material additionalHealthIndicator;
	private int propertyID = Shader.PropertyToID ("_TintColor");
	[SerializeField] private GameObject armorGO;
	public int armorTime;
	public bool isArmorOn = false;


	public bool isInvulnerable;
	private Image healthColorIndicator;

	public bool isCustomSavePosition = false;
	public Vector3 customSavePosition = Vector3.zero;
	public Vector3 homePosition = new Vector3(0,2,0);

	[Header("Events")]
	[SerializeField]private UnityEvent onPointAdd;
	[SerializeField]private UnityEvent onHealthAdd;
	[SerializeField]private UnityEvent onHealthLoss;
	[SerializeField]private UnityEvent onDead;

	[Header("Hit Effect")]
	[SerializeField]private bool isShakeWhenHit = false;
	[SerializeField]private float shakeTime = 2.0f;
	[SerializeField]private float shakeAmount = 3.0f;
	[SerializeField]private float shakeSpeed = 2.0f;

	private string curSceneName;

	[Header("References")]
	[SerializeField]private DataManager DATA_MANAGER;
	[SerializeField]private IntVariable playerPoints;

	private static PlayerManager instance;
	public static PlayerManager Instance {
		get{ return instance; }

	}

	void Awake (){

		if (instance != null) {
			Debug.LogError ("There is two instances off PlayerManager");
			return;
		} else {
			instance = this;
		}
			
		thisTransform = transform;

		healthColor.a = 0.0f; 


	}
	void Start(){
		
		curSceneName = SceneController.Instance.GetCurrentSceneName ();

		health = 0;
		points = 0;

		if (curSceneName.Contains ("Intro"))
			thisTransform.position = DATA_MANAGER.LoadPosition ();
		
		if (GameObject.FindWithTag ("UIColor")) {

				healthColorIndicator = GameObject.FindWithTag ("UIColor").GetComponent<Image> ();
				healthColorIndicator.color = healthColor;

			if(armorGO != null)
				armorGO.SetActive (false);

			if (isUseAdditionalHealthIndicator) {
				Color tempHealthColor = Color.white;
				tempHealthColor.a = healthColor.a;
				additionalHealthIndicator.SetColor (propertyID, tempHealthColor);
			
			
			
			}
				

		
		}

	}
		

	private int _health = 0;

	public int health{
		get{ return _health;}


		set{ if (value + _health < _health) {


				if(isShakeWhenHit)
					StartCoroutine (Shake ());

				StartCoroutine (HealthReduceColor (hurtColor));
				healthColor.a += 0.1f;

				if (isUseAdditionalHealthIndicator) {
					Color tempHealthColor;
					tempHealthColor = Color.white;
					tempHealthColor.a = healthColor.a;
					additionalHealthIndicator.SetColor (propertyID, tempHealthColor);

				}
				onHealthLoss.Invoke ();
			

				if (healthColor.a >= .91f) {

					StartCoroutine (HealthReduceColor (healthColor));
					GameController.Instance.isGameOver = true;
					GameController.Instance.Paused = true;
					AudioManager.Instance.StopAudioPlaying (AudioManager.AudioReferanceType._DIRECT);

					onDead.Invoke ();

				}
					

				
			}else if (value + _health > _health && !(healthColor.a == 0.0f)){
				StartCoroutine(HealthAddColor (addHealthColor));
				healthColor.a -= 0.1f;
				onHealthAdd.Invoke ();
				//EventManager.Instance.PostNotification (EVENT_TYPE.HEALTH_ADD, this, null);
			}
				
		

				;}
	}

	private int _points;
	public int points {
		get{ return _points; }


		set {

			if (value == 0)
				return;
			
				if(value <= 2)
				AudioManager.Instance.PlayInterfaceSound ("SmallWin");
					else if(value >= 3 && value <= 6)
						AudioManager.Instance.PlayInterfaceSound ("MediumWin");
						else if(value > 7)
								AudioManager.Instance.PlayInterfaceSound ("BigWin");

				_points += value;
				playerPoints.SetValue (_points);

				onPointAdd.Invoke ();

		}
	}
		

	public void ResetPositionToHome(){

//		isCustomSavePosition = true;
//		customSavePosition = homePosition;
		DATA_MANAGER.SavePosition (homePosition);
	//	LoadScene ("Intro");

	}


	public static float armorTimer = 0f;
	public void AddArmor (){

		StopAllCoroutines ();
		isArmorOn = true;
		isInvulnerable = true;
		armorGO.SetActive (true);
		StartCoroutine (AddArmorTime());

	}

	IEnumerator AddArmorTime(){

		armorTimer += armorTime;

		while (armorTimer > 0) {
		
			armorTimer -= Time.deltaTime;


			if (armorTime < 4) {
				armorGO.SetActive (false);
				yield return new WaitForSeconds (Random.Range(0.5f, 2f));
				armorGO.SetActive (true);
			}
			
				
			yield return null;
		}

		armorGO.SetActive (false);

		isInvulnerable = false;
		isArmorOn = false;
		healthColorIndicator.color = healthColor;

	}
	IEnumerator HealthReduceColor (Color col){

		healthColorIndicator.color = col;

		yield return new WaitForSeconds (1.0f);

		healthColorIndicator.color = healthColor;

	}
	IEnumerator HealthAddColor (Color col){

		healthColorIndicator.color = col;

		yield return new WaitForSeconds (1.0f);

		healthColorIndicator.color = healthColor;

	}

	IEnumerator Shake(){
	
		Vector3 origPosition = thisTransform.localPosition;
		float ElapsedTime = 0f;

		while (ElapsedTime < shakeTime) {
		
			Vector3 RandomPoint = origPosition + Random.insideUnitSphere * shakeAmount;

			thisTransform.localPosition = Vector3.Lerp (thisTransform.localPosition, RandomPoint, Time.deltaTime * shakeSpeed);
		
			yield return null;
			ElapsedTime += Time.deltaTime;
		}

		thisTransform.localPosition = origPosition;
	
	}
	public void PlayDirectAudio(string audioName){

		AudioManager.Instance.PlayDirectSound (audioName, true);
	
	}
	public void PlayAmbianceAudio(string audioName){

		AudioManager.Instance.PlayAmbientSoundAndActivate(audioName, true, false,0, this.transform);

	}

	public void OnSaveState(){

		if (!isCustomSavePosition) {
			if (thisTransform == null)
				DATA_MANAGER.SavePosition (GameObject.FindWithTag ("Player").transform.position);
			else
				DATA_MANAGER.SavePosition (thisTransform.position);
		}else {
			DATA_MANAGER.SavePosition (customSavePosition);
			isCustomSavePosition = false;

		}
		DATA_MANAGER.SaveStressLevel (UIStressGage.Instance.stress);
		//DATA_MANAGER.SaveItemList (PlayerInventory.Instance.playerItemSlotGOList);

	}
	public void OnApplicationQuit(){

		if(curSceneName.Contains("Intro")){
			//DATA_MANAGER.SaveStressLevel (UIStressGage.Instance.stress);
			DATA_MANAGER.SavePosition (thisTransform.position);
			//	DATA_MANAGER.SaveItemList (PlayerInventory.Instance.playerItemSlotGOList);
		}


	}

}
