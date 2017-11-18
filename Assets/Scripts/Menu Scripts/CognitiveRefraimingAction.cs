using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CognitiveRefraimingAction : MonoBehaviour {

	[SerializeField]float speedModifier;
	//[0]fast speed, [1] reduceSpeed, [2] reset
	[SerializeField]Button[] settingsButtons;
	FolowAllong followAllongScript;

	private Transform camTransform;

	public Text speedDisplay;

	void Start () {
		camTransform = Camera.main.transform;
		settingsButtons = GetComponentsInChildren<Button> ();
		followAllongScript = GetComponentInParent<FolowAllong> ();

		//speedDisplay.text = (followAllongScript.scrollb.value/40f).ToString("P0");
	}
	
	RaycastHit hit; 

	void Update () {

		followAllongScript.scrollSpeed = Mathf.Clamp (followAllongScript.scrollSpeed, -5f, 35f);

		EventSystem.current.SetSelectedGameObject (null);
		if (Physics.Raycast (camTransform.position, camTransform.rotation * Vector3.forward, out hit)) {
		
			foreach (Button button in settingsButtons) {


				if(button.gameObject.GetInstanceID() == hit.transform.gameObject.GetInstanceID()){

					button.Select ();

					switch (button.name) {

					case "AddSpeed":
						speedDisplay.text = string.Empty;

						followAllongScript.scrollSpeed += Time.unscaledDeltaTime * speedModifier;
						speedDisplay.text = (followAllongScript.scrollSpeed/35f).ToString("P0");
						if ((followAllongScript.scrollSpeed /35f) >= 1f)
							speedDisplay.text = "100%";
						break;

					case "ReduceSpeed":

						followAllongScript.scrollSpeed -= Time.unscaledDeltaTime * speedModifier;

						speedDisplay.text = (followAllongScript.scrollSpeed/35f).ToString("P0");

						if ((followAllongScript.scrollSpeed / 35f) <= 0f)
							speedDisplay.text = "0%";
						//Mathf.Clamp (followAllongScript.scrollSpeed, 0f, 5f);
						break;

					case "Restart":

						followAllongScript.scrollb.value = 0f;
						break;


					}



					//followAllongScript.scrollSpeed = 
						


				}
			
			
			
			
			}
		
		} 
		
	}
}
