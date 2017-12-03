using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
/*
[SerializeField]
public class DataCollection{

	public Vector3 position;
	public List<GameObject> slotList;
	public List<string> slotListString;

	public DataCollection(){

		position = Vector3.zero;
		slotList = new List<GameObject> ();
		slotListString = new List<string> ();
	
	}

}*/
[CreateAssetMenu(fileName = "DataManager", menuName = "CustomSO/Managers/DataManager")]
public class DataManager : ScriptableObject{

	public static DataManager Instance
	{ get { return instance; } }

	private static DataManager instance = null;

	public int highScore = 0;
	public float stressLevel = 0f;
	public Vector3 position = Vector3.zero;
	public List<string> slotListItemNames = new List<string>();

	private string curScene;

	[Header("References")]
	[SerializeField]private InventoryList playerInventory;
	[SerializeField]private IntVariable playerPoints;
	[SerializeField]private Vector3 originalPlayerPosition = new Vector3 (0,2,0);




	public void Awake(){
		curScene = SceneManager.GetActiveScene ().name;

	}

	void OnEnable(){
		
		//DeletePPDataTaskProgress ();
		//PlayerPrefs.DeleteAll();
		if (instance) {
			Debug.Log ("There are two DataManagers");
			return;
		}
		instance = this; 

		//	dataCollected = new DataCollection ();

		string Path = "/" + curScene + "Score.json";

		string FullPath = Application.persistentDataPath + Path;

		if(!File.Exists(FullPath)){

			StreamWriter Writer = new StreamWriter (FullPath);
			Writer.WriteLine (JsonUtility.ToJson (this));
			Writer.Close ();
		}

		/*
		string playerDataCollection= Application.persistentDataPath + @"/playerData.json";

		if (!File.Exists(playerDataCollection)) {

			StreamWriter Writer = new StreamWriter (playerDataCollection);
			Writer.WriteLine (JsonUtility.ToJson (this));
			Writer.Close ();

		}
		*/


	}
	/*
	public void SavePlayerData(DataCollection dc){

		string OutputPath = Application.persistentDataPath + @"/playerData.json";

	/*	slotListStrings.Clear ();
		for (int i = 0; i < curSlotList.Count; i++) {
			if (slotListStrings.Contains (curSlotList [i].name)) 
				continue;
			else
				this.slotListStrings.Add (curSlotList [i].name);

		}*/

	/*

		Debug.Log ("LOADING = " + dc.slotList.Count + "POSITION" + dc.position);
	//	dataCollected.slotList.Clear ();

		for (int i = 0; i < dc.slotList.Count; i++) {
			
			if (dataCollected.slotListString.Contains (dc.slotList[i].name))
				continue;
			else
				this.dataCollected.slotListString.Add(dc.slotList[i].name);

		}


		//DataCollection nDC = new DataCollection ();

		dataCollected.position = dc.position;

		dataCollected.slotList = dc.slotList;


		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (dataCollected));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
	}
	public DataCollection LoadPlayerData (){


		string InputPath = Application.persistentDataPath + @"/playerData.json";
		Debug.Log (InputPath);
		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, dataCollected);
		Reader.Close();

		DataCollection dC = new DataCollection();
		dC.slotList = new List<GameObject> ();
		dC.position = dataCollected.position;
	//	dC.slotList = dataCollected.slotList;

		foreach (string str in PlayerManager.Instance.StringToGODict.Keys){

			for (int i = 0; i < dataCollected.slotListString.Count; i++)
				if (str == dataCollected.slotListString[i])
					dataCollected.slotList.Add (PlayerManager.Instance.StringToGODict [str]);

		}


	//	Debug.Log ("LOAD : String to Dict Count: " + PlayerManager.Instance.StringToGODict.Count + "DataCollectedCSlotList: " + dataCollected.slotList.Count + "DcSListCount : " + dC.slotList.Count + "DCposition : " + dataCollected.position);


		return dC;


	}
*/
	
//TEST
	public void DeleteAllData(){
		DeletePlayerPositionData ();
		DeleteHighScoreData ();
		DeletePPDataTaskProgress ();
		DeletePlayerInventoryData ();

	}
	public void DeletePlayerPositionData(){

		SavePosition (originalPlayerPosition);

	}
	public void DeletePlayerInventoryData(){

		SaveItemList(new List<GameObject>());

	}
	public void DeleteHighScoreData(){

	for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
			
		string sceneName = SceneManager.GetSceneByBuildIndex (i).name;
			
		string Path = "/" + sceneName + "Score.json";

		string OutputPath = Application.persistentDataPath + Path;

		highScore = 0;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (this));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
		
		}
//
//		SaveSkyWalkerScore (0);	
//		SaveCollectionsScore (0);
//		SaveHitScore (0);
//		SaveHoopScore (0);
//		SaveMatchScore (0);
//		SaveWackScore (0);

	}
	public void DeletePPDataTaskProgress(){
		PlayerPrefs.DeleteAll ();
	}

	public void SaveItemList(List<GameObject> curSlotList){
		
		
		string OutputPath = Application.persistentDataPath + @"/curItems.json";
	
									//slotListStrings.Clear ();
		for (int i = 0; i < curSlotList.Count; i++) {
		//Debug.Log(("SAVING SLOT LIST CONTAINS : " + curSlotList [i].name));
			if (slotListItemNames.Contains (curSlotList [i].name)) {
			//slotListItemNames.Remove (curSlotList [i].name);
				continue;
			}else
			this.slotListItemNames.Add (curSlotList [i].name);
		
		}
	//Open Following to Clear Off Slot in case of error (Have to save by changing levels)
	//PlayerPrefs.DeleteAll();
	//slotListItemNames.Clear ();
		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (this));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
		
	}
	public List<GameObject> LoadItemList (){


		string InputPath = Application.persistentDataPath + @"/curItems.json";
		Debug.Log (InputPath);
		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, this);
		Reader.Close();

		List<GameObject> sList = new List<GameObject> ();


		foreach(Item item in playerInventory.Items){
		for (int i = 0; i < slotListItemNames.Count; i++)
			if(item.itemName == slotListItemNames[i] ){
				sList.Add (item.itemGO);
			}
			
		}
//		foreach (string str in PlayerInventory.Instance.StringToGODict.Keys){
//			
//			for (int i = 0; i < slotListItemNames.Count; i++)
//				if (str == slotListItemNames [i]) {
//
//			//	Debug.Log ("LOADITEMLIST SLOTLISTSTRINGS :" + slotListStrings[i]);
//					sList.Add (PlayerInventory.Instance.StringToGODict [str]);
//
//				}
//			
//			}


	//	Debug.Log ("LOAD : " + "String to Dict Count: " + PlayerManager.Instance.StringToGODict.Count + "SlotListStrings: " + slotListStrings.Count + "SList : " + sList.Count);
		return sList;


	}
	public void SavePosition(Vector3 position){

		string OutputPath = Application.persistentDataPath + @"/playerpos.json";

		this.position = position;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (this));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
	}
	public Vector3 LoadPosition (){


		string InputPath = Application.persistentDataPath + @"/playerpos.json";
		Debug.Log (InputPath);
		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, this);
		Reader.Close();

		Vector3 pos = Vector3.zero;

		pos = position;

		return pos;


	}
	public void SaveStressLevel(float stressAmount){

		string OutputPath = Application.persistentDataPath + @"/stressLevel.json";

		this.stressLevel = stressAmount;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (this));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
	}
	public float LoadStressLevel (){


		string InputPath = Application.persistentDataPath + @"/stressLevel.json";
		Debug.Log (InputPath);
		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, this);
		Reader.Close();

		float stressAmount = 0;

		stressAmount = this.stressLevel;

		return stressAmount;


	}

	public void SaveScore(int score){

		string Path = "/" + curScene + "Score.json";

		string OutputPath = Application.persistentDataPath + Path;

		highScore = score;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (this));
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
			string Path = "/" + sceneToLoad + "Score.json";

		string InputPath = Application.persistentDataPath + Path;

		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, this);
		Reader.Close();

		int HighScore = 0;


		if (highScore == 0)
			return playerPoints;
	
		HighScore = highScore;

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
