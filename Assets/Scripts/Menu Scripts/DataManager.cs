using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
public class DataManager : MonoBehaviour {

	public static DataManager Instance
	{ get { return instance; } }

	private static DataManager instance = null;

	public int highScore = 0;
	public float stressLevel = 0f;
	public Vector3 position = Vector3.zero;
	public List<string> slotListItemNames = new List<string>();

//	public DataCollection dataCollected;




	void Awake(){
		
		//DeletePPDataTaskProgress ();
		//PlayerPrefs.DeleteAll();
		if (instance) {
			Debug.Log ("There are two HighScoreManagers");
			return;
		}
		instance = this; 

	//	dataCollected = new DataCollection ();

		string skyWalkerPath = Application.persistentDataPath + @"/skyWalkerScore.json";

		if(!File.Exists(skyWalkerPath)){

			StreamWriter Writer = new StreamWriter (skyWalkerPath);
			Writer.WriteLine (JsonUtility.ToJson (this));
			Writer.Close ();
		}

		string collectionsPath = Application.persistentDataPath + @"/collectionsScore.json";


		if (!File.Exists(collectionsPath)) {

			StreamWriter Writer = new StreamWriter (collectionsPath);
			Writer.WriteLine (JsonUtility.ToJson (this));
			Writer.Close ();

		}

		string finderPath = Application.persistentDataPath + @"/finderScore.json";


		if (!File.Exists(finderPath)) {

			StreamWriter Writer = new StreamWriter (finderPath);
			Writer.WriteLine (JsonUtility.ToJson (this));
			Writer.Close ();

		}
		string wackPath = Application.persistentDataPath + @"/wackScore.json";

		if(!File.Exists(wackPath)){

			StreamWriter Writer = new StreamWriter (wackPath);
			Writer.WriteLine (JsonUtility.ToJson (this));
			Writer.Close ();
		}

		string matchPath = Application.persistentDataPath + @"/matchScore.json";


		if (!File.Exists(matchPath)) {

			StreamWriter Writer = new StreamWriter (matchPath);
			Writer.WriteLine (JsonUtility.ToJson (this));
			Writer.Close ();

		}

		string shootPath = Application.persistentDataPath + @"/shootScore.json";


		if (!File.Exists(shootPath)) {

			StreamWriter Writer = new StreamWriter (shootPath);
			Writer.WriteLine (JsonUtility.ToJson (this));
			Writer.Close ();

		}
		string hitPath = Application.persistentDataPath + @"/hitScore.json";


		if (!File.Exists(hitPath)) {

			StreamWriter Writer = new StreamWriter (hitPath);
			Writer.WriteLine (JsonUtility.ToJson (this));
			Writer.Close ();

		}

		string hoopPath = Application.persistentDataPath + @"/hoopScore.json";


		if (!File.Exists(hoopPath)) {

			StreamWriter Writer = new StreamWriter (hoopPath);
			Writer.WriteLine (JsonUtility.ToJson (this));
			Writer.Close ();

		}

		string position = Application.persistentDataPath + @"/playerpos.json";

		if (!File.Exists(position)) {

			StreamWriter Writer = new StreamWriter (position);
			Writer.WriteLine (JsonUtility.ToJson (this));
			Writer.Close ();

		}

		string items = Application.persistentDataPath + @"/curItems.json";

		if (!File.Exists(items)) {

			StreamWriter Writer = new StreamWriter (items);
			Writer.WriteLine (JsonUtility.ToJson (this));
			Writer.Close ();

		}


		string stressLevel = Application.persistentDataPath + @"/stressLevel.json";

		if (!File.Exists(items)) {

			StreamWriter Writer = new StreamWriter (items);
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
	public void DeleteHighScoreSlotandPositionData(Vector3 originalPos){

		//slotListStrings.Clear ();
		SavePosition(originalPos);
		SaveItemList(new List<GameObject>());
		SaveSkyWalkerScore (0);	
		SaveCollectionsScore (0);
		SaveHitScore (0);
		SaveHoopScore (0);
		SaveMatchScore (0);
		SaveWackScore (0);

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


		foreach (string str in PlayerManager.Instance.StringToGODict.Keys){
			
			for (int i = 0; i < slotListItemNames.Count; i++)
				if (str == slotListItemNames [i]) {

			//	Debug.Log ("LOADITEMLIST SLOTLISTSTRINGS :" + slotListStrings[i]);
					sList.Add (PlayerManager.Instance.StringToGODict [str]);

				}
			
			}


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


	public void SaveSkyWalkerScore(int score){

		string OutputPath = Application.persistentDataPath + @"/skyWalkerScore.json";

		highScore = score;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (this));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
	}

	public int LoadSkyWalkerScore (){


		string InputPath = Application.persistentDataPath + @"/skyWalkerScore.json";
		Debug.Log (InputPath);
		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, this);
		Reader.Close();

		int HighScore = 0;

		HighScore = highScore;

		return HighScore;


	}

	public void SaveCollectionsScore(int score){

		string OutputPath = Application.persistentDataPath + @"/collectionsScore.json";

		highScore = score;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (this));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
	}

	public int LoadCollectionsScore (){

		string InputPath = Application.persistentDataPath + @"/collectionsScore.json";

		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, this);
		Reader.Close();

		int HighScore = 0;

		HighScore = highScore;

		return HighScore;


	}
	public void SaveFinderScore(int score){

		string OutputPath = Application.persistentDataPath + @"/finderScore.json";

		highScore = score;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (this));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
	}

	public int LoadFinderScore (){

		string InputPath = Application.persistentDataPath + @"/finderScore.json";

		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, this);
		Reader.Close();

		int HighScore = 0;

		HighScore = highScore;

		return HighScore;


	}

	public void SaveWackScore(int score){

		string OutputPath = Application.persistentDataPath + @"/wackScore.json";

		highScore = score;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (this));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
	}

	public int LoadWackScore (){

		string InputPath = Application.persistentDataPath + @"/wackScore.json";

		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, this);
		Reader.Close();

		int HighScore = 0;

		HighScore = highScore;

		return HighScore;


	}
	public void SaveMatchScore(int score){

		string OutputPath = Application.persistentDataPath + @"/matchScore.json";

		highScore = score;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (this));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
	}

	public int LoadMatchScore (){

		string InputPath = Application.persistentDataPath + @"/matchScore.json";

		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, this);
		Reader.Close();

		int HighScore = 0;

		HighScore = highScore;

		return HighScore;


	}
	public void SaveShootScore(int score){

		string OutputPath = Application.persistentDataPath + @"/shootScore.json";

		highScore = score;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (this));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
	}

	public int LoadShootScore (){

		string InputPath = Application.persistentDataPath + @"/shootScore.json";

		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, this);
		Reader.Close();

		int HighScore = 0;

		HighScore = highScore;

		return HighScore;


	}
	public void SaveHitScore(int score){

		string OutputPath = Application.persistentDataPath + @"/hitScore.json";

		highScore = score;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (this));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
	}

	public int LoadHitScore (){

		string InputPath = Application.persistentDataPath + @"/hitScore.json";

		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, this);
		Reader.Close();

		int HighScore = 0;

		HighScore = highScore;

		return HighScore;


	}
	public void SaveHoopScore(int score){

		string OutputPath = Application.persistentDataPath + @"/hoopScore.json";

		highScore = score;

		StreamWriter Writer = new StreamWriter (OutputPath);
		Writer.WriteLine (JsonUtility.ToJson (this));
		Writer.Close();
		Debug.Log("output to:" + OutputPath);
	}

	public int LoadHoopScore (){

		string InputPath = Application.persistentDataPath + @"/hoopScore.json";

		StreamReader Reader = new StreamReader (InputPath);
		string JSonString = Reader.ReadToEnd ();
		Debug.Log ("Reading:" + JSonString);
		JsonUtility.FromJsonOverwrite (JSonString, this);
		Reader.Close();

		int HighScore = 0;

		HighScore = highScore;

		return HighScore;


	}


	public void CheckHighScore(string sceneName, int score){

		Debug.Log (sceneName);
		if (string.Equals(sceneName,"SkyJumper", System.StringComparison.CurrentCultureIgnoreCase)) {

			if (score > LoadSkyWalkerScore ()) {
				//have to set up file before usage ex: add 0 to Save at Start and then erase
				SaveSkyWalkerScore (score);
				return;
			} 

		} else if (string.Equals(sceneName,"Collections", System.StringComparison.CurrentCultureIgnoreCase)) {

			if (score > LoadCollectionsScore ()) {
				SaveCollectionsScore (score);
				return;
			} 
		} else if (string.Equals(sceneName,"Finder", System.StringComparison.CurrentCultureIgnoreCase)) {

			if (score > LoadFinderScore ()) {
				SaveFinderScore (score);
				return;
			} 
		}else if (string.Equals(sceneName,"Wack", System.StringComparison.CurrentCultureIgnoreCase)) {

			if (score > LoadWackScore ()) {
				SaveWackScore (score);
				return;
			} 
		} else if (string.Equals(sceneName,"Match", System.StringComparison.CurrentCultureIgnoreCase)) {

			if (score > LoadMatchScore ()) {
				SaveMatchScore (score);
				return;
			} 
		}else if (string.Equals(sceneName,"Shoot", System.StringComparison.CurrentCultureIgnoreCase)) {

			if (score > LoadShootScore ()) {
				SaveShootScore (score);
				return;
			} 
		} else if (string.Equals(sceneName,"Hit", System.StringComparison.CurrentCultureIgnoreCase)) {

			if (score > LoadHitScore ()) {
				SaveHitScore (score);
				return;
			} 
		}else if (string.Equals(sceneName,"Hoops", System.StringComparison.CurrentCultureIgnoreCase)) {

			if (score > LoadHoopScore ()) {
				SaveHoopScore (score);
				return;
			} 
		}
	}
}
