using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LookSelect : MonoBehaviour {

	//need to be provided a GO for it to work in VR
	public GameObject selectedCard;
	public GameObject notNullOnSelected;
	public Camera cam;
	public GameObject firstCard;
	public GameObject secondCard;
	public bool isFirstCard;
	public bool isSecondCard;

	public int setCompleted;
	public ParticleSystem particles;
	public int spawnedCards;
	private CardSpawner cardSpawner;
	public Text coinText;


	private int _points;
	public int points{
		get{return _points;}

		set{coinText.text = ":";
			_points += value;
			coinText.text += _points.ToString();  }

	}



	void Start (){

		selectedCard = notNullOnSelected;

		cardSpawner = GetComponent<CardSpawner> ();
		cam = Camera.main;

		spawnedCards = cardSpawner.GetSpawned;
		//spawnedCards = CardSpawner.GetSpawned;
	}
	void Update(){

	Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);


	RaycastHit hit;

	if (Physics.Raycast (ray, out hit, 500)) {
		
			if (hit.transform.gameObject.GetInstanceID () == selectedCard.GetInstanceID () || isSecondCard)
				return;
			
		
			if (hit.transform.gameObject.CompareTag("Card")) {
				
			
			selectedCard = hit.transform.gameObject;
			
			
			
			StartCoroutine ("CardSelect");


		}
	}else{

			if (!isFirstCard || !isSecondCard) {

				StopCoroutine ("CardSelect");

			} 
	}
   }
	IEnumerator CardSelect(){

		if (!isFirstCard && !isSecondCard) {
			
			isFirstCard = true;
			firstCard = selectedCard;

			//1st test *no error* yield return 
			yield return StartCoroutine (RotateCard (firstCard));
		

	

			yield break;
		
		}else if (isFirstCard && !isSecondCard){
			
			isFirstCard = false;
			isSecondCard = true;

			secondCard = selectedCard;

			StartCoroutine (RotateCard (secondCard));

			StartCoroutine (TestCards (firstCard, secondCard));

		

		}

	
	}

	IEnumerator RotateCard (GameObject selected){

		Vector3 rot = new Vector3 (0,90,0);
		bool isRotating = true;
		float timeToRotate = 0.0f;

		Vector3 selectedCurRot = selected.transform.localEulerAngles;

		while (isRotating) {

			yield return null;

			timeToRotate += Time.deltaTime;

			selected.transform.Rotate (rot * Time.deltaTime);


			//error cards are not turning all the way
			if (timeToRotate > 2f){
			//	if (selectedCurRot.y < 5 && selectedCurRot.y > 0) {
			//		selectedCurRot.y = 0;
			//		selected.transform.localEulerAngles = Vector3.zero;
			//	}
					isRotating = false;
					yield break;

			}
		}
	
	}
	IEnumerator TestCards(GameObject a, GameObject b){
	
		yield return new WaitForSeconds (1.4f);
		if (string.Equals(a.name,b.name, System.StringComparison.CurrentCultureIgnoreCase))
		{
			

			Debug.Log ("we have a match!");

			spawnedCards -= 2;

			points = 1;

			particles.Play();
			yield return new WaitForSeconds (3);
			particles.Stop();


			if (spawnedCards == 0) {
				particles.Play ();
				yield return new WaitForSeconds (5);
				particles.Stop();

				if (cardSpawner.GetWave == 0) {
					cardSpawner.ChangeWave (Difficulty.medium);
					spawnedCards = cardSpawner.GetSpawned;
				} else if (cardSpawner.GetWave == 1) {
					cardSpawner.ChangeWave (Difficulty.hard);
					spawnedCards = cardSpawner.GetSpawned;
				}else if (cardSpawner.GetWave == 2) {
					GameController.Instance.isGameOver = true;
				}


			}

		}else {
			

		//	yield return 
			StartCoroutine (RotateCard (firstCard));
			firstCard = null;

		//	yield return *error: turn back faster
			yield return StartCoroutine (RotateCard (secondCard));
			secondCard = null;
			selectedCard = notNullOnSelected;



		}
		isSecondCard = false;
	}


}
