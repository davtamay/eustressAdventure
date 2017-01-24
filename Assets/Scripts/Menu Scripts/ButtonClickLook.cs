using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TagFrenzy;

public class ButtonClickLook : MonoBehaviour {

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
	public bool isMenuCope = false;
	public bool isEnvChanger = false;
	public bool isGame = false;
	public bool isBackButton = false;
	public bool isStartButton = false;
	public bool isReplayButton = false;
	public bool isMusicButton = false;

	private Collider col;

	public enum Stressed{yes,no};
	public enum StressTypes{angry, anxious, dissapointed, frustrated, sad, worried, none};
	public enum MenuCoping {breathing, counting, refraiming, music,paint, none};
	public enum Games{skyjumper, collections, hit, finder, match, wack, hoops, shoot, none};

	public enum MusicPlayerButton{play,stop,next,previous, none}

	public Stressed curStressed;
	public StressTypes StressT;
	public MenuCoping copingMethod;
	public Games games;
	public MusicPlayerButton curMusicButton;

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

		cam = GameObject.FindWithTag ("MainCamera").GetComponent<Camera>();

		currentButton = this.gameObject;

		data = new PointerEventData (EventSystem.current);

	//	if (sAsses == null)
	//		sAsses = GameObject.FindWithTag ("StressAssess").GetComponent<SAssessment> ();
		


	}
	void Update(){

		if (GameController.Instance.IsInfoBubbleActive)
			return;

		Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);


		RaycastHit hit;
		hitButton = null;


			if (col.Raycast (ray, out hit, 400)) {

			if (hit.transform.gameObject.CompareTag ("Button")) {

				if (!GameController.Instance.IsMenuActive) {
					
					hitButton = hit.transform.gameObject;
					buttonFill = hitButton.GetComponent<Image> ();

					StartCoroutine ("ButtonLook");


				} else{


					if (((1 << hit.transform.gameObject.layer) & 1 << 5) != 0) {	
			//		Debug.Log ("i hit layer:" + (hit.transform.gameObject.layer) + "Which is 5 (UI)");

					hitButton = hit.transform.gameObject;
					buttonFill = hitButton.GetComponent<Image> ();

					StartCoroutine ("ButtonLook");

					return;

					} else {
					
				//		Debug.Log ("i hit layer:" + (hit.transform.gameObject.layer) + "Which is 0 (default)");


					}
				
				}
		//	}
			}

			} else {
		
				ExecuteEvents.Execute<IPointerExitHandler> (currentButton, data, ExecuteEvents.pointerExitHandler);
				countDown = timeToSelect;
				buttonFill.fillAmount = 1.0f;

			}

	}	

	IEnumerator ButtonLook(){
			
		// highlight

		ExecuteEvents.Execute<IPointerEnterHandler> (hitButton, data, ExecuteEvents.pointerEnterHandler);
		countDown -= Time.unscaledDeltaTime;

		buttonFill.fillAmount = countDown / timeToSelect;


		if (countDown < 0.0f && hitButton == currentButton) {

			if(!isMenuCope)
			ExecuteEvents.Execute<IPointerClickHandler> (hitButton, data, ExecuteEvents.pointerClickHandler);

			countDown = timeToSelect;


		
				

	UnityEngine.Profiling.Profiler.BeginSample ("ButtonPress");
			if(isStressed){
				switch (curStressed) {

				case Stressed.yes:
					SceneController.Instance.Load ("StressAss");

					break;

				case Stressed.no:
					SceneController.Instance.Load ("AllGames");

					break;



				}
				
			}else if (isSMenuOpener) {

				StressMenu.SetActive (false);
				GameController.Instance.Paused = false;
				GetComponentInParent<DestressPopUp> ().HideDestress ();

					
	UnityEngine.Profiling.Profiler.EndSample ();
			} else if (isEnvChanger) {

				SceneController.Instance.ChangeSkyBox ();

			} else if (isBackButton) {


				if (PlayerManager.Instance != null && HighScoreManager.Instance != null) {
					HighScoreManager.Instance.CheckHighScore (SceneController.Instance.GetCurrentSceneName(), PlayerManager.Instance.points);
				}
				SceneController.Instance.ResetGame ("Intro");
			
			}else if (isStartButton) {

				GameController.Instance.StartGame ();
				GameController.Instance.Paused = false;


			}else if (isReplayButton) {

				SceneController.Instance.ResetCurrentGame ();


			} else if (isMusicButton) {

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

			} else if (isMenuCope && !isSMenuOpener) {
				
				
				switch (copingMethod) {

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
				}
						
			} else if (isGame && !isSMenuOpener) {


				switch (games) {

				case Games.skyjumper:

					SceneController.Instance.Load ("SkyJumper");
					break;

				case Games.collections:
					SceneController.Instance.Load ("Collections");
					break;
				
				case Games.hit:
					SceneController.Instance.Load ("Hit");
					break;

				case Games.finder:
					SceneController.Instance.Load ("Finder");
					break;

				case Games.match:
					SceneController.Instance.Load ("Match");
					break;
				
				case Games.wack:
					SceneController.Instance.Load ("Wack");
					break;
				
				case Games.hoops:
					SceneController.Instance.Load ("Hoops");
					break;

				case Games.shoot:
					SceneController.Instance.Load ("Shoot");
					break;
				}

			}

			buttonFill.fillAmount = 1.0f;
			yield return null;
		}
	}
		
			
	}


	

	




