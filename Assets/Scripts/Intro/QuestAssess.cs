using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestAssess : MonoBehaviour {

	[SerializeField] string[] playerPrefTaskNames;
	[SerializeField] string[] questDescriptions;

	private Dictionary<string,string> taskDictionary;
	private Text[] qTextSpaces;

	public static QuestAssess Instance
	{ get { return instance; } }

	private static QuestAssess instance = null;

	 void Awake(){
		//PlayerPrefs.DeleteAll ();
		if (instance) {
			DestroyImmediate (gameObject);
			return;
		}
		instance = this; 


		taskDictionary = new Dictionary<string, string>();

			for(int i = 0; i < playerPrefTaskNames.Length; i++)
			taskDictionary.Add (playerPrefTaskNames[i], questDescriptions[i]);// [tn].aquestDescriptions;

		int e = 0;
		qTextSpaces = new Text[transform.childCount];

		foreach (Transform child in transform) {

			qTextSpaces [e] = child.GetComponent<Text>();
			e++;

		}
		Debug.Log("TEXT SPASES: " + qTextSpaces.Length);
	}

	int count = 0;


	void Start(){

		OnUpdate ();
	
	}

	public void OnUpdate(){
		 count = 0;
		//for(int i = 0; i < qTextSpaces.Length; i++)
		//	qTextSpaces[i].text = string.Empty;

		foreach (string pPN in taskDictionary.Keys) {

			if (EvaluatePlayerPref (pPN))
				count++;

		}	

	}

	bool EvaluatePlayerPref(string pPName){

		if (!PlayerPrefs.HasKey (pPName)) {
			Debug.Log ("PlayerPrefAssess: there is no " + pPName);
		//	qTextSpaces [count].text = string.Empty;
			return false;
		}else{
		
			if (PlayerPrefs.GetInt (pPName) == 0) {
				Debug.Log ("PlayerPrefAssess: have but not completed " + pPName);
				qTextSpaces [count].text = taskDictionary [pPName];//questDescriptions [qID];
				qTextSpaces [count].color = Color.gray;

			} else if (PlayerPrefs.GetInt (pPName) == 1) {
				Debug.Log ("PlayerPrefAssess: completed " + pPName);
				qTextSpaces [count].text = taskDictionary [pPName];//questDescriptions [qID];
				qTextSpaces [count].color = Color.green;


			}
			return true;
		}
	
	
	}
}
