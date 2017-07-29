using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FolowAllong : MonoBehaviour {

	public int messageLength;
	public float scrollSpeed = 0.05f;

	public Scrollbar scrollb;



	void Start () {

		scrollb = GetComponentInChildren <Scrollbar>();
		scrollb.value = 0;

	
	}
	

	void Update () {

	//	if (messageLength > 300)
	//		scrollSpeed = 9;
	//	else if (messageLength > 200)
	//		scrollSpeed = 8;


		scrollb.value += (scrollSpeed/ messageLength) * Time.unscaledDeltaTime   ;//Mathf.Lerp (0, 1, Time.unscaledDeltaTime * (speed));
		//scrollb.value = (scrollb.value* messageLength) *  scrollSpeed   ;

		if (scrollb.value == 1)
			scrollb.value = 0;
			
	
	}
}
