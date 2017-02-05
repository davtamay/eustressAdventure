using UnityEngine;
using System.Collections;

public class SkyJGameConroller : MonoBehaviour {

	public float objRespondTime;
	public float itemRespondTime;
	public float newObstacleTime;

	private float obsTime;
	private float itemTime;

	private float newObsTime;
	private int currentObs = 1;



	private AddRandomPiece addPiece;



	void Awake(){

		addPiece = GetComponent<AddRandomPiece> ();


		StartCoroutine (OnUpdate ());
	}
	IEnumerator OnUpdate () {

		while (true) {


			obsTime += Time.deltaTime;
			itemTime += Time.deltaTime;

			newObsTime += Time.deltaTime;

			yield return null;

			if (itemTime >= itemRespondTime) { 

				itemTime = 0.0f;
				addPiece.AddNewItem ();

			}

			if (obsTime >= objRespondTime) { 
				addPiece.AddNewObstacle (currentObs);
				obsTime = 0.0f;



				if (newObstacleTime < newObsTime) {

					newObsTime = 0.0f;

					StartCoroutine (GameController.Instance.NewWave ());

					++currentObs;
					continue;
				//	addPiece.AddNewObstacle (currentObs);
				}



			}

			//continue;

			
		

		}
	}
}
