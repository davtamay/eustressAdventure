using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour {

	public enum AudioReferanceType {_MUSIC, _AMBIENT, _DIRECT, _INTERFACE}

	[SerializeField] Transform _MusicAudioSourceParent;
	[SerializeField] Transform _AmbientAudioSourceParent;
	[SerializeField] Transform _DirectAudioSourceParent;
	[SerializeField] Transform _InterfaceAudioSourceParent;

	Dictionary<string, AudioSource> _MusicAudioSourceDictionary = new Dictionary<string,AudioSource>(); 
	Dictionary<string, AudioSource> _AmbientAudioSourceDictionary = new Dictionary<string,AudioSource>(); 
	Dictionary<string, AudioSource> _DirectAudioSourceDictionary = new Dictionary<string,AudioSource>(); 
	Dictionary<string, AudioSource> _InterfaceAudioSourceDictionary = new Dictionary<string,AudioSource>(); 

	List<Transform> audioSourceGroupParent = new List<Transform>();

	private AudioClip _loadedResource;

	[Header("Music Settings")]
	public List<AudioSource> musicList = new List<AudioSource>();

	private int totalMusic;
	public int curMusicIndex;
	public bool isMusicOn = false;

	private static AudioManager _Instance;

	public static AudioManager Instance {
		get{ return (AudioManager)_Instance; }
		set{ _Instance = value; }
	}

	void Awake(){

		if (Instance != null)
			DestroyImmediate (gameObject);
		//Debug.Log ("there is 2 SoundControllers");
		else
			Instance = this;

		foreach(Transform aSParent in transform){

			audioSourceGroupParent.Add(aSParent.GetComponent<Transform>());

		}
		_MusicAudioSourceParent = audioSourceGroupParent[0];
		_AmbientAudioSourceParent = audioSourceGroupParent[1];
		_DirectAudioSourceParent = audioSourceGroupParent[2];
		_InterfaceAudioSourceParent = audioSourceGroupParent[3];

		foreach (Transform aST in _MusicAudioSourceParent) {

			AudioSource tempAS = aST.GetComponent<AudioSource> ();
			_MusicAudioSourceDictionary.Add (aST.name, tempAS);
			musicList.Add (tempAS);
		}

		foreach (Transform aST in _AmbientAudioSourceParent) {

			AudioSource tempAS = aST.GetComponent<AudioSource> ();
			_AmbientAudioSourceDictionary.Add (aST.name, tempAS);

		}
			
		foreach (Transform aST in _DirectAudioSourceParent) {
		
			AudioSource tempAS = aST.GetComponent<AudioSource> ();
			_DirectAudioSourceDictionary.Add (aST.name, tempAS);
		
		}
		foreach (Transform aST in _InterfaceAudioSourceParent) {

			AudioSource tempAS = aST.GetComponent<AudioSource> ();
			_InterfaceAudioSourceDictionary.Add (aST.name, tempAS);

		}

	}
	#region MUSIC_METHODS

	public void StopM(){
		musicList [curMusicIndex ].Stop ();
		//_MusicAudioSourceDictionary
		StopAllCoroutines ();
		isMusicOn = false;


	}
	public void PlayM(){
		musicList [curMusicIndex].Play ();
		isMusicOn = true;

	}

	private AudioSource tempMusic;
	public void PlayMusicNext (){


		if (curMusicIndex >= musicList.Count -1) 
		{
			musicList [musicList.Count -1].Stop ();
			Debug.LogWarning ("Trying to play invalid sound in array, replay tracks");
			curMusicIndex = 0;

			if (isMusicOn) 
				musicList [curMusicIndex].Play();
			return;
		}

		curMusicIndex++;

		if (isMusicOn) {

			musicList [curMusicIndex - 1].Stop ();
			tempMusic = musicList [curMusicIndex];
			tempMusic.Play();

		}

	}
	public void PlayMusicPrevious (){


		if (curMusicIndex <= 0) 
		{
			musicList [curMusicIndex +1].Stop ();
			Debug.LogWarning ("Trying to play invalid sound in array, replay tracks");
			curMusicIndex = musicList.Count -1;

			if (isMusicOn) {
				musicList [0].Stop ();
				musicList [curMusicIndex].Play ();
			}
			return;
		}

		curMusicIndex--;

		if (isMusicOn) {

			musicList [curMusicIndex + 1].Stop ();
			tempMusic = musicList [curMusicIndex];
			tempMusic.Play ();

		}

	}

	public string GetCurrentTrackName(){

		return musicList [curMusicIndex].name;
	}
	#endregion

	#region AMBIENT_METHODS
	public void PlayAmbientSoundAndActivate (string nameOfAS, bool randomizeAudio = false, bool removeFromMemoryWhenDone = false, float nonUsageRemoveTime = 0f, Transform setParent = null){

		AudioSource tempAS;

		if (removeFromMemoryWhenDone) {
			_loadedResource = Resources.Load (nameOfAS) as AudioClip;
			tempAS = _AmbientAudioSourceDictionary [nameOfAS];
			tempAS.clip = _loadedResource;
		} else 
			tempAS = _AmbientAudioSourceDictionary [nameOfAS];

		if (!tempAS.gameObject.activeSelf)
			tempAS.gameObject.SetActive (true);

		if (randomizeAudio) {
			tempAS.pitch = 1.0f + Random.Range (-.1f, .1f);
			tempAS.volume = 1.0f - Random.Range (0.0f, 0.25f);
		}

		tempAS.PlayOneShot (tempAS.clip);

		if (removeFromMemoryWhenDone) {

			StartCoroutine (ReleaseClipFromMemory (tempAS, nonUsageRemoveTime));

		}

		tempAS.transform.SetParent (setParent);
		tempAS.transform.localPosition = Vector3.zero;

	}
	#endregion

	#region Direct_METHODS
	public void PlayDirectSound (string nameOfAS, bool randomizeAudio = false, bool removeFromMemoryWhenDone = false, float nonUsageRemoveTime = 0f){
	
		AudioSource tempAS;

		if (removeFromMemoryWhenDone) {
			_loadedResource = Resources.Load (nameOfAS) as AudioClip;
			tempAS = _DirectAudioSourceDictionary [nameOfAS];
			tempAS.clip = _loadedResource;
		} else 
			tempAS = _DirectAudioSourceDictionary [nameOfAS];

		
		if (randomizeAudio) {
				tempAS.pitch = 1.0f + Random.Range (-.1f, .1f);
				tempAS.volume = 1.0f - Random.Range (0.0f, 0.25f);
		}

			tempAS.PlayOneShot (tempAS.clip);

		if (removeFromMemoryWhenDone) {
		
			StartCoroutine (ReleaseClipFromMemory (tempAS, nonUsageRemoveTime ));
		
		}

	}

	#endregion

	#region Interface_METHODS
	public void PlayInterfaceSound(string nameOfAS, bool randomizeAudio = false, bool removeFromMemoryWhenDone = false, float nonUsageRemoveTime = 0f){

		AudioSource tempAS;

		if (removeFromMemoryWhenDone) {
			_loadedResource = Resources.Load (nameOfAS) as AudioClip;
			tempAS = _InterfaceAudioSourceDictionary [nameOfAS];
			tempAS.clip = _loadedResource;
		} else 
			tempAS = _InterfaceAudioSourceDictionary [nameOfAS];


		if (randomizeAudio) {
			tempAS.pitch = 1.0f + Random.Range (-.1f, .1f);
			tempAS.volume = 1.0f - Random.Range (0.0f, 0.25f);
		}

		tempAS.PlayOneShot (tempAS.clip);

		if (removeFromMemoryWhenDone) {

			StartCoroutine (ReleaseClipFromMemory (tempAS, nonUsageRemoveTime));

		}

	}
	#endregion

	#region GlobalAudio_Methods
	public GameObject MakeCopyOfAudioSourceGO(AudioReferanceType aRT, string nameOfGOAS){
		GameObject tempGOASInstance = null;
	
		switch (aRT) {
		case AudioReferanceType._MUSIC:

			if(_MusicAudioSourceDictionary[nameOfGOAS].transform.IsChildOf(transform)){
				tempGOASInstance = _MusicAudioSourceDictionary [nameOfGOAS].gameObject;
				break;
			}
				
			tempGOASInstance = Instantiate(_MusicAudioSourceDictionary[nameOfGOAS]).gameObject;

			break;

		case AudioReferanceType._AMBIENT:
			
			if(_AmbientAudioSourceDictionary[nameOfGOAS].transform.IsChildOf(transform)){
				tempGOASInstance = _AmbientAudioSourceDictionary [nameOfGOAS].gameObject;
				break;
			}

			tempGOASInstance = Instantiate(_AmbientAudioSourceDictionary[nameOfGOAS]).gameObject;
			break;

		case AudioReferanceType._DIRECT:

			if(_DirectAudioSourceDictionary[nameOfGOAS].transform.IsChildOf(transform)){
				tempGOASInstance = _DirectAudioSourceDictionary [nameOfGOAS].gameObject;
				break;
			}

			tempGOASInstance = Instantiate(_DirectAudioSourceDictionary[nameOfGOAS]).gameObject;
			break;

		case AudioReferanceType._INTERFACE:

			if(_InterfaceAudioSourceDictionary[nameOfGOAS].transform.IsChildOf(transform)){
				tempGOASInstance = _InterfaceAudioSourceDictionary [nameOfGOAS].gameObject;
				break;
			}

			tempGOASInstance = Instantiate(_InterfaceAudioSourceDictionary[nameOfGOAS]).gameObject;
			break;


		}
		return tempGOASInstance;
	
	
	}


	IEnumerator ReleaseClipFromMemory(AudioSource aS, float notPlayingAmount = 0f){
	
		bool isDone = false;
		float timer = 0;

		while (!isDone) {


			if (aS.clip != null) {

				if (!aS.isPlaying) {
					timer += Time.deltaTime;

					if (timer > notPlayingAmount) {
						Resources.UnloadAsset (aS.clip);
						aS.clip = null;
						isDone = true;
					}
				}else 
					timer = 0;
			}
			yield return null;
		}
	
	}

	public AudioSource GetAudioSourceReferance(AudioReferanceType audioType, string nameOfASGO){

		switch (audioType) {
		case AudioReferanceType._MUSIC:
			return _MusicAudioSourceDictionary [nameOfASGO];
				
		//	break;
		case AudioReferanceType._AMBIENT:
			return _AmbientAudioSourceDictionary [nameOfASGO];

		//	break;
		case AudioReferanceType._DIRECT:
			return _DirectAudioSourceDictionary [nameOfASGO];

		//	break;
		case AudioReferanceType._INTERFACE:
			return _InterfaceAudioSourceDictionary [nameOfASGO];
		
		//	break;


		}
		return null;

	}


	public bool CheckIfAudioPlaying(AudioReferanceType audioType, string nameOfASGO){

		switch (audioType) {
		case AudioReferanceType._MUSIC:
			if (_MusicAudioSourceDictionary [nameOfASGO].isPlaying)
				return true;
			else
				return false;
			
		case AudioReferanceType._AMBIENT:
			if (_AmbientAudioSourceDictionary [nameOfASGO].isPlaying)
				return true;
			else
				return false;
		
		case AudioReferanceType._DIRECT:
			if (_DirectAudioSourceDictionary [nameOfASGO].isPlaying)
				return true;
			else
				return false;
			
		case AudioReferanceType._INTERFACE:
			if (_InterfaceAudioSourceDictionary [nameOfASGO].isPlaying)
				return true;
			else
				return false;
		


		}
		return false;

	}
	public bool StopAudioPlaying(AudioReferanceType audioType, string nameOfASGO){

		AudioSource tempAS;

		switch (audioType) {
		case AudioReferanceType._MUSIC:
			tempAS = _MusicAudioSourceDictionary [nameOfASGO];;

			tempAS.Stop ();
			
			break;
		case AudioReferanceType._AMBIENT:
			tempAS = _AmbientAudioSourceDictionary [nameOfASGO];;

			tempAS.Stop ();

			break;
		case AudioReferanceType._DIRECT:
			tempAS = _DirectAudioSourceDictionary [nameOfASGO];

			tempAS.Stop ();

			break;
		case AudioReferanceType._INTERFACE:

			tempAS = _InterfaceAudioSourceDictionary [nameOfASGO];;

			tempAS.Stop ();

			break;


		}
		return false;

	}
	#endregion

	}

