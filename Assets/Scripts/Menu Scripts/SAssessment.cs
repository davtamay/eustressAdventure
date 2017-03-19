using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SAssessment : MonoBehaviour 
{
	
	public string stressFeel;
	public GameObject typeOfFeeling;

	public GameSuggest gSuggest;

	public TextAsset[] cognitiveRefraimingText;

	public GameObject cRObject;
	public RectTransform cRTextRect;
	public Text cRText;

	public FolowAllong folowAllong;




	public string storeText;

	public GameObject sMenu;

	public static SAssessment Instance
	{ get { return instance; } }

	private static SAssessment instance = null;

	void Awake()
	{

		cRObject = GameObject.FindWithTag ("CRText");
		folowAllong = cRObject.GetComponent<FolowAllong> ();
		folowAllong.messageLength = cognitiveRefraimingText [0].text.Length;

		//int widthExpand = cognitiveRefraimingText [0].text.Length;

		cRText = cRObject.GetComponentInChildren<Text> ();
		cRTextRect = cRText.GetComponent <RectTransform> ();
	//	cRTextRect.sizeDelta = new Vector2 (widthExpand * 11f, 50);

	//	cRText.text = cognitiveRefraimingText [0].text;

		//cRText = GameObject.FindWithTag ("CRText").GetComponentInChildren<Text> () as Text;
		//cRTextRect = cRText.GetComponent <RectTransform> ();
		//cRTextRect = (RectTransform)GameObject.FindWithTag ("CRText").GetComponent <RectTransform> ();
		//cRText = cRTextRect.GetComponent<Text> ();

		DontDestroyOnLoad (gameObject);


		if (instance) {
			DestroyImmediate (gameObject);
			return;
		}
		instance = this; 

	//	SceneManager.sceneLoaded += OnLevelWasLoad;
	}


		
	public void Start(){

		sMenu = GameObject.FindWithTag ("StressMenu");



		if (string.Equals (SceneManager.GetActiveScene ().name, "StressAss", System.StringComparison.CurrentCultureIgnoreCase)) {
			typeOfFeeling = GameObject.FindWithTag ("TypeOfFeeling");
			typeOfFeeling.SetActive (true);
			gSuggest = (GameSuggest)GameObject.FindWithTag ("GameSuggest").GetComponent<GameSuggest> ();
		}
	




	}
	

	public void OnLevelWasLoad(){


		cRObject = GameObject.FindWithTag ("CRText");
		folowAllong = cRObject.GetComponent<FolowAllong> ();
		folowAllong.messageLength = cognitiveRefraimingText [0].text.Length;

		cRText = cRObject.GetComponentInChildren<Text> () as Text;
		cRTextRect = cRText.GetComponent <RectTransform> ();
		cRText.text = storeText;

		int widthExpand = storeText.Length;

		cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);

		sMenu = GameObject.FindWithTag ("StressMenu");
		sMenu.SetActive (false);

				if (string.Equals (SceneManager.GetActiveScene ().name, "StressAss", System.StringComparison.CurrentCultureIgnoreCase)) {
			GameController.Instance.Paused = false;
		
			if (typeOfFeeling == null) {
				typeOfFeeling = GameObject.FindWithTag ("TypeOfFeeling");
				typeOfFeeling.SetActive (true);
			}

			if (gSuggest == null) 
				gSuggest = (GameSuggest)GameObject.FindWithTag ("GameSuggest").GetComponent<GameSuggest> ();

		}else GameController.Instance.Paused = true;
			

	}

	public string GetStressFeelType
	{
		get {return stressFeel;}
		private set {stressFeel = value;}
	}


//Type of Stress

	public enum TypeOfStress{angry, anxious, calm, dissapointed, frustrated, happy, sad, worried};




	public void StressFeelChoice(TypeOfStress typeOfStress){

		int widthExpand = 0;

		switch (typeOfStress) {

		case TypeOfStress.angry:
			stressFeel = "Angry";

			widthExpand = cognitiveRefraimingText [0].text.Length;
			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);

			folowAllong.messageLength = cognitiveRefraimingText [0].text.Length;
		

			cRText.text = cognitiveRefraimingText [0].text;

			storeText = cRText.text;
			break;


		case TypeOfStress.anxious:
			stressFeel = "Anxious";
		
			widthExpand = cognitiveRefraimingText [1].text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cognitiveRefraimingText [1].text.Length;

			cRText.text = cognitiveRefraimingText [1].text;

			break;

		case TypeOfStress.sad:
			stressFeel = "Sad";

			widthExpand = cognitiveRefraimingText [2].text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cognitiveRefraimingText [2].text.Length;

			cRText.text = cognitiveRefraimingText [2].text;

			break;	

		
		case TypeOfStress.dissapointed:
			stressFeel = "Disappointed";

			widthExpand = cognitiveRefraimingText [3].text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cognitiveRefraimingText [3].text.Length;

			cRText.text = cognitiveRefraimingText [3].text;

			break;	

		case TypeOfStress.frustrated:
			stressFeel = "Frustrated";

			widthExpand = cognitiveRefraimingText [4].text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cognitiveRefraimingText [4].text.Length;

			cRText.text = cognitiveRefraimingText [4].text;


			break;
		

		case TypeOfStress.worried:
			stressFeel = "Worried";

			widthExpand = cognitiveRefraimingText [5].text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cognitiveRefraimingText [5].text.Length;

			cRText.text = cognitiveRefraimingText [5].text;

			break;	

		
		}
		typeOfFeeling.SetActive (false);

		gSuggest.SelectGames (stressFeel);
		
	}
		
		
		
		



}