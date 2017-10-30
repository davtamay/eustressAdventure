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
	//public Color armorColor;
	public int armorTime;
	public bool isArmorOn = false;
	public Text coinText;
	public bool isInvulnerable;
	private Image healthColorIndicator;

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



		//new
		if (instance != null) {
			Debug.LogError ("There is two instances off PlayerManager");
			return;
		} else {
			instance = this;
		}

		if(SceneController.Instance != null)
			curSceneName = SceneController.Instance.GetCurrentSceneName ();
		
		thisTransform = transform;

		healthColor.a = 0.0f; 

		health = 0;
		points = 0;


	}
	void Start(){

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
				StartCoroutine (HealthReduceColor (hurtColor));

				if(isShakeWhenHit)
				StartCoroutine (Shake ());
				
				healthColor.a += 0.1f;

				if (isUseAdditionalHealthIndicator) {
					Color tempHealthColor;
					tempHealthColor = Color.white;
					tempHealthColor.a = healthColor.a;
					additionalHealthIndicator.SetColor (propertyID, tempHealthColor);

				}

				if (healthColor.a >= .91f) {
					StartCoroutine (HealthReduceColor (healthColor));
					DataManager.Instance.CheckHighScore (curSceneName, _points);
					GameController.Instance.isGameOver = true;
					GameController.Instance.Paused = true;
					AudioManager.Instance.StopAudioPlaying (AudioManager.AudioReferanceType._DIRECT);

				};
			}
				if (value + _health > _health && !(healthColor.a == 0.0f)){
					StartCoroutine(HealthAddColor (addHealthColor));
					healthColor.a -= 0.1f;
				}
				;}
	}

	private int _points;
	public int points{
		get{return _points;}

		//5/13/17

		set{
			if (coinText != null) {
				coinText.text = ":";
				_points += value;
				coinText.text += _points.ToString ();
			}
		}
	}
		




	public static float armorTimer = 0f;
	public void AddArmor (){

		StopAllCoroutines ();
		isArmorOn = true;
		isInvulnerable = true;
		armorGO.SetActive (true);
	//	healthColorIndicator.color = armorColor;
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
	/*public IEnumerator ShowUISlots(){

		UISlots.SetActive(true);
		yield return new WaitForSeconds (4f);
		UISlots.SetActive(false);


	}*/
}
