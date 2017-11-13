using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SAssessment : MonoBehaviour
{
	
	public string stressFeel;

	public CognitiveRefraimingText cRTextSO;

	public GameObject cRObject;
	public RectTransform cRTextRect;

	public Text[] cRText;

	public FolowAllong folowAllong;

	public string storeText;


	public static SAssessment Instance
	{ get { return instance; } }

	private static SAssessment instance = null;

	void Awake()
	{
	


			if (instance) {
				DestroyImmediate (gameObject);
				return;
			}
			instance = this; 

			DontDestroyOnLoad (gameObject);
		
	
	}
	void Start(){

		if (GameObject.FindWithTag ("CRText") != null) {
			cRObject = GameObject.FindWithTag ("CRText").transform.GetChild (0).gameObject;
			folowAllong = cRObject.GetComponent<FolowAllong> ();
			folowAllong.messageLength = cRTextSO.angry.text.Length;


			cRText = cRObject.GetComponentsInChildren<Text> (true);

			cRTextRect = cRText [0].gameObject.GetComponent <RectTransform> ();

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

			widthExpand = cRTextSO.angry.text.Length;
			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);

			folowAllong.messageLength = cRTextSO.angry.text.Length;
		

			cRText[0].text = cRTextSO.angry.text;

			storeText = cRText[0].text;
			break;


		case TypeOfStress.anxious:
			stressFeel = "Anxious";
		
			widthExpand = cRTextSO.anxious.text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cRTextSO.anxious.text.Length;

			cRText[0].text = cRTextSO.anxious.text;

			break;

		case TypeOfStress.sad:
			stressFeel = "Sad";

			widthExpand = cRTextSO.sad.text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cRTextSO.sad.text.Length;

			cRText[0].text = cRTextSO.sad.text;

			break;	

		
		case TypeOfStress.dissapointed:
			stressFeel = "Disappointed";

			widthExpand = cRTextSO.dissapointed.text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cRTextSO.dissapointed.text.Length;

			cRText[0].text = cRTextSO.dissapointed.text;

			break;	

		case TypeOfStress.frustrated:
			stressFeel = "Frustrated";

			widthExpand = cRTextSO.frustrated.text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cRTextSO.frustrated.text.Length;

			cRText[0].text = cRTextSO.frustrated.text;


			break;
		

		case TypeOfStress.worried:
			stressFeel = "Worried";

			widthExpand = cRTextSO.worried.text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cRTextSO.worried.text.Length;

			cRText[0].text = cRTextSO.worried.text;

			break;	

		
		}

		
	}
		
		
		
		



}