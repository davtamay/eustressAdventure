
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour {
	//new
	private static PlayerManager instance;
	public static PlayerManager Instance {
		get{ return instance; }
	
	}


	private string curSceneName;

	void Awake (){

		//new
		if (instance != null) {
			Debug.LogError ("There is two instances off PlayerManager");
			return;
		} else {
			instance = this;
		}

		healthColor.a = 0.0f; 

		health = 0;
		points = 0;

		healthColorIndicator = GameObject.FindWithTag ("UIColor").GetComponent<Image>();
		curSceneName = SceneController.Instance.GetCurrentSceneName ();

	}

	public Color hurtColor;
	public Color addHealthColor;

	public Color healthColor;
	public Color armorColor;
	public int armorTime;
	public Text coinText;


	public bool isInvulnerable;



	private Image healthColorIndicator;
	private int _health;

	public int health{
		get{ return _health;}


		set{ if (value + _health < _health) {
				StartCoroutine (HealthReduceColor (hurtColor));
				healthColor.a += 0.1f;
				if (healthColor.a >= .91f) {
					HighScoreManager.Instance.CheckHighScore (curSceneName, _points);
					GameController.Instance.Paused = true;
					GameController.Instance.isGameOver = true;

		
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

		set{coinText.text = ":";
			_points += value;
			coinText.text += _points.ToString();  }

	}
		


	void Start(){

		//healthColorIndicator = GameObject.FindWithTag ("UIColor").GetComponent<Image>();


		healthColorIndicator.color = healthColor;

	
	}
	public void AddArmor (){


		isInvulnerable = true;
		healthColorIndicator.color = armorColor;
		StartCoroutine (AddArmorTime());


	}

	IEnumerator AddArmorTime(){

		yield return new WaitForSeconds (armorTime);
		isInvulnerable = false;
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
}
