using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public class OnEnableAddLocalizationText : MonoBehaviour {

	private LocalizedText localizedReference;
	private Text textComponent;
	private Color originalColor;
	private Color color0Alpha;

	[SerializeField]private float fadeInOutSpeed;
	[SerializeField]private float fadeOutOffset;

	[SerializeField]private string[] localizedTextInOrder;


	[SerializeField]private PlayableDirector PDirectorReference;
	[SerializeField]private TimelineClip[] activationTimeLineClips;

	void Awake(){
	
		localizedReference = transform.parent.GetComponent<LocalizedText> ();
		textComponent = transform.parent.GetComponent<Text> ();
		originalColor = textComponent.color;
		color0Alpha = originalColor;
		color0Alpha.a = 0;
		textComponent.color = color0Alpha;

		var timeLineAsset = PDirectorReference.playableAsset as TimelineAsset;


		foreach (var track in timeLineAsset.GetOutputTracks()) {

			var activationTrack = track as ActivationTrack;

			if (activationTrack != null) {

				//	foreach (var clips in activationTrack.GetClips()) {
				activationTimeLineClips = activationTrack.GetClips () as TimelineClip[];
				//		var activationAsset = clips.asset as ActivationControlPlayable;


			}
		}
	}

	private int count;

	void OnEnable(){


		localizedReference.key = localizedTextInOrder [count];
		localizedReference.OnUpdate ();
	
	
		StartCoroutine (FadeInOutBeforeDisable());
		//StartCoroutine (FadeIn ());


	}

	/*IEnumerator FadeIn(){
	
		while (true) {
		
			yield return null;

			textComponent.color = Color.Lerp (textComponent.color, originalColor, Time.unscaledDeltaTime * fadeInOutSpeed);
		
			if (textComponent.color.a > 9.8f) {
				
				yield break;

			}
		}
	
	
	}*/


	IEnumerator FadeInOutBeforeDisable(){
		bool isFadingOut = false;
		float timer = 0;

		while (true) {
			Debug.Log (isFadingOut);
			timer += Time.unscaledDeltaTime;

			if (!isFadingOut) {
				textComponent.color = Color.Lerp (textComponent.color, originalColor, Time.unscaledDeltaTime * fadeInOutSpeed);

				if (textComponent.color.a > 0.9f)
					isFadingOut = true;

			}else if (isFadingOut && activationTimeLineClips [count].duration - fadeOutOffset < timer  ) {
				
				textComponent.color = Color.Lerp (textComponent.color, color0Alpha, Time.unscaledDeltaTime * fadeInOutSpeed);


				if (textComponent.color.a < 0.02f) {
					yield break;

				}
				
			
				yield return null;
			
			}
		
			yield return null;
		
		}
	

			
			
			
			
			
			
	
	
	}

	void OnDisable(){
		count++;
	//	textComponent.color = originalColor;
		localizedReference.key = string.Empty;
		localizedReference.OnUpdate ();
	
	
	}
}
