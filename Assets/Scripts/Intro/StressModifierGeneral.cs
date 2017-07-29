using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressModifierGeneral : MonoBehaviour {

	[SerializeField]private float stressEffect;
	[SerializeField]private float timeUntilStressEffect;

	float timer = 0;
	void Update () {

		timer += Time.deltaTime;
		Debug.Log ("Is RUNNING");

		if (timeUntilStressEffect > timer) {
			UIStressGage.Instance.stress = stressEffect;
			timer = 0;
		}


	}
}
