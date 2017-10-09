using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TimeLineOnEnableHelpStop : MonoBehaviour {

	private TimeLineController timeLineController;
	public UnityEvent onEnabled;
	public UnityEvent onDisabled;
	[SerializeField]private bool isUsingSprite;
	[SerializeField]private bool isUsingTextMesh;
	[SerializeField]private Goal thisGoal;


	[SerializeField]private bool isStopTimeLine = true;
	[SerializeField]private float waitTime = 0;


	[SerializeField]private bool addingTextOnStop;
	[SerializeField]private string helpText;
	[SerializeField]private int helpTextSize;
	[SerializeField]private string localizationKey;

	private bool isDone;

	void Awake(){
	
		timeLineController =  GameObject.FindGameObjectWithTag ("TimeLine").GetComponent<TimeLineController> ();

	}


	void OnEnable () {
		

		if (isDone)
			return;

		onEnabled.Invoke ();

		if(isStopTimeLine)
		timeLineController.StopTimeLine ();

		if(addingTextOnStop)
			HelpUIManager.Instance.AddText (localizationKey,helpTextSize);
		
		HelpUIManager.Instance.TurnOnHelpInfo (this.gameObject,isUsingSprite,isUsingTextMesh, waitTime);
		HelpUIManager.Instance.curGoal = thisGoal;

	
	}

	void OnDisable(){
		

		if (isDone)
			return;

		isDone = true;

		onDisabled.Invoke ();

		if (addingTextOnStop)
			HelpUIManager.Instance.RemoveText();
		

		timeLineController.ResumeTimeLine ();

		//HelpUIManager.Instance.curGoal = Goal.NONE;


	}
	

}
