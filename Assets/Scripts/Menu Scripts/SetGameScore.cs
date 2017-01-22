using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Game {FINDER, WACK, SKYJUMPER, MATCH, HIT, COLLECTIONS, SHOOT, HOOP}

public class SetGameScore : MonoBehaviour {


	public Game curGame;
	private Text curText;

	void Start () {

		curText = GetComponent<Text> ();

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


		
		
		
		
		}


	}

}
