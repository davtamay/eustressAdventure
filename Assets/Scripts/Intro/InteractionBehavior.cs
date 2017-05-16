using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;


public class InteractionBehavior : MonoBehaviour {

	[SerializeField]protected UnityEvent onInteraction;
	[SerializeField]protected GameObject infoCanvasPrefab;
	[SerializeField]protected Vector3 infoOffset;
	[SerializeField]protected float yInfoRotationOffset;
	[SerializeField]protected float xInfoRotationOffset;
	[SerializeField]protected float zInfoRotationOffset;
	[SerializeField]protected Vector3 InfoSize;
	[TextArea][SerializeField]protected string infoText;
	[SerializeField]protected float timeActive;

	protected Text infoTextComponent;

	protected float distanceToInteraction;
	protected Transform thisTransform;
	protected Transform player;


	public virtual void Awake(){

		thisTransform = transform;
		player = GameObject.FindWithTag ("Player").transform;

		if (infoCanvasPrefab != null) {

			infoCanvasPrefab = Instantiate (infoCanvasPrefab, new Vector3(thisTransform.position.x + infoOffset.x, thisTransform.position.y + infoOffset.y, thisTransform.position.z + infoOffset.z), Quaternion.identity) ;
			infoCanvasPrefab.transform.SetParent(thisTransform);
			infoCanvasPrefab.transform.localScale += InfoSize;
			infoTextComponent = infoCanvasPrefab.GetComponentInChildren<Text> ();
			infoTextComponent.text = infoText;
			infoCanvasPrefab.SetActive (false);
		}


	
	}

	Coroutine infoActive; 

	public void ReachedProximity(){
	

		infoCanvasPrefab.SetActive (true);
		infoActive = StartCoroutine (InfoActive ());
	
	
	}
	
	public IEnumerator InfoActive(){
		infoTextComponent.text = infoText;
		float timer = timeActive;
		float time = 0;

		while (true) {

			time += Time.deltaTime;

			infoCanvasPrefab.transform.localPosition = infoOffset;

		//	infoCanvasPrefab.transform.LookAt (2* thisTransform.position - player.position, Vector3.up);
			infoCanvasPrefab.transform.LookAt (player.position);
			infoCanvasPrefab.transform.localRotation = Quaternion.AngleAxis (yInfoRotationOffset, Vector3.up);
			infoCanvasPrefab.transform.localRotation *= Quaternion.AngleAxis (xInfoRotationOffset, Vector3.right);
			infoCanvasPrefab.transform.localRotation *= Quaternion.AngleAxis (zInfoRotationOffset, Vector3.forward);


			if (time > timer) {
			
				infoCanvasPrefab.SetActive (false);
				StopCoroutine (infoActive);
			}

			yield return null;

		}
	}




}
