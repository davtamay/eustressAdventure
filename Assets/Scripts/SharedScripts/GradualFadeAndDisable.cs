using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradualFadeAndDisable : MonoBehaviour {

	public float fadeSpeed;
	public float growSpeed;

	public Vector3 growSize;

	private Text origText;
	private Vector3 origSize;
	public Color originalColor;
	private Text fadeToText;


	public Color fadeToColor;
	void Start()
	{
		origText = GetComponent<Text> ();
		origSize = origText.transform.localScale;

		originalColor = origText.color;


		//fadeToColor = origText.color;
		//fadeToColor.a = 0;

		//fadeToText.color = alphaZero;



	}

	void Update()
	{
		origText.color = Color.Lerp (origText.color, fadeToColor, Time.deltaTime * fadeSpeed);
		origText.transform.localScale = Vector3.Lerp (origText.transform.localScale, growSize, Time.deltaTime * growSpeed);

		if (origText.color.a <= 0.01)
			gameObject.SetActive (false);
	}
}
