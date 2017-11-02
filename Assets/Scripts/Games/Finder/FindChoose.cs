using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FindChoose : MonoBehaviour {

	private Camera cam;

	private int amountOfFinders;
	private int objectsFound;
	private FinderSpawner spawner;
	private PlayerManager playerManager;

	public Image timer;
	public float timeSpeedMultiplier;
	public float SpeedUpTimePerWinning;

	public int levelToHitForGameOver;
	public int currentLevel;

	[SerializeField]private ParticleSystem rockFallingPS;

	private Transform player;
	private Animator playerAnimator;

	private string curScene;

	private float time;


	private GameObject newWave;
	private float timeUntilNewWave;

	void Start () {
		cam = Camera.main;

		if(SceneController.Instance !=null)
		curScene = SceneController.Instance.GetCurrentSceneName ();

		player = GameObject.FindWithTag ("Player").transform;

		playerAnimator = player.GetComponent<Animator> ();
		playerManager = player.GetComponent <PlayerManager> () as PlayerManager;

		spawner = GetComponent<FinderSpawner>();
		amountOfFinders = spawner.GetAmountOfFindTargets();

		StartCoroutine (OnUpdate ());

	}
	Vector3 pastMovePos;
	//int waveMultiplier = 0;
	IEnumerator OnUpdate(){

		while (true) {

			yield return null;
		
			if (GameController.Instance.IsStartMenuActive)
				continue;

			time += timeSpeedMultiplier * Time.deltaTime;

			timer.fillAmount -= time;

			if (timer.fillAmount <= 0) {
				pastMovePos = player.position;
				objectsFound = 0;

				spawner.TriggerObjects (500, player.parent.position.y -10 );
			
				timeSpeedMultiplier -= SpeedUpTimePerWinning;

				rockFallingPS.Play ();
				yield return StartCoroutine(spawner.TriggerFinderObjects (player.parent.position.y - 10 ));
				playerAnimator.Play ("Fall");

				playerAnimator.SetTrigger ("Reset");
				yield return new WaitUntil (() => playerAnimator.GetCurrentAnimatorStateInfo (0).IsName("Stop"));//playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !playerAnimator.IsInTransition(0));

			
				Vector3 differenceUp = Vector3.zero;
				differenceUp.y = 10;//player.parent.position.y - pastMovePos.y;//player.parent.position.y - originalPos.y ;


				player.parent.position -= differenceUp;
			//	playerAnimator.applyRootMotion = false;
				yield return new WaitForEndOfFrame ();

				StartCoroutine(spawner.ActivateFinder ());


				timer.fillAmount = 1;
				time = 0;
				currentLevel -= 1;

				if (currentLevel == levelToHitForGameOver) {

					DataManager.Instance.CheckHighScore (curScene, currentLevel);
					GameController.Instance.isGameOver = true;
					GameController.Instance.Paused = true;


				}
			
			}


			Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);

			RaycastHit hit;


			if (Physics.Raycast (ray, out hit, 500)) {


				if (hit.transform.gameObject.CompareTag ("FinderObject")) {

					AudioManager.Instance.PlayDirectSound ("SmallWin");
					hit.transform.GetComponent<Renderer> ().sharedMaterial.SetColor("_EmissionColor", Color.green);

					hit.transform.GetComponent<Collider> ().enabled = false;
					++objectsFound;


					if (objectsFound == amountOfFinders) {
						rockFallingPS.Stop ();
						rockFallingPS.Play ();

						pastMovePos = player.position;
						objectsFound = 0;

						spawner.TriggerObjects (500, player.parent.position.y + 10);


						yield return new WaitForSeconds (0.5f);
						yield return StartCoroutine(spawner.TriggerFinderObjects (player.parent.position.y + 10));


						timeSpeedMultiplier += SpeedUpTimePerWinning;
						/*
						if (timer.fillAmount > 0.70f)
							UpdateCoinText (3);
						else if (timer.fillAmount > 0.35f)
							UpdateCoinText (2);
						else if (timer.fillAmount > 0.00f)
							UpdateCoinText (1);
						*/

						playerAnimator.Play ("ClimbUp");

						yield return new WaitUntil (() => playerAnimator.GetCurrentAnimatorStateInfo (0).IsName("Stop"));//playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !playerAnimator.IsInTransition(0));
					
						StartCoroutine(spawner.ActivateFinder ());

						playerAnimator.SetTrigger ("Reset");
						playerAnimator.applyRootMotion = true;


						yield return new WaitForEndOfFrame ();

						Vector3 differenceUp = Vector3.zero;
						differenceUp.y = player.position.y - pastMovePos.y;//player.parent.position.y - originalPos.y ;

						player.parent.position += differenceUp;

						yield return null;
						playerAnimator.applyRootMotion = false;
					
						timer.fillAmount = 1;
						time = 0;
						currentLevel += 1;


					}
				}	
				//Debug.Log ("amountoffinders:" + amountOfFinders);
				//Debug.Log ("objectsfound:" + objectsFound);

			

			}

		}
	
	}
	void UpdateCoinText(int coins){


		playerManager.points += coins;



	}

}






