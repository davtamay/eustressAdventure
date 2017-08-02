using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour {


	private Dictionary<GameObject,SpriteRenderer> totalGOToSpriteInSceneDict;
	private Dictionary<int, SpriteRenderer> playerIntToSpriteUISlotsDict;

	public Dictionary<string, GameObject> StringToGODict;
	public List<GameObject> playerItemSlotGOList;

	private GameObject UISlots;
	private int curSlot;

	public Color hurtColor;
	public Color addHealthColor;
	public Color healthColor;
	public Color armorColor;
	public int armorTime;
	public bool isArmorOn = false;
	public Text coinText;
	public bool isInvulnerable;
	private Image healthColorIndicator;
	private string curSceneName;

	private static PlayerManager instance;
	public static PlayerManager Instance {
		get{ return instance; }

	}

	void Awake (){
		UISlots = GameObject.FindWithTag ("UISlot");
		//new
		if (instance != null) {
			Debug.LogError ("There is two instances off PlayerManager");
			return;
		} else {
			instance = this;
		}

		healthColor.a = 0.0f; 

		health = 0;
		points = 0;


		if(SceneController.Instance != null)
		curSceneName = SceneController.Instance.GetCurrentSceneName ();







	}
	void Start(){

		if (GameObject.FindWithTag ("UIColor") != null) {
			healthColorIndicator = GameObject.FindWithTag ("UIColor").GetComponent<Image> ();
			healthColorIndicator.color = healthColor;
		}

	//FIXME Games may use Item game tag besided the intro, so check if there are no interuptions/bugs 5/13/17
		if(SceneController.Instance.GetCurrentSceneName() == "Intro")
		if (GameObject.FindWithTag ("Item") != null) {

			totalGOToSpriteInSceneDict = new Dictionary<GameObject,SpriteRenderer> ();
			playerIntToSpriteUISlotsDict = new Dictionary<int,SpriteRenderer> ();
			playerItemSlotGOList = new List<GameObject> ();
			StringToGODict = new Dictionary<string, GameObject>();

			foreach(GameObject GO in GameObject.FindGameObjectsWithTag("Item")){

				SpriteRenderer curSprRend = GO.GetComponentInChildren<SpriteRenderer> (true);//GO.transform.GetChild (0).transform.GetChild (0).GetComponentInChildren<SpriteRenderer> ();
				//Debug.Log(curSprRend.sprite.name);

				totalGOToSpriteInSceneDict.Add (GO, curSprRend);

			
				StringToGODict.Add (GO.name, GO);
				//Debug.Log (GO.name);
				//new 5/26/17
				//StringToGODict[GO.name] =  GO;
				Debug.Log ("GOInEnvironment: " + GO.name);
			}
			//Debug.Log("TOTALGOSPRITEINSCENDIC" + totalGOToSpriteInSceneDict.Values.Count);
			//UISlots = GameObject.FindWithTag ("UISlot");


			int itemCount = 0;

			foreach (Transform curTran in UISlots.transform) {


				playerIntToSpriteUISlotsDict.Add(itemCount, curTran.GetComponentInChildren<SpriteRenderer>(true));

				itemCount++;

				curTran.gameObject.SetActive (false);
				//Debug.Log ("SLOTS:" + curTran.name);
			}
				
			//Needs to initiate before using liststringnames
			int loadItemListCount = DataManager.Instance.LoadItemList ().Count;

		//	foreach (GameObject gO in DataManager.Instance.LoadItemList ())
		//		Debug.Log("LOADED LIST + : " + gO.name);
			
			//Debug.Log("playeINTTOSPRITEUISLOTSDIC" + playerItemSlotGOList.Count + "ITEMCOUNT" + itemCount);
			if (loadItemListCount != 0) {
				playerItemSlotGOList.Clear ();
				for (int i = 0; i < loadItemListCount ; i++) {

					if (!playerItemSlotGOList.Contains (StringToGODict[DataManager.Instance.slotListItemNames[i]])) {
				//	foreach (GameObject gO in DataManager.Instance.LoadItemList ())
				//		Debug.Log("LOADED LIST AFTER + : " + gO.name);
					//Debug.Log("LOADED LIST AFTERTHEFACT : " + DataManager.Instance.LoadItemList ().Count);
					//	Debug.Log("!!!!!!GO BeingSent to be added to SLOTS: " + playerItemSlotGOList[i].name);
						playerItemSlotGOList.Add (StringToGODict[DataManager.Instance.slotListItemNames[i]]);
						AddItemToSlot (StringToGODict[DataManager.Instance.slotListItemNames[i]]);

					
						StringToGODict[DataManager.Instance.slotListItemNames[i]].SetActive (false);

			
					}
				
				}
			}

			//new 5/20/17
		/*	if (DataManager.Instance.LoadPlayerData().slotList.Count != 0) {
				playerSlotGOList.Clear ();
				for (int i = 0; i < DataManager.Instance.LoadPlayerData().slotList.Count; i++) {
					
					if (!playerSlotGOList.Contains (DataManager.Instance.LoadPlayerData().slotList [i])) {
						playerSlotGOList.Add (DataManager.Instance.LoadPlayerData().slotList [i]);
						AddItemToSlot (DataManager.Instance.LoadPlayerData().slotList [i]);

						DataManager.Instance.LoadPlayerData().slotList[i].SetActive (false);

					}

				}
			}*/

			//UISlots.SetActive (false);

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

		playerIntToSpriteUISlotsDict [curSlot].sprite = totalGOToSpriteInSceneDict [GO].sprite;
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




	private int _health;

	public int health{
		get{ return _health;}


		set{ if (value + _health < _health) {
				StartCoroutine (HealthReduceColor (hurtColor));
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
		healthColorIndicator.color = armorColor;
		StartCoroutine (AddArmorTime());


	}

	IEnumerator AddArmorTime(){

		armorTimer = armorTime;

		while (armorTimer > 0) {
		
			armorTimer -= Time.deltaTime;
			yield return null;
		}

		//yield return new WaitForSeconds (armorTime);
		isInvulnerable = false;
		isArmorOn = false;
		healthColorIndicator.color = healthColor;

	}
	IEnumerator HealthReduceColor (Color col){
	
		if (isArmorOn) {
		
			healthColorIndicator.color = armorColor;
			yield break;
		
		}
		

		healthColorIndicator.color = col;

		yield return new WaitForSeconds (1.0f);

		healthColorIndicator.color = healthColor;

	}
	IEnumerator HealthAddColor (Color col){

		if (isArmorOn) {
		
			healthColorIndicator.color = armorColor;
			yield break;

		}

		healthColorIndicator.color = col;

		yield return new WaitForSeconds (1.0f);

		healthColorIndicator.color = healthColor;

	}
	/*public IEnumerator ShowUISlots(){

		UISlots.SetActive(true);
		yield return new WaitForSeconds (4f);
		UISlots.SetActive(false);


	}*/
}
