using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicTrackName : MonoBehaviour {

	public Text trackText;


	public void Awake(){
	
		trackText = GetComponent<Text> ();
	
		//if (MusicController.Instance.GetCurrentTrackName () == null)
			trackText.text = "Track:";
	
	
	}

	void Update(){
//	IEnumerator Start () {
		


	//	while (true) {
		
			//yield return null;
			//Debug.Log ("testing this track");
			trackText.text = "Track: "+ AudioManager.Instance.GetCurrentTrackName();//MusicController.Instance.GetCurrentTrackName();
			//if I leave space, text does not show up and cannot use yield return WaitForSeconds ???
	}

	//	}
	
//	}

	

}
