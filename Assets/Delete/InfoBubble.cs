using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoBubble : MonoBehaviour {
	/*
	private Button infoButton;
	private Image infoButtonImage;
	private Camera cam;
	private Collider myCollider;
	private float timer;
	[SerializeField]private float timeToSelect;
	
	private static bool firstRunIntro ;
	private static bool firstRunStressMenu;
	private static bool firstRunGameMenu;

	private enum InfoType{INTRO, STRESS_MENU, GAME_MENU}
	[SerializeField] private InfoType infoType;


	void Start(){

		
		//GameController.Instance.IsInfoBubbleActive = true;


		switch (infoType) {

		case InfoType.INTRO:

			if (firstRunIntro == false) {
				firstRunIntro = true;
				StartCoroutine (OnUpdate ());

			} else {
				transform.parent.gameObject.SetActive (false);
				GameController.Instance.IsInfoBubbleActive = false;
			}
			break;

			case InfoType.STRESS_MENU:

			if (firstRunStressMenu == false) {
				firstRunStressMenu = true;
				StartCoroutine (OnUpdate());
			} else{
				transform.parent.gameObject.SetActive (false);
				GameController.Instance.IsInfoBubbleActive = false;
			}
			break;

			case InfoType.GAME_MENU:

			if (firstRunGameMenu == false) {
				firstRunGameMenu = true;
				StartCoroutine (OnUpdate());
			} else{
				transform.parent.gameObject.SetActive (false);
				GameController.Instance.IsInfoBubbleActive = false;
			}
			break;
		}


		myCollider = GetComponentInChildren <Collider>();
		infoButton = GetComponentInChildren<Button> ();
		infoButtonImage = infoButton.GetComponent<Image> ();

		cam = Camera.main;





	}

	IEnumerator OnUpdate(){
	
		timer = timeToSelect;

		while (true) {
			yield return null;

			Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);

			RaycastHit hit;

			if (myCollider.Raycast (ray, out hit, 400)) {

				timer -= Time.deltaTime;

				infoButtonImage.fillAmount = timer / timeToSelect;

				if (infoButtonImage.fillAmount <= 0) {
					//infoButton.onClick.Invoke ();
					timer = timeToSelect;
					transform.parent.gameObject.SetActive (false);
					yield break;
				}



			} else 
				infoButtonImage.fillAmount = 1;


		}
	
	
	}

	void OnDisable(){

		GameController.Instance.IsInfoBubbleActive = false;
	}
*/
}
