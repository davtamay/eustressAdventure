using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Goal{OPENMENU, CLICK_COPING, WALK, JUMP, CLICK_ACTION,CLICK_INFORMATION, WAITEXPLANATION, NONE};
public class HelpUIManager : MonoBehaviour {


	public GameObject uIHelpGO;

	private bool isUsingSprite;
	private SpriteRenderer spriteHelp;
	private bool isUsingTextMesh;
	private TextMesh textMeshHelp;

	[SerializeField] private float fadingSpeed;

	public Goal curGoal;

	[SerializeField]private GameObject armStressMenuForEnabled;
	[SerializeField]private GameObject copingGOCheckForEnabled;
	[SerializeField]private GameObject feetForEnabled;


	public static HelpUIManager Instance
	{ get { return instance; } }

	private static HelpUIManager instance = null;


	void Awake(){
		
		if (instance) {
			DestroyImmediate (gameObject);
			return;
		}
		instance = this; 


	
	}

//	private bool canWalk;
	IEnumerator Start () {
		
		armStressMenuForEnabled.SetActive (false);
		feetForEnabled.SetActive (false);
		//doesnt store dynamic value
	//	canWalk = GameObject.FindWithTag ("Player").GetComponent<PlayerLookMove> ().enabled;

		while (true) {
			
			yield return null;

			if (uIHelpGO != null) {

				if (isUsingSprite) {
					Color tempColor = spriteHelp.color;
					tempColor.a = Mathf.PingPong (Time.realtimeSinceStartup * fadingSpeed, 1);
					spriteHelp.color = tempColor;
				} else if (isUsingTextMesh) {
					Color tempColor = textMeshHelp.color;
					tempColor.a = Mathf.PingPong (Time.realtimeSinceStartup * fadingSpeed, 1);
					textMeshHelp.color = tempColor;
				}

				switch(curGoal){
				case Goal.OPENMENU:

					if (!armStressMenuForEnabled.activeInHierarchy)
						armStressMenuForEnabled.SetActive (true);
					else {
						if (GameController.Instance.IsMenuActive) {
					
							TurnOffHelpInfo ();
							curGoal = Goal.NONE;
					
						}
					}
					break;

				case Goal.CLICK_COPING:

					if (copingGOCheckForEnabled.activeInHierarchy) {

						TurnOffHelpInfo ();
						curGoal = Goal.NONE;
					
					}
						
					break;

				case Goal.WALK:
					if (!feetForEnabled.activeInHierarchy)
						feetForEnabled.SetActive (true);
					else {

						if (GameObject.FindWithTag ("Player").GetComponent<PlayerLookMove> ().enabled) {
					
							TurnOffHelpInfo ();
							curGoal = Goal.NONE;
				
						}
					}

					break;
				
				case Goal.JUMP:
					break;

				case Goal.CLICK_ACTION:
					break;

				case Goal.CLICK_INFORMATION:
					break;

				case Goal.WAITEXPLANATION:

					TurnOffHelpInfo ();
					curGoal = Goal.NONE;

					break;
				case Goal.NONE:
					break;

				


				}

			}
		
		
		}
		
	}


	private float waitTime = 0;
	public void TurnOffHelpInfo(){
	
		if (waitTime <= 0) {
			isUsingSprite = false;
			isUsingTextMesh = false;

			if (uIHelpGO != null) {
				uIHelpGO.SetActive (false);
				uIHelpGO = null;
			//	curGoal = Goal.NONE;
			}
		}else
			StartCoroutine(TurnOffHelpInfoAfterSeconds(waitTime));
			
	}

	IEnumerator TurnOffHelpInfoAfterSeconds(float wait){
		waitTime = 0;
		yield return new WaitForSecondsRealtime (wait);

		//uIHelpGO.SetActive (false);
		//curGoal = Goal.NONE;



		if (uIHelpGO != null) {
			curGoal = Goal.NONE;
			uIHelpGO.SetActive (false);
			uIHelpGO = null;

		
		}
		isUsingSprite = false;
		isUsingTextMesh = false;

	}


	public void TurnOnHelpInfo(GameObject helpGO, bool isSprite, bool isTextMesh, float waitT = 0){

		waitTime = waitT;
		uIHelpGO = helpGO;

		if (isSprite) {
			spriteHelp = uIHelpGO.GetComponent<SpriteRenderer> ();
			isUsingSprite = true;
		}else if(isTextMesh){
			textMeshHelp = uIHelpGO.GetComponent<TextMesh> ();
			isUsingTextMesh = true;
		}
		//uIBox.gameObject.SetActive (true);
	}

}
