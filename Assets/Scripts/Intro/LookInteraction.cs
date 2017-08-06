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
	[SerializeField] float lookDistance;

	[SerializeField]UnityEvent onLookClick;

	//Check for bugs since I invalidated this in script
	[SerializeField] bool isItemForSlot;


	[SerializeField]bool isSpriteChangeOnClick = false;

	[SerializeField]private Sprite spriteToChange;

	[SerializeField]UnityEvent onSecondaryLookClick;
	[SerializeField] bool isFirstNotInvokedInsteadOffSecond;

	public Sprite originalSprite;
	public Image image;

	private Collider thisCollider;
	public Collider parentCollider;

	private Animator thisAnimator;
	private Camera cam;
	float timer;


	void Awake () {

		cam = Camera.main;
		timer = lookTime;
		thisCollider = GetComponent<Collider> ();
		thisAnimator = GetComponent<Animator> ();
			

		//if(!isItemForSlot)
			parentCollider = transform.parent.GetComponent<Collider> ();

		image = GetComponentInChildren<Image> ();
		originalSprite = image.sprite;

		imageGO = image.transform.parent.gameObject;
		imageGO.SetActive (false);
		thisCollider.enabled = false;
		
	}


	private float timeActive;

	IEnumerator EnableAndDisable(){

		timeActive = timeUntilImageDeactivate;
		isActive = true;
		imageGO.SetActive (true);
		thisCollider.enabled = true;

		thisAnimator.SetBool ("IsActive", true);

		while (0 < timeActive) {

			yield return new WaitForEndOfFrame();
			//yield return null

			imageGO.transform.LookAt (cam.transform);

			timeActive -= Time.deltaTime;

		}
		thisAnimator.SetBool ("IsActive", false);

		while (!thisAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle"))
			yield return null;
			
			
		isActive = false;
		imageGO.SetActive (false);
		thisCollider.enabled = false;
	}

	private bool isActive;
	private bool isOriginalImage = true;
	void Update () {

		RaycastHit hit;
		Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);
		
		if (thisCollider.Raycast (ray, out hit, lookDistance)) {

			timeActive = timeUntilImageDeactivate;

			if (isItemForSlot) {

			//	timer = lookTime;

				if (!isActive) {
					StartCoroutine (EnableAndDisable ());
				}else
					timeActive = timeUntilImageDeactivate;
			
			}


			timer -= Time.deltaTime;
			image.fillAmount = timer / lookTime;

			if (0f > timer) {
				timer = lookTime;



				if(isOriginalImage)
					onLookClick.Invoke ();
				else{

					if (isFirstNotInvokedInsteadOffSecond)
						onSecondaryLookClick.Invoke ();
					
					else {
						onLookClick.Invoke ();
						//onSecondaryLookClick.Invoke ();
					}
						
					}	


				if (isSpriteChangeOnClick) {
					ChangeSprite ();

				}
					
			//	onLookClick.Invoke ();

				isActive = false;
				imageGO.SetActive (false);
				thisCollider.enabled = false;


				
				timeActive = 0;

				if (isItemForSlot) 
					PlayerManager.Instance.AddItemToSlot (transform.parent.gameObject);{
					//DataManager.Instance.SaveItemList (PlayerManager.Instance.playerItemSlotGOList);
				}
					
			}



		
		} else {

			timer = lookTime;
			image.fillAmount = timer / lookTime;
		//	imageGO.SetActive (false);
		
		}

		//if(!isItemForSlot)
		if(parentCollider != null)
		if (parentCollider.Raycast (ray, out hit, lookDistance)) {

			timeActive = timeUntilImageDeactivate;
			//timer = lookTime;

			if (!isActive) {
				StartCoroutine (EnableAndDisable ());
			}


		}




		}

	public void ChangeSprite(){

		if (isOriginalImage)
			image.sprite = spriteToChange;
		else
			image.sprite = originalSprite;

		isOriginalImage = !isOriginalImage;
	
	
	}
	public void SetToOriginalSprite(){
	
		image.sprite = originalSprite;
		isOriginalImage = true;
	
	}

}
