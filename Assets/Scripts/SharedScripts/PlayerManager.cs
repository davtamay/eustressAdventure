using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


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
	public Text coinText;
	public bool isInvulnerable;
	private Image healthColorIndicator;

	public bool isCustomSavePosition = false;
	public Vector3 customSavePosition = Vector3.zero;

	[SerializeField]private bool isShakeWhenHit = false;
	[SerializeField]private float shakeTime = 2.0f;
	[SerializeField]private float shakeAmount = 3.0f;
	[SerializeField]private float shakeSpeed = 2.0f;

	private string curSceneName;

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

		//EventManager.Instance.AddListener (EVENT_TYPE.GAME_LOST, OnEvent);
		//EventManager.Instance.AddListener (EVENT_TYPE.HEALTH_ADD, OnEvent);
		//EventManager.Instance.AddListener (EVENT_TYPE.HEALTH_REDUCE, OnEvent);
		EventManager.Instance.AddListener (EVENT_TYPE.SCENE_CHANGING, OnEvent);
		//EventManager.Instance.AddListener (EVENT_TYPE.APPLICATION_QUIT, OnEvent);


		if(GameObject.FindWithTag ("PointText"))
		coinText = GameObject.FindWithTag ("PointText").GetComponent<Text>();

		curSceneName = SceneController.Instance.GetCurrentSceneName ();

		health = 0;
		points = 0;

		if(curSceneName.Contains("Intro"))
			thisTransform.position = DataManager.Instance.LoadPosition ();
		
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

				EventManager.Instance.PostNotification (EVENT_TYPE.HEALTH_REDUCE, this, null);

				if (healthColor.a >= .91f) {

					StartCoroutine (HealthReduceColor (healthColor));
					GameController.Instance.isGameOver = true;
					GameController.Instance.Paused = true;
					AudioManager.Instance.StopAudioPlaying (AudioManager.AudioReferanceType._DIRECT);

					EventManager.Instance.PostNotification (EVENT_TYPE.GAME_LOST, this, null);

				}
					

				
			}else if (value + _health > _health && !(healthColor.a == 0.0f)){
				StartCoroutine(HealthAddColor (addHealthColor));
				healthColor.a -= 0.1f;
				EventManager.Instance.PostNotification (EVENT_TYPE.HEALTH_ADD, this, null);
			}
				
		

				;}
	}

	private int _points;
	public int points {
		get{ return _points; }


		set {

			if (value == 0)
				return;
			
			if (coinText != null) {
				coinText.text = ":";


//				if (value == 0)
//					return;



				if(value <= 2)
				AudioManager.Instance.PlayInterfaceSound ("SmallWin");
					else if(value >= 3 && value <= 6)
						AudioManager.Instance.PlayInterfaceSound ("MediumWin");
						else if(value > 7)
								AudioManager.Instance.PlayInterfaceSound ("BigWin");

				_points += value;
				coinText.text += _points.ToString ();

				EventManager.Instance.PostNotification (EVENT_TYPE.POINTS_ADD, this, value);
			}
		}
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
	public void OnApplicationQuit(){
	
		if(curSceneName.Contains("Intro")){
			DataManager.Instance.SaveStressLevel (UIStressGage.Instance.stress);
			DataManager.Instance.SavePosition (thisTransform.position);
			DataManager.Instance.SaveItemList (PlayerInventory.Instance.playerItemSlotGOList);
		}


	}

	void OnEvent(EVENT_TYPE Event_Type, Component Sender, params object[] Param){

		switch(Event_Type){

		case EVENT_TYPE.SCENE_CHANGING:
		
			if(curSceneName.Contains("Intro")){
				if (!isCustomSavePosition)
					DataManager.Instance.SavePosition (thisTransform.position);
				else {
					DataManager.Instance.SavePosition (customSavePosition);
					isCustomSavePosition = false;

				}
				DataManager.Instance.SaveStressLevel (UIStressGage.Instance.stress);
				DataManager.Instance.SaveItemList (PlayerInventory.Instance.playerItemSlotGOList);

			}
			break;


		

		
		

		}




	}

}
