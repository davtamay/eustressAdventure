using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Game {FINDER, WACK, SKYJUMPER, MATCH, HIT, COLLECTIONS, SHOOT, HOOP}

public class SetGameScore : MonoBehaviour {


	public Game curGame;
	private TextMesh curText;


	void Start () {

		curText = GetComponent<TextMesh> ();

		switch (curGame) {
		
		case Game.FINDER:
			curText.text = "HighScore: " + HighScoreManager.Instance.LoadFinderScore();
			break;

		case Game.SKYJUMPER:
			curText.text = "HighScore: " + HighScoreManager.Instance.LoadSkyWalkerScore();
			break;

		case Game.COLLECTIONS:
			curText.text = "HighScore: " + HighScoreManager.Instance.LoadCollectionsScore();
			break;

		case Game.WACK:
			curText.text = "HighScore: " + HighScoreManager.Instance.LoadWackScore();
			break;
		case Game.MATCH:
			curText.text = "HighScore: " + HighScoreManager.Instance.LoadMatchScore();
			break;

		case Game.SHOOT:
			curText.text = "HighScore: " + HighScoreManager.Instance.LoadShootScore();
			break;

		case Game.HIT:
			curText.text = "HighScore: " + HighScoreManager.Instance.LoadHitScore();
			break;

		case Game.HOOP:
			curText.text = "HighScore: " + HighScoreManager.Instance.LoadHoopScore();
			break;


		
		
		
		
		}


	}

}
