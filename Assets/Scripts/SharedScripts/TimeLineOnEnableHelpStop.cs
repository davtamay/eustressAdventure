using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineOnEnableHelpStop : MonoBehaviour {

	public TimeLineController timeLineController;
	[SerializeField]private bool isUsingSprite;
	[SerializeField]private bool isUsingTextMesh;
	[SerializeField]private Goal thisGoal;

	[SerializeField]private float waitTime = 0;

	void Awake(){
	
		timeLineController =  GameObject.FindGameObjectWithTag ("TimeLine").GetComponent<TimeLineController> ();
	}


	void OnEnable () {

		timeLineController.StopTimeLine ();
		HelpUIManager.Instance.TurnOnHelpInfo (this.gameObject,isUsingSprite,isUsingTextMesh, waitTime);
		HelpUIManager.Instance.curGoal = thisGoal;
	}

	void OnDisable(){
		timeLineController.ResumeTimeLine ();
		//HelpUIManager.Instance.curGoal = Goal.NONE;


	}
	

}
