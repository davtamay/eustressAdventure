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
		//timerText.text = GameController.Instance.TimeToAdd(ref isDone);
		timerText.text = WaveManager.Instance.TimeToAdd (ref isDone);
		StartCoroutine (OnUpdate ());
	}

	public void SetUpcomingTimerDoneTextToShow(string text){
	
		textToShow = text;
	
	}

	public void SetGameOver(string text){

		WaveManager.Instance.StopTimer ();
		//GameController.Instance.StopTimer ();

		timerText.color = originalColor;
		timerText.text = text;
		StopAllCoroutines ();

	}
	
	IEnumerator OnUpdate(){


		while (true) {
		
			timerText.text = WaveManager.Instance.TimeToAdd(ref isDone);
			//timerText.text = GameController.Instance.TimeToAdd(ref isDone);
		

			timerText.color = originalColor;
			if (WaveManager.Instance.GetCurrentTime() <= 10f)
		//	if (GameController.Instance.GetCurrentTime () <= 10f )
				timerText.color = Color.red;

			else if (WaveManager.Instance.GetCurrentTime () <= 25f)
		//	else if (GameController.Instance.GetCurrentTime () <= 25f)
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

}
