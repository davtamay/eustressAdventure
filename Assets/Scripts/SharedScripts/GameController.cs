using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private GameObject Menu;

	private GameObject gameStart;
	private GameObject gameOver;


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

	}

	void Start(){
		gameStart = GameObject.FindWithTag ("GameStart");
		gameOver = GameObject.FindWithTag ("GameOver");


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
	public bool isGameOver{
		set{ gameOver.SetActive (value);}

	}

		


}
