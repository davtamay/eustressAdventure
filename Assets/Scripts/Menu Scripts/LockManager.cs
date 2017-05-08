using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour {


	//public Game curGame = Game.COLLECTIONS;
	[SerializeField]private int finderScoreToBeat;
	[SerializeField]private int wackScoreToBeat;
	[SerializeField]private int skyJumperScoreToBeat;
	[SerializeField]private int matchScoreToBeat;
	[SerializeField]private int hitScoreToBeat;
	[SerializeField]private int collectionsScoreToBeat;
	[SerializeField]private int shootScoreToBeat;
	[SerializeField]private int hoopcoreToBeat;

	[SerializeField] private Game curGame;


	private Collider GameAssessCollider;
	private ParticleSystem.MainModule particleSys;

	private Transform game;
	private Transform lockGame;

	void Start(){

		GameAssessCollider = GetComponentInChildren<Collider> ();



		foreach (Transform gO in transform) {
			if (gO.name == "Game")
				game = gO;

			if (gO.name == "Lock")
				lockGame = gO;
		
		
		}


		switch (curGame) {

		case Game.SKYJUMPER:

			if (DataManager.Instance.LoadFinderScore () > finderScoreToBeat)
				lockGame.gameObject.SetActive (false);
			else
				game.gameObject.SetActive (false);

				break;
		
		case Game.COLLECTIONS:
			break;
		case Game.FINDER:
			break;
		case Game.HIT:

			if (DataManager.Instance.LoadFinderScore () > finderScoreToBeat)
				lockGame.gameObject.SetActive (false);

			else {

				game.gameObject.SetActive (false);
				GameAssessCollider.enabled = false;
				
			
			}


			break;
		case Game.MATCH:
			break;
		case Game.WACK:
			break;
		case Game.SHOOT:
			break;
		case Game.HOOP:
			break;


		}

	}
	void CheckLock(){
	
	
	
	
	
	}
	//static may turn everything off since it is only one instance;
	/*public static void CheckLock(Game curGame){

		switch (curGame) {
		
		case Game.SKYJUMPER:

			if (HighScoreManager.Instance.LoadFinderScore() > finderScoreToBeat)


				break;

		}
		
		
		
		
	}*/


}
