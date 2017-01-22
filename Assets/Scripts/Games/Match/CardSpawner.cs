using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Difficulty{easy,medium,hard}
public class CardSpawner : MonoBehaviour {


	//public Difficulty difficulty = Difficulty.easy;
	public Texture2D[] images;
	public Sprite[] sprites;

	public Transform easy;
	public Transform medium;
	public Transform hard;

	private Vector3 easyStartPos;
	private Vector3 mediumStartPos;
	//private Vector3 hardStartPos;

	public bool isEasy;
	public bool	isMedium;
	public bool isHard;

	[SerializeField] List<GameObject> cardObjects;

	private int cardsToSpawn;
	public int GetSpawned{

		get{return cardsToSpawn;}

	}

	private int wave;
	public int GetWave {

		get{ return wave;}

	}


	void Awake () {

		easyStartPos = easy.position;
		mediumStartPos = medium.position;
	//	hardStartPos = hard.position;
	

		ChangeWave (Difficulty.easy);
		StartCoroutine (testOn ());

	}

	IEnumerator testOn(){
	
		yield return new WaitForSeconds (8);
		//ChangeWave (Difficulty.hard);
	}

	public void ChangeWave (Difficulty diff){


		switch (diff) {

		case Difficulty.easy:

			cardsToSpawn = 16;
				//Destroy (medium.gameObject);
				//Destroy (hard.gameObject);
				wave = 0;
				medium.gameObject.SetActive (false);
				hard.gameObject.SetActive (false);
				break;

			case Difficulty.medium:

				cardsToSpawn = 32;
				//Destroy (hard.gameObject);
				wave = 1;
				easy.position = easyStartPos;
				medium.gameObject.SetActive (true);
				break;

			case Difficulty.hard:

				cardsToSpawn = 48;
				wave = 2;
				easy.position = easyStartPos;
				medium.position = mediumStartPos;
				hard.gameObject.SetActive (true);
				break;


		}

		foreach (GameObject gO in cardObjects)
			gO.SetActive (false);


		Shuffle (sprites);
		for (int i = 0; i < cardsToSpawn; i++) {
			images [i] = Extensions.textureFromSprite (sprites [i]);
			images [i].name = sprites [i].name;
		}

		InstantiateCards ();




	}



	public void InstantiateCards (){

		Debug.Log (cardsToSpawn);
		int cardNumber = 0;

		for (int i = 0; i < cardsToSpawn; i++){

			GameObject topCard = GameObject.CreatePrimitive (PrimitiveType.Quad);
			GameObject bottomCard = GameObject.CreatePrimitive (PrimitiveType.Quad);

			topCard.name = images[i].name;


			topCard.transform.tag = "Card";


			topCard.GetComponent<Renderer> ().material.color = Color.blue;
			bottomCard.GetComponent<Renderer> ().material.SetTexture ("_MainTex", images[i]);

			cardObjects.Add (topCard);
			cardObjects.Add (bottomCard);


			if (i <= 15) {

				topCard.transform.rotation = easy.transform.rotation;
				topCard.transform.position = easy.position + ((-2 * Vector3.right) * cardNumber);//new Vector 0,0,0.1f
				bottomCard.transform.position = easy.position + ((-2 * Vector3.right) * cardNumber) + new Vector3 (0, 0, 0f);
				isEasy = true;
			
			} else if (i > 15 && i <= 31) {

				if (i == 16)
					cardNumber = 0;

				topCard.transform.rotation = medium.transform.rotation;
				topCard.transform.position = medium.position + ((2 * Vector3.forward) * cardNumber);
				bottomCard.transform.position = medium.position + ((2 * Vector3.forward) * cardNumber) + new Vector3 (0, 0, 0);
				isEasy = false;
				isMedium = true;
		
			} else if (i > 31 && i <= 47) {


				if (i == 32)
					cardNumber = 0;

				topCard.transform.rotation = hard.transform.rotation;
				topCard.transform.position = hard.position + ((2 * Vector3.forward) * cardNumber);
				bottomCard.transform.position = hard.position + ((2 * Vector3.forward) * cardNumber) + new Vector3 (0, 0, 0);
				isMedium = false;
				isHard = true;

			}
				

			bottomCard.transform.parent = topCard.transform;
			bottomCard.transform.localRotation = Quaternion.Euler (0, 180, 0);



			++cardNumber;

			if (0 == cardNumber % 5) {



				if (isEasy) {

					easy.gameObject.SetActive (false);
					//Destroy (easy.gameObject);
					topCard.transform.position = easy.position + (Vector3.down * 1.5f);
					bottomCard.transform.position = easy.position + (Vector3.down * 1.5f);
					easy.position = topCard.transform.position;

				
				} else if (isMedium) {

					medium.gameObject.SetActive (false);
					//Destroy (medium.gameObject);
					topCard.transform.position = medium.position + (Vector3.down * 1.5f);
					bottomCard.transform.position = medium.position + (Vector3.down * 1.5f);
					medium.position = topCard.transform.position;
				

				} else if (isHard) {

					easy.gameObject.SetActive (false);
					//Destroy (hard.gameObject);
					topCard.transform.position = hard.position + (Vector3.down * 1.5f);
					bottomCard.transform.position = hard.position + (Vector3.down * 1.5f);
					hard.position = topCard.transform.position;
				
				}

				cardNumber = 1;
			}

		} 

	}
	

	void Shuffle (Sprite [] array){

		for (int t = 0; t < cardsToSpawn; t++){

			Sprite tmp = array [t];
			int r = Random.Range (t, cardsToSpawn);
			array [t] = array [r];
			array [r] = tmp;
		}

	}
	

}
