using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameSelection : MonoBehaviour {

	private ParticleSystem.MainModule particleSys;


	[SerializeField] private string gameSceneName;
	[SerializeField] private float timeUntilSceneChange;

	//Not working with ParticleSystem.MainModule

	//[SerializeField] private Color stepOutColor;
	//[SerializeField] private Color stepInColor;
	//private Collider thisCollider;
	public bool isOn = true;


	void Awake(){

		//thisCollider = GetComponent<Collider> ();
	
		particleSys = GetComponent <ParticleSystem> ().main;
		particleSys.startColor = new ParticleSystem.MinMaxGradient (Color.green);


		//AudioGO.transform.position = transform.position;
		//AudioManager.Instance.PlayAmbientSoundAndActivate ("Portal", false, false, 0, transform);
	}
	void Start(){
	
		GameObject AudioGO = AudioManager.Instance.MakeCopyOfAudioSourceGO (AudioManager.AudioReferanceType._AMBIENT, "Portal");
		AudioGO.transform.SetParent (transform);
		AudioGO.transform.localPosition = Vector3.zero;
		AudioGO.transform.SetParent (AudioManager.Instance.GetAudioSourceReferance(AudioManager.AudioReferanceType._AMBIENT, "Portal").transform.parent);
	
	}


	void OnTriggerEnter (Collider other){
	
	
		if (other.CompareTag ("Player")) {

			if (!other.GetComponent<PlayerLookMove> ().enabled) {
				isOn = false;
				return;
			} else
				isOn = true;

			particleSys.startColor = new ParticleSystem.MinMaxGradient(Color.blue);


				
		}
	
	
	}

	private float timer = 0;

	void OnTriggerStay(Collider other){

		if (!isOn)
			return;
			
		if (other.CompareTag ("Player")) {

			timer += Time.deltaTime;

			if (timer > timeUntilSceneChange) {
			
				SceneController.Instance.Load (gameSceneName);
				timer = 0;
			
			
			}

		
		}


	}

	void OnTriggerExit(Collider other){
	

		if (other.CompareTag ("Player")) {

			if (!other.GetComponent<PlayerLookMove> ().enabled) {
				isOn = true;
				return;
			}

			timer = 0;
			particleSys.startColor = new ParticleSystem.MinMaxGradient(Color.green);
		}
	

	
	}





}
