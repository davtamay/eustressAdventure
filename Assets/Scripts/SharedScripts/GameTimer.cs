using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class GameTimer : MonoBehaviour {

	private Text timerText;
	private Color originalColor;

	private bool isDone;

	[SerializeField] private bool isShowTextWhenDone = true;
	[SerializeField] private string textToShow = "TimeUp!";
	[SerializeField] private bool isPauseGameWhenDone = false;

	//private string timer;

	// Use this for initialization
	void Start () {
		timerText = GetComponent<Text> ();
		originalColor = timerText.color;
		timerText.text = GameController.Instance.TimeToAdd(ref isDone);
		StartCoroutine (OnUpdate ());
	}

	public void SetUpcomingTimerDoneTextToShow(string text){
	
		textToShow = text;
	
	}

	public void SetGameOver(string text){

		GameController.Instance.StopTimer ();
		timerText.color = originalColor;
		timerText.text = text;
		StopAllCoroutines ();

	}
	
	IEnumerator OnUpdate(){


		while (true) {
		

			timerText.text = GameController.Instance.TimeToAdd(ref isDone);
		

			timerText.color = originalColor;

			if (GameController.Instance.GetCurrentTime () <= 10f )
				timerText.color = Color.red;
			else if (GameController.Instance.GetCurrentTime () <= 25f)
				timerText.color = Color.yellow;


			if (isDone) {

				if (isShowTextWhenDone) {
					timerText.color = originalColor;
					timerText.text = textToShow;

				}

				if (isPauseGameWhenDone)
					GameController.Instance.Paused = true;
					
				isDone = false;
			}
		
		
			yield return new WaitForSeconds (0.2f);
		}

	
	
	}
/*	void Update () {
		timerText.text = GameController.Instance.TimeToAdd(ref isDone);


		if (isDone) {
			timerText.text = "TimeUp!";
			isDone = false;
		}

	}*/
}
