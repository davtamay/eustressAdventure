using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountingDownS : MonoBehaviour {

	private Text countingT;
	public int countDownTime = 10;
	public float countDownSpeed = 1.0f ;

	private LocalizedText localizedText;
	[SerializeField]private string[] localizedKeys;

	private float time;
	void Start () {

		countingT = GetComponent<Text>();
		localizedText = GetComponent<LocalizedText> ();

		countingT.text = "10";
		time = countDownTime;
		}

	public void ResetTime()
	{

		time = 10f;


	}

	void Update(){

		if (time >= 0) {

			
			time -= Time.unscaledDeltaTime * countDownSpeed;


			countingT.text = time.ToString ("#");

			countDownTime = (int)time;



			switch (countDownTime) {

			case 5:

				countingT.fontSize = 28;
				countingT.text = "Everything is well";
				localizedText.key = localizedKeys [0];
				localizedText.OnUpdate ();
				break;

			case 3:

				countingT.fontSize = 28;
				countingT.text = "You are well";
				localizedText.key = localizedKeys [1];
				localizedText.OnUpdate ();
				break;

			case 0:

				countingT.fontSize = 28;
				countingT.text = "Everything is OKAY!";
				localizedText.key = localizedKeys [2];
				localizedText.OnUpdate ();
				break;
				
			default:

				countingT.fontSize = 100;
				break;

			}

		} else { 
			time = 10.0f;
		}
	
	}



		 



		
}
