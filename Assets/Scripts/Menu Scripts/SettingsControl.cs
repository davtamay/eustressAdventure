using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsControl : MonoBehaviour {

	[SerializeField]AudioMixer mainMixer;


	private static SettingsControl instance;
	public static SettingsControl Instance {
		get{ return instance; }

	}

	void Awake (){


		if (instance != null) {
			Debug.LogError ("There is two instances off SettingControl");
			return;
		} else {
			instance = this;
		}
	}


	void Start(){

		gameObject.SetActive (false);
	}
		

	void OnDisable(){
	
		ChangeMusicVol(PlayerPrefs.GetFloat("MusicVolume"));
		ChangeSoundVol(PlayerPrefs.GetFloat("SoundVolume"));
	
	
	}


	public void ChangeMusicVol(float vol){

		mainMixer.SetFloat ("MusicVolume", Mathf.Log10 (vol) * 20f);

	}
	public void ChangeSoundVol(float vol){
		mainMixer.SetFloat ("DirectVolume", Mathf.Log10 (vol) * 20f);
		mainMixer.SetFloat ("AmbientVolume", Mathf.Log10 (vol) * 20f);
		mainMixer.SetFloat ("InterfaceVolume", Mathf.Log10 (vol) * 20f);
	}



}
