using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

	public static bool isSceneLoading = false;

	private Transform player;

	private Animator anim = null;

	public bool isCustomSavePosition = false;
	public Vector3 customSavePosition = Vector3.zero;

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
		

	void Start(){

	
		stressMenu.SetActive (false);

		player = GameObject.FindWithTag ("Player").transform;

		if (GetCurrentSceneName () == "Intro")
		player.position = DataManager.Instance.LoadPosition ();
		
		UIStressGage.Instance.stress = DataManager.Instance.LoadStressLevel ();


		SceneManager.sceneLoaded += OnLevelLoad;
		
		anim = GetComponentInChildren<Animator>();

	}

	void OnLevelLoad(Scene scene, LoadSceneMode sceneMode){
		

		SAssessment.Instance.OnLevelLoad ();

		if (GameObject.FindWithTag ("StressMenu") != null) {
			stressMenu = GameObject.FindWithTag ("StressMenu");
			stressMenu.SetActive (false);

		} else
			Debug.LogWarning ("There is no StressMenu in this scene");

		RenderSettings.skybox = skyboxes [currentSkybox];


		//if Async is available and not crashing unity activate this...
		//	anim.SetTrigger ("FadeOut");

			
		if (string.Equals (SceneManager.GetActiveScene ().name, "Intro", System.StringComparison.CurrentCultureIgnoreCase)) {
			GameController.Instance.Paused = false;
			player = GameObject.FindWithTag ("Player").transform;

			player.position = DataManager.Instance.LoadPosition ();
			UIStressGage.Instance.stress = DataManager.Instance.LoadStressLevel ();

		} else {

			//this plays after the fact before it finishes loading the level....
		//	OrientationAdjustment.Instance.OrientationChangeToGlobalFront ();
			GameController.Instance.Paused = true;

		}

		//if Async is available and not crashing unity deactivate this...
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

	public void Load(string scene){
	
		anim.SetTrigger ("FadeIn");



		if (string.Equals (SceneManager.GetActiveScene ().name, "Intro", System.StringComparison.CurrentCultureIgnoreCase)) {
			if (!isCustomSavePosition)
				DataManager.Instance.SavePosition (player.position);
			else {
				DataManager.Instance.SavePosition (customSavePosition);
				isCustomSavePosition = false;

			}
			DataManager.Instance.SaveStressLevel (UIStressGage.Instance.stress);
			DataManager.Instance.SaveItemList (PlayerManager.Instance.playerItemSlotGOList);

		}

		StartCoroutine (ChangeScene (scene));

	}


	AsyncOperation async;
	public IEnumerator ChangeScene (string scene){

		//works... deactivate once async works
		isSceneLoading = true;
		while (true) {
			yield return null;

			if (anim.GetCurrentAnimatorStateInfo (0).IsTag ("Faded")) {
				SceneManager.LoadScene (scene);
				yield break;

			}
		}
	

		//FIXME LOADSCENASYNC CRASHES UNITY... because of components of camera from gvr and using async..
		//doesnotwork...AS OF V2017.20
		//SceneManager.LoadSceneAsync ("TEST");
		/*

		while (true) {
			
			yield return null;

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Faded")) {


				if (!isSceneLoading) {
					isSceneLoading = true;
					async = SceneManager.LoadSceneAsync (scene);
					StartCoroutine (WhileSceneIsLoading ());
					yield break;
				
				}
					
			}
		}

*/

	}//this plays before OnlevelLoad()...
	IEnumerator WhileSceneIsLoading(){
		
		RawImage Rimage = GetComponentInChildren<RawImage> (true);
		Vector2 RiSize = Rimage.rectTransform.sizeDelta;
		Rimage.gameObject.SetActive (true);

		Rimage.rectTransform.sizeDelta = new Vector2 (0, 50);
		while (!async.isDone) {
			//image.color = Color.red;//new Color(image.color.r, image.color.g, image.color.b, Mathf.PingPong(Time.time,1));

			Rimage.rectTransform.sizeDelta = new Vector2((500 * async.progress), Rimage.rectTransform.sizeDelta.y );
			yield return null;


		}
		

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
			DataManager.Instance.SaveItemList (PlayerManager.Instance.playerItemSlotGOList);



		}
	}




}
