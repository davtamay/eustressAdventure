using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsControl : MonoBehaviour {

	[SerializeField]AudioMixer mainMixer;

/*	void Start(){
		mainSlider = GetComponent<Slider> ();
	
		mainSlider.onValueChanged.AddListener(delegate(float arg0) {
			ChangeMusicVol(mainSlider.value);
		});
	}*/
	//SettingsControl(){ 
	void Start(){

		gameObject.SetActive (false);
	}

	void OnDisable(){
	
		ChangeMusicVol(PlayerPrefs.GetFloat("MusicVolume"));
		ChangeSoundVol(PlayerPrefs.GetFloat("SoundVolume"));
	
	
	}
	//void Start(){
		//ChangeMusicVol(PlayerPrefs.GetFloat("MusicVolume"));
		//ChangeSoundVol(PlayerPrefs.GetFloat("SoundVolume"));
	//	mainMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
	//	mainMixer.SetFloat("DirectVolume", PlayerPrefs.GetFloat("SoundVolume"));
	//	mainMixer.SetFloat("AmbientVolume", PlayerPrefs.GetFloat("SoundVolume"));
	//	mainMixer.SetFloat("InterfaceVolume", PlayerPrefs.GetFloat("SoundVolume"));
	//	gameObject.SetActive (false);

	//}
	//void OnEnable(){
		//gameObject.SetActive (false);

	//}

	public void ChangeMusicVol(float vol){

		mainMixer.SetFloat ("MusicVolume", Mathf.Log10 (vol) * 20f);

	}
	public void ChangeSoundVol(float vol){
		mainMixer.SetFloat ("DirectVolume", Mathf.Log10 (vol) * 20f);
		mainMixer.SetFloat ("AmbientVolume", Mathf.Log10 (vol) * 20f);
		mainMixer.SetFloat ("InterfaceVolume", Mathf.Log10 (vol) * 20f);
	}



}
