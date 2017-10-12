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
	[SerializeField]private bool checkForOnlyOneCall = true;
	[SerializeField]private bool addingTextOnEnable = true;
	//[SerializeField]private string helpText;
	[SerializeField]private int helpTextSize;
	[SerializeField]private string[] localizationKeyOrder;

	private bool isDone;

	//void Awake(){
	
		//timeLineController =  GameObject.FindGameObjectWithTag ("TimeLine").GetComponent<TimeLineController> ();

	//}

	int count = 0;
	void OnEnable () {
		
	//	Debug.Log ("ONENABLEDCALLED");
		if (checkForOnlyOneCall)
		if (isDone) 
			return;
		


		onEnabled.Invoke ();

		if (isStopTimeLine) {
			Debug.Log ("isStopTime Called");
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

	void OnDisable(){
		
	
		if(checkForOnlyOneCall)
			if (isDone)
				return;
		Debug.Log ("ONEDisabledCALLED");
		isDone = true;

		onDisabled.Invoke ();

		if (addingTextOnEnable)
			HelpUIManager.Instance.RemoveText();
		

		TimeLineController.Instance.ResumeTimeLine ();

		//HelpUIManager.Instance.curGoal = Goal.NONE;


	}
	//public void ResumeTimeLine(){

		//timeLineController.ResumeTimeLine ();

	//}

}
