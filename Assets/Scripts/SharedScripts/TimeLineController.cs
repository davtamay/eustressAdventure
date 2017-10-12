using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TimeLineController : MonoBehaviour {

	public PlayableDirector mainPD;
	[SerializeField] bool isChangeLevelWhenComplete;
	[SerializeField] string levelName;

	[SerializeField]private int curStop = 0;

	[SerializeField] UnityEvent onTimeLineStop1;
	[SerializeField] UnityEvent onTimeLineStop2;
	[SerializeField] UnityEvent onTimeLineStop3;
	[SerializeField] UnityEvent onTimeLineStop4;
	[SerializeField] UnityEvent onTimeLineStop5;
	[SerializeField] UnityEvent onTimeLineStop6;

	[SerializeField] UnityEvent onTimeLineCompletion;

	public static TimeLineController Instance
	{ get { return instance; } }

	private static TimeLineController instance = null;



		

	void Awake(){

		if (instance) {
			DestroyImmediate (gameObject);
			return;
		}
		instance = this; 

		mainPD = GetComponent<PlayableDirector> ();//GameObject.FindGameObjectWithTag ("TimeLine").GetComponent<PlayableDirector> ();


	}

	IEnumerator Start () {
	


		while (true) {
		
			yield return null;

				
			if (mainPD.duration <= mainPD.time) {
			
				onTimeLineCompletion.Invoke ();

				if(isChangeLevelWhenComplete)
				SceneController.Instance.Load (levelName);

			}
		
			
		
		}

	}

	public void StopTimeLine ()
	{
		
		switch (curStop) {
		case 0:
			onTimeLineStop1.Invoke ();
			break;
		case 1:
			onTimeLineStop2.Invoke ();
			break;
		case 2:
			onTimeLineStop3.Invoke ();
			break;
		case 3:
			onTimeLineStop4.Invoke ();
			break;
		case 4:
			onTimeLineStop5.Invoke ();
			break;
		case 5:
			onTimeLineStop6.Invoke ();
			break;

		}

		curStop++;
		mainPD = GetComponent<PlayableDirector> ();
		mainPD.Pause ();


	}
	public void ResumeTimeLine(){
	
		mainPD = GetComponent<PlayableDirector> ();
		mainPD.Resume ();
	
	}
		

	//while ( mainPD.state != PlayState.Playing)
	//{


	//}




}
