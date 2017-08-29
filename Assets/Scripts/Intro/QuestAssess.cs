using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestAssess : MonoBehaviour {

	[SerializeField] string[] playerPrefTaskNames;
	[SerializeField] string[] questDescriptions;

	private Dictionary<string,string> taskDictionary;
	private Text[] qTextSpaces;

	public static QuestAssess Instance
	{ get { return instance; } }

	private static QuestAssess instance = null;

	private Transform cam;

	[SerializeField]private float scrollSpeed;

	private Button[] navigationButtons;
	private Scrollbar questScrollBar;

	 void Awake(){

		if (instance) {
			DestroyImmediate (gameObject);
			return;
		}
		instance = this; 

		cam = Camera.main.transform;

		Transform tempParent = transform.parent.parent.parent;
		//FIXME This may be causing errors to appear on console NullReferenceExemption(Null);
		navigationButtons = tempParent.GetComponentsInChildren <Button>();
		questScrollBar = tempParent.parent.GetComponentInChildren<Scrollbar> ();

		taskDictionary = new Dictionary<string, string>();

			for(int i = 0; i < playerPrefTaskNames.Length; i++)
			taskDictionary.Add (playerPrefTaskNames[i], questDescriptions[i]);// [tn].aquestDescriptions;

		int e = 0;
		qTextSpaces = new Text[transform.childCount];

		foreach (Transform child in transform) {

			qTextSpaces [e] = child.GetComponent<Text>();
			e++;

		}
		Debug.Log("TEXT SPASES: " + qTextSpaces.Length);
	}
		

	int count = 0;


	void Start(){

		OnUpdate ();
	
	}

	void Update(){



		RaycastHit hit;

		if(Physics.Raycast(cam.position, cam.rotation * Vector3.forward, out hit)){

			if (string.Equals (hit.transform.name, navigationButtons [0].transform.name, System.StringComparison.CurrentCultureIgnoreCase)) {
				if (questScrollBar.value != 1) {
					questScrollBar.value += Time.unscaledDeltaTime * scrollSpeed;
					navigationButtons [0].Select ();
				}

			} else if (string.Equals (hit.transform.name, navigationButtons [1].transform.name, System.StringComparison.CurrentCultureIgnoreCase)) {
				if (questScrollBar.value != 0) {
					questScrollBar.value -= Time.unscaledDeltaTime * scrollSpeed;
					navigationButtons [1].Select ();
				}
			}


		}
		EventSystem.current.SetSelectedGameObject (null);
	
	}

	public void OnUpdate(){
		 count = 0;
		//for(int i = 0; i < qTextSpaces.Length; i++)
		//	qTextSpaces[i].text = string.Empty;

		foreach (string pPN in taskDictionary.Keys) {

			if (EvaluatePlayerPref (pPN))
				count++;

		}	

	}

	bool EvaluatePlayerPref(string pPName){

		if (!PlayerPrefs.HasKey (pPName)) {
		//	Debug.Log ("PlayerPrefAssess: there is no " + pPName);
		//	qTextSpaces [count].text = string.Empty;
			return false;
		}else{
		
			if (PlayerPrefs.GetInt (pPName) == 0) {
			//	Debug.Log ("PlayerPrefAssess: have but not completed " + pPName);
				qTextSpaces [count].text = taskDictionary [pPName];//questDescriptions [qID];
				qTextSpaces [count].color = Color.gray;

			} else if (PlayerPrefs.GetInt (pPName) == 1) {
			//	Debug.Log ("PlayerPrefAssess: completed " + pPName);
				qTextSpaces [count].text = taskDictionary [pPName];//questDescriptions [qID];
				qTextSpaces [count].color = Color.green;


			}
			return true;
		}
	
	
	}
}
