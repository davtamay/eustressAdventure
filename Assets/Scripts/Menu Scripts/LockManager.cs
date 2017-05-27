using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour {


	public static bool isFinderAvailable;
	public static bool isWackAvailable;
	public static bool isSkyJumperAvailable;
	public static bool isMatchAvailable;
	public static bool isHitAvailable;
	public static bool isCollectionsAvailable;
	public static bool isShootAvailable;
	public static bool isHoopAvailable;

	//public Game curGame = Game.COLLECTIONS;
	[SerializeField]private int finderScoreToBeat;
	[SerializeField]private int wackScoreToBeat;
	[SerializeField]private int skyJumperScoreToBeat;
	[SerializeField]private int matchScoreToBeat;
	[SerializeField]private int hitScoreToBeat;
	[SerializeField]private int collectionsScoreToBeat;
	[SerializeField]private int shootScoreToBeat;
	[SerializeField]private int hoopScoreToBeat;

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

			//if (DataManager.Instance.LoadFinderScore () > finderScoreToBeat) {
			if(isSkyJumperAvailable){
				lockGame.gameObject.SetActive (false);
				game.gameObject.SetActive (true);

			}
			else
				game.gameObject.SetActive (false);

			break;
		
		case Game.COLLECTIONS:

			if (isCollectionsAvailable) {
				lockGame.gameObject.SetActive (false);
				game.gameObject.SetActive (true);
			}
			else
				game.gameObject.SetActive (false);
			
			break;
		
		case Game.FINDER:

			if (isFinderAvailable) {
				lockGame.gameObject.SetActive (false);
				game.gameObject.SetActive (true);
			}else
				game.gameObject.SetActive (false);
			
			break;
		case Game.HIT:

			if (isHitAvailable){
				lockGame.gameObject.SetActive (false);
				game.gameObject.SetActive (true);
			}else {

				game.gameObject.SetActive (false);
				//GameAssessCollider.enabled = false;
				
			
			}


			break;
		case Game.MATCH:
			if (isMatchAvailable) {
				lockGame.gameObject.SetActive (false);
				game.gameObject.SetActive (true);
			}else
				game.gameObject.SetActive (false);
			
			break;
		case Game.WACK:
			if (isWackAvailable) {
				lockGame.gameObject.SetActive (false);
				game.gameObject.SetActive (true);
			}else
				game.gameObject.SetActive (false);
			
			break;
		case Game.SHOOT:
			if (isShootAvailable) {
				lockGame.gameObject.SetActive (false);
				game.gameObject.SetActive (true);
			}else
				game.gameObject.SetActive (false);
			
			break;
		case Game.HOOP:
			if (isHoopAvailable) {
				lockGame.gameObject.SetActive (false);
				game.gameObject.SetActive (true);
			}else
				game.gameObject.SetActive (false);
			
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
