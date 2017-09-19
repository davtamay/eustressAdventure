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

		//slider.value += 0.00001f;//Invoke (string.Empty, 0);
		cam = Camera.main.transform;
	//	transform.parent.gameObject.SetActive (false);// = false;
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
}
