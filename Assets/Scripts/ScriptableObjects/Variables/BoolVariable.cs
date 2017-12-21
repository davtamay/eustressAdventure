using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "bool", menuName = "CustomSO/Variables/bool")]
public class BoolVariable : ScriptableObject {

	public bool isOn = false;

	public void SetValue(bool value)
	{
		isOn = value;
	}
	public void SetValue(BoolVariable value)
	{
		isOn = value.isOn;
	}

//	public static implicit operator bool(BoolVariable reference){
//
//		return reference.isOn;
//
//	}

}
