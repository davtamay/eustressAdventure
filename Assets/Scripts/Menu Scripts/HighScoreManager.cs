﻿using System.Collections;
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
