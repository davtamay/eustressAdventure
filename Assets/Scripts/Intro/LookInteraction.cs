using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class LookInteraction : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler {

	[SerializeField] GameObject imageGO;
	[SerializeField] float lookTime;

	[SerializeField] float timeUntilImageDeactivate = 5f;
	private float originalIndicatedDeactivationTime;
	[SerializeField] bool isUnscaledTime;
	[SerializeField] float lookDistance;
	[SerializeField] bool isLookAtPlayer = true;


	[SerializeField]UnityEvent onLookClick;

	//Check for bugs since I invalidated this in script
	[SerializeField] bool isItemForSlot;
	//[SerializeField] ItemInventoryRunTimeSet itemInventory;
	//[SerializeField] Item item;


	[SerializeField]bool isSpriteChangeOnClick = false;
	[SerializeField]private Sprite spriteToChange;

	[SerializeField]bool isColorChangeOnClick = false;
	[SerializeField]private Color colorToChange;
	[SerializeField]private Image alternativeImageToChangeColor;
	private Color originalColor;


	[SerializeField]UnityEvent onSecondaryLookClick;
	[SerializeField] bool isSecondLookEventCalled;

	public Sprite originalSprite;
	public Image image;


	private Collider thisCollider;

	[SerializeField]private bool isOnFromStart;
	[SerializeField] bool isStayOn;
	public Collider lookTriggerCollider;

	private Animator thisAnimator;
	private Camera cam;
	float timer;

	[Header("References")]
	[SerializeField]private BoolVariable isSceneLoading;

	void Awake () {

		cam = Camera.main;
		timer = lookTime;
		thisCollider = GetComponent<Collider> ();
		thisAnimator = GetComponent<Animator> ();
			
		originalIndicatedDeactivationTime = timeUntilImageDeactivate;
		//if(!isItemForSlot)

		if(!isOnFromStart && lookTriggerCollider == null)
			lookTriggerCollider = transform.parent.GetComponent<Collider> ();

	
		if(image == null)
		image = GetComponentInChildren<Image> ();


		
		originalSprite = image.sprite;

		if (isColorChangeOnClick)
			originalColor = image.color;


		imageGO = image.transform.parent.gameObject;

		//thisCollider.enabled = false;

		if (isOnFromStart) {
			lookTriggerCollider = null;
			imageGO.SetActive (true);
		}else
			if(!isStayOn)
			imageGO.SetActive (false);

	}

//	void OnEnable(){
//
//		if (isItemForSlot){
//
//		}
//			//itemInventory.Add (item, transform.parent.gameObject);
//	}
//
//	void OnDisable(){
//
//		if (isItemForSlot)
//			itemInventory.Remove (item);
//
//	}
//	void Start(){

//		if(lookTriggerCollider){
//			EventTrigger eT = lookTriggerCollider.gameObject.AddComponent <EventTrigger>();
//			EventTrigger.Entry entry = new EventTrigger.Entry();
//			entry.eventID = EventTriggerType.PointerEnter;
//			eT.triggers.Add(entry);
//			entry.eventID = EventTriggerType.PointerExit;
//			eT.triggers.Add(entry);
//			entry.eventID = EventTriggerType.PointerClick;
//			eT.triggers.Add(entry);
//
//
//
//		}
//			

//	}
//
//	void OnEnable(){
//
//		StopAllCoroutines ();
//		//new
//		StartCoroutine (EnableAndDisable());
//	}



	private float timeActive;

	IEnumerator EnableAndDisable(){
		
		timeActive = timeUntilImageDeactivate;

		isActive = true;
		imageGO.SetActive (true);
		//thisCollider.enabled = true;

		thisAnimator.SetBool ("IsActive", true);

		while (0 < timeActive) {

			yield return new WaitForEndOfFrame();
			//yield return new WaitForSecondsRealtime(1
			if(isLookAtPlayer)
			imageGO.transform.LookAt (cam.transform);

			if (isUnscaledTime)
				timeActive -= Time.unscaledDeltaTime;
			else
				timeActive -= Time.deltaTime;

		}
		thisAnimator.SetBool ("IsActive", false);

		while (!thisAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle"))
			yield return null;
			
			
		if(!isStayOn)
			imageGO.SetActive (false);

		timeUntilImageDeactivate = originalIndicatedDeactivationTime;

		isActive = false;


	//	thisCollider.enabled = false;
	}

	public void DisableImage(){
		if(!isStayOn)
		imageGO.SetActive (false);
	}

	public void TriggerSelections(){

		//timeUntilImageDeactivate;
//
//		if (!isActive) {
//			timeUntilImageDeactivate = Mathf.Infinity;
//			StopAllCoroutines ();
//			StartCoroutine (EnableAndDisable ());
//		} else {
//			timeActive = Mathf.Infinity;
//		}
		StopAllCoroutines ();
		timeUntilImageDeactivate = Mathf.Infinity;
		StartCoroutine (EnableAndDisable ());

//
	}
	public void RemoveSelections(){

		StopAllCoroutines ();
		timeUntilImageDeactivate = 0;
		StartCoroutine (EnableAndDisable ());

//		if (isActive){
//			
//			//timeUntilImageDeactivate = 0;
//			timeActive = 0;
//		thisAnimator.SetBool ("IsActive", false);
//
//		}else{
//
//			if(!isStayOn)
//				imageGO.SetActive (false);
//
//			timeUntilImageDeactivate = originalIndicatedDeactivationTime;
//
//			StopAllCoroutines ();
//
//		}
			


	}


	private bool isActive;
	private bool isOriginalImage = true;

	[SerializeField]private bool isCustomTime;
	[SerializeField]private float customTime;
	private float originalTime;

	public void OnPointerEnter(PointerEventData eventData){
		if (!EventSystem.current.IsPointerOverGameObject ()) {
			//)
			//if (eventData.currentInputModule.IsPointerOverGameObject (eventData.pointerId))
			//	return;

		//	Debug.Log ("I AM ONNNNNBUTTON!!!!!!" + EventSystem.current.currentSelectedGameObject.name);
			//if(eventData.pointerCurrentRaycast)
			//if(eventData.selectedObject)
			//	Debug.Log("I AM ONNNNNBUTTON!!!!!!" +  eventData.selectedObject.name);

			//	buttonFill.fillAmount = 1;
//			if (isSceneLoading) {
//				StopAllCoroutines ();
//				return;
//			}

//			if (lookTriggerCollider != null && EventSystem.current.gameObject != null)
//			if (lookTriggerCollider.gameObject.GetInstanceID () == EventSystem.current.gameObject.GetInstanceID ()) {
//
//				timeActive = timeUntilImageDeactivate;
//
//				if (!isActive) {
//					StartCoroutine (EnableAndDisable ());
//				}
//
//				return;
//			}

			if (isCustomTime) {
				originalTime = GazeInputModule.GazeTimeInSeconds;
				GazeInputModule.GazeTimeInSeconds = customTime;
			}
			
			StartCoroutine (OnHover ());

		}
	}
	IEnumerator OnHover(){
		float secondsUntilClick = GazeInputModule.GazeTimeInSeconds;
		float totalTimeToWait = secondsUntilClick;

		if (isItemForSlot) {

			if (!isActive) {
				StartCoroutine (EnableAndDisable ());
			}else
				totalTimeToWait = secondsUntilClick;

		}
			
		while(true){

			if (isUnscaledTime)
				secondsUntilClick -= Time.unscaledDeltaTime;
			else
				secondsUntilClick -= Time.deltaTime;
			
			image.fillAmount = secondsUntilClick/totalTimeToWait;
				
			yield return null;
		}



	}
	public void OnPointerExit(PointerEventData eventData){
		StopAllCoroutines ();
		image.fillAmount = 1;

		if (isCustomTime) 
			GazeInputModule.GazeTimeInSeconds = originalTime;
		

		Debug.Log("I AM Out!!!!!!");
		timeActive = 0;


	}
	public void OnPointerClick(PointerEventData eventData){
		
		Debug.Log("I AM clickeed!!!!!!");

		if (isCustomTime) 
			GazeInputModule.GazeTimeInSeconds = originalTime;

		if(isOriginalImage)
			onLookClick.Invoke ();
		else{

			if (isSecondLookEventCalled)
				onSecondaryLookClick.Invoke ();

			else {
				onLookClick.Invoke ();
			}

		}	


		isOriginalImage = !isOriginalImage;

		if (isSpriteChangeOnClick) {
			ChangeSprite ();

		}

		if (isColorChangeOnClick) {

			if (isOriginalImage) {
				if (alternativeImageToChangeColor)
					alternativeImageToChangeColor.color = originalColor;
				else
					image.color = originalColor;
			} else {
				if (alternativeImageToChangeColor)
					alternativeImageToChangeColor.color = colorToChange;
				else
					image.color = colorToChange;
			}

		}


		//isActive = false;
	
		if (!isStayOn) 
			imageGO.SetActive (false);
			

		timeActive = 0;

		if (isItemForSlot) {

			PlayerInventory.Instance.AddItemToSlot (transform.parent.gameObject);
		//	Debug.Log (transform.parent.gameObject.name);
			var tempAS = AudioManager.Instance.GetAudioSourceReferance (AudioManager.AudioReferanceType._DIRECT,"PickUp");
			tempAS.transform.position = transform.position;
			AudioManager.Instance.PlayDirectSound ("PickUp");


		}
		StopAllCoroutines ();
		image.fillAmount = 1;
	}
//	IEnumerator OnUpdate () {
//		
//		while(true){
//			yield return null;
//
//				if(GameController.Instance.Paused && !isUnscaledTime)
//					continue;
//			
//		RaycastHit hit;
//		Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);
//
//
//		if (thisCollider.Raycast (ray, out hit, lookDistance)) {
//
//			timeActive = timeUntilImageDeactivate;
//
//			if (isItemForSlot) {
//
//				if (!isActive) {
//					StartCoroutine (EnableAndDisable ());
//				}else
//					timeActive = timeUntilImageDeactivate;
//			
//			}
//
//			if (isUnscaledTime)
//				timer -= Time.unscaledDeltaTime;
//			else
//				timer -= Time.deltaTime;
//				
//			image.fillAmount = timer / lookTime;
//
//			if (0f > timer) {
//				timer = lookTime;
//
//				if(isOriginalImage)
//					onLookClick.Invoke ();
//				else{
//
//					if (isSecondLookEventCalled)
//						onSecondaryLookClick.Invoke ();
//					
//					else {
//						onLookClick.Invoke ();
//					}
//						
//				}	
//
//
//				isOriginalImage = !isOriginalImage;
//
//				if (isSpriteChangeOnClick) {
//					ChangeSprite ();
//
//				}
//
//				if (isColorChangeOnClick) {
//
//					if (isOriginalImage)
//						image.color = originalColor;
//					else
//						image.color = colorToChange;
//				
//				
//				}
//			
//
//				isActive = false;
//					if(!isStayOn)
//				imageGO.SetActive (false);
//
//
//				
//				timeActive = 0;
//
//				if (isItemForSlot) {
//					
//					PlayerInventory.Instance.AddItemToSlot (transform.parent.gameObject);
//					var tempAS = AudioManager.Instance.GetAudioSourceReferance (AudioManager.AudioReferanceType._DIRECT,"PickUp");
//					tempAS.transform.position = transform.position;
//					AudioManager.Instance.PlayDirectSound ("PickUp");
//					
//
//				}
//					
//			}
//
//
//
//		
//		} else {
//
//			timer = lookTime;
//			image.fillAmount = timer / lookTime;
//		//	imageGO.SetActive (false);
//		
//		}
//
//		//if(!isItemForSlot)
//		if(lookTriggerCollider != null)
//		if (lookTriggerCollider.Raycast (ray, out hit, lookDistance)) {
//
//			timeActive = timeUntilImageDeactivate;
//			//timer = lookTime;
//
//			if (!isActive) {
//				StartCoroutine (EnableAndDisable ());
//			}
//
//
//		}
//
//
//	}
//
//		}

	public void ChangeSprite(){

		if (isOriginalImage)
			image.sprite = spriteToChange;
		else
			image.sprite = originalSprite;


	
	
	}
	public void SetToOriginalSprite(){
	
		image.sprite = originalSprite;
		isOriginalImage = true;
	
	}

}
