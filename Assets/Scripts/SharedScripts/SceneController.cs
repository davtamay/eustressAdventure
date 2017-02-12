using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	private int curskybox;
	public int currentSkybox{
		get{ return curskybox;}
		set{ curskybox = value;} 

	}
	public Material[] skyboxes = new Material[18];
	private GameObject stressMenu;

	public static SceneController Instance
	{ get { return instance; } }

	private static SceneController instance = null;


	void Awake()
	{
		
		RenderSettings.skybox = skyboxes [currentSkybox];

		DontDestroyOnLoad (gameObject);


		if (instance) {
				DestroyImmediate (gameObject);
				return;
			}
		instance = this; 


	}
	void Start(){
		stressMenu = GameObject.FindWithTag ("StressMenu");
		stressMenu.SetActive (false);

		SceneManager.sceneLoaded += OnLevelLoad;
	}
	void OnLevelLoad(Scene scene, LoadSceneMode sceneMode){
		//new test stressmenu 2/12/17

		if(string.Equals (SceneManager.GetActiveScene ().name, "StressAss", System.StringComparison.CurrentCultureIgnoreCase))
			GameController.Instance.Paused = false;
		else GameController.Instance.Paused = true;

		SAssessment.Instance.OnLevelWasLoad ();

		RenderSettings.skybox = skyboxes [currentSkybox];

	


	}

	public void Load(string scene){

		SceneManager.LoadScene (scene);
	
	}
	public void ResetCurrentGame(){
		
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);


	
	}

	public void ResetGame (string scene){

		SceneManager.LoadScene (scene);

	}
	public void ChangeSkyBox () {



		if (currentSkybox < skyboxes.Length) {
			++currentSkybox;
		} else {
			currentSkybox = 0;
		}
		RenderSettings.skybox = skyboxes [currentSkybox];
	}

	public string GetCurrentSceneName(){
	
		return SceneManager.GetActiveScene ().name;
	
	}



}
