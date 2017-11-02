using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SAssessment : MonoBehaviour
{
	
	public string stressFeel;

	public TextAsset[] cognitiveRefraimingText;

	public GameObject cRObject;
	public RectTransform cRTextRect;
	public Text[] cRText;

	public FolowAllong folowAllong;

	public string storeText;

	public GameObject sMenu;

	public static SAssessment Instance
	{ get { return instance; } }

	private static SAssessment instance = null;

	void Awake()
	{
	
		if (GameObject.FindWithTag ("CRText") != null) {
			cRObject = GameObject.FindWithTag ("CRText").transform.GetChild (0).gameObject;
			folowAllong = cRObject.GetComponent<FolowAllong> ();
			folowAllong.messageLength = cognitiveRefraimingText [0].text.Length;


			cRText = cRObject.GetComponentsInChildren<Text> (true);
			cRTextRect = cRText [0].gameObject.GetComponent <RectTransform> ();
	
		}

			if (instance) {
				DestroyImmediate (gameObject);
				return;
			}
			instance = this; 

			DontDestroyOnLoad (gameObject);
		
	
	}


	public void OnLevelLoad(){

		if (GameObject.FindWithTag ("CRText") != null) {
			cRObject = GameObject.FindWithTag ("CRText").transform.GetChild (0).gameObject;
			folowAllong = cRObject.GetComponent<FolowAllong> ();
			folowAllong.messageLength = cognitiveRefraimingText [0].text.Length;

			cRText = cRObject.GetComponentsInChildren<Text> (true);
			cRTextRect = cRText [0].gameObject.GetComponent <RectTransform> ();
			cRText [0].text = storeText;

			int widthExpand = storeText.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);


		}
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
		

			cRText[0].text = cognitiveRefraimingText [0].text;

			storeText = cRText[0].text;
			break;


		case TypeOfStress.anxious:
			stressFeel = "Anxious";
		
			widthExpand = cognitiveRefraimingText [1].text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cognitiveRefraimingText [1].text.Length;

			cRText[0].text = cognitiveRefraimingText [1].text;

			break;

		case TypeOfStress.sad:
			stressFeel = "Sad";

			widthExpand = cognitiveRefraimingText [2].text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cognitiveRefraimingText [2].text.Length;

			cRText[0].text = cognitiveRefraimingText [2].text;

			break;	

		
		case TypeOfStress.dissapointed:
			stressFeel = "Disappointed";

			widthExpand = cognitiveRefraimingText [3].text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cognitiveRefraimingText [3].text.Length;

			cRText[0].text = cognitiveRefraimingText [3].text;

			break;	

		case TypeOfStress.frustrated:
			stressFeel = "Frustrated";

			widthExpand = cognitiveRefraimingText [4].text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cognitiveRefraimingText [4].text.Length;

			cRText[0].text = cognitiveRefraimingText [4].text;


			break;
		

		case TypeOfStress.worried:
			stressFeel = "Worried";

			widthExpand = cognitiveRefraimingText [5].text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cognitiveRefraimingText [5].text.Length;

			cRText[0].text = cognitiveRefraimingText [5].text;

			break;	

		
		}

		
	}
		
		
		
		



}