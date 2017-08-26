using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType {_MUSIC, _AMBIENT, _DIRECT, _INTERFACE}
public class AudioManager : MonoBehaviour {

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

		//DontDestroyOnLoad (gameObject);


		//int i = 0;
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



		//StartCoroutine (OnUpdate ());

	}
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
		tempAS.transform.position = Vector3.zero;

	}
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

	/*public void PlayDirectSound (string resourceName, Vector3 position){


		_loadedResource = Resources.Load (resourceName) as AudioClip;
		_DirectAudioSourceParent.transform.position = position;

		_DirectAudioSourceParent.pitch = 1.0f + Random.Range (-.1f, .1f);
		_DirectAudioSourceParent.volume = 1.0f - Random.Range (0.0f, 0.25f);

		_DirectAudioSourceParent.PlayOneShot (_loadedResource);


	}*/


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


	public bool CheckIfAudioPlaying(AudioType audioType, string nameOfASGO){

		switch (audioType) {
		case AudioType._MUSIC:
			if (_MusicAudioSourceDictionary [nameOfASGO].isPlaying)
				return true;
			else
				return false;
			break;
		case AudioType._AMBIENT:
			if (_AmbientAudioSourceDictionary [nameOfASGO].isPlaying)
				return true;
			else
				return false;
			break;
		case AudioType._DIRECT:
			if (_DirectAudioSourceDictionary [nameOfASGO].isPlaying)
				return true;
			else
				return false;
			break;
		case AudioType._INTERFACE:
			if (_InterfaceAudioSourceDictionary [nameOfASGO].isPlaying)
				return true;
			else
				return false;
			break;


		}
		return false;

	}
	//void CheckIfAudioIsPlaying (enum. dirent, interface, musi)
	/*
	IEnumerator OnUpdate(){

		while (true) {
			yield return null;
		
			foreach (Transform aS in audioSourceGroupParent) {

				if (!aS.isPlaying && _loadedResource != null) {
					Resources.UnloadAsset (_loadedResource);
					_loadedResource = null;
				
				}

				yield return new WaitForSecondsRealtime (3);
			}
		
		}







	}*/
	

	}

