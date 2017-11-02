using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;



public class WackGameManager : MonoBehaviour {

	[SerializeField]public WaveController waveController;
//	[SerializeField]public WackLookClick wacklookClick;

	public Transform centerPos;
	//[SerializeField]private WackBerryController wackBerryController;
	public GameObject[] totalMoles;
	public List<GameObject> activeMoles; 

	public Transform[]totalBranches;
	private List<Transform> totalBerries;
	[SerializeField]private int berriesLeft;
	[SerializeField]private Text berriesText;
	[SerializeField]private GameTimer gameTimer;


	public static WackGameManager Instance
	{ get { return instance; } }

	private static WackGameManager instance = null;

	void Awake()
	{

		if (instance) {
			Debug.LogError ("Two instances of singleton (WackGameManager)");

			return;
		}
		instance = this; 


		totalMoles = waveController.GetAllGOInAllWaves ().ToArray();


	}
	public void Start(){
	
		totalBerries = new List<Transform> ();

		foreach (Transform be in totalBranches) {

			foreach (Transform b in be)
				totalBerries.Add (b);

		}

		berriesLeft = totalBerries.Count;
		berriesText.text = ": " + berriesLeft; 
	
	
	}
	public bool BranchHasBerries(Transform branch){


		foreach (Transform be in branch) {

			if (be.gameObject.activeInHierarchy)
				return true;

		}

		return false;
	}


	public void ReduceBerry(Transform branch){

		foreach (Transform be in branch) {

			if (be.gameObject.activeInHierarchy) {

				be.gameObject.SetActive (false);
				totalBerries.Remove (be);
				berriesLeft -= 1;

				if (berriesLeft == 0) {
					GameController.Instance.isGameOver = true;
					gameTimer.SetGameOver ("No More Berries :(");
					StartCoroutine (GetComponent<WackLookClick>().TurnOffAll (Mathf.Infinity));
					waveController.StopWaveAndDisableObjects ();
					//text = "No More Berries :(";
				}

				berriesText.text = ": " + berriesLeft; 

				break;
			}

		}
	}

	public Transform GetClosestBush(Transform moleTrans){
		Transform closestBush = null;

		float closestBushDistance = Mathf.Infinity;

		foreach (Transform bs in WackGameManager.Instance.totalBranches) {

			if (Vector3.Distance (moleTrans.position, bs.position) < closestBushDistance) {

				if (!BranchHasBerries (bs))
					continue;

				closestBush = bs;
				closestBushDistance = Vector3.Distance (moleTrans.position, bs.position);

			}

		}
		return closestBush;
	//	StartCoroutine (SeekBush ());

	}

		

	public void UpdateMoleActiveList(){

		if (totalMoles.Length == 0)
			return;
		
		for (int i = 0; i < totalMoles.Length; i++) {

			if (totalMoles [i].activeInHierarchy) {
				if (activeMoles.Contains (totalMoles [i]))
					continue;
				else
				activeMoles.Add (totalMoles [i]);
				   }
	
		}
		//Debug.Log ("Number of active moles: " + WackGameManager.Instance.activeMoles.Count);
	}

	//public void Sto
}