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

	private Transform player;

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

		player = GameObject.FindWithTag ("Player").transform;

		SceneManager.sceneLoaded += OnLevelLoad;

		anim = GetComponentInChildren<Animator>();
	}

	void OnLevelLoad(Scene scene, LoadSceneMode sceneMode){



		if(string.Equals (SceneManager.GetActiveScene ().name, "StressAss", System.StringComparison.CurrentCultureIgnoreCase))
			GameController.Instance.Paused = false;
		else GameController.Instance.Paused = true;


		SAssessment.Instance.OnLevelWasLoad ();

		RenderSettings.skybox = skyboxes [currentSkybox];

	//	while (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Faded"))
	//		return;

			anim.SetTrigger ("FadeOut");
			
		if (string.Equals (SceneManager.GetActiveScene ().name, "Intro", System.StringComparison.CurrentCultureIgnoreCase)) {
			GameController.Instance.Paused = false;
			player = GameObject.FindWithTag ("Player").transform;
			player.position = DataManager.Instance.LoadPosition ();
			//new
	//		PlayerManager.Instance.playerSlotGOList.Clear();
	//		PlayerManager.Instance.playerSlotGOList = DataManager.Instance.LoadItemList();
			Debug.Log ("SceneController playerslot count: " + PlayerManager.Instance.playerSlotGOList.Count);
		}
			

	}

	public void Load(string scene){

		if (string.Equals (SceneManager.GetActiveScene ().name, "Intro", System.StringComparison.CurrentCultureIgnoreCase)) {
			DataManager.Instance.SavePosition (player.position);
			//new
			DataManager.Instance.SaveItemList (PlayerManager.Instance.playerSlotGOList);
		}


		anim.SetTrigger ("FadeIn");
		StartCoroutine (ChangeScene (scene));

	}

	public IEnumerator ChangeScene (string scene){
		
		while (true) {

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Faded")) {
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
