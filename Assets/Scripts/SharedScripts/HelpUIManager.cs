using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public enum Goal{OPENMENU, CLICK_COPING, WALK, JUMP, CLICK_ACTION,CLICK_INFORMATION, WAITEXPLANATION,WAITFORTIMELINE, NONE};
public class HelpUIManager : MonoBehaviour {


	public GameObject uIHelpGO;

	private bool isUsingSprite;

	//public Text textSpeech;
	public GameObject interactionTextGO;
	private GeneralInteraction gInteraction;


	private SpriteRenderer spriteHelp;
	private bool isUsingTextMesh;
	private TextMesh textMeshHelp;



	[SerializeField] private float fadingSpeed;

	public Goal curGoal;

	[Header("GoalBindings")]
	[SerializeField]private GameObject armStressMenuForEnabled;
	[SerializeField]private GameObject copingGOCheckForEnabled;
	[SerializeField]private GameObject feetForEnabled;
	[SerializeField]private PlayableDirector explanationPlayable;

	//[SerializeField]private Button backToGameButton;

	private PlayerLookMove playerLookMove;
	private float originalRechargeTime;


	public static HelpUIManager Instance
	{ get { return instance; } }

	private static HelpUIManager instance = null;


	void Awake(){
		
		if (instance) {
			DestroyImmediate (gameObject);
			return;
		}
		instance = this; 


		//textOnSprite.transform.GetChild(0).gameObject.SetActive (false);
			


	}

//	private bool canWalk;
	private bool isFirstTime = true;
	private float counter = 0;
	IEnumerator Start () {

		gInteraction = interactionTextGO.GetComponent<GeneralInteraction> ();
		playerLookMove = GameObject.FindWithTag ("Player").GetComponent<PlayerLookMove> ();
		originalRechargeTime = playerLookMove.jumpRechargeTime;
		playerLookMove.jumpRechargeTime = Mathf.Infinity;

		armStressMenuForEnabled.SetActive (false);
		feetForEnabled.SetActive (false);
		//doesnt store dynamic value
	//	canWalk = GameObject.FindWithTag ("Player").GetComponent<PlayerLookMove> ().enabled;

		while (true) {

		//	Debug.Log ("THIS IS TEXT SPEECH" + textSpeech);
			yield return null;

			if (uIHelpGO != null) {

				if (isFirstTime) {

				
					counter = 0;
					isFirstTime = false;


				}
				counter += Time.unscaledDeltaTime;
				if (isUsingSprite) {
					Color tempColor = spriteHelp.color;
					tempColor.a = Mathf.PingPong ((counter * fadingSpeed) * fadingSpeed, 1);
				
					spriteHelp.color = tempColor;

				} else if (isUsingTextMesh) {
					Color tempColor = textMeshHelp.color;
					tempColor.a = Mathf.PingPong ((counter * fadingSpeed) * fadingSpeed, 1);

					textMeshHelp.color = tempColor;
				}

				switch(curGoal){
				case Goal.OPENMENU:

					if (!armStressMenuForEnabled.activeInHierarchy)
						armStressMenuForEnabled.SetActive (true);
					else {
						//USE SCRIPTABLE CHECKER
						yield return null;
//						if (GameController.Instance.IsMenuActive) {
//					
//							TurnOffHelpInfo ();
//							curGoal = Goal.NONE;
//							
//						}
					}
					break;

				case Goal.CLICK_COPING:

					if (copingGOCheckForEnabled.activeInHierarchy) {

						TurnOffHelpInfo ();
						curGoal = Goal.NONE;
					
					}
						
					break;

				case Goal.WALK:
					if (!feetForEnabled.activeInHierarchy) {
						
						feetForEnabled.SetActive (true);

					}else {
						//USE SCRIPTABLE CHECKER
						//if(!GameController.Instance.IsMenuActive)
						if (GameObject.FindWithTag ("Player").GetComponent<PlayerLookMove> ().enabled) {
				
							TurnOffHelpInfo ();

							//curGoal = Goal.NONE;
				
						}
					}

					break;
				
				case Goal.JUMP:

					if(playerLookMove.jumpRechargeTime == Mathf.Infinity)
					playerLookMove.jumpRechargeTime = originalRechargeTime;
					else{
						if(playerLookMove.isGoingUp)
							TurnOffHelpInfo ();
						}



					break;

				case Goal.CLICK_ACTION:
					break;

				case Goal.CLICK_INFORMATION:
					break;

				case Goal.WAITEXPLANATION:

					TurnOffHelpInfo ();
					curGoal = Goal.NONE;

					break;

				case Goal.WAITFORTIMELINE:

					//if (isFirstTime)
					//	explanationPlayable.Play ();
					
					if (explanationPlayable.duration <= explanationPlayable.time) {
						
						TurnOffHelpInfo ();
						TimeLineController.Instance.ResumeTimeLine ();

						curGoal = Goal.NONE;
					}
					break;

				case Goal.NONE:
					break;

				


				}
					

			}
		
		
		}
		
	}


	private float waitTime = 0;
	public void TurnOffHelpInfo(){

		if (waitTime <= 0.0f) {

			isUsingSprite = false;
			isUsingTextMesh = false;

			if (uIHelpGO != null) {
				uIHelpGO.SetActive (false);
				uIHelpGO = null;
			
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

			if (isUsingSprite) {

				while (spriteHelp.color.a > 0.03f)
					yield return null;
				
			} else if (isUsingTextMesh) {
			
				while (textMeshHelp.color.a > 0.03f)
					yield return null;
			
			}

			curGoal = Goal.NONE;
			uIHelpGO.SetActive (false);
			uIHelpGO = null;

		
		}
		isUsingSprite = false;
		isUsingTextMesh = false;
	
	}


	public void TurnOnHelpInfo(GameObject helpGO, bool isSprite = false, bool isTextMesh = false, float waitT = 0){




		waitTime = waitT;
		uIHelpGO = helpGO;

		if (isSprite) {
			spriteHelp = uIHelpGO.GetComponent<SpriteRenderer> ();
			isUsingSprite = true;
		}else if(isTextMesh){
			textMeshHelp = uIHelpGO.GetComponent<TextMesh> ();
			isUsingTextMesh = true;
		}

	}

	public void AddText(string key){

		gInteraction.SetTextLocalizedKey (key);
		/*Debug.Log ("THIS IS TEXTSPEECH: " + textSpeech);
		if(!textSpeech.isActiveAndEnabled)
		textSpeech.transform.GetChild(0).gameObject.SetActive (true);
		var tempLocalizedText = textSpeech.gameObject.GetComponent<LocalizedText> ();
		tempLocalizedText.key = key;
		tempLocalizedText.OnUpdate ();
		//textOnSprite.text = LocalizationManager.Instance.GetLocalizedValue (key);
		//textOnSprite.fontSize = size;
		*/

	
	}
	public void RemoveText(){

		if (gInteraction != null)
		gInteraction.RemoveLocalizedKey ();
	/*
		if (textSpeech != null && textSpeech.isActiveAndEnabled) {
			textSpeech.transform.GetChild (0).gameObject.SetActive (false);
			textSpeech.text = string.Empty;
		}
		//textOnSprite.transform.GetChild(0).gameObject.SetActive (false);*/
	}

}
