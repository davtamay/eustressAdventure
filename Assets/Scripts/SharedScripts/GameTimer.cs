using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class GameTimer : MonoBehaviour {

	private Text timerText;
	private bool isDone;

	[SerializeField] private bool isShowTextWhenDone = true;
	[SerializeField] private string textToShow = "TimeUp!";
	[SerializeField] private bool isPauseGameWhenDone = false;

	//private string timer;

	// Use this for initialization
	void Start () {
		timerText = GetComponent<Text> ();
		timerText.text = GameController.Instance.TimeToAdd(ref isDone);
		StartCoroutine (OnUpdate ());
	}
	
	IEnumerator OnUpdate(){

		bool isMediumWarningOn = false;
		bool isHardWarningOn = false;

		while (true) {
		
			timerText.text = GameController.Instance.TimeToAdd(ref isDone);

			if (GameController.Instance.GetCurrentTime () <= 30f && !isMediumWarningOn) {
				timerText.color = Color.yellow;
				isMediumWarningOn = true;
			} else if (GameController.Instance.GetCurrentTime () <= 10f && !isHardWarningOn) {
				timerText.color = Color.red;
				isHardWarningOn = true;
			}




				

			if (isDone) {

				if (isShowTextWhenDone)
					timerText.text = textToShow;

				if (isPauseGameWhenDone)
					GameController.Instance.Paused = true;
					
				isDone = false;
			}
		
		
			yield return new WaitForSeconds (0.2f);
		}
		isMediumWarningOn = false;
		isHardWarningOn = false;
	
	
	}
/*	void Update () {
		timerText.text = GameController.Instance.TimeToAdd(ref isDone);


		if (isDone) {
			timerText.text = "TimeUp!";
			isDone = false;
		}

	}*/
}
