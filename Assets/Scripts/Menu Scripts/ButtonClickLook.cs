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
	[SerializeField]private UnityEvent OnClick;
	[SerializeField] bool isClickEventCalled = false;


	public bool isSMenuOpener = false;
	private Camera cam;
	//private GameObject currentButton;
	private float timeToSelect = 2.0f;
	private float countDown = 2.0f;
	private Image buttonFill;
	private Button button;
	private GameObject StressMenu;

	private SAssessment sAsses;

	public bool isSType = false;

	public bool isStressed = false;
	public bool isButtonInvoke = false;

	public bool isEnvChanger = false;

	public bool isGame = false;
	[SerializeField] private string gameScene;
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



	public enum StressTypes{angry, anxious, dissapointed, frustrated, sad, worried, none};
	//public enum MenuCoping {breathing, counting, refraiming, music, paint, meditation, none};

	public enum MusicPlayerButton{play,stop,next,previous, none}
	public MusicPlayerButton curMusicButton;

	public StressTypes StressT;




	//static GameObject hitButton;

	PointerEventData data;

	void Awake(){

		if (isSMenuOpener)
			StressMenu = GameObject.FindWithTag ("StressMenu");
	
	}

	void Start () {
		
		buttonFill = GetComponent <Image> ();
		buttonFill.fillAmount = 1.0f;

		button = GetComponent <Button> ();
		col = GetComponent <Collider> ();

		cam = Camera.main;

		//data = new PointerEventData (EventSystem.current);

//		if (isAllowWalk)
//			buttonFill.color = Color.red;


	}

	private bool isLooking = false;
	CoroutineController controller;


	void Update(){

		if (SceneController.isSceneLoading)
		return;
	

		Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);


		RaycastHit hit;
		//hitButton = null;


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

			EventSystem.current.SetSelectedGameObject (null);

			} else {

			if (controller != null)
			if (controller.state == CoroutineState.Running)
				controller = null;


			//	ExecuteEvents.Execute<IPointerExitHandler> (currentButton, data, ExecuteEvents.pointerExitHandler);
			//previousHitButton = currentButton;
			//	StopCoroutine("ButtonLook");
				//previousHitButton = null;
			//	hitButton = null;
			//StopCoroutine(buttonCoroutine);
				isLooking = false;
				countDown = timeToSelect;
				buttonFill.fillAmount = 1.0f;
				//previousHitButton = null;
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


			if (countDown < 0.0f){ //&& hitButton == currentButton) {

				//	if(!isMenuCope)
				//	ExecuteEvents.Execute<IPointerClickHandler> (hitButton, data, ExecuteEvents.pointerClickHandler);

				countDown = timeToSelect;
				buttonFill.fillAmount = 1.0f;
			

				//has to be called to prevent multiple clicks when clicking on button....

//FIXME ButtonOnClick clicks multiple times once it completes countdown.. It is not iterative it is one select				//StartCoroutine(DisableAndEnableCollider(col));



				//UnityEngine.Profiling.Profiler.BeginSample ("ButtonPress");
				if (isSMenuOpener) {

					StressMenu.SetActive (false);
					GameController.Instance.Paused = false;

					//	UnityEngine.Profiling.Profiler.EndSample ();
			
				} else if (isRestartProgress) {

					//PlayerPrefs.SetInt ("MainGameEvent") == 0;

					DataManager.Instance.DeleteHighScoreSlotandPositionData (homePosition);
					DataManager.Instance.DeletePPDataTaskProgress ();
					
					LoadScene ("IntroTimeLine");
					
			
			
			
				}/* else if (isAllowWalk) {
			
					isWalking = !isWalking;
					if (isWalking) {

						buttonFill.color = Color.green;
						GameController.Instance.Paused = false;
						GameObject.FindWithTag ("Player").GetComponent<PlayerLookMove> ().enabled = true;


					} else {

						buttonFill.color = Color.red;
						GameObject.FindWithTag ("Player").GetComponent<PlayerLookMove> ().enabled = false;
				
					}
			
			
				} */else if (isEnvChanger) {

					SceneController.Instance.ChangeSkyBox ();

				} else if (isBackToIntroButton) {


					if (PlayerManager.Instance != null && DataManager.Instance != null) {
						DataManager.Instance.CheckHighScore (SceneController.Instance.GetCurrentSceneName (), PlayerManager.Instance.points);
					}
					LoadScene ("Intro");

					//SceneController.Instance.ResetGame ("Intro");

			
				} else if (isStartButton) {

					GameController.Instance.StartGame ();
					GameController.Instance.Paused = false;
					


				} else if (isReplayButton)
					SceneController.Instance.ResetCurrentGame ();
				else if (isResetPositiontoHome) {

			
					SceneController.Instance.isCustomSavePosition = true;
					SceneController.Instance.customSavePosition = homePosition;

					LoadScene ("Intro");

				} else if (isMusicButton) {

					switch (curMusicButton) {

					case MusicPlayerButton.stop:
						//MusicController.Instance.StopM ();
						AudioManager.Instance.StopM();
						break;

					case MusicPlayerButton.next:
						//MusicController.Instance.PlayMusicNext ();
						AudioManager.Instance.PlayMusicNext();
						break;
				
					case MusicPlayerButton.previous:
					//	MusicController.Instance.PlayMusicPrevious ();
						AudioManager.Instance.PlayMusicPrevious();
						break;

					case MusicPlayerButton.play:
						//MusicController.Instance.PlayM ();
						AudioManager.Instance.PlayM();
						break;

					}


				} else if (isSType) { //&& !isMenuActive) {

					if (sAsses == null)
						sAsses = GameObject.FindWithTag ("StressAssess").GetComponent<SAssessment> ();
				
				
					switch (StressT) {

					case StressTypes.angry:
						sAsses.StressFeelChoice (SAssessment.TypeOfStress.angry);
						break;
						
					case StressTypes.anxious:
						sAsses.StressFeelChoice (SAssessment.TypeOfStress.anxious);
						break;
				

					case StressTypes.dissapointed:
						sAsses.StressFeelChoice (SAssessment.TypeOfStress.dissapointed);
						break;
					case StressTypes.frustrated:
						sAsses.StressFeelChoice (SAssessment.TypeOfStress.frustrated);
						break;


					case StressTypes.sad:
						sAsses.StressFeelChoice (SAssessment.TypeOfStress.sad);
						break;

					case StressTypes.worried:
						sAsses.StressFeelChoice (SAssessment.TypeOfStress.worried);
						break;

					case StressTypes.none:
						break;

					}

				} else if (isGame && !isSMenuOpener) {

					LoadScene (gameScene);
		

				}

				if (isButtonInvoke) 
					button.onClick.Invoke ();
				
				if (isClickEventCalled)
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


	

	




