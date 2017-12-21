using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "DataCollection", menuName = "CustomSO/Data/DataCollection")]
public class DataCollection : ScriptableObject {
	[Header("Score Data")]
	public int highScore = 0;
	[Header("Stress Data")]
	public float stressLevel = 0f;
	public CopingMechanismsList copingMechanismList;
	[Header("Position Data")]
	public Vector3 position = Vector3.zero;

	[Header("Task Data")]
	public List<string> slotListItemNames = new List<string>();
	public List<Task> taskList = new List<Task>();
	public List<Task_Status> taskStatus = new List<Task_Status> ();
	public Dictionary<Task, Task_Status> taskDictionary = new Dictionary<Task, Task_Status>();


}
