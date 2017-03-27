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
	public Material[] skyboxes;
	private GameObject stressMenu;

	public static SceneController Instance
	{ get { return instance; } }

	private static SceneController instance = null;

	private Animator anim = null;

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

		anim = GetComponentInChildren<Animator>();
	}

	void OnLevelLoad(Scene scene, LoadSceneMode sceneMode){


		//new test stressmenu 2/12/17
		if(string.Equals (SceneManager.GetActiveScene ().name, "StressAss", System.StringComparison.CurrentCultureIgnoreCase))
			GameController.Instance.Paused = false;
		else GameController.Instance.Paused = true;

		SAssessment.Instance.OnLevelWasLoad ();

		RenderSettings.skybox = skyboxes [currentSkybox];

	//	while (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Faded"))
	//		return;

			anim.SetTrigger ("FadeOut");





	}

	public void Load(string scene){

		anim.SetTrigger ("FadeIn");
		StartCoroutine (ChangeScene (scene));

	}

	public IEnumerator ChangeScene (string scene){
		
		while (true) {

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Faded")) {
				Debug.Log (anim.GetCurrentAnimatorStateInfo (0));
				SceneManager.LoadScene (scene);
				break;
			}
			yield return null;
		}



	}
	public void ResetCurrentGame(){
		
		//SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		Load (SceneManager.GetActiveScene ().name);


	
	}

	public void ResetGame (string scene){

	//	SceneManager.LoadScene (scene);
		Load (scene);

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
