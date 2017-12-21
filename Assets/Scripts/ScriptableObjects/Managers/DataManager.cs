using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;

//FIXME PLAYER ITEM AND SLOTLIST IS NOT CREATED ON START ERROR IN START


[CreateAssetMenu(fileName = "DataManager", menuName = "CustomSO/Managers/DataManager")]
public class DataManager : ScriptableObject{

//	public int highScore = 0;
//	public float stressLevel = 0f;
//	public Vector3 position = Vector3.zero;
//	public List<string> slotListItemNames = new List<string>();

	private string curScene;

	[Header("References")]
	public DataCollection playerData;
	[SerializeField]private InventoryList masterPlayerInventory;
	public TaskList masterTaskList;
	[SerializeField]private IntVariable playerPoints;
	[SerializeField]private Vector3 originalPlayerPosition = new Vector3 (0,2,0);




	public void Awake(){
		curScene = SceneManager.GetActiveScene ().name;

	}

	void OnEnable(){
		
//		string Path = "/" + curScene + "Score.json";
//
//		string FullPath = Application.persistentDataPath + "/" + playerData.name + Path;
//
//		if(!File.Exists(FullPath)){
//
//			StreamWriter Writer = new StreamWriter (FullPath);
//			Writer.WriteLine (JsonUtility.ToJson (playerData));
//			Writer.Close ();
//		}

		/*
		string playerDataCollection= Application.persistentDataPath + @"/playerData.json";

		if (!File.Exists(playerDataCollection)) {

			StreamWriter Writer = new StreamWriter (playerDataCollection);
			Writer.WriteLine (JsonUtility.ToJson (playerData));
			Writer.Close ();

		}
		*/


	}

	public void DeleteAllData(){
		DeletePlayerPositionData ();
		DeleteHighScoreData ();
		//DeletePPDataTaskProgress ();
		DeletePlayerTaskData ();
		DeletePlayerInventoryData ();

	}
	public void DeletePlayerPositionData(){

		SavePosition (originalPlayerPosition);

	}
	public void DeletePlayerInventoryData(){

		SaveItemList(new List<GameObject>());

	}
	public void DeletePlayerTaskData(){

		playerData.taskList.Clear ();
		playerData.taskStatus.Clear ();

		string OutputPath = Application.persistentDataPath + "/" + playerData.name + "curTaskStatus.json";
		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (playerData));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);

	}
	public void DeleteHighScoreData(){

	for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
			
		string sceneName = SceneManager.GetSceneByBuildIndex (i).name;
			
		string Path = "/" + playerData.name + sceneName + "Score.json";

		string OutputPath = Application.persistentDataPath + Path;

		playerData.highScore = 0;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (playerData));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
		
		}

	}
	public void DeletePPDataTaskProgress(){
		PlayerPrefs.DeleteAll ();
	}

	public void SaveItemList(List<GameObject> curSlotList){
		
		
		string OutputPath = Application.persistentDataPath + "/" + playerData.name + "curItems.json";
	
									//slotListStrings.Clear ();
		for (int i = 0; i < curSlotList.Count; i++) {
		//Debug.Log(("SAVING SLOT LIST CONTAINS : " + curSlotList [i].name));
			if (playerData.slotListItemNames.Contains (curSlotList [i].name)) {
			//slotListItemNames.Remove (curSlotList [i].name);
				continue;
			}else
			playerData.slotListItemNames.Add (curSlotList [i].name);
		
		}
	//Open Following to Clear Off Slot in case of error (Have to save by changing levels)
	//PlayerPrefs.DeleteAll();
	//slotListItemNames.Clear ();
		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (playerData));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
		
	}
	public List<GameObject> LoadItemList (){


	string InputPath = Application.persistentDataPath + "/" + playerData.name + "curItems.json";
		Debug.Log (InputPath);
		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, playerData);
		Reader.Close();

		List<GameObject> sList = new List<GameObject> ();


		foreach(Item item in masterPlayerInventory.Items){
		for (int i = 0; i < playerData.slotListItemNames.Count; i++)
			if(item.itemName == playerData.slotListItemNames[i] ){
				sList.Add (item.itemGO);
			}
			
		}
		return sList;


	}


	public void SaveTaskStatus(Task task, Task_Status status){

//		DeletePlayerTaskData ();

		string OutputPath = Application.persistentDataPath + "/" + playerData.name + "curTaskStatus.json";

		for (int i = 0; i < masterTaskList.Items.Count; i++) {

			if(masterTaskList.Items[i] == task){

				if (playerData.taskList.Contains (task))
					return;

				playerData.taskList.Add(task);
				playerData.taskStatus.Add(status);
				//playerData.taskList.Add(task);
				//playerData.taskStatus.Add(status);
				break;
			}

		}

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (playerData));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);

	}

	public List<Task> LoadTaskStatusList (){

		//DeletePlayerTaskData ();
		string InputPath = Application.persistentDataPath + "/" + playerData.name + "curTaskStatus.json";
		Debug.Log (InputPath);
		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, playerData);
		Reader.Close();

		playerData.taskDictionary.Clear ();
		for (int i = 0; i < playerData.taskList.Count; i++) {

			//Enum.Parse (Task_Status, playerData.taskStatus [i]);
		
//			if (playerData.taskDictionary.ContainsKey (playerData.taskList [i]))
//				continue;
		
			playerData.taskList [i].taskStatus = playerData.taskStatus[i];
			playerData.taskDictionary.Add (playerData.taskList [i], playerData.taskStatus [i]);

		}

		return playerData.taskList;


	}



	public void SavePosition(Vector3 position){

	string OutputPath = Application.persistentDataPath + "/" + playerData.name + "playerpos.json";

		playerData.position = position;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (playerData));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
	}
	public Vector3 LoadPosition (){


	string InputPath = Application.persistentDataPath + "/" + playerData.name + "playerpos.json";
		Debug.Log (InputPath);
		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, playerData);
		Reader.Close();

		Vector3 pos = Vector3.zero;

		pos = playerData.position;

		return pos;


	}
	public void SaveStressLevel(float stressAmount){

	string OutputPath = Application.persistentDataPath + "/" + playerData.name + "stressLevel.json";

		playerData.stressLevel = stressAmount;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (playerData));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
	}
	public float LoadStressLevel (){


	string InputPath = Application.persistentDataPath + "/" + playerData.name + "stressLevel.json";
		Debug.Log (InputPath);
		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, playerData);
		Reader.Close();

		float stressAmount = 0;

		stressAmount = playerData.stressLevel;

		return stressAmount;


	}

	public void SaveCopingMechanisms(){

		string OutputPath = Application.persistentDataPath + "/" + playerData.name + "copingMech.json";

		//playerData.stressLevel = stressAmount;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (playerData));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
	}
	public void LoadCopingMechanisms (){


		string InputPath = Application.persistentDataPath + "/" + playerData.name + "copingMech.json";
		Debug.Log (InputPath);
		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, playerData);
		Reader.Close();

		//float stressAmount = 0;

		//stressAmount = playerData.stressLevel;

		//return stressAmount;


	}


	public void SaveScore(int score){

	string Path = "/" + playerData.name + curScene + "Score.json";

		string OutputPath = Application.persistentDataPath +  Path;

		playerData.highScore = score;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (playerData));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
	}
/// <summary>
/// Loads the score for active scene or input scene name to load specific score.
/// </summary>
/// <returns>The score.</returns>
/// <param name="scene">Scene.</param>
	public int LoadScore (string scene = ""){
		
		string sceneToLoad = curScene;
		
		if (scene != "")
			sceneToLoad = scene;

		//string curSceneName = SceneManager.GetActiveScene ().name;
		string Path = "/" + playerData.name + sceneToLoad + "Score.json";

	string InputPath = Application.persistentDataPath + Path;

		if(!File.Exists(InputPath)){

		StreamWriter Writer = new StreamWriter (InputPath);
		Writer.WriteLine (JsonUtility.ToJson (playerData));
		Writer.Close ();

		}

		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, playerData);
		Reader.Close();

		int HighScore = 0;


		if (playerData.highScore == 0)
			return playerPoints.Value;
	
		HighScore = playerData.highScore;

		return HighScore;


	}



	public void CheckHighScore(){

		if (playerPoints.Value > LoadScore ()) {
			//have to set up file before usage ex: add 0 to Save at Start and then erase
			SaveScore (playerPoints.Value);
			return;
		} 


	}
}
//
//
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using System.IO;
////[System.Serializable]
////public class DataCollection{
////
////	public int highScore = 0;
////	public float stressLevel = 0f;
////	public Vector3 position = Vector3.zero;
////	public List<string> slotListItemNames = new List<string>();
////
////
//////	public Vector3 position;
////	public List<GameObject> slotList;
////	public List<string> slotListString;
//
////	public DataCollection(){
////
////		position = Vector3.zero;
////		slotList = new List<GameObject> ();
////		slotListString = new List<string> ();
////	
////	}
//
////}
//[CreateAssetMenu(fileName = "DataManager", menuName = "CustomSO/Managers/DataManager")]
//public class DataManager : ScriptableObject{
//
//	//	public int highScore = 0;
//	//	public float stressLevel = 0f;
//	//	public Vector3 position = Vector3.zero;
//	//	public List<string> slotListItemNames = new List<string>();
//
//	private string curScene;
//
//	[Header("References")]
//	public DataCollection playerData;
//	[SerializeField]private InventoryList playerInventory;
//	[SerializeField]private IntVariable playerPoints;
//	[SerializeField]private Vector3 originalPlayerPosition = new Vector3 (0,2,0);
//
//
//
//
//	//	public void Awake(){
//	//		
//	//
//	//	}
//
//	void OnEnable(){
//
//		string FullPath;
//		Debug.Log ("Created Directory at:" + SceneManager.sceneCountInBuildSettings);
//
//		FullPath = Application.persistentDataPath + "/" + playerData.name;
//
//
//		if (!File.Exists (FullPath)) {
//
//			StreamWriter Writer = new StreamWriter (FullPath);
//			Writer.WriteLine (JsonUtility.ToJson (playerData));
//			Writer.Close ();
//			Debug.Log ("Created Directory at:" + FullPath);
//
//		}
//		FullPath = string.Empty;
//		for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
//
//			FullPath = string.Empty;
//			string sceneName = SceneManager.GetSceneByBuildIndex (i).name;
//
//			if (sceneName == string.Empty)
//				continue;
//
//			string Path = "/" + sceneName + "Score.json";
//
//			FullPath = Application.persistentDataPath + "/" + playerData.name + Path;
//
//
//			//playerData.highScore = 0;
//			if (!File.Exists (FullPath)) {
//
//				StreamWriter Writer = new StreamWriter (FullPath);
//				Writer.WriteLine (JsonUtility.ToJson (playerData));
//				Writer.Close ();
//				Debug.Log ("Created Directory at:" + FullPath);
//
//			}
//
//
//		}
//
//
//		curScene = SceneManager.GetActiveScene ().name;
//		//		string Path = "/" + curScene + "Score.json";
//		//
//		//		string FullPath = Application.persistentDataPath + "/" + playerData.name + Path;
//		//
//		//		if(!File.Exists(FullPath)){
//		//
//		//			StreamWriter Writer = new StreamWriter (FullPath);
//		//			Writer.WriteLine (JsonUtility.ToJson (playerData));
//		//			Writer.Close ();
//		//		}
//
//		/*
//		string playerDataCollection= Application.persistentDataPath + @"/playerData.json";
//
//		if (!File.Exists(playerDataCollection)) {
//
//			StreamWriter Writer = new StreamWriter (playerDataCollection);
//			Writer.WriteLine (JsonUtility.ToJson (playerData));
//			Writer.Close ();
//
//		}
//		*/
//
//
//	}
//	/*
//	public void SavePlayerData(DataCollection dc){
//
//		string OutputPath = Application.persistentDataPath + @"/playerData.json";
//
//	/*	slotListStrings.Clear ();
//		for (int i = 0; i < curSlotList.Count; i++) {
//			if (slotListStrings.Contains (curSlotList [i].name)) 
//				continue;
//			else
//				playerData.slotListStrings.Add (curSlotList [i].name);
//
//		}*/
//
//	/*
//
//	Debug.Log ("LOADING = " + dc.slotList.Count + "POSITION" + dc.position);
//	//	dataCollected.slotList.Clear ();
//
//	for (int i = 0; i < dc.slotList.Count; i++) {
//
//		if (dataCollected.slotListString.Contains (dc.slotList[i].name))
//			continue;
//		else
//			playerData.dataCollected.slotListString.Add(dc.slotList[i].name);
//
//	}
//
//
//	//DataCollection nDC = new DataCollection ();
//
//	dataCollected.position = dc.position;
//
//	dataCollected.slotList = dc.slotList;
//
//
//	StreamWriter Writer = new StreamWriter (OutputPath);
//	Writer.WriteLine (JsonUtility.ToJson (dataCollected));
//	Writer.Close();
//	Debug.Log("output to:" + OutputPath);
//}
//public DataCollection LoadPlayerData (){
//
//
//	string InputPath = Application.persistentDataPath + @"/playerData.json";
//	Debug.Log (InputPath);
//	StreamReader Reader = new StreamReader (InputPath);
//	string JSonString = Reader.ReadToEnd ();
//	Debug.Log ("Reading:" + JSonString);
//	JsonUtility.FromJsonOverwrite (JSonString, dataCollected);
//	Reader.Close();
//
//	DataCollection dC = new DataCollection();
//	dC.slotList = new List<GameObject> ();
//	dC.position = dataCollected.position;
//	//	dC.slotList = dataCollected.slotList;
//
//	foreach (string str in PlayerManager.Instance.StringToGODict.Keys){
//
//		for (int i = 0; i < dataCollected.slotListString.Count; i++)
//			if (str == dataCollected.slotListString[i])
//				dataCollected.slotList.Add (PlayerManager.Instance.StringToGODict [str]);
//
//	}
//
//
//	//	Debug.Log ("LOAD : String to Dict Count: " + PlayerManager.Instance.StringToGODict.Count + "DataCollectedCSlotList: " + dataCollected.slotList.Count + "DcSListCount : " + dC.slotList.Count + "DCposition : " + dataCollected.position);
//
//
//	return dC;
//
//
//}
//*/
//
////TEST
//public void DeleteAllData(){
//	DeletePlayerPositionData ();
//	DeleteHighScoreData ();
//	DeletePPDataTaskProgress ();
//	DeletePlayerInventoryData ();
//
//}
//public void DeletePlayerPositionData(){
//
//	SavePosition (originalPlayerPosition);
//
//}
//public void DeletePlayerInventoryData(){
//
//	SaveItemList(new List<GameObject>());
//
//}
//public void DeleteHighScoreData(){
//
//	for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
//
//		string sceneName = SceneManager.GetSceneByBuildIndex (i).name;
//
//		string Path = "/" + sceneName + "Score.json";
//
//		string OutputPath = Application.persistentDataPath + "/" + playerData.name + Path;
//
//		playerData.highScore = 0;
//
//		StreamWriter Writer = new StreamWriter (OutputPath);
//		Writer.WriteLine (JsonUtility.ToJson (playerData));
//		Writer.Close();
//		Debug.Log("output to:" + OutputPath);
//
//	}
//	//
//	//		SaveSkyWalkerScore (0);	
//	//		SaveCollectionsScore (0);
//	//		SaveHitScore (0);
//	//		SaveHoopScore (0);
//	//		SaveMatchScore (0);
//	//		SaveWackScore (0);
//
//}
//public void DeletePPDataTaskProgress(){
//	PlayerPrefs.DeleteAll ();
//}
//
//public void SaveItemList(List<GameObject> curSlotList){
//
//
//	string OutputPath = Application.persistentDataPath + "/" + playerData.name  + @"/curItems.json";
//
//	//slotListStrings.Clear ();
//	for (int i = 0; i < curSlotList.Count; i++) {
//		//Debug.Log(("SAVING SLOT LIST CONTAINS : " + curSlotList [i].name));
//		if (playerData.slotListItemNames.Contains (curSlotList [i].name)) {
//			//slotListItemNames.Remove (curSlotList [i].name);
//			continue;
//		}else
//			playerData.slotListItemNames.Add (curSlotList [i].name);
//
//	}
//	//Open Following to Clear Off Slot in case of error (Have to save by changing levels)
//	//PlayerPrefs.DeleteAll();
//	//slotListItemNames.Clear ();
//	StreamWriter Writer = new StreamWriter (OutputPath);
//	Writer.WriteLine (JsonUtility.ToJson (playerData));
//	Writer.Close();
//	Debug.Log("output to:" + OutputPath);
//
//}
//public List<GameObject> LoadItemList (){
//
//
//	string InputPath = Application.persistentDataPath + "/" + playerData.name + @"/curItems.json";
//	Debug.Log (InputPath);
//	StreamReader Reader = new StreamReader (InputPath);
//	string JSonString = Reader.ReadToEnd ();
//	Debug.Log ("Reading:" + JSonString);
//	JsonUtility.FromJsonOverwrite (JSonString, playerData);
//	Reader.Close();
//
//	List<GameObject> sList = new List<GameObject> ();
//
//
//	foreach(Item item in playerInventory.Items){
//		for (int i = 0; i < playerData.slotListItemNames.Count; i++)
//			if(item.itemName == playerData.slotListItemNames[i] ){
//				sList.Add (item.itemGO);
//			}
//
//	}
//	//		foreach (string str in PlayerInventory.Instance.StringToGODict.Keys){
//	//			
//	//			for (int i = 0; i < slotListItemNames.Count; i++)
//	//				if (str == slotListItemNames [i]) {
//	//
//	//			//	Debug.Log ("LOADITEMLIST SLOTLISTSTRINGS :" + slotListStrings[i]);
//	//					sList.Add (PlayerInventory.Instance.StringToGODict [str]);
//	//
//	//				}
//	//			
//	//			}
//
//
//	//	Debug.Log ("LOAD : " + "String to Dict Count: " + PlayerManager.Instance.StringToGODict.Count + "SlotListStrings: " + slotListStrings.Count + "SList : " + sList.Count);
//	return sList;
//
//
//}
//public void SavePosition(Vector3 position){
//
//	string OutputPath = Application.persistentDataPath + "/" + playerData.name + @"/playerpos.json";
//
//	playerData.position = position;
//
//	StreamWriter Writer = new StreamWriter (OutputPath);
//	Writer.WriteLine (JsonUtility.ToJson (playerData));
//	Writer.Close();
//	Debug.Log("output to:" + OutputPath);
//}
//public Vector3 LoadPosition (){
//
//
//	string InputPath = Application.persistentDataPath + "/" + playerData.name + @"/playerpos.json";
//	Debug.Log (InputPath);
//	StreamReader Reader = new StreamReader (InputPath);
//	string JSonString = Reader.ReadToEnd ();
//	Debug.Log ("Reading:" + JSonString);
//	JsonUtility.FromJsonOverwrite (JSonString, playerData);
//	Reader.Close();
//
//	Vector3 pos = Vector3.zero;
//
//	pos = playerData.position;
//
//	return pos;
//
//
//}
//public void SaveStressLevel(float stressAmount){
//
//	string OutputPath = Application.persistentDataPath + "/" + playerData.name + @"/stressLevel.json";
//
//	playerData.stressLevel = stressAmount;
//
//	StreamWriter Writer = new StreamWriter (OutputPath);
//	Writer.WriteLine (JsonUtility.ToJson (playerData));
//	Writer.Close();
//	Debug.Log("output to:" + OutputPath);
//}
//public float LoadStressLevel (){
//
//
//	string InputPath = Application.persistentDataPath + "/" + playerData.name + @"/stressLevel.json";
//	Debug.Log (InputPath);
//	StreamReader Reader = new StreamReader (InputPath);
//	string JSonString = Reader.ReadToEnd ();
//	Debug.Log ("Reading:" + JSonString);
//	JsonUtility.FromJsonOverwrite (JSonString, playerData);
//	Reader.Close();
//
//	float stressAmount = 0;
//
//	stressAmount = playerData.stressLevel;
//
//	return stressAmount;
//
//
//}
//
//public void SaveScore(int score){
//
//	string Path = "/" + curScene + "Score.json";
//
//	string OutputPath = Application.persistentDataPath + "/" + playerData.name + Path;
//
//	playerData.highScore = score;
//
//	StreamWriter Writer = new StreamWriter (OutputPath);
//	Writer.WriteLine (JsonUtility.ToJson (playerData));
//	Writer.Close();
//	Debug.Log("output to:" + OutputPath);
//}
///// <summary>
///// Loads the score for active scene or input scene name to load specific score.
///// </summary>
///// <returns>The score.</returns>
///// <param name="scene">Scene.</param>
//public int LoadScore (string scene = ""){
//
//	string sceneToLoad = curScene;
//
//	if (scene != "")
//		sceneToLoad = scene;
//
//	//string curSceneName = SceneManager.GetActiveScene ().name;
//	string Path = "/" + sceneToLoad + "Score.json";
//
//	string InputPath = Application.persistentDataPath + "/" + playerData.name + Path;
//
//	if(!File.Exists(InputPath)){
//
//		StreamWriter Writer = new StreamWriter (InputPath);
//		Writer.WriteLine (JsonUtility.ToJson (playerData));
//		Writer.Close ();
//
//	}
//
//	StreamReader Reader = new StreamReader (InputPath);
//	string JSonString = Reader.ReadToEnd ();
//	Debug.Log ("Reading:" + JSonString);
//	JsonUtility.FromJsonOverwrite (JSonString, playerData);
//	Reader.Close();
//
//	int HighScore = 0;
//
//
//	if (playerData.highScore == 0)
//		return playerPoints.Value;
//
//	HighScore = playerData.highScore;
//
//	return HighScore;
//
//
//}
//
//
//
//public void CheckHighScore(){
//
//	if (playerPoints.Value > LoadScore ()) {
//		//have to set up file before usage ex: add 0 to Save at Start and then erase
//		SaveScore (playerPoints.Value);
//		return;
//	} 
//
//
//}
//}
//
