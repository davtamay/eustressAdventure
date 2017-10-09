using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Microsoft.CSharp;

public class LocalizedText : MonoBehaviour {

	public string key;

	private Text text;
	private TextMesh textMesh;
	[SerializeField] private bool isTextMesh = false;

	private string[] lineArray;

	void Awake(){
	
		if (isTextMesh) {
			textMesh = GetComponent<TextMesh> ();
			if (textMesh == null) 
				Debug.LogWarning ("Cannot get LocalizedText TextMesh Component");

		}else {
			text = GetComponent<Text> ();
			if (text == null) 
				Debug.LogWarning ("Cannot get LocalizedText Text Component");
			
		}

	
	}
	void Start(){
		LocalizationManager.Instance.presentLocalizedTexts.Add (this);
	}
	public void OnUpdate () 
	{


		if (isTextMesh) {
			textMesh.text = LocalizationManager.Instance.GetLocalizedValue (key);
			textMesh.text = textMesh.text.Replace ("\\n", "\n");

		} else {
			text.text = LocalizationManager.Instance.GetLocalizedValue (key);
			text.text = text.text.Replace("\\n", "\n");
		}
		
	//	StartCoroutine (SetText ());
		//text.text = LocalizationManager.Instance.GetLocalizedValue (key);
	}
	private bool isFirstTime = true;

	/*public void OnEnable(){
	
		OnUpdate ();
	
	}*/
	/*void OnDisable(){

		StartCoroutine (SetText ());
	

	
	}*/

	IEnumerator SetText(){

		while (!LocalizationManager.Instance.GetIsReady ())
			yield return null;

	if(isTextMesh)
		textMesh.text = LocalizationManager.Instance.GetLocalizedValue (key);
	else
		text.text = LocalizationManager.Instance.GetLocalizedValue (key);
		

	}
	protected void OnDestroy(){

		LocalizationManager.Instance.presentLocalizedTexts.Remove (this);


	}

}
