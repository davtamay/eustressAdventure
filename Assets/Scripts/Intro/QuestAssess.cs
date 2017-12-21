using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum Task_Status {NOT_IDENTIFIED, IDENTIFIED, COMPLETED}

public class QuestAssess : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {

	//[SerializeField] string[] playerPrefTaskNames;
	//[SerializeField] string[] questDescriptions;
	[SerializeField] private Transform questSlotsParent;
	//private Dictionary<string,string> taskDictionary;
	[SerializeField]private Text[] qTextSpaces;

//	public static QuestAssess Instance
//	{ get { return instance; } }
//
//	private static QuestAssess instance = null;

	private Transform cam;

	[SerializeField]private float scrollSpeed;

	[SerializeField]private Button[] navigationButtons;
	[SerializeField]private Scrollbar questScrollBar;

	[SerializeField]private List<Task> currentTaskList;

	[Header("References")]
	[SerializeField]private DataManager DATA_MANAGER;

	 void Awake(){

//		if (instance) {
//			DestroyImmediate (gameObject);
//			return;
//		}
//		instance = this; 

		cam = Camera.main.transform;

		Transform tempParent = transform.parent.parent.parent;
		//FIXME This may be causing errors to appear on console NullReferenceExemption(Null);
		if(navigationButtons.Length == 0)
		navigationButtons = tempParent.GetComponentsInChildren <Button>();

		if (questScrollBar == null)
		questScrollBar = tempParent.parent.GetComponentInChildren<Scrollbar> ();

		//taskDictionary = new Dictionary<string, string>();

		//	for(int i = 0; i < playerPrefTaskNames.Length; i++)
		//	taskDictionary.Add (playerPrefTaskNames[i], questDescriptions[i]);// [tn].aquestDescriptions;

		int e = 0;
		if (qTextSpaces != null) {
			qTextSpaces = new Text[questSlotsParent.childCount];

			foreach (Transform child in questSlotsParent) {

				qTextSpaces [e] = child.GetComponent<Text> ();
				e++;

			}
			Debug.Log ("TEXT SPASES: " + qTextSpaces.Length);
		}
	}
		

	int count = 0;


	void OnEnable(){

		currentTaskList = DATA_MANAGER.LoadTaskStatusList();
		OnUpdate ();
	
	}
//	void OnEnable(){
//
//		questScrollBar.value = ;
//
//	}
	public void OnPointerEnter(PointerEventData eventData){
		Debug.LogFormat("PointerData : {0}, ChildrenData : {1}", eventData.pointerCurrentRaycast.gameObject.name,navigationButtons [0].gameObject.name ) ;
		if (eventData.pointerCurrentRaycast.gameObject.name == navigationButtons [0].gameObject.name)
			StartCoroutine (SliderAdjust (true));
		else if(eventData.pointerCurrentRaycast.gameObject.name == navigationButtons [1].gameObject.name)
			StartCoroutine (SliderAdjust (false));

	}

	IEnumerator SliderAdjust(bool isAdding){

		while(true){
			yield return null;
			if(isAdding){
				if (questScrollBar.value != 1) {
					questScrollBar.value += Time.unscaledDeltaTime * scrollSpeed;
					navigationButtons [0].Select ();
				}
			}else{
				if (questScrollBar.value != 0) {
					questScrollBar.value -= Time.unscaledDeltaTime * scrollSpeed;
					navigationButtons [1].Select ();
				}

			}


		}

	}

	public void OnPointerExit(PointerEventData eventData){
		StopAllCoroutines ();
		EventSystem.current.SetSelectedGameObject (null);
	}

//	void Update(){
//
//
//
//		RaycastHit hit;
//
//		if(Physics.Raycast(cam.position, cam.rotation * Vector3.forward, out hit)){
//
//			if (string.Equals (hit.transform.name, navigationButtons [0].transform.name, System.StringComparison.CurrentCultureIgnoreCase)) {
//				if (questScrollBar.value != 1) {
//					questScrollBar.value += Time.unscaledDeltaTime * scrollSpeed;
//					navigationButtons [0].Select ();
//				}
//
//			} else if (string.Equals (hit.transform.name, navigationButtons [1].transform.name, System.StringComparison.CurrentCultureIgnoreCase)) {
//				if (questScrollBar.value != 0) {
//					questScrollBar.value -= Time.unscaledDeltaTime * scrollSpeed;
//					navigationButtons [1].Select ();
//				}
//			}
//
//
//		}
//		EventSystem.current.SetSelectedGameObject (null);
//	
//	}

	public void OnUpdate(){
		 count = 0;
		//for(int i = 0; i < qTextSpaces.Length; i++)
		//	qTextSpaces[i].text = string.Empty;

//		foreach (string pPN in taskDictionary.Keys) {
//
//			if (EvaluatePlayerPref (pPN))
//				count++;
//
//		}	
		foreach(KeyValuePair<Task,Task_Status> t in DATA_MANAGER.playerData.taskDictionary){

			if(Task_Status.IDENTIFIED == t.Value){

				qTextSpaces [count].text = t.Key.taskDescription;
				qTextSpaces [count].color = Color.gray;


			}

			if(Task_Status.COMPLETED == t.Value){
			
				qTextSpaces [count].text = t.Key.taskDescription;
				qTextSpaces [count].color = Color.green;

			}
			count++;
		
		
		}


	}
//	void EvaluateTaskList( )

//	bool EvaluatePlayerPref(string pPName){
//
//		if (!PlayerPrefs.HasKey (pPName)) {
//	
//			return false;
//		}else{
//		
//			if (PlayerPrefs.GetInt (pPName) == 0) {
//			//	Debug.Log ("PlayerPrefAssess: have but not completed " + pPName);
//				qTextSpaces [count].text = taskDictionary [pPName];//questDescriptions [qID];
//				qTextSpaces [count].color = Color.gray;
//
//			} else if (PlayerPrefs.GetInt (pPName) == 1) {
//			//	Debug.Log ("PlayerPrefAssess: completed " + pPName);
//				qTextSpaces [count].text = taskDictionary [pPName];//questDescriptions [qID];
//				qTextSpaces [count].color = Color.green;
//
//
//			}
//			return true;
//		}
//	
//	
//	}
}
