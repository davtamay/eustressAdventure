using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class GameTimer : MonoBehaviour {

	private Text timerText;
	private bool isDone;
	//private string timer;

	// Use this for initialization
	void Start () {
		timerText = GetComponent<Text> ();
		timerText.text = GameController.Instance.TimeToAdd(ref isDone);
	}
	
	// Update is called once per frame
	void Update () {
		timerText.text = GameController.Instance.TimeToAdd(ref isDone);


		if (isDone) {
			timerText.text = "TimeUp!";
			isDone = false;
		}

	}
}
