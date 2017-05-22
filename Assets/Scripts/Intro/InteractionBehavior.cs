using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;


public class InteractionBehaviour : MonoBehaviour {

	[SerializeField]protected UnityEvent onInteraction;
	[SerializeField]protected GameObject infoCanvasPrefab;
	[SerializeField]protected Vector3 infoOffset;
	[SerializeField]protected bool isAutomaticRotation = false;
	[SerializeField]protected float yInfoRotationOffset;
	[SerializeField]protected float xInfoRotationOffset;
	[SerializeField]protected float zInfoRotationOffset;
	[SerializeField]protected Vector3 InfoSize;
	[TextArea][SerializeField]protected string infoText;
	[SerializeField]protected float timeActive;
	[SerializeField]protected Color infoBackGround = Color.cyan;

	protected Text infoTextComponent;

	protected float distanceToInteraction;
	protected Transform thisTransform;
	protected Transform player;

	private ParticleSystem.MainModule pS;


	public virtual void Awake(){

		thisTransform = transform;
		player = GameObject.FindWithTag ("Player").transform;

		if (infoCanvasPrefab != null) {

			infoCanvasPrefab = Instantiate (infoCanvasPrefab, new Vector3(thisTransform.position.x + infoOffset.x, thisTransform.position.y + infoOffset.y, thisTransform.position.z + infoOffset.z), Quaternion.identity) ;
			infoCanvasPrefab.transform.SetParent(thisTransform);
			infoCanvasPrefab.transform.localScale += InfoSize;

			pS = infoCanvasPrefab.GetComponentInChildren<ParticleSystem> ().main;
			pS.startColor = new ParticleSystem.MinMaxGradient (infoBackGround);

			infoTextComponent = infoCanvasPrefab.GetComponentInChildren<Text> ();
			infoTextComponent.text = infoText;

			infoCanvasPrefab.SetActive (false);
		}


	
	}

	Coroutine infoActive; 

	public void TriggerInfo(){
		
		infoCanvasPrefab.SetActive (true);

		infoActive = StartCoroutine (InfoActive ());
	
	
	}
	
	public IEnumerator InfoActive(){
		infoTextComponent.text = infoText;
		float timer = timeActive;
		float time = 0;

		while (true) {

			time += Time.deltaTime;



			if (!isAutomaticRotation) {
				infoCanvasPrefab.transform.localRotation = Quaternion.AngleAxis (yInfoRotationOffset, Vector3.up);
				infoCanvasPrefab.transform.localRotation *= Quaternion.AngleAxis (xInfoRotationOffset, Vector3.right);
				infoCanvasPrefab.transform.localRotation *= Quaternion.AngleAxis (zInfoRotationOffset, Vector3.forward);

			} else {
				infoCanvasPrefab.transform.LookAt (player.position, Vector3.up);
				infoCanvasPrefab.transform.localRotation *= Quaternion.AngleAxis (180, Vector3.up);
			}

			//infoCanvasPrefab.transform.localPosition = infoOffset;

			if (time > timer) {
			
				infoCanvasPrefab.SetActive (false);
				StopCoroutine (infoActive);
			}

			yield return null;

		}
	}

	void OnDrawGizmos(){

		thisTransform = transform;
		GameObject Info = GameObject.FindWithTag ("EditorOnly");

		Vector2 InfoScale = Info.GetComponent<RectTransform>().sizeDelta;
	
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube (new Vector3(thisTransform.position.x + infoOffset.x, thisTransform.position.y + infoOffset.y, thisTransform.position.z + infoOffset.z), new Vector3(InfoScale.x + InfoSize.x, InfoScale.y + InfoSize.y, 1 + InfoSize.z));
	}





}
