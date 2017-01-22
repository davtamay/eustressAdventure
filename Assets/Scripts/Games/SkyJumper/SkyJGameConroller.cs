using UnityEngine;
using System.Collections;

public class SkyJGameConroller : MonoBehaviour {

	public float objRespondTime;
	public float itemRespondTime;
	public float newObstacleTime;

	private float obsTime;
	private float itemTime;

	private float newObsTime;
	private int currentObs;


	private AddRandomPiece addPiece;



	void Awake(){
		addPiece = GetComponent<AddRandomPiece> ();
	
	}
	void Update () {

		obsTime += Time.deltaTime;
		itemTime += Time.deltaTime;

		newObsTime += Time.deltaTime;

		if (obsTime >= objRespondTime) { 

			obsTime = 0.0f;

			if (newObstacleTime < newObsTime) {
				currentObs++;
				newObsTime = 0.0f;
			}

				addPiece.AddNewObstacle (currentObs);

		}
		if (itemTime >= itemRespondTime) { 

			itemTime = 0.0f;
			addPiece.AddNewItem ();

		}
		return;

			
		

	
	}
}
