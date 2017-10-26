using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour {


	private Dictionary<GameObject,SpriteRenderer> totalGOToSpriteInSceneDict;
	private Dictionary<int, SpriteRenderer> playerIntToSpriteUISlotsDict;

	public Dictionary<string, GameObject> StringToGODict;
	public List<GameObject> playerItemSlotGOList;

	public GameObject[] allItemGOInScene;

	private Transform thisTransform;
	private GameObject UISlots;
	private int curSlot;

	public Color hurtColor;
	public Color addHealthColor;
	public Color healthColor;
	[SerializeField] public GameObject armorGO;
	//public Color armorColor;
	public int armorTime;
	public bool isArmorOn = false;
	public Text coinText;
	public bool isInvulnerable;
	private Image healthColorIndicator;
	private string curSceneName;


	[SerializeField]private bool isShakeWhenHit = false;
	[SerializeField]private float shakeTime = 2.0f;
	[SerializeField]private float shakeAmount = 3.0f;
	[SerializeField]private float shakeSpeed = 2.0f;


	private static PlayerManager instance;
	public static PlayerManager Instance {
		get{ return instance; }

	}

	void Awake (){



		//new
		if (instance != null) {
			Debug.LogError ("There is two instances off PlayerManager");
			return;
		} else {
			instance = this;
		}

		thisTransform = transform;

		UISlots = GameObject.FindWithTag ("UISlot");

		allItemGOInScene = GameObject.FindGameObjectsWithTag ("Item");




		healthColor.a = 0.0f; 

		health = 0;
		points = 0;


		if(SceneController.Instance != null)
		curSceneName = SceneController.Instance.GetCurrentSceneName ();







	}
	void Start(){

		if (GameObject.FindWithTag ("UIColor")) {

				healthColorIndicator = GameObject.FindWithTag ("UIColor").GetComponent<Image> ();
				healthColorIndicator.color = healthColor;
				armorGO.SetActive (false);
		}

	//FIXME Games may use Item game tag besided the intro, so check if there are no interuptions/bugs 5/13/17
//		if(SceneController != null)
		if(curSceneName == "Intro")
		if (GameObject.FindWithTag ("Item") != null) {

			totalGOToSpriteInSceneDict = new Dictionary<GameObject,SpriteRenderer> ();
			playerIntToSpriteUISlotsDict = new Dictionary<int,SpriteRenderer> ();
			playerItemSlotGOList = new List<GameObject> ();
			StringToGODict = new Dictionary<string, GameObject>();

			foreach(GameObject GO in allItemGOInScene){

				SpriteRenderer curSprRend = GO.GetComponentInChildren<SpriteRenderer> (true);//GO.transform.GetChild (0).transform.GetChild (0).GetComponentInChildren<SpriteRenderer> ();
				//Debug.Log(curSprRend.sprite.name);

				totalGOToSpriteInSceneDict.Add (GO, curSprRend);

				StringToGODict[GO.name] =  GO;
			
				//Debug.Log ("GOInEnvironment: " + GO.name);
			}


			int itemCount = 0;


			foreach (Transform curTran in UISlots.transform) {


				playerIntToSpriteUISlotsDict.Add(itemCount, curTran.GetComponentInChildren<SpriteRenderer>(true));

				itemCount++;

				Debug.Log ("SLOTS:" + curTran.name);
				curTran.gameObject.SetActive (false);
				
			}
				
			//Needs to initiate before using liststringnames
			int loadItemListCount = DataManager.Instance.LoadItemList ().Count;

			foreach (GameObject gO in DataManager.Instance.LoadItemList ())
				Debug.Log("LOADED LIST + : " + gO.name);
			

			if (loadItemListCount != 0) {
				playerItemSlotGOList.Clear ();
				for (int i = 0; i < loadItemListCount ; i++) {

					if (!playerItemSlotGOList.Contains (StringToGODict[DataManager.Instance.slotListItemNames[i]])) {
				
						playerItemSlotGOList.Add (StringToGODict[DataManager.Instance.slotListItemNames[i]]);
						AddItemToSlot (StringToGODict[DataManager.Instance.slotListItemNames[i]]);

					
						StringToGODict[DataManager.Instance.slotListItemNames[i]].SetActive (false);

			
					}
				
				}
			}

		}


	}


	public void AddItemToSlot(GameObject gO){
		int curSlot = 0;

		GameObject GO = StringToGODict [gO.name];

		//Debug.Log("When Adding playerITEMSLOTGOLIST :" + PlayerManager.Instance.playerItemSlotGOList.Count);

		foreach (SpriteRenderer curSR in playerIntToSpriteUISlotsDict.Values) {

			if(curSR.sprite != null)
			if (curSR.sprite == totalGOToSpriteInSceneDict [GO].sprite) {
				return;
			}

			if (playerIntToSpriteUISlotsDict [curSlot].gameObject.activeSelf) {
				++curSlot;
				continue;}

			
		}

		if(playerIntToSpriteUISlotsDict.ContainsValue(playerIntToSpriteUISlotsDict [curSlot]))
		playerIntToSpriteUISlotsDict [curSlot].sprite = totalGOToSpriteInSceneDict [GO].sprite;
		else
		playerIntToSpriteUISlotsDict.Add(curSlot, totalGOToSpriteInSceneDict [GO]);


		GameObject mainObject = totalGOToSpriteInSceneDict [GO].transform.parent.parent.parent.gameObject;
	
		if(!playerItemSlotGOList.Contains(StringToGODict[mainObject.name]))
		playerItemSlotGOList.Add (totalGOToSpriteInSceneDict [GO].transform.parent.parent.parent.gameObject);
		
		
		playerIntToSpriteUISlotsDict [curSlot].gameObject.SetActive (true);
	//	StartCoroutine (ShowUISlots ());
	//	Debug.Log("When Adding playerITEMSLOTGOLIST :" + PlayerManager.Instance.playerItemSlotGOList.Count);
		DataManager.Instance.SaveItemList (PlayerManager.Instance.playerItemSlotGOList);
	

	}

	public void RemoveItemFromSlot(GameObject gO){

		GameObject GO = StringToGODict [gO.name];
	

		foreach (SpriteRenderer curSR in playerIntToSpriteUISlotsDict.Values) {
			
			if (curSR.sprite == totalGOToSpriteInSceneDict [GO].sprite){
				
				curSR.sprite = null;
				curSR.gameObject.SetActive (false);

				GameObject mainObject = totalGOToSpriteInSceneDict [GO].transform.parent.parent.parent.gameObject;
				playerItemSlotGOList.Remove (mainObject);
				StringToGODict.Remove (mainObject.name);

			}
			
		}
		DataManager.Instance.SaveItemList (PlayerManager.Instance.playerItemSlotGOList);
	}




	private int _health = 0;

	public int health{
		get{ return _health;}


		set{ if (value + _health < _health) {
				StartCoroutine (HealthReduceColor (hurtColor));

				if(isShakeWhenHit)
				StartCoroutine (Shake ());
				
				healthColor.a += 0.1f;
				if (healthColor.a >= .91f) {
					DataManager.Instance.CheckHighScore (curSceneName, _points);
					GameController.Instance.isGameOver = true;
					GameController.Instance.Paused = true;

				};
			}
				if (value + _health > _health && !(healthColor.a == 0.0f)){
					StartCoroutine(HealthAddColor (addHealthColor));
					healthColor.a -= 0.1f;
				}
				;}
	}

	private int _points;
	public int points{
		get{return _points;}

		//5/13/17

		set{
			if (coinText != null) {
				coinText.text = ":";
				_points += value;
				coinText.text += _points.ToString ();
			}
		}
	}
		




	public static float armorTimer = 0f;
	public void AddArmor (){

		StopAllCoroutines ();
		isArmorOn = true;
		isInvulnerable = true;
		armorGO.SetActive (true);
	//	healthColorIndicator.color = armorColor;
		StartCoroutine (AddArmorTime());


	}

	IEnumerator AddArmorTime(){

		armorTimer = armorTime;

		while (armorTimer > 0) {
		
			armorTimer -= Time.deltaTime;
			yield return null;
		}

		armorGO.SetActive (false);

		isInvulnerable = false;
		isArmorOn = false;
		healthColorIndicator.color = healthColor;

	}
	IEnumerator HealthReduceColor (Color col){

		

		healthColorIndicator.color = col;

		yield return new WaitForSeconds (1.0f);

		healthColorIndicator.color = healthColor;

	}
	IEnumerator HealthAddColor (Color col){


		healthColorIndicator.color = col;

		yield return new WaitForSeconds (1.0f);

		healthColorIndicator.color = healthColor;

	}

	IEnumerator Shake(){
	
		Vector3 origPosition = thisTransform.localPosition;
		float ElapsedTime = 0f;

		while (ElapsedTime < shakeTime) {
		
			Vector3 RandomPoint = origPosition + Random.insideUnitSphere * shakeAmount;

			thisTransform.localPosition = Vector3.Lerp (thisTransform.localPosition, RandomPoint, Time.deltaTime * shakeSpeed);
		
			yield return null;
			ElapsedTime += Time.deltaTime;
		}

		thisTransform.localPosition = origPosition;
	
	}
	/*public IEnumerator ShowUISlots(){

		UISlots.SetActive(true);
		yield return new WaitForSeconds (4f);
		UISlots.SetActive(false);


	}*/
}
