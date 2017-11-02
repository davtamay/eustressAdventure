using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class LookInteraction : MonoBehaviour {

	[SerializeField] GameObject imageGO;
	[SerializeField] float lookTime;
	[SerializeField] float timeUntilImageDeactivate = 5f;
	[SerializeField] bool isUnscaledTime;
	[SerializeField] float lookDistance;
	[SerializeField] bool isLookAtPlayer = true;


	[SerializeField]UnityEvent onLookClick;

	//Check for bugs since I invalidated this in script
	[SerializeField] bool isItemForSlot;


	[SerializeField]bool isSpriteChangeOnClick = false;
	[SerializeField]private Sprite spriteToChange;

	[SerializeField]bool isColorChangeOnClick = false;
	[SerializeField]private Color colorToChange;
	private Color originalColor;


	[SerializeField]UnityEvent onSecondaryLookClick;
	[SerializeField] bool isSecondLookEventCalled;

	public Sprite originalSprite;
	public Image image;

	private Collider thisCollider;
	public Collider lookTriggerCollider;

	private Animator thisAnimator;
	private Camera cam;
	float timer;


	void Awake () {

		cam = Camera.main;
		timer = lookTime;
		thisCollider = GetComponent<Collider> ();
		thisAnimator = GetComponent<Animator> ();
			

		//if(!isItemForSlot)
		if(lookTriggerCollider == null)
			lookTriggerCollider = transform.parent.GetComponent<Collider> ();

		image = GetComponentInChildren<Image> ();
		originalSprite = image.sprite;

		if (isColorChangeOnClick)
			originalColor = image.color;

		imageGO = image.transform.parent.gameObject;
		imageGO.SetActive (false);
		//thisCollider.enabled = false;


	}
	void Start(){
		StartCoroutine (OnUpdate ());
	}

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
			
			
		isActive = false;
		imageGO.SetActive (false);
	//	thisCollider.enabled = false;
	}

	public void DisableImage(){
		imageGO.SetActive (false);
	}



	private bool isActive;
	private bool isOriginalImage = true;
	IEnumerator OnUpdate () {
		
		while(true){
			yield return null;

				if(GameController.Instance.Paused && !isUnscaledTime)
					continue;
			
		RaycastHit hit;
		Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);


		if (thisCollider.Raycast (ray, out hit, lookDistance)) {
	//	if (thisCollider.Raycast (ray, out hit, lookDistance)) {

			timeActive = timeUntilImageDeactivate;

			if (isItemForSlot) {

			//	timer = lookTime;

				if (!isActive) {
					StartCoroutine (EnableAndDisable ());
				}else
					timeActive = timeUntilImageDeactivate;
			
			}

			if (isUnscaledTime)
				timer -= Time.unscaledDeltaTime;
			else
				timer -= Time.deltaTime;
				
			image.fillAmount = timer / lookTime;

			if (0f > timer) {
				timer = lookTime;

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

					if (isOriginalImage)
						image.color = originalColor;
					else
						image.color = colorToChange;
				
				
				}
			//	onLookClick.Invoke ();

				isActive = false;
				imageGO.SetActive (false);
				//thisCollider.enabled = false;


				
				timeActive = 0;

				if (isItemForSlot) {
					
					PlayerInventory.Instance.AddItemToSlot (transform.parent.gameObject);
					var tempAS = AudioManager.Instance.GetAudioSourceReferance (AudioManager.AudioReferanceType._DIRECT,"PickUp");
					tempAS.transform.position = transform.position;
					AudioManager.Instance.PlayDirectSound ("PickUp");
					//DataManager.Instance.SaveItemList (PlayerManager.Instance.playerItemSlotGOList);

				}
					
			}



		
		} else {

			timer = lookTime;
			image.fillAmount = timer / lookTime;
		//	imageGO.SetActive (false);
		
		}

		//if(!isItemForSlot)
		if(lookTriggerCollider != null)
		if (lookTriggerCollider.Raycast (ray, out hit, lookDistance)) {

			timeActive = timeUntilImageDeactivate;
			//timer = lookTime;

			if (!isActive) {
				StartCoroutine (EnableAndDisable ());
			}


		}


	}

		}

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
