using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Playables;

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

	public static bool isPlayingTimeLine;
	public static bool isSceneLoading = false;

	private Transform player;

	private Animator anim = null;

	private Canvas sceneCanvas;

	void Awake()
	{

		RenderSettings.skybox = skyboxes [currentSkybox];


		if (instance) {
				Debug.LogWarning ("There are two instances of scene controller - deleting late instance.");
				DestroyImmediate (gameObject);
				
				return;
			}
		instance = this; 

		DontDestroyOnLoad (gameObject);




	}
		

	IEnumerator Start(){
		
		//EventManager.Instance.AddListener (EVENT_TYPE.GAME_PAUSED, OnEvent);
		//EventManager.Instance.AddListener (EVENT_TYPE.SCENE_CHANGING, OnEvent);
		//EventManager.Instance.AddListener (EVENT_TYPE.SCENE_LOADED, OnEvent);
	//	EventManager.Instance.AddListener (EVENT_TYPE.APPLICATION_QUIT, OnEvent);

		yield return null;

		sceneCanvas = GetComponentInChildren<Canvas> ();
		sceneCanvas.worldCamera = Camera.main;
		sceneCanvas.planeDistance = 0.2f;


		RenderSettings.skybox = skyboxes [currentSkybox];


		if(OrientationAdjustment.Instance != null)
			OrientationAdjustment.Instance.OrientationChangeToGlobalFront ();


	
		//EventManager.Instance.PostNotification (EVENT_TYPE.STRESSMENU_CLOSED, this, null);

		//if(stressMenu != null)
		//stressMenu.SetActive (false);


		if(UIStressGage.Instance != null)
		UIStressGage.Instance.stress = DataManager.Instance.LoadStressLevel ();

		SceneManager.sceneLoaded += OnLevelLoad;
		
		anim = GetComponentInChildren<Animator>();

		EventManager.Instance.PostNotification (EVENT_TYPE.SCENE_LOADED, this, null);

		//StartCoroutine(TakeOffFade());
	}



	IEnumerator TakeOffFade(){
		
		anim.SetTrigger ("FadeOut");

		while (true) {
			yield return null;

			if (anim.GetCurrentAnimatorStateInfo (0).IsTag ("Clear")) {

				isSceneLoading = false;
				yield break;
			
			}
		}
	
	}

	public void Load(string scene){
			
		if (!isSceneLoading) 
			isSceneLoading = true;
		else {
			Debug.LogWarning ("There is more that one scene attempting to load.");
			return;
		}
	
		EventManager.Instance.PostNotification (EVENT_TYPE.SCENE_CHANGING, this, null);

		anim.SetTrigger ("FadeIn");

		StartCoroutine (ChangeScene (scene));


	}
	


	AsyncOperation async;
	public IEnumerator ChangeScene (string scene){


		while (true) {
			
			yield return null;

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Faded")) {


//				if (!isSceneLoading) {
//					
//					isSceneLoading = true;

					async = SceneManager.LoadSceneAsync (scene);

					StartCoroutine (WhileSceneIsLoading ());

					yield break;
//				
//				}
					
			}
		}
			

	}//this plays before OnlevelLoad()...
	IEnumerator WhileSceneIsLoading(){
		
		RawImage Rimage = GetComponentInChildren<RawImage> (true);
		Vector2 RiSize = Rimage.rectTransform.sizeDelta;
		Rimage.gameObject.SetActive (true);

		Rimage.rectTransform.sizeDelta = new Vector2 (0, 50);
		while (!async.isDone) {
		//while (async.progress > 0.89f) {
			//image.color = Color.red;//new Color(image.color.r, image.color.g, image.color.b, Mathf.PingPong(Time.time,1));

			Rimage.rectTransform.sizeDelta = new Vector2((500 * async.progress), Rimage.rectTransform.sizeDelta.y );
			yield return null;


		}

		LocalizationManager.Instance.ObtainTextReferences ();

		isSceneLoading = false;
		Rimage.gameObject.SetActive (false);
			
		OrientationAdjustment.Instance.OrientationChangeToGlobalFront ();
	
		yield return null;
	
	}

	void OnLevelLoad(Scene scene, LoadSceneMode sceneMode){

		sceneCanvas = GetComponentInChildren<Canvas> ();
		sceneCanvas.worldCamera = Camera.main;

		RenderSettings.skybox = skyboxes [currentSkybox];


		if(OrientationAdjustment.Instance != null)
			OrientationAdjustment.Instance.OrientationChangeToGlobalFront ();

		StartCoroutine(TakeOffFade());

		EventManager.Instance.PostNotification (EVENT_TYPE.SCENE_LOADED, this, null);

	}

	public void ResetCurrentGame(){
		
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
