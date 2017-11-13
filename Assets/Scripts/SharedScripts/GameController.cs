using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject stressMenu;

	private GameObject gameStart;
	private GameObject gameOver;

	public GameObject newWaveGO;
	private float timeUntilNewWave;

	private float timer;
	private	bool isTimerOn;
	[SerializeField] private float timerSpeed = 1.2f;

	private GameObject hitEffect;
	//private List<GameObject> hitEffects;


	public static GameController Instance
	{ get { return instance; } }

	private static GameController instance = null;


	void Awake()
	{

		if (instance) {
			return;
		}
		instance = this; 

		stressMenu = GameObject.FindWithTag ("StressMenu");


	}

	void Start(){
		

		gameStart = GameObject.FindWithTag ("GameStart");
		gameOver = GameObject.FindWithTag ("GameOver");

	/*	if (GameObject.FindWithTag ("NewWave")) {
			//newWaveGO = null;
			newWaveGO = GameObject.FindWithTag ("NewWave");
			timeUntilNewWave = newWaveGO.transform.GetComponent<NewWaveTransition> ().timeUntilDisapear;
			newWaveGO.gameObject.SetActive (false);
		}*/

		if (hitEffect == null) {
		
			hitEffect = Resources.Load ("HitEffect") as GameObject;//GameObject.FindWithTag ("HitEffect");
			//hitEffect.SetActive (false);
		
		}
	
		if (gameOver != null) {

			gameOver.SetActive (false);
		}

		Paused = true;
	}

	private bool isMenuPause;
	private bool paused;
	public bool Paused {

		get { return paused; }

		set {

			if (paused == true && IsMenuActive)
				isMenuPause = true;
			
			paused = value;
		
			if (paused) {

				EventManager.Instance.PostNotification (EVENT_TYPE.GAME_PAUSED, this, null);
				AudioManager.Instance.PauseAmbientAS();
				Time.timeScale = 0;


			}else {

				if (isMenuPause) {
					isMenuPause = false;
					return;
				}

					EventManager.Instance.PostNotification (EVENT_TYPE.GAME_UNPAUSED, this, null);
					AudioManager.Instance.UnPauseAmbientAS ();
					Time.timeScale = 1;

				}

			}
		}


	private bool isMenuActive;
	public bool IsMenuActive{

		get{ return stressMenu.activeInHierarchy; }
		
	}


	/*public void SetMenuActive(){
	
		stressMenu.SetActive (true);
	}*/

	//private bool isStartMenuActive;
	public bool IsStartMenuActive{
		
		get{ 
			if(gameStart == null)
				gameStart = GameObject.FindWithTag ("GameStart");

			return gameStart.activeInHierarchy; }

	}
	public void StartGame (){
	
		EventManager.Instance.PostNotification (EVENT_TYPE.GAME_START, this, null);
		gameStart.SetActive (false);
		Paused = false;
	
	}
	/// <summary>
	/// Check or AddTime to GameTimer
	/// </summary>
	/// <returns>Current Time in string 00:00</returns>
	/// <param name="isDone">Add Ref bool local variable to have reference to current timer completion.</param>
	/// <param name="time">Add time to current game timer could be left out if only checking isDone.</param>
	public string TimeToAdd(ref bool isDone, float time = 0f){

		timer += time;

		if (time > 0f) {

			isTimerOn = true;
			StopAllCoroutines ();
			StartCoroutine (StartTimer ());
		}

		string minutes = Mathf.Floor(timer /60).ToString("00");
		string seconds = Mathf.Floor (timer % 60).ToString ("00");

		if (timer < 0f) {
			isDone = true;
			isTimerOn = false;
		}

		return minutes + ":" + seconds;
	
	
	}
	public float GetCurrentTime(){

		return timer;
	}

	float timerTimeScale = 1;
	public void StopTimer(){


		timerTimeScale = 0;

	}
	public void ResumeTimer(){


		timerTimeScale = 1;
	}

	private IEnumerator StartTimer(){
		

		while (isTimerOn) {

			timer -= Time.deltaTime * timerSpeed * timerTimeScale;

			yield return null;
		}

		yield break;
	
	
	}
	bool isWaveImageOn;
	public IEnumerator NewWave(){
		isWaveImageOn = true;
		newWaveGO.SetActive (true);
		yield return new WaitForSeconds (timeUntilNewWave);
		isWaveImageOn = false;
	}
	public bool GetWaveActiveState(){

		if (isWaveImageOn)
			return true;

		return false;
	
	
	}
	public float GetNewWaveTime{
	
		get{return timeUntilNewWave;}	
	}


	                       
	public bool isGameOver{
		set{ 
			DataManager.Instance.CheckHighScore (SceneController.Instance.GetCurrentSceneName(),PlayerManager.Instance.points);
			gameOver.SetActive (value);
			EventManager.Instance.PostNotification (EVENT_TYPE.GAME_LOST, this,null);
		
		}

	}

	public IEnumerator HitEffectLocation(Vector3 hitLoc){

		Instantiate (hitEffect, hitLoc, Quaternion.identity);
	
		yield return null;


	
	
	}


		


}
