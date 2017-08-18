using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using TagFrenzy;

public class GameSuggest : MonoBehaviour {

	public Button[] angryGames;
	public Button[] anxiousGames;
	public Button[] dissapointedGames;
	public Button[] frustratedGames;
	public Button[] sadGames;
	public Button[] worriedGames; //annoyed


	public Button first;
	public Button second;
	public Button third;

	 
	public GameObject suggestions;




	void Start(){
		
	
	//var	games = MultiTag.FindGameObjectsWithTags (Tags.Game);


	//	foreach (GameObject gO in games) {
		
		//	gO.SetActive (false);
		
	//	}
	}
		
	public void SelectGames(string typeofstress){




		switch (typeofstress) {

		case "Angry":

			Debug.Log ("angry");

			RandomizeGames (angryGames);

			break;

		case "Anxious":

			RandomizeGames (anxiousGames);

			break;


		case "Disappointed":

			RandomizeGames (dissapointedGames);

			break;

		case "Frustrated":

			RandomizeGames (frustratedGames);
			break;


		case "Sad":

			RandomizeGames (sadGames);
			break;

		case "Worried":

			RandomizeGames (worriedGames);
			break;

		}

	}



		void RandomizeGames(Button[] arrayOfGames){


			first = arrayOfGames [Random.Range (0, arrayOfGames.Length)];

			second = arrayOfGames [Random.Range (0, arrayOfGames.Length)];
			

			while (second.GetInstanceID() == first.GetInstanceID()) {

				second = arrayOfGames [Random.Range (0, arrayOfGames.Length)];
	
				}

					third = arrayOfGames [Random.Range (0, arrayOfGames.Length)];

					while (third.GetInstanceID() == second.GetInstanceID() 
							|| third.GetInstanceID() == first.GetInstanceID()){
				
					third = arrayOfGames [Random.Range (0, arrayOfGames.Length)];
					Debug.Log (third.name);
					}
			
			suggestions.SetActive (true);
			first.gameObject.SetActive (true);
			second.gameObject.SetActive (true);
			third.gameObject.SetActive (true);
	
		
		}

		void ActivateAllGames(){

				
			foreach (Transform child in transform) {

				child.gameObject.SetActive (true);

	
		}

		}
	


}
