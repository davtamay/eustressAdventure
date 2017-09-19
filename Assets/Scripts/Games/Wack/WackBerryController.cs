using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WackBerryController : MonoBehaviour {


	[SerializeField] private List<SpriteRenderer> totalBerries;

	private int berriesLeft;

	void Awake(){
	
		totalBerries = new List<SpriteRenderer> ();

		foreach (Transform be in transform)
			totalBerries.Add (be.GetComponent<SpriteRenderer> ());

		berriesLeft = totalBerries.Count;
	}
	public void ReduceOneBerry(){
	
		if (berriesLeft > 0) {
			berriesLeft -= 1;
			totalBerries [berriesLeft].sprite = null;

			if (berriesLeft == 0)
				GameController.Instance.isGameOver = true;
	
		}
	
	}

}
