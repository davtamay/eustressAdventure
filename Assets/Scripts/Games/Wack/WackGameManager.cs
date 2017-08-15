using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;



public class WackGameManager : MonoBehaviour {



	public GameObject[] totalMoles;
	public List<GameObject> activeMoles; 




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


	}


	//CheckifMolesAreActiveInHierarcy (AKA scene - taking into account parent active)

	public void AddMolesToActiveList(){
		
		for (int i = 0; i < totalMoles.Length; i++) {

			if (totalMoles [i].activeInHierarchy) {
				if(activeMoles.Contains(totalMoles[i]))
				   continue;
				else
				activeMoles.Add (totalMoles [i]);
				   }
	
		}
		Debug.Log ("Number of active moles: " + WackGameManager.Instance.activeMoles.Count);
	}
}