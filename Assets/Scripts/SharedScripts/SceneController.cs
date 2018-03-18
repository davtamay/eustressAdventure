using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Playables;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
	//public static bool isSceneLoading = false;

	//private Transform player;

	private Animator anim = null;

	private Canvas sceneCanvas;

	[SerializeField]private GameObject loadingSceneActivator;

	[Header("Events")]
	//[SerializeField]private UnityEvent onGameInitialized;
	[SerializeField]private UnityEvent onSceneChange;

	//[Header("References")]
	[SerializeField]private BoolVariable isSceneLoading;

	void Awake()
	{

		RenderSettings.skybox = skyboxes [currentSkybox];


		if (instance) {
			Debug.LogWarning ("There are two instances of scene controller - deleting late instance.");
			DestroyImmediate (gameObject);

			return;
		}
		instance = this; 

		//DontDestroyOnLoad (gameObject);


		originalGazeTime = GazeInputModule.GazeTimeInSeconds;

	}

	float originalGazeTime;
	IEnumerator Start(){

		//SceneManager.sceneLoaded += OnLevelLoad;

		anim = GetComponentInChildren<Animator>();

		//anim.SetTrigger ("FadeOut");

		yield return null;

		StartCoroutine(TakeOffFade());
	}



	IEnumerator TakeOffFade(){

		anim.SetTrigger ("FadeOut");

		while (true) {
			yield return null;

			if (anim.GetCurrentAnimatorStateInfo (0).IsTag ("Clear")) {
			
				//GazeInputModule.GazeTimeInSeconds = originalGazeTime;
				isSceneLoading.isOn = false;
				//isSceneLoading.isOn = false;
				yield break;

			}
		}

	}

	public void Load(string scene){
	//	GazeInputModule.GazeTimeInSeconds = Mathf.Infinity;

		if (!isSceneLoading.isOn) {
			isSceneLoading.isOn = true;
		//	GazeInputModule.GazeTimeInSeconds = Mathf.Infinity;
		}
		else {
			Debug.LogWarning ("There is more that one scene attempting to load.");
			return;
		}


		onSceneChange.Invoke ();

		anim.SetTrigger ("FadeIn");

		StartCoroutine (ChangeScene (scene));


	}



	AsyncOperation async = null;
	public IEnumerator ChangeScene (string scene){


		while (true) {

			yield return null;

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Faded")) {


				//				if (!isSceneLoading) {
				//					
				//					isSceneLoading = true;

				//async = SceneManager.LoadSceneAsync (scene);

				StartCoroutine (WhileSceneIsLoading (scene));

				yield break;

			}
		}


	}//this plays before OnlevelLoad()...
	IEnumerator WhileSceneIsLoading(string scene){

		RawImage Rimage = GetComponentInChildren<RawImage> (true);
		Vector2 RiSize = Rimage.rectTransform.sizeDelta;
		Rimage.gameObject.SetActive (true);

		if(loadingSceneActivator)
			loadingSceneActivator.SetActive (true);

		Rimage.rectTransform.sizeDelta = new Vector2 (0, 50);

		async = SceneManager.LoadSceneAsync (scene);
		async.allowSceneActivation = false;

		//image.color = Color.red;//new Color(image.color.r, image.color.g, image.color.b, Mathf.PingPong(Time.time,1));
		while (async.progress < 0.9f) {

			Rimage.rectTransform.sizeDelta = new Vector2 (0.5f * (500 * async.progress), Rimage.rectTransform.sizeDelta.y);
			yield return new WaitForSecondsRealtime (0.05f);

		}
		async.allowSceneActivation = true;
		float perc = 0.5f;
		while(!async.isDone)
		{
			yield return null;
			perc = Mathf.Lerp(perc, 1f, 0.05f);
			Rimage.rectTransform.sizeDelta = new Vector2 (perc * (500 * async.progress), Rimage.rectTransform.sizeDelta.y);
		}
	//	async.allowSceneActivation = true;
		//		LocalizationManager.Instance.ObtainTextReferences ();
		//
		//		isSceneLoading = false;
		//		Rimage.gameObject.SetActive (false);
		//			
		//		loadingSceneActivator.SetActive (false);
		//
		//
		//		OrientationAdjustment.Instance.OrientationChangeToGlobalFront ();

		//yield return null;

	}

	void OnLevelLoad(Scene scene, LoadSceneMode sceneMode){

		sceneCanvas = GetComponentInChildren<Canvas> ();
		sceneCanvas.worldCamera = Camera.main;

		RenderSettings.skybox = skyboxes [currentSkybox];


		if(OrientationAdjustment.Instance != null)
			OrientationAdjustment.Instance.OrientationChangeToGlobalFront ();

		StartCoroutine(TakeOffFade());

		//	EventManager.Instance.PostNotification (EVENT_TYPE.SCENE_LOADED, this, null);

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
