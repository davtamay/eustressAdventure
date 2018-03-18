using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "CustomSO/Data/Task")]
public class Task : ScriptableObject {

	public string taskDescription;
	public Task_Status taskStatus;

}
