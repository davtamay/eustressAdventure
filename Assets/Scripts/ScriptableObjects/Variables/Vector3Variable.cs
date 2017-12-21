using UnityEngine;

[CreateAssetMenu (fileName = "Vector3Value", menuName = "CustomSO/Variables/Vector3")]
public class Vector3Variable : ScriptableObject {

	#if UNITY_EDITOR
	[Multiline]
	public string DeveloperDescription = "";
	#endif
	public Vector3 Value;

	public void SetValue(Vector3 value)
	{
		Value = value;
	}

	public void SetValue(Vector3Variable value)
	{
		Value = value.Value;
	}

	public void ApplyChange(Vector3 amount)
	{
		Value += amount;
	}

	public void ApplyChange(Vector3Variable amount)
	{
		Value += amount.Value;
	}


	public static implicit operator Vector3(Vector3Variable reference){

		return reference.Value;
	}
}

