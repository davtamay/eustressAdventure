using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Difficulty{easy,medium,hard}
public class CardSpawner : MonoBehaviour {



	public Texture2D[] images;
	public Sprite[] sprites;

	//[SerializeField] Vector3 cardScale
	[SerializeField] float topPadding = 1.5f;
	[SerializeField] float sidePadding = -2;

	private LookSelect lookSelect;

	public Transform easy;
	public Transform medium;
	public Transform hard;

	private Vector3 easyStartPos;
	private Vector3 mediumStartPos;
	private Vector3 hardStartPos;




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
		lookSelect = GetComponent<LookSelect> ();

		easyStartPos = easy.position;
		mediumStartPos = medium.position;
		hardStartPos = hard.position;
	


		ChangeWave (Difficulty.easy);
	

		if (isHard)
			ChangeWave (Difficulty.hard);
		else if (isMedium)
			ChangeWave (Difficulty.medium);
		else if (isEasy)
			ChangeWave (Difficulty.easy);

		

	}



	public void ChangeWave (Difficulty diff){
		lookSelect.isFirstCard = false;
		lookSelect.isSecondCard = false;
	
		switch (diff) {

			case Difficulty.easy:

				cardsToSpawn = 16;

				wave = 0;
				medium.gameObject.SetActive (false);
				hard.gameObject.SetActive (false);
				break;

			case Difficulty.medium:

				cardsToSpawn = 32;
				
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

		foreach (GameObject gO in cardObjects) {
		//	cardObjects.Remove (gO);


			Destroy (gO);//gO.SetActive (false);
		}

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

			topCard.GetComponent<MeshCollider> ().convex = true;





			topCard.name = images[i].name;


			topCard.transform.tag = "Card";


			topCard.GetComponent<Renderer> ().material.color = Color.blue;
			bottomCard.GetComponent<Renderer> ().material.SetTexture ("_MainTex", images[i]);

			cardObjects.Add (topCard);
			cardObjects.Add (bottomCard);


			if (i <= 15) {

				topCard.transform.localRotation = easy.transform.rotation;
				topCard.transform.localPosition = easy.position + ((sidePadding * Vector3.right) * cardNumber);//new Vector 0,0,0.1f
				//topCard.transform.localScale = easy.transform.localScale;
				bottomCard.transform.localPosition = easy.position + ((sidePadding * Vector3.right) * cardNumber) + new Vector3 (0, 0, 0f);
				//\
				isEasy = true;
			
			} else if (i > 15 && i <= 31) {

				if (i == 16)
					cardNumber = 0;

				topCard.transform.localRotation = medium.transform.rotation;
				topCard.transform.localPosition = medium.position + ((sidePadding * Vector3.right) * cardNumber);
			//	topCard.transform.localScale = medium.transform.localScale;
				bottomCard.transform.localPosition = medium.position + ((sidePadding * Vector3.right) * cardNumber) + new Vector3 (0, 0, 0);
			//	bottomCard.transform.localScale = medium.transform.localScale;

				isEasy = false;
				isMedium = true;
		
			} else if (i > 31 && i <= 47) {


				if (i == 32)
					cardNumber = 0;

				topCard.transform.rotation = hard.transform.rotation;
				topCard.transform.position = hard.position + ((sidePadding * Vector3.right) * cardNumber);
			//	topCard.transform.localScale = hard.transform.localScale;
				bottomCard.transform.position = hard.position + ((sidePadding * Vector3.right) * cardNumber) + new Vector3 (0, 0, 0);
			//	bottomCard.transform.localScale = hard.transform.localScale;
				isMedium = false;
				isHard = true;

			}

			bottomCard.transform.parent = topCard.transform;

			topCard.transform.localScale = easy.transform.localScale;


			bottomCard.transform.localRotation = Quaternion.Euler (0, 180, 0);



			++cardNumber;

			if (0 == cardNumber % 5) {



				if (isEasy) {

					easy.gameObject.SetActive (false);
					//Destroy (easy.gameObject);
					topCard.transform.position = easy.position + (-topCard.transform.up * topPadding );
					bottomCard.transform.position = easy.position + (-topCard.transform.up *topPadding);
					easy.position = topCard.transform.position;
				

				
				} else if (isMedium) {

					medium.gameObject.SetActive (false);
					//Destroy (medium.gameObject);
					topCard.transform.position = medium.position + (-topCard.transform.up * topPadding);
					bottomCard.transform.position = medium.position + (-topCard.transform.up * topPadding);
					medium.position = topCard.transform.position;
				

				} else if (isHard) {

					easy.gameObject.SetActive (false);
					hard.gameObject.SetActive (false);
					//Destroy (hard.gameObject);
					topCard.transform.position = hard.position + (-topCard.transform.up * topPadding);
					bottomCard.transform.position = hard.position + (-topCard.transform.up * topPadding);
					hard.position = topCard.transform.position;
				
				}

				cardNumber = 1;
			}

		} 

		easy.position = easyStartPos;
		medium.position = mediumStartPos;
		hard.position = hardStartPos;
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
