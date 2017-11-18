using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffects : MonoBehaviour {
	[SerializeField]private float timeUntilDisable;

	[SerializeField]private bool isColorEffectOn;
	public float fadeSpeed = 1.2f;
	public Color originalColor;
	public Color fadeToColor;

	[SerializeField]private bool isSizeEffectOn;
	public float sizeSpeed = 1.2f;
	public Vector3 sizeGoal = new Vector3 (2.2f, 2.2f, 1f);

	[SerializeField]private bool isMoveEffectOn;
	public float moveSpeed = 1.2f;
	public Vector3 moveToGoal = new Vector3 (2.2f, 2.2f, 1f);

	[SerializeField]private bool isRotateEffectOn;
	public float rotateSpeed = 1.2f;
	public Vector3 rotateToGoal = new Vector3 (2.2f, 2.2f, 1f);

	[SerializeField]private bool isShakingEffectOn;
	[SerializeField] private float shakeAmount;
	[SerializeField] private float shakeSpeed;
	[SerializeField] private float shakeTime;

	//[SerializeField]private bool isColorEffectOn;


	private Text origText;
	private Transform thisTransform;
	//private Vector3 origSize;

	private Text fadeToText;



	void Start()
	{
		origText = GetComponent<Text> ();
		thisTransform = transform;
		originalColor = origText.color;

	}

	float timer;
	Coroutine shakeCoroutine;
	void Update()
	{
		if (GameController.Instance.IsStartMenuActive)
			return;
		
		timer += Time.deltaTime;

		if (timeUntilDisable <= timer)
			thisTransform.gameObject.SetActive (false);
		
		if (isColorEffectOn) 
			origText.color = Color.Lerp (origText.color, fadeToColor, Time.deltaTime * fadeSpeed);
	
	

		if (isSizeEffectOn) 
			thisTransform.localScale = Vector3.Lerp (thisTransform.localScale, sizeGoal, Time.deltaTime * sizeSpeed);


		if(isMoveEffectOn)
			thisTransform.position = Vector3.Lerp (thisTransform.position, moveToGoal, Time.deltaTime * moveSpeed);

		

		if(isRotateEffectOn)
			thisTransform.rotation = Quaternion.Lerp (thisTransform.rotation, Quaternion.LookRotation(rotateToGoal), Time.deltaTime * rotateSpeed);

		if(isShakingEffectOn){

			if(shakeCoroutine == null){
				shakeCoroutine = StartCoroutine (Shake ());


			}


		}
			
	}

	IEnumerator Shake(){

		Vector3 origPosition = thisTransform.localPosition;
		float ElapsedTime = 0f;

		while (ElapsedTime < shakeTime) {

			Vector3 RandomPoint = origPosition + Random.insideUnitSphere * shakeAmount;

			thisTransform.localPosition = Vector3.Lerp (thisTransform.localPosition, RandomPoint, Time.deltaTime * shakeSpeed);

			yield return null;
			ElapsedTime += Time.deltaTime;
		}

		thisTransform.localPosition = origPosition;

	}
}
