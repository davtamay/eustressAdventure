using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TimeLineOnEnableHelpStop : MonoBehaviour {
	
	//private TimeLineController timeLineController;
	public UnityEvent onEnabled;
	public UnityEvent onDisabled;
	[SerializeField]private bool isUsingSprite;
	[SerializeField]private bool isUsingTextMesh;
	[SerializeField]private Goal thisGoal;


	[SerializeField]private bool isStopTimeLine = true;
	[SerializeField]private float waitTime = 0;
	//ONLY USE FOR THOSE THAT STOP THE TIMELINE
	[SerializeField]private bool isOneCallPerTrack = true;
//	[SerializeField]private bool isOneCallPerActivation;

	[SerializeField]private bool addingTextOnEnable = true;
	//[SerializeField]private string helpText;
	[SerializeField]private int helpTextSize;
	[SerializeField]private string[] localizationKeyOrder;

	private bool isDone;

	//void Awake(){
	
		//timeLineController =  GameObject.FindGameObjectWithTag ("TimeLine").GetComponent<TimeLineController> ();

	//}

	int count = 0;
	//to prevent text from instantiating after timelinestop...
//	int lastCount = 0;
//	public IEnumerator checkForRedundancyCoroutine;
//	public bool isCoroutine;
	//public AnimationCurve curveTest;
//	public bool isCoroutineActive;
	void OnEnable () {

//		if(isOneCallPerActivation){
//
//			if(!isCoroutineActive)
//				StartCoroutine (CheckForRedundancyCalls());
//			 else
//				enabled =false;
//		}

	//	Debug.Log ("ONENABLEDCALLED");
		if (isOneCallPerTrack)
		if (isDone) 
			return;
		


		onEnabled.Invoke ();

		if (isStopTimeLine) {
			if(TimeLineController.Instance != null)
			TimeLineController.Instance.StopTimeLine ();

		}
		if (addingTextOnEnable) {
		


			if (localizationKeyOrder.Length - 1 >= count)
				
				HelpUIManager.Instance.AddText (localizationKeyOrder [count]);
			else {
				Debug.LogWarning ("Amount of apperances of Text in TimeLine is more than text set up in component");
				count = 0;
			}

			HelpUIManager.Instance.TurnOnHelpInfo (this.gameObject, isUsingSprite, isUsingTextMesh, waitTime);
			count++;



		}
				
				HelpUIManager.Instance.curGoal = thisGoal;

	
	}

	IEnumerator CheckForRedundancyCalls (){
		//isCoroutineActive = true;
		float timeToCheckAgainst = Time.realtimeSinceStartup + waitTime;

		while(timeToCheckAgainst > Time.realtimeSinceStartup){

		
				yield return null;

		}
//		isCoroutineActive = false;
//		//checkForRedundancyCoroutine = null;


	}

	void OnDisable(){

//		if (isOneCallPerActivation && isCoroutineActive)
//			return;
//		//StopAllCoroutines ();
	
		if(isOneCallPerTrack)
			if (isDone)
				return;
		
		isDone = true;

		onDisabled.Invoke ();

		//if (addingTextOnEnable)
		//	HelpUIManager.Instance.RemoveText();
		

		TimeLineController.Instance.ResumeTimeLine ();

		HelpUIManager.Instance.curGoal = Goal.NONE;


	}
	//public void ResumeTimeLine(){

		//timeLineController.ResumeTimeLine ();

	//}

}
