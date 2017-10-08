using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft;

public class LocalizedText : MonoBehaviour {

	public string key;

	private Text text;

	public void OnUpdate () 
	{
		//dynamic 

		text = GetComponent<Text> ();

		//if (text == null)
		//	text = GetComponent<TextMesh>();

		if (text == null) {
		
			Debug.LogWarning ("Cannot get LocalizedText - Text or TextMesh - Component");
		}




		text.text = LocalizationManager.Instance.GetLocalizedValue (key);
	}

	void OnEnable(){

		if (!LocalizationManager.Instance.GetIsReady ())
			return;
		
		OnUpdate ();
	
	
	
	}

}
