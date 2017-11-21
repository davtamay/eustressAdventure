using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class CurrentTimeToText : MonoBehaviour {
	

	IEnumerator Start () {

		Text textComp = GetComponent<Text> ();

		while(true){


			textComp.text = System.DateTime.Now.ToString("hh:mm tt");
			yield return new WaitForSecondsRealtime (20);
		}
		
		
	}
	

}
