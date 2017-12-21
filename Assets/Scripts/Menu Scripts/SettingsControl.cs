using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsControl : MonoBehaviour {

	[SerializeField]AudioMixer mainMixer;
	[SerializeField] string soundToSample = "SmallWin";
	[SerializeField] string musicToSample = "Clouds";

	[Header("References")]
	[SerializeField]private AudioManager AUDIO_MANAGER;
	[SerializeField]private SliderButtonManipulator musicButtonManipulator;
	[SerializeField]private SliderButtonManipulator soundButtonManipulator;

//
	IEnumerator Start (){
		while (musicButtonManipulator.slider == null)
			yield return null;

//		Debug.Log ("THISISMUSICVOLUME" + PlayerPrefs.GetFloat ("MusicVolume"));

		if (!PlayerPrefs.HasKey ("MusicVolume")) 
		{
			PlayerPrefs.SetFloat ("MusicVolume" ,0.5f);
			PlayerPrefs.SetFloat ("SoundVolume", 0.5f);
		
		}

			musicButtonManipulator.slider.value = PlayerPrefs.GetFloat ("MusicVolume");
			soundButtonManipulator.slider.value = PlayerPrefs.GetFloat ("SoundVolume");


	}


//
//	void Start(){
//	//	EventManager.Instance.AddListener (EVENT_TYPE.SCENE_LOADED,OnEvent);
//
//	}
//	void OnEvent(EVENT_TYPE Event_Type, Component Sender, params object[] Param){
//
//		switch(Event_Type){
//
//		case EVENT_TYPE.SCENE_LOADED:
//			
//			isAudioReady = true;
//
//			break;
//
//
//		}
//	}
		

	public void ChangeMusicVol(float vol){
		
		mainMixer.SetFloat ("MusicVolume", Mathf.Log10 (vol) * 20f);

//		if (musicButtonManipulator.slider == null)
//			return;
		PlayerPrefs.SetFloat ("MusicVolume", vol);
//		musicSliderValue = musicButtonManipulator.slider.value;
	}

	public void ChangeSoundVol(float vol){
		
		mainMixer.SetFloat ("DirectVolume", Mathf.Log10 (vol) * 20f);
		mainMixer.SetFloat ("AmbientVolume", Mathf.Log10 (vol) * 20f);
		mainMixer.SetFloat ("InterfaceVolume", Mathf.Log10 (vol) * 20f);

//		if (soundButtonManipulator.slider == null)
//			return;
		PlayerPrefs.SetFloat ("SoundVolume", vol);
//		soundSliderValue = soundButtonManipulator.slider.value;

	}

	private bool isInitialSoundSkip;
	public void TrySoundSampler(){
			
		if (!isInitialSoundSkip){
			isInitialSoundSkip = true;
			return;
		}


			
		
		AUDIO_MANAGER.PlayInterfaceSound (soundToSample);
	}

	private bool isInitialMusicSkip;
	public void TryMusicSampler(){

		if (!isInitialMusicSkip) {
			isInitialMusicSkip = true;
			return;
		}

		if( !AUDIO_MANAGER.isMusicOn) {
			
			if (!AUDIO_MANAGER.GetAudioSourceReferance (AudioManager.AudioReferanceType._MUSIC, musicToSample))
				return;
			
			AudioSource tempAS = AUDIO_MANAGER.GetAudioSourceReferance(AudioManager.AudioReferanceType._MUSIC, musicToSample);

			if (tempAS == null || tempAS.isPlaying)
				return;

			tempAS.Play ();


			StartCoroutine("StopMusic");
		
		}
	}
	//float timer = 0;

	IEnumerator StopMusic(){
		yield return new WaitForSecondsRealtime (5f);
		AudioSource tempAS = AUDIO_MANAGER.GetAudioSourceReferance(AudioManager.AudioReferanceType._MUSIC,musicToSample);
		tempAS.Stop ();
		//isSampleMusicPlaying = false;


	}






}
