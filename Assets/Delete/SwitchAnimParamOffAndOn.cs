using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[SerializeField]
public class SwitchAnimParamOffAndOn : MonoBehaviour {

	[SerializeField]string nameOfParameter;


	public Animator thisAnimator;

	public bool isUsePDirectorSingleTrackClips = true;
	[SerializeField]private PlayableDirector PDirectorReference;
	[SerializeField] private string nameOfTrack = "SpeakActivation1";
	[SerializeField]float timeDisableOffsetFalseCall;
	[SerializeField]private TimelineClip[] activationTimeLineClips;


	private int count = 0;

	void Awake(){

		thisAnimator = transform.parent.parent.GetComponentInParent<Animator> (); 

		if (isUsePDirectorSingleTrackClips) {
			var timeLineAsset = PDirectorReference.playableAsset as TimelineAsset;

			foreach (var track in timeLineAsset.GetOutputTracks()) {

				var activationTrack = track as ActivationTrack;

				if (activationTrack != null && activationTrack.name == nameOfTrack) {

					activationTimeLineClips = activationTrack.GetClips () as TimelineClip[];

				}

			}
		}
	}
	public void SetIsUsingPDirector(bool condition){

		isUsePDirectorSingleTrackClips = condition;
		
	}

	void OnEnable(){
		Debug.Log ("THIS IS ENABLING ANIMATION");
		thisAnimator.SetBool (nameOfParameter, true);

		if(isUsePDirectorSingleTrackClips)
		StartCoroutine (FalseCall());

	}


	IEnumerator FalseCall(){

		float timer = 0;

		while (true) {
		
			timer += Time.unscaledDeltaTime;

			if (activationTimeLineClips [count].duration - timeDisableOffsetFalseCall < timer) {
				thisAnimator.SetBool (nameOfParameter, false);
				yield break;
			}
			
		
			yield return null;
		
		}


	}


	void OnDisable(){

		if (isUsePDirectorSingleTrackClips)
			count++;
		else
		thisAnimator.SetBool (nameOfParameter, false);
	
	}




}
