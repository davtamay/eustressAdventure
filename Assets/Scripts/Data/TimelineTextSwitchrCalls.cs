using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TimelineTextSwitchrCalls : MonoBehaviour {

	private Text textComponent;
	private string originalText;
	private string curText;
	private string oldText;
	[SerializeField] private UnityEvent onUnpause;


	//public RangeInt value;
	void Start(){

		textComponent = GetComponent<Text> ();
		originalText = textComponent.text;
		curText = originalText;

		StartCoroutine (OnUpdate ());
	}
	[SerializeField]private bool isActive;
	IEnumerator OnUpdate(){

		while (true) {
			yield return null;

			if (isActive)
				continue;

			curText = textComponent.text;

			Debug.Log ("THISISCALLEDTIMELINE");
		//HAVE TO TRANVERSE TEXT WAIT WITH WAIT1 IN TIMELINE TO BE ABLE TO STOP AND NOT BE CALLED MULTIPLE TIMES
			if (string.Equals (originalText, curText, System.StringComparison.CurrentCultureIgnoreCase)){
				continue;
		
			}else{
		
				if (string.Equals (curText, oldText, System.StringComparison.CurrentCultureIgnoreCase))
					continue;


				oldText = textComponent.text;

				switch (textComponent.text) {

				case "Wait":

					yield return StartCoroutine (Wait ());

					break;
				case "Wait1":

					yield return StartCoroutine (Wait ());

					break;
				
				}


			}
			

		}
	}
	void OnDestroy(){

		StopAllCoroutines ();
	}

	IEnumerator Wait (){
		isActive = true;
		TimeLineController.Instance.StopTimeLine ();

		yield return new WaitForSecondsRealtime (5);

		onUnpause.Invoke();
		TimeLineController.Instance.ResumeTimeLine ();

		isActive = false;
	}
}
