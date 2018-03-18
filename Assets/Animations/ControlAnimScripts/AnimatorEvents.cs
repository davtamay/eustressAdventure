using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour {

	[SerializeField]UnityEvent onFirstEvent;
	[SerializeField]UnityEvent onSecondEvent;
	[SerializeField]UnityEvent onThirdEvent;
	[SerializeField]UnityEvent onFourthEvent;


	public void InitiateFirstEvent(){
	
		onFirstEvent.Invoke ();
	}
	public void InitiateSecondEvent(){

		onSecondEvent.Invoke ();
	}
	public void InitiateThirdEvent(){

		onThirdEvent.Invoke ();
	}
	public void InitiateFourthEvent(){

		onThirdEvent.Invoke ();
	}

}
