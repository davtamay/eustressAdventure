using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStressGage : MonoBehaviour {


	public static UIStressGage Instance
	{ get { return instance; } }

	private static UIStressGage instance = null;

	private Transform stressGageArrow;

	//[SerializeField]private int naturalStress = 5;
	public bool isAddStress;
	public bool isReduceStress;
	private Vector3 originalPos;

	
	//180 to down

	void Awake(){
		
		if (instance) {
			Debug.Log ("There are two UIStressGage Scripts");
			return;
		}
		instance = this; 

		stressGageArrow = transform.GetChild (0);
		originalPos = stressGageArrow.localEulerAngles;

		stress = 2;
	}




	private float _stress;
	public float stress { 

		get { return _stress; } 

		set {if (_stress + value <= 0) {
				stressGageArrow.localEulerAngles = new Vector3 (0, 0,180);
				_stress = 0;
				value = 0;
				return;
			}
			if (_stress + value >= 180) {
				stressGageArrow.localEulerAngles = new Vector3 (0, 0, 0);
				_stress = 180;
				value = 0;
				return;
			}





			stressGageArrow.Rotate (new Vector3(0,0,-1 * value), Space.Self);

			_stress += value;
		}

	}


	void Update () {

		if (isAddStress) {
			stress = 5;
			isAddStress = false;
		}
		if (isReduceStress) {
			stress = -5;
			isReduceStress = false;
		}
		//isRotate = false;
		//}
	//	Debug.Log (_stress);


	}

	//void ADDStress (int stressAmount ){

	//	stressGageArrow.Rotate (-1 * stressAmount, Space.Self);
	
	
	//}





}
