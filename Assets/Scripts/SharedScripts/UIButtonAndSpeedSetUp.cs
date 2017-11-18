using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonAndSpeedSetUp : MonoBehaviour {

	public float speedModifier;

	[SerializeField]Button[] settingsButtons;

	public CountingDownS scrollScript;


	private Transform camTransform;

	public Text speedDisplay;


	void Start () {
		camTransform = Camera.main.transform;
		settingsButtons = GetComponentsInChildren<Button> ();

		scrollScript = GetComponentInParent<CountingDownS> ();

		speedDisplay.text = (scrollScript.countDownSpeed/2.5f).ToString("P0");
		//followAllongScript = GetComponentInParent<FolowAllong> ();
	}

	RaycastHit hit; 

	void Update () {

	//	speedDisplay.text = string.Empty;

		scrollScript.countDownSpeed = Mathf.Clamp (scrollScript.countDownSpeed, 0f, 2.5f);

		EventSystem.current.SetSelectedGameObject (null);
		if (Physics.Raycast (camTransform.position, camTransform.rotation * Vector3.forward, out hit)) {

			foreach (Button button in settingsButtons) {


				if(button.gameObject.GetInstanceID() == hit.transform.gameObject.GetInstanceID()){

					button.Select ();

					switch (button.name) {

					case "AddSpeed":
						speedDisplay.text = string.Empty;

						scrollScript.countDownSpeed += Time.unscaledDeltaTime * speedModifier;


						speedDisplay.text = (scrollScript.countDownSpeed/2.5f).ToString("P0");
						if ((scrollScript.countDownSpeed / 2.5f) >= 1f)
							speedDisplay.text = "100%";
						break;

					case "ReduceSpeed":
						speedDisplay.text = string.Empty;
						scrollScript.countDownSpeed -= Time.unscaledDeltaTime * speedModifier;


						speedDisplay.text = (scrollScript.countDownSpeed / 2.5f).ToString ("P0");
						if ((scrollScript.countDownSpeed / 2.5f) <= 0)
							speedDisplay.text = "0%";
						//Mathf.Clamp (followAllongScript.scrollSpeed, 0f, 5f);
						break;

					case "Restart":
						scrollScript.ResetTime ();
						//followAllongScript.scrollb.value = 0f;
						break;


					}



					//followAllongScript.scrollSpeed = 



				}




			}

		} 

	}
}

