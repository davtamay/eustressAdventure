using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;


public class InteractionBehaviour : MonoBehaviour {

	//[SerializeField]
	public UnityEvent onInitialInteraction;

	[Header("UI Text Settings")]
	[SerializeField]protected GameObject infoCanvasPrefab;
	[SerializeField]protected Vector3 infoOffset;
	[SerializeField]protected bool isAutomaticRotation = false;
	[SerializeField]protected bool isAutomaticStayUpRight = true;

	[SerializeField]protected float yInfoRotationOffset;
	[SerializeField]protected float xInfoRotationOffset;
	[SerializeField]protected float zInfoRotationOffset;
	[SerializeField]protected Vector2 InfoSize;

	private LineRendererGuide lRenderGuide;
	[SerializeField]protected Transform lineRendererStart;
	//[SerializeField]protected TextAlignment textAllignment = TextAlignment.Left;
	[SerializeField]protected TextAnchor textAnchor = TextAnchor.UpperLeft;
	[TextArea(0,15)][SerializeField]protected string infoText;
	[SerializeField]protected float timeActive;
	[SerializeField]protected Color infoBackGround = Color.cyan;

	[Header("Action")] [SerializeField]protected UnityEvent onActionSelect;
	[TextArea(0,15)][SerializeField]protected string ActionText;


	[Header("OnInfoPrefabDisable")] [SerializeField]protected UnityEvent onInfoDisable;
	[SerializeField]protected bool isUseQuickDisable = false;
	//[TextArea(0,15)][SerializeField]protected string informationText;

	[Header("OnCompletion")] public UnityEvent onCompletion;
	[TextArea(0,15)][SerializeField]protected string completionText;


	protected Text infoTextComponent;
	protected Transform thisTransform;
	protected float distanceToInteraction;

	protected Transform player;

	protected Animator infoCanvasAnimator;


	//protected ParticleSystem.MainModule thisParticleSystem;

	protected LocalizedText localizedText;
	public virtual void Awake(){

		thisTransform = transform;
		player = GameObject.FindWithTag ("Player").transform;


		if (infoCanvasPrefab != null) {

			infoCanvasPrefab = Instantiate (infoCanvasPrefab, new Vector3(thisTransform.position.x + infoOffset.x, thisTransform.position.y + infoOffset.y, thisTransform.position.z + infoOffset.z), Quaternion.identity) ;

			infoCanvasPrefab.transform.SetParent(thisTransform);


			lRenderGuide = infoCanvasPrefab.GetComponentInChildren<LineRendererGuide> (true);
			lRenderGuide.startPos = lineRendererStart;


			infoCanvasAnimator = infoCanvasPrefab.GetComponent<Animator> ();

			localizedText = GetComponentInChildren<LocalizedText> (true);

			//thisParticleSystem = infoCanvasPrefab.GetComponentInChildren<ParticleSystem> ().main;
			//thisParticleSystem.startColor = new ParticleSystem.MinMaxGradient (infoBackGround);

			infoTextComponent = infoCanvasPrefab.GetComponentInChildren<Text> ();
			infoTextComponent.alignment = textAnchor;//(TextAnchor)textAllignment;
			//infoTextComponent.text = infoText;

			Transform infoRect = infoCanvasPrefab.transform;
			infoRect.localScale = new Vector3 (InfoSize.x,InfoSize.y, 1);
			//infoRect.sizeDelta = new Vector2 (InfoSize.x,InfoSize.y);
			//	infoTextRect.sizeDelta = new Vector2 (InfoSize.x,InfoSize.y);


		//	RectTransform infoTextRect = infoTextComponent.transform.GetComponent<RectTransform> ();
		//	infoTextRect.sizeDelta = new Vector2 (InfoSize.x,InfoSize.y);

			//RectTransform infoImageRect = infoTextComponent.transform.GetComponent<RectTransform> ();
			//infoImageRect.sizeDelta = new Vector2 (InfoSize.x,InfoSize.y);

			//thisParticleSystem.startSizeX = new ParticleSystem.MinMaxCurve(infoTextRect.sizeDelta.x/50 );
			//thisParticleSystem.startSizeY = new ParticleSystem.MinMaxCurve(infoTextRect.sizeDelta.y/50 );

		//	thisParticleSystem.startSizeX = new ParticleSystem.MinMaxCurve(InfoSize.x/50 );
		//	thisParticleSystem.startSizeY = new ParticleSystem.MinMaxCurve(InfoSize.y/50 );

			infoCanvasPrefab.SetActive (false);
		}



	
	}


	//Coroutine infoActive; 
	//private bool isResetTime;
	public void TriggerInfo(){
		
		Debug.Log ("TRIGGERINGINFO");
			
	
		infoCanvasPrefab.SetActive (true);
		infoCanvasAnimator.SetBool ("IsActive", true);
		//infoActive = 
			StartCoroutine (InfoActive ());

	
	}

	public float timerForActivation;
	public IEnumerator InfoActive(){
		
		timerForActivation = timeActive;

		float time = 0;


		while (true) {

			time += Time.deltaTime;



			if (!isAutomaticRotation) {
				infoCanvasPrefab.transform.localRotation = Quaternion.AngleAxis (yInfoRotationOffset, Vector3.up);
				infoCanvasPrefab.transform.localRotation *= Quaternion.AngleAxis (xInfoRotationOffset, Vector3.right);
				infoCanvasPrefab.transform.localRotation *= Quaternion.AngleAxis (zInfoRotationOffset, Vector3.forward);

			} else {
				yield return new WaitForEndOfFrame ();
				infoCanvasPrefab.transform.LookAt (player.position, Vector3.up);
				infoCanvasPrefab.transform.localRotation *= Quaternion.AngleAxis (180, Vector3.up);
				if (isAutomaticStayUpRight) {
					infoCanvasPrefab.transform.eulerAngles = new Vector3 (0, infoCanvasPrefab.transform.eulerAngles.y, infoCanvasPrefab.transform.eulerAngles.z);

				}
			}
				

			if (time > timerForActivation) {
				infoCanvasAnimator.SetBool ("IsActive", false);

				if (infoCanvasAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || isUseQuickDisable)
				 {
					onInfoDisable.Invoke ();
					infoCanvasPrefab.SetActive (false);
					timerForActivation = timeActive;
					yield break;
				}

			}

			yield return null;

		}
	}
	public void TakeOffInfo(){
	
		Debug.Log ("unTRIGGERINGINFO");
	//	if (infoCanvasPrefab.activeInHierarchy) {

			timerForActivation = 0f;

		//new10/11/2017
		if(isUseQuickDisable)
		infoCanvasAnimator.SetBool ("IsActive", false);
			/*infoCanvasAnimator.SetBool ("IsActive", false);
			if (infoCanvasAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			{
				StopAllCoroutines ();
				infoCanvasAnimator.SetBool ("IsActive", false);
				infoCanvasPrefab.SetActive (false);
				StopCoroutine (InfoActive ());
				StopCoroutine (infoActive);

				return;
			}*/
	//	}
	
	
	}
	public void SetTextLocalizedKey(string nKey){

		localizedText.key = nKey;
		localizedText.OnUpdate ();

	
	}
	public void RemoveLocalizedKey(){

		localizedText.key = string.Empty;
		localizedText.OnUpdate ();


	}
	void OnDrawGizmos(){

		thisTransform = transform;
		//GameObject Info = GameObject.FindWithTag ("EditorOnly");

		//Vector2 InfoScale = new Vector2 (InfoSize.x, InfoSize.y)//Info.GetComponent<RectTransform>().sizeDelta;
	
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube (new Vector3(thisTransform.position.x + infoOffset.x, thisTransform.position.y + infoOffset.y, thisTransform.position.z + infoOffset.z),new Vector3(InfoSize.x, InfoSize.y, 1));// new Vector3(InfoScale.x + InfoSizeOffset.x, InfoScale.y + InfoSizeOffset.y, 1));
	}





}
