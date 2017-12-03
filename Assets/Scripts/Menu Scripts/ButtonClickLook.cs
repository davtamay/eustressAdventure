using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using TagFrenzy;
[Serializable]
public class Vector3_UnityEvent : UnityEvent<Vector3>{}
//[RequireComponent(typeof (Collider))]
public class ButtonClickLook : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler {
	//currently only have meditation on this event;
	public Vector3_UnityEvent vector3_UnityEvent;
	public Vector3 vector3_UnityEvent_Parameter;



	[SerializeField]private UnityEvent OnStart;
	[SerializeField]private UnityEvent OnClick;

	[SerializeField] private bool isOnClickEventCalled = false;
	[SerializeField] private bool isOnStartInvoke = false;

	private float timeToSelect = 2.0f;
	private float countDown = 2.0f;
	private Image buttonFill;
	private Button button;

	public bool isStressed = false;

	public bool isEnvChanger = false;

	public bool isSceneChange = false;
	[SerializeField] private string scene;
	[SerializeField] private bool isResetPositiontoHome;


	[SerializeField] private bool isRestartProgress;

	public bool isBackToIntroButton = false;
	public bool isStartButton = false;
	public bool isReplayButton = false;


	private bool isWalking = false;

	private Collider col;

	[SerializeField] private bool isStressModified;
	[SerializeField] private float stressModifiedAmount;

//	public enum MusicPlayerButton{play,stop,next,previous, none}
//	public MusicPlayerButton curMusicButton;

	private float secondsUntilClick;

	[SerializeField]private bool isCustomTime;
	[SerializeField]private float customTime;

	[SerializeField]private float originalTime;

	[Header("References")]
	[SerializeField]private DataManager DATA_MANAGER;
	[SerializeField]private AudioManager AUDIO_MANAGER;
	[SerializeField]private BoolVariable isSceneLoading;


	void Awake(){
	
		buttonFill = GetComponent <Image> ();
		buttonFill.fillAmount = 1.0f;

		secondsUntilClick = GazeInputModule.GazeTimeInSeconds;
			
		button = GetComponent <Button> ();
		col = GetComponent <Collider> ();

		//cam = Camera.main;

		if (isOnStartInvoke)
			OnStart.Invoke ();


	}
	public void Start(){
		vector3_UnityEvent.AddListener (delegate {InvokeVector3UnityEvent();});

	}
	public void InvokeVector3UnityEvent(){

	
		vector3_UnityEvent.Invoke (vector3_UnityEvent_Parameter);

	}

	public void OnPointerEnter(PointerEventData eventData){
		if (!EventSystem.current.IsPointerOverGameObject ()) {
			//block back objects from being clicked https://www.youtube.com/watch?v=EVZiv7DLU6E

			Debug.Log ("I AM ONNNNNBUTTON!!!!!!");


			if (isCustomTime) {
				originalTime = GazeInputModule.GazeTimeInSeconds;
				GazeInputModule.GazeTimeInSeconds = customTime;
			}

	
			StartCoroutine (ReduceFillAmount (eventData));

		}
	}
	IEnumerator ReduceFillAmount(PointerEventData eventData){

		if (isSceneLoading.isOn) {
			EventSystem.current.SetSelectedGameObject (null);
			//StopAllCoroutines ();
			//yield break;
		}
			
		secondsUntilClick = GazeInputModule.GazeTimeInSeconds;
		float totalTimeToWait = secondsUntilClick;

		while(true){
			
			secondsUntilClick -= Time.unscaledDeltaTime;
			buttonFill.fillAmount = secondsUntilClick/totalTimeToWait;
			yield return null;

		}

	}
	public void OnPointerExit(PointerEventData eventData){

		StopAllCoroutines ();
		buttonFill.fillAmount = 1;

		if (isCustomTime) 
			GazeInputModule.GazeTimeInSeconds = originalTime;

		Debug.Log("I AM Out!!!!!!");

	}
	public void OnPointerClick(PointerEventData eventData){
		StopAllCoroutines ();
		buttonFill.fillAmount = 1;

		Debug.Log("I AM clickeed!!!!!!");

		if (isCustomTime) {
			GazeInputModule.GazeTimeInSeconds = originalTime;
//			secondsUntilClick = GazeInputModule.GazeTimeInSeconds;
		}

		else if (isEnvChanger)
			SceneController.Instance.ChangeSkyBox ();
		else if (isBackToIntroButton) {

			DATA_MANAGER.CheckHighScore ();
				
			
			LoadScene ("Intro");

		}else if (isStartButton) {
			GameController.Instance.StartGame ();
			AUDIO_MANAGER.PlayInterfaceSound ("SpecialSelect");
			GameController.Instance.Paused = false;

		} else if (isReplayButton) {
			SceneController.Instance.ResetCurrentGame ();
		}

		if (isSceneChange)
			LoadScene (scene);
		
		if (isOnClickEventCalled)
			OnClick.Invoke ();

	}
		
	void LoadScene(string scene){

		SceneController.Instance.Load (scene);
	
	}
	public void LoadURL(string url){

		if (isStressModified)
			UIStressGage.Instance.stress = stressModifiedAmount;
		
		Application.OpenURL (url);
		Application.Quit ();
	

	}
		
			
	}


	

	




