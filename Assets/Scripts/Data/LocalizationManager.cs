using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName ="Localization_Manager", menuName = "CustomSO/Managers/Localisation_Manager")]
public class LocalizationManager : ScriptableObject {
	
		
		private Dictionary<string, string> localizedText;
		
		[SerializeField]private bool isLocalizedTextReady = false;

		[SerializeField]private string stringDefault = "localization_en.json";

		private string missingTextString = "Localized text not found";

		public List<LocalizedText> registeredLocalizedTexts;

		public static LocalizationManager Instance;

		void OnEnable ()
		{
		if (!Instance) {
			Instance = this;
		} else if (Instance != this) {
			Debug.LogWarning("There are two localizationManagers in scene - deleting late instance.");
			//Destroy (gameObject);
		}

		//DontDestroyOnLoad (gameObject);

		ObtainTextReferences ();

		}

		public void ObtainTextReferences(){
				
			ResetLocalizationTextReady ();

		//IF THERE ISN'T A PLAYERPREF FOR "LANGUAGE" LOAD DEFAULT LOCALIZED FILE,  IF THERE IS THEN USE LANGUAGE THAT WAS LAST SET
		if (PlayerPrefs.HasKey ("Language"))
			LoadLocalizedText (PlayerPrefs.GetString ("Language"));
		else {
			PlayerPrefs.SetString("Language",stringDefault);
			LoadLocalizedText (PlayerPrefs.GetString ("Language"));
		}
	
		}


		public void LoadLocalizedText(string fileName)
		{

			localizedText = new Dictionary<string, string> ();
			string filePath = Path.Combine (Application.streamingAssetsPath, fileName);
//DIRECTORY OF STREAMING ASSETS VARY BETWEEN ANDROID AND USING OTHER PLATFORMS THUS DIFFERENT LOOK UP
#if UNITY_EDITOR
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


#elif UNITY_ANDROID

		filePath = "jar:file://" + Application.dataPath + "!/assets/" + fileName;
		WWW wwwfile = new WWW (filePath);
		while (!wwwfile.isDone) {}

		string AdataAsJson = wwwfile.text ;
		
		LocalizationData AloadedData = JsonUtility.FromJson<LocalizationData> (AdataAsJson);

		for (int i = 0; i < AloadedData.items.Length; i++) 
		{
			Debug.Log(AloadedData.items[i]);
		localizedText.Add (AloadedData.items [i].key, AloadedData.items [i].value);   
		}

			Debug.Log ("Data loaded, dictionary contains: " + localizedText.Count + " entries");
#endif

		isLocalizedTextReady = true;


		PlayerPrefs.SetString ("Language", fileName);
		PlayerPrefs.Save ();

		//UPDATED AS OF 11.29.17 TO SCRIPTABLE OBJECT
		//StartCoroutine (SetLocalizedTextUpdate ());
		SetLocalizedTextUpdate ();
		}

//UPDATED ALL AVAILABLE LOCALIZEDTEXT COMPONENTS REGISTERED TO SET THEIR TEXT COMPONENTS;

	//UPDATED AS OF 11.29.17 TO SCRIPTABLE OBJECT
//	IEnumerator SetLocalizedTextUpdate(){
//	
//		while (!GetIsReady())
//			yield return null;
//		
//		foreach (var lT in registeredLocalizedTexts) {
//
//			lT.OnUpdate ();
//		}
//
//	}
	void SetLocalizedTextUpdate()
	{
			
		foreach (var lT in registeredLocalizedTexts) {

			lT.OnUpdate ();
		}

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
	//CHECK FOR OBTAINING FILE NAME AND LOCALIZEDTEXT BEING SET UP
	public bool GetIsReady()
	{
	
	//return isReferencesReady && isTextReady;
		return isLocalizedTextReady;
	}

	public void ResetLocalizationTextReady()
	{
		
	isLocalizedTextReady = false;

	}

	
}
