using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SoundObject
{
	public AudioSource source;
	public GameObject sourceGO;
	public Transform sourceTR;

	public AudioClip clip;
	public string name;


	public SoundObject(AudioClip aClip, string aName, float aVolume)
	{

		sourceGO = new GameObject ("AudioSource_" + aName);
		sourceTR = sourceGO.transform;
		source = sourceGO.AddComponent<AudioSource> ();
		source.name = "AudioSource_" + aName;
		source.playOnAwake = false;
		source.clip = aClip;
		source.volume = aVolume;
		clip = aClip;
		name = aName;

	}

	public void PlaySound(Vector3 atPosition)
	{
		sourceTR.position = atPosition;
		source.PlayOneShot (clip);

	}
		

}
public class BaseSoundController : MonoBehaviour 
{
	public static BaseSoundController Instance;

	public AudioClip[] GameSounds;

	private int totalSounds;
	private List<SoundObject> soundObjectList;
	private SoundObject tempSoundObj;

	public float volume = 1;
	public string gamePrefsName = "DefaultGame";


	public void Awake ()
	{
	
		if (Instance != null)
			Debug.Log ("there is 2 SoundControllers");
		else
			Instance = this;


	}

	void Start (){
	
		volume = PlayerPrefs.GetFloat (gamePrefsName + "_SFXVol");//get volume from playerPrefs
		soundObjectList = new List <SoundObject>();

		foreach (AudioClip theSound in GameSounds) 
		{
			tempSoundObj = new SoundObject (theSound, theSound.name, volume);
			soundObjectList.Add (tempSoundObj);
			totalSounds++;
		}
	
	}

	public void PlaySoundByIndex (int anIndexNumber, Vector3 aPosition)
	{
		if (anIndexNumber > soundObjectList.Count) 
		{
			Debug.LogWarning ("Trying to play invalid sound in array, will play last sound in array");
			anIndexNumber = soundObjectList.Count - 1;
		}

		tempSoundObj = soundObjectList [anIndexNumber];
		tempSoundObj.PlaySound (aPosition);


	}



}
