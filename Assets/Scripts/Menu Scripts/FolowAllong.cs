using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//public enum TypeOfStress{ANGRY,ANXIOUS,DISAPOINTED, FRUSTRATED, SAD, WORRIED};
public class FolowAllong : MonoBehaviour {

	public int messageLength;
	public float scrollSpeed = 0.05f;

	public Scrollbar scrollb;

	public string stressFeel;

	public CognitiveRefraimingText cRTextSO;

	//public GameObject cRObject;
	public RectTransform cRTextRect;

	public Text[] cRText;

	public FolowAllong folowAllong;

	public string storeText;



	void Start () {

		scrollb = GetComponentInChildren <Scrollbar>();
		scrollb.value = 0;


			folowAllong = GetComponent<FolowAllong> ();
			folowAllong.messageLength = cRTextSO.angry.text.Length;
			cRText = GetComponentsInChildren<Text> (true);
			cRTextRect = cRText [0].gameObject.GetComponent <RectTransform> ();




	
	}
	

	void Update () {

	//	if (messageLength > 300)
	//		scrollSpeed = 9;
	//	else if (messageLength > 200)
	//		scrollSpeed = 8;


		scrollb.value += (scrollSpeed/ messageLength) * Time.unscaledDeltaTime   ;//Mathf.Lerp (0, 1, Time.unscaledDeltaTime * (speed));
		//scrollb.value = (scrollb.value* messageLength) *  scrollSpeed   ;

		if (scrollb.value == 1)
			scrollb.value = 0;
			
	
	}

	public void StressFeelChoice(string typeOfStress){

		int widthExpand = 0;

		switch (typeOfStress) {

		case "ANGRY":
			stressFeel = "Angry";

			widthExpand = cRTextSO.angry.text.Length;
			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);

			folowAllong.messageLength = cRTextSO.angry.text.Length;


			cRText[0].text = cRTextSO.angry.text;

			storeText = cRText[0].text;
			break;


		case "ANXIOUS":
			stressFeel = "Anxious";

			widthExpand = cRTextSO.anxious.text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cRTextSO.anxious.text.Length;

			cRText[0].text = cRTextSO.anxious.text;

			break;

		case "SAD":
			stressFeel = "Sad";

			widthExpand = cRTextSO.sad.text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cRTextSO.sad.text.Length;

			cRText[0].text = cRTextSO.sad.text;

			break;	


		case "DISAPPOINTED":
			stressFeel = "Disappointed";

			widthExpand = cRTextSO.dissapointed.text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cRTextSO.dissapointed.text.Length;

			cRText[0].text = cRTextSO.dissapointed.text;

			break;	

		case "FRUSTRATED":
			stressFeel = "Frustrated";

			widthExpand = cRTextSO.frustrated.text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cRTextSO.frustrated.text.Length;

			cRText[0].text = cRTextSO.frustrated.text;


			break;


		case "WORRIED":
			stressFeel = "Worried";

			widthExpand = cRTextSO.worried.text.Length;

			cRTextRect.sizeDelta = new Vector2 (widthExpand * 10f, 50);
			folowAllong.messageLength = cRTextSO.worried.text.Length;

			cRText[0].text = cRTextSO.worried.text;

			break;	


		}


	}
}
