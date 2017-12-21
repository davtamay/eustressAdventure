using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForCopingActivation : MonoBehaviour {

	[SerializeField]private BoolVariable[] copingBoolToCheck;
	[SerializeField]private GameObject[] selectionEffect;

//	private BoolVariable copingBoolToCheck;

	void OnEnable(){

		for (int i = 0; i < copingBoolToCheck.Length; i++) {

			if (copingBoolToCheck [i].isOn)
				selectionEffect [i].SetActive (true);
			else
				selectionEffect [i].SetActive (false);
				


		}

	}
}
