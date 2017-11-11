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
//	[SerializeField] AudioMixer mainMixer;
	//[SerializeField] PlayableDirector mainPD;
	public Material[] skyboxes;
	private GameObject stressMenu;

	public static SceneController Instance
	{ get { return instance; } }

	private static SceneController instance = null;

	public static bool isPlayingTimeLine;
	public static bool isSceneLoading = false;

	private Transform player;

	private Animator anim = null;

	public bool isCustomSavePosition = false;
	public Vector3 customSavePosition = Vector3.zero;

	private Canvas sceneCanvas;

	void Awake()
	{

		
		RenderSettings.skybox = skyboxes [currentSkybox];


		if (instance) {
				DestroyImmediate (gameObject);
				return;
			}
		instance = this; 

		DontDestroyOnLoad (gameObject);

		stressMenu = GameObject.FindWithTag ("StressMenu");


	}
		

	IEnumerator Start(){

		yield return null;

		sceneCanvas = GetComponentInChildren<Canvas> ();
		sceneCanvas.worldCamera = Camera.main;
		sceneCanvas.planeDistance = 0.2f;
	/*	sceneCanvas.GetComponentInChildren<Image> ().color = new Color (0, 0, 0, 1);
	
		while (!LocalizationManager.Instance.GetIsReady()) {

			yield return null;

		}*/
		//TEST
		/*var tempLTs	= GameObject.FindGameObjectsWithTag ("Text");

		for (int i = 0; i < tempLTs.Length; i++) {

			LocalizationManager.Instance.presentLocalizedTexts.Add(tempLTs[i].GetComponent<LocalizedText> ());
		}
			
		LocalizationManager.Instance.LoadLocalizedText(PlayerPrefs.GetString("Language"));*/



	
		if(stressMenu != null)
		stressMenu.SetActive (false);

		player = GameObject.FindWithTag ("Player").transform;

		//if (GetCurrentSceneName () == "Intro") 
		if(GetCurrentSceneName().Contains("Intro"))
		player.position = DataManager.Instance.LoadPosition ();

		if(UIStressGage.Instance != null)
		UIStressGage.Instance.stress = DataManager.Instance.LoadStressLevel ();


		SceneManager.sceneLoaded += OnLevelLoad;
		
		anim = GetComponentInChildren<Animator>();



	//	anim.SetTrigger ("FadeIn");

	}

	void OnLevelLoad(Scene scene, LoadSceneMode sceneMode){
			
		sceneCanvas = GetComponentInChildren<Canvas> ();
		sceneCanvas.worldCamera = Camera.main;


	/*	if (string.Equals (SceneManager.GetActiveScene ().name, "IntroTimeLine", System.StringComparison.CurrentCultureIgnoreCase)) {
			mainPD = GameObject.FindGameObjectWithTag ("TimeLine").GetComponent<PlayableDirector> ();
			StartCoroutine (ChangeSceneWhenTimeLineFinishes (mainPD.duration));
		}*/

		SAssessment.Instance.OnLevelLoad ();

		if (GameObject.FindWithTag ("StressMenu") != null) {
			stressMenu = GameObject.FindWithTag ("StressMenu");
			stressMenu.SetActive (false);

		} else
			Debug.LogWarning ("There is no StressMenu in this scene");

		RenderSettings.skybox = skyboxes [currentSkybox];


		//if Async is available and not crashing unity activate this...
		//	anim.SetTrigger ("FadeOut");

			
	//	if (string.Equals (SceneManager.GetActiveScene ().name, "Intro", System.StringComparison.CurrentCultureIgnoreCase)) {
		if(GetCurrentSceneName().Contains("Intro")){
			GameController.Instance.Paused = false;
			player = GameObject.FindWithTag ("Player").transform;

			player.position = DataManager.Instance.LoadPosition ();
			UIStressGage.Instance.stress = DataManager.Instance.LoadStressLevel ();

		} else {

			//this plays after the fact before it finishes loading the level....
		//	OrientationAdjustment.Instance.OrientationChangeToGlobalFront ();
			if(GameController.Instance != null)
			GameController.Instance.Paused = true;

		}


		//if Async is available and not crashing unity deactivate this...
		if(OrientationAdjustment.Instance != null)
		OrientationAdjustment.Instance.OrientationChangeToGlobalFront ();
		
		StartCoroutine(TakeOffFade());

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

	//private bool isFirstScene = true;

	public void Load(string scene){
			


		anim.SetTrigger ("FadeIn");


		//if (string.Equals (SceneManager.GetActiveScene ().name, "Intro", System.StringComparison.CurrentCultureIgnoreCase)) {
		if(GetCurrentSceneName().Contains("Intro")){
			if (!isCustomSavePosition)
				DataManager.Instance.SavePosition (player.position);
			else {
				DataManager.Instance.SavePosition (customSavePosition);
				isCustomSavePosition = false;

			}
			DataManager.Instance.SaveStressLevel (UIStressGage.Instance.stress);
			DataManager.Instance.SaveItemList (PlayerInventory.Instance.playerItemSlotGOList);

		}

		StartCoroutine (ChangeScene (scene));

	}


	AsyncOperation async;
	public IEnumerator ChangeScene (string scene){

		//works... deactivate once async works
		/*isSceneLoading = true;
		while (true) {
			yield return null;

			if (anim.GetCurrentAnimatorStateInfo (0).IsTag ("Faded")) {
				SceneManager.LoadScene (scene);
				yield break;

			}
		}*/
	

		//FIXME LOADSCENASYNC CRASHES UNITY... because of components of camera from gvr and using async..
		//doesnotwork...AS OF V2017.20
		//SceneManager.LoadSceneAsync ("TEST");


		while (true) {
			
			yield return null;

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Faded")) {


				if (!isSceneLoading) {
					
					isSceneLoading = true;

					async = SceneManager.LoadSceneAsync (scene);

					//async.allowSceneActivation = false;


					StartCoroutine (WhileSceneIsLoading ());

					yield break;
				
				}
					
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
	//test
		//LocalizationManager.Instance.ResetReady();
		LocalizationManager.Instance.ObtainTextReferences ();

		//async.allowSceneActivation = true;
		isSceneLoading = false;
		Rimage.gameObject.SetActive (false);
			
		OrientationAdjustment.Instance.OrientationChangeToGlobalFront ();
	
		yield return null;
	


	
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
	public void OnApplicationQuit(){

		if (GameObject.FindWithTag ("Player") == null)
			return;

		player = GameObject.FindWithTag ("Player").transform;

		if (string.Equals (SceneManager.GetActiveScene ().name, "Intro", System.StringComparison.CurrentCultureIgnoreCase)) {
			DataManager.Instance.SaveStressLevel (UIStressGage.Instance.stress);
			DataManager.Instance.SavePosition (player.position);
			DataManager.Instance.SaveItemList (PlayerInventory.Instance.playerItemSlotGOList);



		}
	}




}
