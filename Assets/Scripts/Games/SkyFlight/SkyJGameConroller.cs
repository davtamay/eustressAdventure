using UnityEngine;
using System.Collections;

public class SkyJGameConroller : MonoBehaviour {

	public float obstacleRespondTime;

	public float itemRespondTime;
	public float newWaveTime;
	[SerializeField]private float newWaveObstacleRespondTimeDecrement;

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

			if (obsTime >= obstacleRespondTime) { 
				addPiece.AddNewObstacle (currentObs);
				obsTime = 0.0f;

				if (newWaveTime < newObsTime) {


					obstacleRespondTime -= newWaveObstacleRespondTimeDecrement * PlayerManager.Instance.points;
					newObsTime = 0.0f;

					//FIXME Have to add real wave transition TI
					//yield return StartCoroutine (WaveManager.Instance.NewWaveIntermission ());



				//	yield return StartCoroutine (GameController.Instance.NewWave ());
				//	AudioManager.Instance.PlayInterfaceSound("MediumWin");
					++currentObs;
					continue;
				
				}



			}



			
		

		}
	}
}
