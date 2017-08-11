using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressModifierGeneral : MonoBehaviour {

	[SerializeField]private float stressEffect;
	[SerializeField]private float timeUntilStressEffect;

	float timer = 0;
	void Update () {

		timer += Time.deltaTime;


		if (timeUntilStressEffect < timer) {
			Debug.Log (this.name + " - stress Is RUNNING");
			UIStressGage.Instance.stress = stressEffect;
			timer = 0;
		}


	}


}
