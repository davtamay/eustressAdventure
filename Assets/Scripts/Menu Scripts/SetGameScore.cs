using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Game {FINDER, WACK, SKYJUMPER, MATCH, HIT, COLLECTIONS, SHOOT, HOOP}

public class SetGameScore : MonoBehaviour {


	//public Game curGame;
	private TextMesh curTextMesh;
	[SerializeField]private Text curText; 

	[SerializeField] private bool isUsingTextMesh;

	[SerializeField] private string sceneToLoadScoreFrom;

	[Header("References")]
	[SerializeField]private DataManager DATA_MANAGER;


	void Awake(){


		if (isUsingTextMesh)
			curTextMesh = GetComponent<TextMesh> ();
		else
			curText = GetComponent<Text> ();



	}

	void OnEnable(){
	 
		if (isUsingTextMesh) {
			if(curTextMesh == null)
			curTextMesh = GetComponent<TextMesh> ();


			curTextMesh.text = "HighScore:" + DATA_MANAGER.LoadScore(sceneToLoadScoreFrom);
//			switch (curSceneName) {
//
//			case "Game.FINDER":
//				curTextMesh.text = "HighScore: " + DATA_MANAGER.LoadFinderScore ();
//				break;
//
//			case "SkyFlight":
//				curTextMesh.text = "HighScore: " + DATA_MANAGER.LoadSkyWalkerScore ();
//				break;
//
//			case "Collections":
//				curTextMesh.text = "HighScore: " + DATA_MANAGER.LoadCollectionsScore ();
//				break;
//
//			case "BerryScary":
//				curTextMesh.text = "HighScore: " + DATA_MANAGER.LoadWackScore ();
//				break;
//			case "Match":
//				curTextMesh.text = "HighScore: " + DATA_MANAGER.LoadMatchScore ();
//				break;
//
//			case "Shoot":
//				curTextMesh.text = "HighScore: " + DATA_MANAGER.LoadShootScore ();
//				break;
//
//			case "Hit":
//				curTextMesh.text = "HighScore: " + DATA_MANAGER.LoadHitScore ();
//				break;
//
//			case "Hoops":
//				curTextMesh.text = "HighScore: " + DATA_MANAGER.LoadHoopScore ();
//				break;
//			}


		} else {

			if(curText == null)
			curText = GetComponent<Text> ();


			curText.text = "HighScore:" + DATA_MANAGER.LoadScore(sceneToLoadScoreFrom);


			//	Debug.LogWarning ("Set Game Score is not registering Text component on Enable");
//			switch (curSceneName) {
//			case "Game.FINDER":
//				curText.text = "HighScore: " + DATA_MANAGER.LoadFinderScore ();
//				break;
//
//			case "SkyFlight":
//				curText.text = "HighScore: " + DATA_MANAGER.LoadSkyWalkerScore ();
//				break;
//
//			case "Collections":
//				curText.text = "HighScore: " + DATA_MANAGER.LoadCollectionsScore ();
//				break;
//
//			case "BerryScary":
//				curText.text = "HighScore: " + DATA_MANAGER.LoadWackScore ();
//				break;
//			case "Match":
//				curText.text = "HighScore: " + DATA_MANAGER.LoadMatchScore ();
//				break;
//
//			case "Shoot":
//				curText.text = "HighScore: " + DATA_MANAGER.LoadShootScore ();
//				break;
//
//			case "Hit":
//				curText.text = "HighScore: " + DATA_MANAGER.LoadHitScore ();
//				break;
//
//			case "Hoops":
//				curText.text = "HighScore: " + DATA_MANAGER.LoadHoopScore ();
//				break;
//
//		}
		
		
		
		}


	}

}
