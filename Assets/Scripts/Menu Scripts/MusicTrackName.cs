using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicTrackName : MonoBehaviour {

	public Text trackText;
	private LocalizedText localizedText;

	void Start(){
		trackText = GetComponent<Text> ();
		localizedText = GetComponent<LocalizedText> ();

		//while(!LocalizationManager.Instance.GetIsReady())
		localizedText.OnUpdate();
		trackText.text += " " + AudioManager.Instance.GetCurrentTrackName();

	}
	public void OnUpdateTrackName(){
//	IEnumerator Start () {

		
		trackText.text = string.Empty;
		localizedText.OnUpdate();

	//	while (true) {
		
			//yield return null;
			//Debug.Log ("testing this track");
			trackText.text += " " + AudioManager.Instance.GetCurrentTrackName();//MusicController.Instance.GetCurrentTrackName();
			//if I leave space, text does not show up and cannot use yield return WaitForSeconds ???
	}

	//	}
	
//	}

	

}
