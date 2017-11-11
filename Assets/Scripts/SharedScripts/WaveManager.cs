using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[ExecuteInEditMode]
public class WaveManager : MonoBehaviour {

	private Transform thisTransform;

	[Header("Main Game Events")]
	[SerializeField]UnityEvent onGameStart;
	[SerializeField]UnityEvent onGameCompleted;

	[Header("Wave Transition Events")]
	[SerializeField]UnityEvent onWaveEnd;
	[SerializeField]UnityEvent onNewWaveStart;
	[SerializeField]UnityEvent onNewWaveObjectsEnabled;

	[SerializeField]private GameObject newWaveIntermissionIndicator;
	[SerializeField]private float newWaveIntermissionTime = 3;
	[SerializeField]private bool isDeactivateIndicatorAfterIntermission;

	[Tooltip("Do you want to leave wave on for the next wave?")]
	[SerializeField]private bool leaveWavesActiveWhenDone;

	[Header("Timer Settings")]
	[SerializeField]float timerSpeed = 1.2f;

	[Header("Waves (Based on children)")]
	[Tooltip("Create waves by parenting children with objects to spawn")]
	[SerializeField]private int currentWave = 0;
	public List<WaveObject> waveEvents;

	private Dictionary<int,WaveObject>waveID;
	private List<GameObject> totalGOs;
	private List<int> myIndices;

	private static WaveManager instance;
	public static WaveManager Instance
	{ get { return instance; } }

	void Awake(){

		if (instance) {
			Debug.LogWarning ("There are two WaveManagers in scene - deleting late instance.");
			DestroyImmediate(this.gameObject);
			return;
		}
		instance = this; 
		
		thisTransform = transform;

		totalGOs = new List<GameObject> ();

		foreach (Transform gOP in transform) {
			foreach (Transform gOC in gOP) {
				totalGOs.Add (gOC.gameObject);
				gOC.gameObject.SetActive (false);
			}
		}
}
		
#if UNITY_EDITOR
//INSPECTOR SET UP TO SHOW WAVE OBJECTS DEPENDING ON PRESENT CHILDREN IN PARENT 
	void Update(){

	if(!Application.isPlaying)
		if (transform.hasChanged) {
			
			waveID = new Dictionary<int, WaveObject> ();

				int count = 0;
				int waveNum = 0;
				List<int> tempArray = new List<int> ();
				foreach (Transform t in transform) {

					if (t.parent == transform) {
						
						tempArray.Add (t.GetInstanceID());

					if (count >= waveEvents.Count) {
						waveID.Add (t.GetInstanceID (), new WaveObject ());
						waveEvents.Add (waveID [t.GetInstanceID ()]);

						waveID [t.GetInstanceID ()].InstanceID = t.GetInstanceID();
						waveID [t.GetInstanceID ()].waveTransformParent = t;
						waveID [t.GetInstanceID ()].waveNumber = waveNum;
						++waveNum;
							
					} else {
						waveID.Add (t.GetInstanceID (), waveEvents [count]);

						waveID [t.GetInstanceID ()].InstanceID = t.GetInstanceID();
						waveID [t.GetInstanceID ()].waveTransformParent = t;
						waveID [t.GetInstanceID ()].waveNumber = waveNum;
						++waveNum;
					}
						++count;
					}

				}

				while(waveEvents.Count > tempArray.Count){

					if (tempArray.Count == 0 || waveEvents.Count == 1){
						waveEvents.Clear ();
						break;
					}

				waveEvents.Remove(waveEvents.Last ());

				}
						
			}
	}
//THIS IS THE CUSTOM DATA BLOCK FOR EACH WAVE
#endif
	[System.Serializable]
	public class WaveObject{

		[HideInInspector]	
		public int InstanceID;
		public int waveNumber;
		[Tooltip("This is your parent holding the spawned objects.")]
		public Transform waveTransformParent;
		public UnityEvent onIndividualWaveStart;
		public int numberToSpawn;
		public int timeUntilNextWave;

	}


	void Start(){
		
		if (Application.isPlaying) {
			//CHECK FOR PRESENCE OF WAVE INDICATOR IN SCENE
			if (GameObject.FindWithTag ("NewWave")) {
				newWaveIntermissionIndicator = GameObject.FindWithTag ("NewWave");
				//OBTAIN INDICATORS OWN TIME UNTIL DISAPEAR - USEFUL FOR INDICATORS THAT USE FADING EFFECTS
				if(newWaveIntermissionIndicator.transform.GetComponent<NewWaveTransition> () != null)
					newWaveIntermissionTime = newWaveIntermissionIndicator.transform.GetComponent<NewWaveTransition> ().timeUntilDisapear;
				newWaveIntermissionIndicator.gameObject.SetActive (false);
			}

			if (waveEvents.Count == 0)
				return;
		
			StartCoroutine (OnUpdate ());
		}
	}


	private float timer;
	private bool isDone;
	private bool isByPassIsDoneCheck = false;

	IEnumerator OnUpdate(){

		//ADD TIME FOR FIRST WAVE
		TimeToAdd (ref isDone, waveEvents [0].timeUntilNextWave);

		//GET RANDOMIZED INDEX LIST OF CHILDREN
		RandomizeGOToEnable (waveEvents [0].numberToSpawn, waveEvents [0].waveTransformParent);

		//ACTIVATE 
		foreach(int gO in myIndices)
			waveEvents [0].waveTransformParent.GetChild (gO).gameObject.SetActive (true);

		onGameStart.Invoke ();

		while (true) {
			yield return null;
		
			//CHECK TIMER FOR COMPLETION OR BYPASS THE CHECK TO SET COMPLETION YOURSELF
			if (!isByPassIsDoneCheck)
				TimeToAdd (ref isDone);
			else
				isByPassIsDoneCheck = false;

			if (isDone){ 
				isDone = false;

				if (!leaveWavesActiveWhenDone)
					transform.GetChild (currentWave).gameObject.SetActive (false);

				if (currentWave + 1 == transform.childCount) {
					onGameCompleted.Invoke ();
					Debug.Log ("GAME ENDED!");
					EndWavesAndDisableAllObjects ();
					yield break;
				}

				onWaveEnd.Invoke ();
				yield return StartCoroutine (NewWaveIntermission ());
				onNewWaveStart.Invoke ();

				++currentWave;

				TimeToAdd (ref isDone, waveEvents [currentWave].timeUntilNextWave);

				RandomizeGOToEnable (waveEvents [currentWave].numberToSpawn, waveEvents [currentWave].waveTransformParent);

				foreach (int gO in myIndices) 
					waveEvents [currentWave].waveTransformParent.GetChild (gO).gameObject.SetActive (true);

				onNewWaveObjectsEnabled.Invoke ();

				waveEvents[currentWave].onIndividualWaveStart.Invoke ();
			
			}
	}
		
	}
	/// <summary>
	/// Stop all waves and deactivate all active GameObjects in waves.
	/// </summary>
	public void EndWavesAndDisableAllObjects(){

		StopAllCoroutines ();

		foreach (Transform gOP in transform) {
			foreach (Transform gOC in gOP) {
				totalGOs.Add (gOC.gameObject);
				gOC.gameObject.SetActive (false);
			}
		}

	}
	/// <summary>
	/// Randomizes children indixes of provided parent by setting up myIndices List.
	/// </summary>
	/// <param name="numToRespawn">Number that you want to respawn.</param>
	/// <param name="parent">parent that you want to randomize children for..</param>
	private void RandomizeGOToEnable(int numToRespawn, Transform parent){

		int ChildCount = parent.childCount;

		if (numToRespawn > ChildCount) {
			Debug.LogWarning ("numToRespawn/GOToRespond can't be bigger than available childCount");
			return;
		}

		myIndices = new List<int> (ChildCount);

		for (int i = 0; i < numToRespawn; i++) {

		int myIndexPull = Random.Range(0, ChildCount);


		while (myIndices.Contains (myIndexPull)) {

			myIndexPull = Random.Range (0, ChildCount);

		}

		myIndices.Add (myIndexPull);

	}
}

	public List<GameObject> GetAllGOInAllWaves(){
	
		return totalGOs;
	
	}

	bool isTimerOn = false;
	float timerTimeScale = 1;
	Coroutine timerCoroutine;
	/// <summary>
	/// Check or AddTime to WaveTimer
	/// </summary>
	/// <returns>Current Time in string 00:00</returns>
	/// <param name="isDone">Add Ref bool local variable to have reference to current timer completion flag.</param>
	/// <param name="time">Add time to current wave timer, it could be left out if only checking isDone.</param>
	public string TimeToAdd(ref bool isDone, float time = 0f){

		timer += time;

		if (time > 0f) {

			isTimerOn = true;
				
			//PREVENT RUNNING TWO COROUTINES WHEN ADDING TIME
			if (timerCoroutine != null)
				StopCoroutine (timerCoroutine);
			
				timerCoroutine = StartCoroutine (StartTimer ());

		}

		string minutes = Mathf.Floor(timer /60).ToString("00");
		string seconds = Mathf.Floor (timer % 60).ToString ("00");

		if (timer < 0f) {
			isDone = true;
			isTimerOn = false;
		}

		return minutes + ":" + seconds;

	}

	private IEnumerator StartTimer(){

		while (isTimerOn) {

			timer -= Time.deltaTime * timerSpeed * timerTimeScale;

			yield return null;
		}
			
	}

	public void ResumeTimer(){

		timerTimeScale = 1;
	}

	public void StopTimer(){

		timerTimeScale = 0;

	}
	public void CompleteTimer(){

		timer = 0;
	}
	public void ChangeTONextWaveAndAddTime(){

		isByPassIsDoneCheck = true;
		isDone = true;
	}
		
	public float GetCurrentTime(){

		return timer;
	}

	bool isWaveIndicatorOn;
	public IEnumerator NewWaveIntermission(){
		isWaveIndicatorOn = true;

		if (newWaveIntermissionIndicator != null) {
			newWaveIntermissionIndicator.SetActive (true);
			yield return new WaitForSeconds (newWaveIntermissionTime);
			if (isDeactivateIndicatorAfterIntermission)
				newWaveIntermissionIndicator.SetActive (false);
		}else
			yield return new WaitForSeconds (newWaveIntermissionTime);

		isWaveIndicatorOn = false;
	}

	public float GetNewWaveTime{

		get{return newWaveIntermissionTime;}	
	}

	public bool GetNewWaveTransitionState(){

		if (isWaveIndicatorOn)
			return true;

		return false;

	}




}




