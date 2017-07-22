using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject stressMenu;

	private GameObject gameStart;
	private GameObject gameOver;

	private GameObject newWave;
	private float timeUntilNewWave;



	private Camera[] cameras;
	private Camera cam;

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


	//	if (string.Equals (SceneController.Instance.GetCurrentSceneName(), "MainMenu", System.StringComparison.CurrentCultureIgnoreCase))
	//		stressMenu.SetActive (false);
		
		//cam = GameObject.FindWithTag ("MainCamera").GetComponent<Camera> ();
		cam = Camera.main;
		cameras = cam.GetComponentsInChildren<Camera> ();

	}

	void Start(){
	//	hitEffects = new List<GameObject> ();

		gameStart = GameObject.FindWithTag ("GameStart");
		gameOver = GameObject.FindWithTag ("GameOver");

		if (newWave == null) {
			newWave = GameObject.FindWithTag ("NewWave");

			if (newWave != null) {
				timeUntilNewWave = newWave.GetComponent<NewWave> ().timeUntilDisapear;
				newWave.gameObject.SetActive (false);
			}

		}

		if (hitEffect == null) {
		
			hitEffect = Resources.Load ("HitEffect") as GameObject;//GameObject.FindWithTag ("HitEffect");
			//hitEffect.SetActive (false);
		
		}
	
		if (gameOver != null) {

			gameOver.SetActive (false);
		}
	}

	private bool paused;
	public bool Paused {

		get { return paused; }

		set {
			paused = value;
			
			if (paused) {
				Time.timeScale = 0;
			} else {
				Time.timeScale = 1;
			}
		}
	}

	private bool isMenuActive;
	public bool IsMenuActive{

		get{ return stressMenu.activeInHierarchy; }
		
	}

	private bool isInfoBubbleActive;
	public bool IsInfoBubbleActive{

		get{ return isInfoBubbleActive;}

		set { isInfoBubbleActive = value;}
	}

	



	public void SetMenuActive(){
	
		stressMenu.SetActive (true);
	}

	//private bool isStartMenuActive;
	public bool IsStartMenuActive{

		get{ return gameStart.activeSelf; }

	}
	public void StartGame (){
	
		gameStart.SetActive (false);
	
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

	private IEnumerator StartTimer(){
		

		while (isTimerOn) {

			timer -= Time.deltaTime * timerSpeed;

			yield return null;
		}

		yield break;
	
	
	}


	public IEnumerator NewWave(){

		newWave.SetActive (true);
		yield return new WaitForSeconds (timeUntilNewWave);
	}
	public float GetNewWaveTime{
	
		get{return timeUntilNewWave;}	
	}

	                       
	public bool isGameOver{
		set{ gameOver.SetActive (value);
			if (true == value)
				MakeOnlyUIVisible ();
			else
				MakeEverythingVisible ();
		
		}

	}
	public void MakeOnlyUIVisible(){

		foreach (Camera c in cameras) 
			c.cullingMask = 1 << 5;

		
	
	}
	public void MakeEverythingVisible(){
		foreach (Camera c in cameras) 
			c.cullingMask = -1;
	}

	public IEnumerator HitEffectLocation(Vector3 hitLoc){

		Instantiate (hitEffect, hitLoc, Quaternion.identity);
	
		yield return null;


	
	
	}

		


}
