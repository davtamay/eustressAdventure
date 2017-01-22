using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountingDownS : MonoBehaviour {

	private Text countingT;
	public int countDownTime = 10;
	public float countDownSpeed = 1.0f ;

	private float time;
	void Start () {

		countingT = GetComponentInChildren <Text>();
		countingT.text = "10";
		time = countDownTime;
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
				break;

			case 3:

				countingT.fontSize = 28;
				countingT.text = "You are well";
				break;

			case 0:

				countingT.fontSize = 28;
				countingT.text = "Everything is OKAY!";
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
