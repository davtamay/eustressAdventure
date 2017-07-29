using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TagFrenzy;

public class ButtonClickLook : MonoBehaviour {
	//currently only have meditation on this event;
	[SerializeField]private UnityEvent OnClick;
	[SerializeField] bool isClickEventCalled = false;


	public bool isSMenuOpener = false;
	private Camera cam;
	private GameObject currentButton;
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


	public bool isAllowWalk = false;
	private bool isWalking = false;

	private Collider col;

	[SerializeField] private bool isStressModified;
	[SerializeField] private float stressModifiedAmount;



	public enum StressTypes{angry, anxious, dissapointed, frustrated, sad, worried, none};
	//public enum MenuCoping {breathing, counting, refraiming, music, paint, meditation, none};

	public enum MusicPlayerButton{play,stop,next,previous, none}
	public MusicPlayerButton curMusicButton;

	public StressTypes StressT;
	//public MenuCoping copingMethod;



	GameObject hitButton;
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

		currentButton = this.gameObject;

		//data = new PointerEventData (EventSystem.current);

		if (isAllowWalk)
			buttonFill.color = Color.red;


	}
	void Update(){

	
	//	if(!isSMenuOpener)
	//	if (GameController.Instance.IsInfoBubbleActive) 
	//		return;
		


		Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);


		RaycastHit hit;
		hitButton = null;


			if (col.Raycast (ray, out hit, 400)) {

			if (hit.transform.gameObject.CompareTag ("Button")) {

				if (!GameController.Instance.IsMenuActive) {
					
					hitButton = hit.transform.gameObject;

					StartCoroutine ("ButtonLook");


				} else{


					if (((1 << hit.transform.gameObject.layer) & 1 << 5) != 0) {	
			//		Debug.Log ("i hit layer:" + (hit.transform.gameObject.layer) + "Which is 5 (UI)");

					hitButton = hit.transform.gameObject;
					buttonFill = hitButton.GetComponent<Image> ();

					StartCoroutine ("ButtonLook");

					return;

					} 
				
				}
		//	}
			}

			} else {
		
			//	ExecuteEvents.Execute<IPointerExitHandler> (currentButton, data, ExecuteEvents.pointerExitHandler);
				countDown = timeToSelect;
				buttonFill.fillAmount = 1.0f;

			}

	}	

	IEnumerator ButtonLook(){
			
		// highlight

		//ExecuteEvents.Execute<IPointerEnterHandler> (hitButton, data, ExecuteEvents.pointerEnterHandler);
		countDown -= Time.unscaledDeltaTime;

		buttonFill.fillAmount = countDown / timeToSelect;


		if (countDown < 0.0f && hitButton == currentButton) {

		//	if(!isMenuCope)
		//	ExecuteEvents.Execute<IPointerClickHandler> (hitButton, data, ExecuteEvents.pointerClickHandler);

			countDown = timeToSelect;


		
				

	//UnityEngine.Profiling.Profiler.BeginSample ("ButtonPress");
			if (isSMenuOpener) {

				StressMenu.SetActive (false);
				GameController.Instance.Paused = false;
				//GetComponentInParent<OrientationAdjustment> ().ShowGame ();

					
			//	UnityEngine.Profiling.Profiler.EndSample ();
			
			} else if (isRestartProgress) {

				DataManager.Instance.DeleteHighScoreSlotandPositionData (homePosition);
				DataManager.Instance.DeletePPDataTaskProgress ();
				SceneController.Instance.Load ("Intro");
			
			
			
			}else if (isAllowWalk) {
			
				isWalking = !isWalking;
				if (isWalking) {

					buttonFill.color = Color.green;
					GameController.Instance.Paused = false;
					GameObject.FindWithTag ("Player").GetComponent<PlayerLookMove> ().enabled = true;
					//GameObject.FindWithTag ("Player").GetComponent<CharacterController> ().enabled = true;

				} else {

					buttonFill.color = Color.red;
					GameObject.FindWithTag ("Player").GetComponent<PlayerLookMove> ().enabled = false;
					//	GameObject.FindWithTag ("Player").GetComponent<CharacterController> ().enabled = false;
				
				}
			
			
			} else if (isEnvChanger) {

				SceneController.Instance.ChangeSkyBox ();

			} else if (isBackToIntroButton) {


				if (PlayerManager.Instance != null && DataManager.Instance != null) {
					DataManager.Instance.CheckHighScore (SceneController.Instance.GetCurrentSceneName (), PlayerManager.Instance.points);
				}
				SceneController.Instance.ResetGame ("Intro");
			
			} else if (isStartButton) {

				GameController.Instance.StartGame ();
				GameController.Instance.Paused = false;

				//SceneController.Instance.Load ("Intro");

			} else if (isReplayButton) {


				SceneController.Instance.ResetCurrentGame ();


				//	GetComponentInParent<DestressPopUp> ().HideDestress ();

			} else if (isResetPositiontoHome) {


				//DataManager.Instance.SavePosition (homePosition);
				SceneController.Instance.isCustomSavePosition = true;
				SceneController.Instance.customSavePosition = homePosition;

				SceneController.Instance.Load ("Intro");

				//GameObject.FindWithTag ("Player").transform.position = homePosition;

			}else if (isMusicButton) {

				switch (curMusicButton) {

				case MusicPlayerButton.stop:
					MusicController.Instance.StopM ();
					break;

				case MusicPlayerButton.next:
					MusicController.Instance.PlayMusicNext ();
					break;
				
				case MusicPlayerButton.previous:
					MusicController.Instance.PlayMusicPrevious ();
					break;

				case MusicPlayerButton.play:
					MusicController.Instance.PlayM ();
					break;

				


				}



			}else if (isSType){ //&& !isMenuActive) {

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

			} else if (isButtonInvoke && !isSMenuOpener) {
				
				button.onClick.Invoke ();
				/*switch (copingMethod) {

				case MenuCoping.breathing:
					button.onClick.Invoke ();
					break;

				case MenuCoping.counting:
					button.onClick.Invoke ();
					break;

				case MenuCoping.refraiming:
					button.onClick.Invoke ();
					break;
				
				case MenuCoping.music:
					button.onClick.Invoke ();
					break;

				case MenuCoping.paint:
					button.onClick.Invoke ();
					break;

				case MenuCoping.none:
					button.onClick.Invoke ();
					break;

				case MenuCoping.meditation:
					button.onClick.Invoke ();
					//button.onClick.Invoke ();
					break;
				}
						*/
			} else if (isGame && !isSMenuOpener) {

				LoadScene (gameScene);
				yield break;
		

			}

			if (isClickEventCalled)
				OnClick.Invoke ();

			buttonFill.fillAmount = 1.0f;
			yield return null;
		}
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


	

	




