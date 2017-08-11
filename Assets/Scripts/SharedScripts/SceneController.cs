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

	[SerializeField] private bool isSceneLoading = false;

	private Transform player;

	private Animator anim = null;

	public bool isCustomSavePosition = false;
	public Vector3 customSavePosition = Vector3.zero;

	void Awake()
	{
		
		RenderSettings.skybox = skyboxes [currentSkybox];

		DontDestroyOnLoad (gameObject);


		if (instance) {
				DestroyImmediate (gameObject);
				return;
			}
		instance = this; 


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
		

		SAssessment.Instance.OnLevelWasLoad ();

		stressMenu = GameObject.FindWithTag ("StressMenu");
		stressMenu.SetActive (false);

		RenderSettings.skybox = skyboxes [currentSkybox];

	//	while (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Faded"))
	//		return;

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
			
			

	}

	public void Load(string scene){



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


		anim.SetTrigger ("FadeIn");
		StartCoroutine (ChangeScene (scene));

	}

	public void OnApplicationQuit(){

		if (string.Equals (SceneManager.GetActiveScene ().name, "Intro", System.StringComparison.CurrentCultureIgnoreCase)) {
			DataManager.Instance.SaveStressLevel (UIStressGage.Instance.stress);
			DataManager.Instance.SavePosition (player.position);
			DataManager.Instance.SaveItemList (PlayerManager.Instance.playerItemSlotGOList);



		}
	}

	AsyncOperation async;
	public IEnumerator ChangeScene (string scene){
		
		while (true) {
			yield return null;

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Faded")) {

				if (!isSceneLoading) {

					isSceneLoading = true;

					async = SceneManager.LoadSceneAsync (scene);
					StartCoroutine (WhileSceneIsLoading ());

					break;
				
				}

			//	SceneManager.LoadScene (scene);
			//	break;
			}
		}



	}//this plays before OnlevelLoad()...
	IEnumerator WhileSceneIsLoading(){
		//Text text = GetComponentInChildren<Text> (true);
		//text.gameObject.SetActive(true);
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
			anim.SetTrigger ("FadeOut");
		//if (!string.Equals (SceneManager.GetActiveScene ().name, "Intro", System.StringComparison.CurrentCultureIgnoreCase))
			OrientationAdjustment.Instance.OrientationChangeToGlobalFront ();
	
		//text.gameObject.SetActive(false);



	
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
