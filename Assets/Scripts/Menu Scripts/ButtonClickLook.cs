using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using TagFrenzy;

[RequireComponent(typeof (Collider))]
public class ButtonClickLook : MonoBehaviour {
	//currently only have meditation on this event;
	[SerializeField]private UnityEvent OnStart;
	[SerializeField]private UnityEvent OnClick;

	[SerializeField] private bool isOnClickEventCalled = false;
	[SerializeField] private bool isOnStartInvoke = false;
	[SerializeField] private bool isButtonInvoke = false;



	public bool isSMenuOpener = false;
	private Camera cam;
	//private GameObject currentButton;
	private float timeToSelect = 2.0f;
	private float countDown = 2.0f;
	private Image buttonFill;
	private Button button;
	private GameObject stressMenu;

	private SAssessment sAsses;



	public bool isStressed = false;



	public bool isEnvChanger = false;

	public bool isSceneChange = false;
	[SerializeField] private string scene;
	[SerializeField] private bool isResetPositiontoHome;
	[SerializeField] private Vector3 homePosition;

	[SerializeField] private bool isRestartProgress;

	public bool isBackToIntroButton = false;
	public bool isStartButton = false;
	public bool isReplayButton = false;
	public bool isMusicButton = false;


	//public bool isAllowWalk = false;
	private bool isWalking = false;

	private Collider col;

	[SerializeField] private bool isStressModified;
	[SerializeField] private float stressModifiedAmount;

	[SerializeField] private bool isLocalizedButton;
	private enum localizationLanguage{LANGUAGE_ENGLISH, LANGUAGE_SPANISH}
	[SerializeField]private localizationLanguage language;

	public enum MusicPlayerButton{play,stop,next,previous, none}
	public MusicPlayerButton curMusicButton;



	PointerEventData data;

	void Awake(){
	
		buttonFill = GetComponent <Image> ();
		buttonFill.fillAmount = 1.0f;

		button = GetComponent <Button> ();
		col = GetComponent <Collider> ();

		cam = Camera.main;

		if (isOnStartInvoke)
			OnStart.Invoke ();
	}

	void Start(){

		if (isSMenuOpener) 
			stressMenu = GameObject.FindWithTag ("StressMenu");


//		EventManager.Instance.AddListener (EVENT_TYPE.GAME_INIT, OnEvent);
//		EventManager.Instance.AddListener (EVENT_TYPE.GAME_PAUSED, OnEvent);
//		EventManager.Instance.AddListener (EVENT_TYPE.STRESSMENU_CLOSED, OnEvent);
//		EventManager.Instance.AddListener (EVENT_TYPE.POINTS_ADD, OnEvent);
//		EventManager.Instance.AddListener (EVENT_TYPE.PROGRESS_RESTART_POSITION, OnEvent);
//		EventManager.Instance.AddListener (EVENT_TYPE.PROGRESS_RESTART_ALL, OnEvent);
//		EventManager.Instance.AddListener (EVENT_TYPE.RETURN_SCENE_MAIN, OnEvent);
	


	}

	private bool isLooking = false;
	CoroutineController controller;
	void Update(){

		if (SceneController.isSceneLoading){
			StopAllCoroutines ();
			return;
		}

		Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);


		RaycastHit hit;
	
			if (col.Raycast (ray, out hit, 400)) {
			
			if (hit.transform.gameObject.CompareTag ("Button")) {

				hit.transform.GetComponent<Button> ().Select ();

				if (controller != null )
				if (controller.state == CoroutineState.Running)
					return;
				
					isLooking = true;

				if (controller == null ) {
					this.StartCoroutineEx (ButtonLook (), out controller);
					AudioManager.Instance.PlayInterfaceSound ("ButtonSelect");

					return;
				}

	
			}

			} else {

			EventSystem.current.SetSelectedGameObject (null);

			if (controller != null)
			if (controller.state == CoroutineState.Running)
				controller = null;

				isLooking = false;
				countDown = timeToSelect;
				buttonFill.fillAmount = 1.0f;
	
			}

	}	


	IEnumerator ButtonLook(){
			
		countDown = timeToSelect;
		buttonFill.fillAmount = 1.0f;

		while (isLooking) {
			yield return null;

			// highlight
			//ExecuteEvents.Execute<IPointerEnterHandler> (hitButton, data, ExecuteEvents.pointerEnterHandler);
			countDown -= Time.unscaledDeltaTime;

			buttonFill.fillAmount = countDown / timeToSelect;


			if (countDown < 0.0f){ 

				countDown = timeToSelect;
				buttonFill.fillAmount = 1.0f;


				//UnityEngine.Profiling.Profiler.BeginSample ("ButtonPress");
				if (isSMenuOpener) {
					GameController.Instance.Paused = false;
				} else if (isRestartProgress) {

					AudioManager.Instance.PlayInterfaceSound ("SpecialSelect");
					DataManager.Instance.DeleteHighScoreSlotandPositionData (homePosition);
					DataManager.Instance.DeletePPDataTaskProgress ();

					EventManager.Instance.PostNotification (EVENT_TYPE.PROGRESS_RESTART_ALL, this, null);

				} else if (isEnvChanger)
					SceneController.Instance.ChangeSkyBox ();
				else if (isBackToIntroButton) {

					if (PlayerManager.Instance != null && DataManager.Instance != null) {
						DataManager.Instance.CheckHighScore (SceneController.Instance.GetCurrentSceneName (), PlayerManager.Instance.points);
					}
					LoadScene ("Intro");
					EventManager.Instance.PostNotification (EVENT_TYPE.RETURN_SCENE_MAIN, this, null);

				}else if (isStartButton) {
					GameController.Instance.StartGame ();
					AudioManager.Instance.PlayInterfaceSound ("SpecialSelect");
					GameController.Instance.Paused = false;

				} else if (isReplayButton) {
					SceneController.Instance.ResetCurrentGame ();
				} else if (isResetPositiontoHome) {
				
					PlayerManager.Instance.isCustomSavePosition = true;
					PlayerManager.Instance.customSavePosition = homePosition;
					LoadScene ("Intro");

					EventManager.Instance.PostNotification (EVENT_TYPE.PROGRESS_RESTART_POSITION, this, null);
				}else if (isMusicButton) {

					switch (curMusicButton) {

					case MusicPlayerButton.stop:
						AudioManager.Instance.StopM();
						break;

					case MusicPlayerButton.next:
						AudioManager.Instance.PlayMusicNext();
						break;
				
					case MusicPlayerButton.previous:
						AudioManager.Instance.PlayMusicPrevious();
						break;

					case MusicPlayerButton.play:
						AudioManager.Instance.PlayM();
						break;

					}
				}else if (isLocalizedButton){

					if(LocalizationManager.Instance == null){
						Debug.LogError ("There is no LocalizationManager available in scene Setting language does not work on click.");
						break;
					}
					switch(language){

					case localizationLanguage.LANGUAGE_ENGLISH:
						LocalizationManager.Instance.LoadLocalizedText ("localization_en.json");
						break;
					case localizationLanguage.LANGUAGE_SPANISH:
						LocalizationManager.Instance.LoadLocalizedText ("localization_sp.json");
						break;

					}

				}

				if (isSceneChange)
					LoadScene (scene);

				if (isButtonInvoke) 
					button.onClick.Invoke ();
				
				if (isOnClickEventCalled)
					OnClick.Invoke ();


				countDown = timeToSelect;
				buttonFill.fillAmount = 1.0f;
				isLooking = false;
			
			
				//to prevent multiple clicks in one time
				yield return new WaitForSecondsRealtime (7f);
				controller = null;

			}
		}

	}



	void LoadScene(string scene){
		col.enabled = false;
		SceneController.Instance.Load (scene);
	
	
	
	}
	public void LoadURL(string url){

		if (isStressModified)
			UIStressGage.Instance.stress = stressModifiedAmount;
		
		Application.OpenURL (url);
		Application.Quit ();
	

	}
		
			
	}


	

	




