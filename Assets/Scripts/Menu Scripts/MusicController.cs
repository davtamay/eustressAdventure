using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MusicObject //: MonoBehaviour
{
	public AudioSource source;
	public GameObject sourceGO;
	public Transform sourceTR;

	public AudioClip clip;
	public string name;

	//private float volumeON = 1f;
	//private float targetVolume;

	//private float volume = 0;

	//private int targetFadeState =0;




	public MusicObject(AudioClip aClip, string aName, float aVolume)
	{
		

	//	targetFadeState = 1;
		//targetVolume = volumeON;
	//	source.volume = aVolume;// volume;
		sourceGO = new GameObject ("AudioSource_" + aName);
		sourceTR = sourceGO.transform;
		sourceTR.parent = MusicController.Instance.myTransform;
		source = sourceGO.AddComponent<AudioSource> ();
		source.name = "AudioSource_" + aName;
		source.playOnAwake = false;
		source.clip = aClip;
		source.volume = aVolume;
		clip = aClip;
		name = aName;


}
	public void PlayMusic (){
		//MusicController.Instance.PlayM(source, targetFadeState,targetVolume, volumeON);
		source.Play();
	}
	public void StopMusic (){

		source.Stop ();
		//MusicController.Instance.StopM(source);
	}
}
public class MusicController : MonoBehaviour 
{

	public static MusicController Instance;

	public Transform myTransform;
	//public string gamePrefsName = "DefaultGame";
	//private int fadeState;
	private int targetFadeState;
	public float fadeTime = 15f;
	public bool isFadeInAtStart = true;		
	public bool isMusicOn = false;
	public bool isloopMusic = true;

	private float volumeON = 1f;
	//private float targetVolume;

	public AudioClip[] musicList;
	public List<MusicObject> musicObjectList;

	private float volume = 1;

	private int totalMusic;
	public int curMusicIndex;

	public bool isMusicChanged = false;

	private MusicObject tempMusicObj;

	public void Awake ()
	{
		myTransform = transform;



		if (Instance != null)
			DestroyImmediate (gameObject);
			//Debug.Log ("there is 2 MusicControllers");
		else
			Instance = this;

		DontDestroyOnLoad (gameObject);
	}

	void Start()
	{
		musicObjectList = new List <MusicObject>();

		foreach (AudioClip theMusic in musicList) 
		{
			
			tempMusicObj = new MusicObject (theMusic, theMusic.name, volume);
			musicObjectList.Add (tempMusicObj);


			totalMusic++;
		
		}


	}
		

	public void StopM(){
		musicObjectList [curMusicIndex ].StopMusic ();
		StopAllCoroutines ();
		isMusicOn = false;
	
	
	}
	public void PlayM(){
		musicObjectList [curMusicIndex].PlayMusic ();
		isMusicOn = true;
	
	}

		
	public void PlayMusicNext (){


		if (curMusicIndex >= musicObjectList.Count -1) 
		{
			musicObjectList [musicObjectList.Count -1].StopMusic ();
			Debug.LogWarning ("Trying to play invalid sound in array, replay tracks");
			curMusicIndex = 0;
		
			if (isMusicOn) 
				musicObjectList [curMusicIndex].source.Play();
			return;
		}
			
		curMusicIndex++;

		if (isMusicOn) {
			
			musicObjectList [curMusicIndex - 1].StopMusic ();
			tempMusicObj = musicObjectList [curMusicIndex];
			tempMusicObj.PlayMusic ();

		}
	
	}
	public void PlayMusicPrevious (){


		if (curMusicIndex <= 0) 
		{
			musicObjectList [curMusicIndex +1].StopMusic ();
			Debug.LogWarning ("Trying to play invalid sound in array, replay tracks");
			curMusicIndex = musicObjectList.Count -1;

			if (isMusicOn) {
				musicObjectList [0].StopMusic ();
				musicObjectList [curMusicIndex].source.Play ();
			}
			return;
		}

		curMusicIndex--;

		if (isMusicOn) {

			musicObjectList [curMusicIndex + 1].StopMusic ();
			tempMusicObj = musicObjectList [curMusicIndex];
			tempMusicObj.PlayMusic ();

		}

	}

	public string GetCurrentTrackName(){
	
		return musicObjectList [curMusicIndex].name;
	}
		
	public void FadeIn(float fadeAmount)
	{
		volume = 0;
	//	fadeState = 0;
		//targetFadeState = 1;
		fadeTime = fadeAmount;

	}
	public void FadeOut(float fadeAmount)
	{
		volume = volumeON;
	//	fadeState = 1;
	//	targetFadeState = 0;
	//	targetVolume = 0;
		fadeTime = fadeAmount;
	}


	

}
