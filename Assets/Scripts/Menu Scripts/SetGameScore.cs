using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Game {FINDER, WACK, SKYJUMPER, MATCH, HIT, COLLECTIONS, SHOOT, HOOP}

public class SetGameScore : MonoBehaviour {


	public Game curGame;
	private TextMesh curTextMesh;
	private Text curText; 

	[SerializeField] private bool isUsingTextMesh;


	void Start () {

		if (isUsingTextMesh) {
			curTextMesh = GetComponent<TextMesh> ();
			switch (curGame) {

			case Game.FINDER:
				curTextMesh.text = "HighScore: " + DataManager.Instance.LoadFinderScore ();
				break;

			case Game.SKYJUMPER:
				curTextMesh.text = "HighScore: " + DataManager.Instance.LoadSkyWalkerScore ();
				break;

			case Game.COLLECTIONS:
				curTextMesh.text = "HighScore: " + DataManager.Instance.LoadCollectionsScore ();
				break;

			case Game.WACK:
				curTextMesh.text = "HighScore: " + DataManager.Instance.LoadWackScore ();
				break;
			case Game.MATCH:
				curTextMesh.text = "HighScore: " + DataManager.Instance.LoadMatchScore ();
				break;

			case Game.SHOOT:
				curTextMesh.text = "HighScore: " + DataManager.Instance.LoadShootScore ();
				break;

			case Game.HIT:
				curTextMesh.text = "HighScore: " + DataManager.Instance.LoadHitScore ();
				break;

			case Game.HOOP:
				curTextMesh.text = "HighScore: " + DataManager.Instance.LoadHoopScore ();
				break;
			}


		} else {
			curText = GetComponent<Text> ();
			switch (curGame) {

			case Game.FINDER:
				curText.text = "HighScore: " + DataManager.Instance.LoadFinderScore();
				break;

			case Game.SKYJUMPER:
				curText.text = "HighScore: " + DataManager.Instance.LoadSkyWalkerScore();
				break;

			case Game.COLLECTIONS:
				curText.text = "HighScore: " + DataManager.Instance.LoadCollectionsScore();
				break;

			case Game.WACK:
				curText.text = "HighScore: " + DataManager.Instance.LoadWackScore();
				break;
			case Game.MATCH:
				curText.text = "HighScore: " + DataManager.Instance.LoadMatchScore();
				break;

			case Game.SHOOT:
				curTextMesh.text = "HighScore: " + DataManager.Instance.LoadShootScore();
				break;

			case Game.HIT:
				curText.text = "HighScore: " + DataManager.Instance.LoadHitScore();
				break;

			case Game.HOOP:
				curText.text = "HighScore: " + DataManager.Instance.LoadHoopScore();
				break;



		}
		
		
		
		}


	}

}
