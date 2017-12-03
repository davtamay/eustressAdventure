using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour {

	[SerializeField] private IntVariable value;
	[SerializeField] private string initialText;
	[SerializeField] private string textBeforeValue;
	[SerializeField] private string textAfterValue;

	private Text textComponent;

	void Start () {

		textComponent = GetComponent<Text> ();
		textComponent.text = initialText;
		
	}
	

	public void OnUpdate () {

		textComponent.text = textBeforeValue + value.Value + textAfterValue;
	}
}
