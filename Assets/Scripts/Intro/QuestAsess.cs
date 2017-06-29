using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestAsess : MonoBehaviour {

	[SerializeField] string[] questDescriptions;
	private Text[] qTextSpaces;

	void Awake(){
		int i = 0;
		qTextSpaces = new Text[transform.childCount];

		foreach (Transform child in transform) {

			qTextSpaces [i] = child.GetComponent<Text>();
			i++;

		}
		Debug.Log("TEXT SPASES: " + qTextSpaces.Length);
	}

	int count = 0;
	void Start(){

		foreach (Text qS in qTextSpaces) {

			if (EvaluatePlayerPref ("test2", 0))
				count++;

		}
	
	
	}

	bool EvaluatePlayerPref(string pPName, int qID){

		if (!PlayerPrefs.HasKey (pPName)) {
			Debug.Log ("PlayerPrefAssess: there is no " + pPName);
			return false;
		}else{
		
			if (PlayerPrefs.GetInt (pPName) == 0) {
				Debug.Log ("PlayerPrefAssess: have but not completed " + pPName);
				qTextSpaces [count].text = questDescriptions [qID];
				qTextSpaces [count].color = Color.red;

			} else if (PlayerPrefs.GetInt (pPName) == 1) {
				Debug.Log ("PlayerPrefAssess: completed " + pPName);
				qTextSpaces [count].text = questDescriptions [qID];
				qTextSpaces [count].color = Color.green;


			}
			return true;
		}
	
	
	}
}
