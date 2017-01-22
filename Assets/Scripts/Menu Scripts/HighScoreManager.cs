using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScoreManager : MonoBehaviour {

	public static HighScoreManager Instance
	{ get { return instance; } }

	private static HighScoreManager instance = null;

	public int highScore = 0;




	void Awake(){

		if (instance) {
			Debug.Log ("There are two HighScoreManagers");
			return;
		}
		instance = this; 



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
		}
	}
}
