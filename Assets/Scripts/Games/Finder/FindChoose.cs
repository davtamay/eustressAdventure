using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FindChoose : MonoBehaviour {

	private Camera cam;
	//public Text coinText;
	//public int amountOfCoins;
	private int amountOfFinders;
	private int objectsFound;
	private FinderSpawner spawner;
	private PlayerManager playerManager;

	public Image timer;
	public float timeAmount;

	public int amountUntilGameOver;
	public int currentLevel;

	private string curScene;

	private float time;
	void Start () {

		curScene = SceneController.Instance.GetCurrentSceneName ();


		playerManager = GameObject.FindWithTag ("Player").GetComponent <PlayerManager> () as PlayerManager;
		cam = GameObject.FindWithTag ("MainCamera").GetComponent<Camera>();

		spawner = GetComponent<FinderSpawner>();
		amountOfFinders = spawner.GetAmountOfFindTargets();



	}
	void Update(){
		if (GameController.Instance.IsStartMenuActive)
			return;

		time += timeAmount * Time.deltaTime;

		timer.fillAmount -= time;


		Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);

		RaycastHit hit;


		if (Physics.Raycast (ray, out hit, 500)) {


			if (hit.transform.gameObject.CompareTag("FinderObject")) {

				hit.transform.GetComponent<Renderer> ().material.color = Color.green;
				hit.transform.GetComponent<Collider>().enabled = false;
				++objectsFound;


				if (objectsFound == amountOfFinders){
					if (timer.fillAmount > 0.70f)
						UpdateCoinText (3);
					else if (timer.fillAmount > 0.35f)
						UpdateCoinText (2);
					else if (timer.fillAmount > 0.00f)
						UpdateCoinText (1);
					
					objectsFound = 0;
					spawner.TriggerMoreObjects(100);
					spawner.TriggerFinderObjects ();
					timer.fillAmount = 1;
					time = 0;
					currentLevel += 1;
					if (currentLevel == amountUntilGameOver) {

						HighScoreManager.Instance.CheckHighScore (curScene, playerManager.points);
						GameController.Instance.isGameOver = true;
						GameController.Instance.Paused = true;

					
					}

					return;

			}
		}	
		Debug.Log ("amountoffinders:" + amountOfFinders);
		Debug.Log ("objectsfound:" + objectsFound);

			

			}


	
	}
	void UpdateCoinText(int coins){

		//amountOfCoins += coins;

		playerManager.points += coins;
		//coinText.text = ":" + amountOfCoins;


	}

}






