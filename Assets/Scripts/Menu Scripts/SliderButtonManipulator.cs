using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SliderButtonManipulator : MonoBehaviour {

	[SerializeField] private float scrollSpeed;
	[SerializeField] private string nameOfPP;

	private Button[] navigationButtons;
	private Slider slider;
	private Transform cam;

	void Awake () {



		navigationButtons = GetComponentsInChildren <Button>();
		slider = GetComponent<Slider> ();
		slider.value = PlayerPrefs.GetFloat (nameOfPP);


		Debug.Log ("MUSIC AND SOUND" + PlayerPrefs.GetFloat (nameOfPP, slider.value));

		cam = Camera.main.transform;
	
	}

	
	// Update is called once per frame
	void Update () {

		RaycastHit hit;

		if(Physics.Raycast(cam.position, cam.rotation * Vector3.forward, out hit)){

			EventSystem.current.SetSelectedGameObject (null);

			if (string.Equals (hit.transform.name, navigationButtons [0].transform.name, System.StringComparison.CurrentCultureIgnoreCase)) {
				slider.value += Time.unscaledDeltaTime * scrollSpeed;
				navigationButtons[0].Select();

			} else if (string.Equals (hit.transform.name, navigationButtons [1].transform.name, System.StringComparison.CurrentCultureIgnoreCase)) {
				slider.value -= Time.unscaledDeltaTime * scrollSpeed;
				navigationButtons[1].Select();
			}

			//PlayerPrefs.SetFloat (nameOfPP, slider.value);
			//Debug.Log("NAMEOFPP" + PlayerPrefs.GetFloat(nameOfPP));

		}
		
	}

	void OnDisable(){
		PlayerPrefs.SetFloat (nameOfPP, slider.value);
		PlayerPrefs.Save ();
	}
	void OnApplicationQuit(){



		PlayerPrefs.SetFloat (nameOfPP, slider.value);
	}

//	void OnEvent(EVENT_TYPE Event_Type, Component Sender, params object[] Param){
//		switch(Event_Type){
//
//		case EVENT_TYPE.APPLICATION_QUIT:
//			
//			break;
//
//		}
//
//
//
//	}
}
