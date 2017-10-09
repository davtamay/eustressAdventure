using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour {
	
		public static LocalizationManager Instance;

		private Dictionary<string, string> localizedText;
		
	//	private bool isReady = false;
		public bool isReferencesReady = false;
		public bool isTextReady = false;

		private string missingTextString = "Localized text not found";

		public List<LocalizedText> presentLocalizedTexts;

		// Use this for initialization
		void Awake ()
		{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);

		ObtainTextReferences ();

		}

		public void ObtainTextReferences(){

		/*	presentLocalizedTexts.Clear ();
			var tempLTs	= GameObject.FindGameObjectsWithTag ("Text");

			for (int i = 0; i < tempLTs.Length; i++) {

			presentLocalizedTexts.Add(tempLTs[i].GetComponentInChildren<LocalizedText> (true));
			}*/
			
		var stringDefault = "localization_en.json";
		if (PlayerPrefs.HasKey ("Language"))
			LoadLocalizedText (PlayerPrefs.GetString ("Language"));
		else {
			PlayerPrefs.SetString("Language",stringDefault);
			LoadLocalizedText (PlayerPrefs.GetString ("Language"));
		}
			
		//	LoadLocalizedText(PlayerPrefs.SetString("Language",stringDefault));

			isReferencesReady = true;
	
	
		}

	
	


		public void LoadLocalizedText(string fileName)
		{


			localizedText = new Dictionary<string, string> ();
			string filePath = Path.Combine (Application.streamingAssetsPath, fileName);

			if (File.Exists (filePath)) {
				string dataAsJson = File.ReadAllText (filePath);
				LocalizationData loadedData = JsonUtility.FromJson<LocalizationData> (dataAsJson);

				for (int i = 0; i < loadedData.items.Length; i++) 
				{
					localizedText.Add (loadedData.items [i].key, loadedData.items [i].value);   
				}

				Debug.Log ("Data loaded, dictionary contains: " + localizedText.Count + " entries");
			} else 
			{
			Debug.LogError ("Cannot find file!" + "at: " + fileName);

			}

			//isReady = true;
		isTextReady = true;


		PlayerPrefs.SetString ("Language", fileName);
		PlayerPrefs.Save ();

		StartCoroutine (SetLocalizedTextUpdate ());
		
		}

	IEnumerator SetLocalizedTextUpdate(){
	
		while (!GetIsReady())
			yield return null;
		
		foreach (var lT in presentLocalizedTexts) {

			lT.OnUpdate ();
		}

		//isTextReady = false;
		//isReferencesReady = false;

	}

	public string GetLocalizedValue(string key)
	{
		string result = missingTextString;
		if (localizedText.ContainsKey (key)) 
		{
			result = localizedText [key];
		}

		return result;

	}

		public bool GetIsReady()
		{

		return isReferencesReady && isTextReady;
		//isReady;
		}
		public void ResetReady()
		{
		isReferencesReady = false;
		isTextReady = false;
		}

	
}
