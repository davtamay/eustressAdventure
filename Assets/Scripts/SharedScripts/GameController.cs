using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	//public GameObject stressMenu;

	private GameObject gameStart;
	//private GameObject gameOver;

	public GameObject newWaveGO;
	private float timeUntilNewWave;

	private float timer;
	private	bool isTimerOn;
	private float timerSpeed = 1.2f;

	private GameObject hitEffect;
	//private List<GameObject> hitEffects;
	[Header("Events")]
	[SerializeField]private UnityEvent onPause;
	[SerializeField]private UnityEvent onUnPause;
	[SerializeField]private UnityEvent onGameStart;
	[SerializeField]private UnityEvent onGameOver;

	[Header("References")]
	[SerializeField]private DataManager DATA_MANAGER;
	[SerializeField]private BoolVariable isCheckForStartUI;
	[SerializeField]private BoolVariable isCheckForStressMenuUI;
	[SerializeField]private AudioManager audioManager;
	public static GameController Instance
	{ get { return instance; } }

	private static GameController instance = null;


	void Awake()
	{

		if (instance) {
			return;
		}
		instance = this; 

		//stressMenu = GameObject.FindWithTag ("StressMenu");

		isCheckForStartUI.SetValue (true);
	}

	void Start(){

		onGameStart.Invoke ();

		if (hitEffect == null) 
			hitEffect = Resources.Load ("HitEffect") as GameObject;

		
		
	

		Paused = true;
	}

	private bool isMenuPause;
	private bool paused;
	public bool Paused {

		get { return paused; }

		set {				
			
			paused = value;
		
			if (paused) {
				
				if(isMenuActive)
					isMenuPause = true;

				onPause.Invoke();
				audioManager.PauseAmbientAS();
				Time.timeScale = 0;


			}else {

				if (IsGameOver)
					return;
		
				if(isMenuPause || isCheckForStartUI.isOn){
					isMenuPause = false;
					return;
				}

					onUnPause.Invoke();
					audioManager.UnPauseAmbientAS ();
					Time.timeScale = 1;

				}

			}
		}


	private bool isMenuActive;
	public bool IsMenuActive{

		get{ return isCheckForStressMenuUI; }
		
	}
		

	//private bool isStartMenuActive;
	public bool IsStartMenuActive{
		
		get{ 

			return isCheckForStartUI;
		}
			

	}
	public void StartGame (){

		Paused = false;
	
	}
	/// <summary>
	/// Check or AddTime to GameTimer
	/// </summary>
	/// <returns>Current Time in string 00:00</returns>
	/// <param name="isDone">Add Ref bool local variable to have reference to current timer completion.</param>
	/// <param name="time">Add time to current game timer could be left out if only checking isDone.</param>
//	public string TimeToAdd(ref bool isDone, float time = 0f){
//
//		timer += time;
//
//		if (time > 0f) {
//
//			isTimerOn = true;
//			StopAllCoroutines ();
//			StartCoroutine (StartTimer ());
//		}
//
//		string minutes = Mathf.Floor(timer /60).ToString("00");
//		string seconds = Mathf.Floor (timer % 60).ToString ("00");
//
//		if (timer < 0f) {
//			isDone = true;
//			isTimerOn = false;
//		}
//
//		return minutes + ":" + seconds;
//	
//	
//	}
//	public float GetCurrentTime(){
//
//		return timer;
//	}
//
//	float timerTimeScale = 1;
//	public void StopTimer(){
//
//
//		timerTimeScale = 0;
//
//	}
//	public void ResumeTimer(){
//
//
//		timerTimeScale = 1;
//	}
//
//	private IEnumerator StartTimer(){
//		
//
//		while (isTimerOn) {
//
//			timer -= Time.deltaTime * timerSpeed * timerTimeScale;
//
//			yield return null;
//		}
//
//		yield break;
//	
//	
//	}
//	bool isWaveImageOn;
//	public IEnumerator NewWave(){
//		isWaveImageOn = true;
//		newWaveGO.SetActive (true);
//		yield return new WaitForSeconds (timeUntilNewWave);
//		isWaveImageOn = false;
//	}
//	public bool GetWaveActiveState(){
//
//		if (isWaveImageOn)
//			return true;
//
//		return false;
//	
//	
//	}
//	public float GetNewWaveTime{
//	
//		get{return timeUntilNewWave;}	
//	}


	private bool IsGameOver;                 
	public bool isGameOver{

		get{ return IsGameOver;}

		set{ 
			if (value) {
				onGameOver.Invoke ();
				DATA_MANAGER.CheckHighScore ();
				//gameOver.SetActive (value);
				IsGameOver = value;


			}
		}

	}

	public IEnumerator HitEffectLocation(Vector3 hitLoc){

		Instantiate (hitEffect, hitLoc, Quaternion.identity);
	
		yield return null;


	
	
	}


		


}
