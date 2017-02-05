using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private GameObject Menu;

	private GameObject gameStart;
	private GameObject gameOver;

	private GameObject newWave;
	private float timeUntilNewWave;

	public Camera[] cameras;
	private Camera cam;





	public static GameController Instance
	{ get { return instance; } }

	private static GameController instance = null;


	void Awake()
	{
	//	DontDestroyOnLoad (gameObject);

		if (instance) {
	//		DestroyImmediate (gameObject);
			return;
		}
		instance = this; 

		Menu = GameObject.FindWithTag ("StressMenu");

		cam = Camera.main;
		cameras = cam.GetComponentsInChildren<Camera> ();

	}

	void Start(){
		gameStart = GameObject.FindWithTag ("GameStart");
		gameOver = GameObject.FindWithTag ("GameOver");

		newWave = GameObject.FindWithTag ("NewWave");
		timeUntilNewWave = newWave.GetComponent<NewWave> ().timeUntilDisapear;
		newWave.gameObject.SetActive (false);
	
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

		get{ return Menu.activeInHierarchy; }
		
	}

	private bool isInfoBubbleActive;
	public bool IsInfoBubbleActive{

		get{ return isInfoBubbleActive;}

		set { isInfoBubbleActive = value;}
	}

	



	public void SetMenuActive(){
	
		Menu.SetActive (true);
	}

	//private bool isStartMenuActive;
	public bool IsStartMenuActive{

		get{ return gameStart.activeSelf; }

	}
	public void StartGame (){
	
		gameStart.SetActive (false);
	
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

		


}
