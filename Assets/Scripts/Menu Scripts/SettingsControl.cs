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
		EventManager.Instance.AddListener (EVENT_TYPE.SCENE_LOADED,OnEvent);
		gameObject.SetActive (false);
		ChangeMusicVol(PlayerPrefs.GetFloat("MusicVolume"));
		ChangeSoundVol(PlayerPrefs.GetFloat("SoundVolume"));
	}
	void OnEvent(EVENT_TYPE Event_Type, Component Sender, params object[] Param){

		switch(Event_Type){

		case EVENT_TYPE.SCENE_LOADED:
			
			isAudioReady = true;

			break;


		}
	}
		

	public void ChangeMusicVol(float vol){
		
		mainMixer.SetFloat ("MusicVolume", Mathf.Log10 (vol) * 20f);

	}

	public void ChangeSoundVol(float vol){
		
		mainMixer.SetFloat ("DirectVolume", Mathf.Log10 (vol) * 20f);
		mainMixer.SetFloat ("AmbientVolume", Mathf.Log10 (vol) * 20f);
		mainMixer.SetFloat ("InterfaceVolume", Mathf.Log10 (vol) * 20f);

	}
	private bool isAudioReady;
	public void TrySoundSampler(){
			
		if (!isAudioReady)
			return;
		
		AudioManager.Instance.PlayDirectSound ("SmallWin");
	}


	public void TryMusicSampler(){

	
		
		if (isAudioReady && !AudioManager.Instance.isMusicOn) {

			//isSampleMusicPlaying = true;
			if (!AudioManager.Instance.GetAudioSourceReferance (AudioManager.AudioReferanceType._MUSIC, "Clouds"))
				return;
			
			AudioSource tempAS = AudioManager.Instance.GetAudioSourceReferance(AudioManager.AudioReferanceType._MUSIC,"Clouds");

			if (tempAS == null || tempAS.isPlaying)
				return;

			tempAS.Play ();


			StartCoroutine("StopMusic");
		
		}
	}
	//float timer = 0;

	IEnumerator StopMusic(){
		yield return new WaitForSecondsRealtime (5f);
		AudioSource tempAS = AudioManager.Instance.GetAudioSourceReferance(AudioManager.AudioReferanceType._MUSIC,"Clouds");
		tempAS.Stop ();
		//isSampleMusicPlaying = false;


	}






}
