
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour {


	private Dictionary<GameObject,SpriteRenderer> totalGoInSceneDict;
	private Dictionary<int, SpriteRenderer> playerSlotsUIDict;

	public Dictionary<string, GameObject> StringToGODict;
	public List<GameObject> playerSlotGOList;

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

		if (GameObject.FindWithTag ("Item") != null) {

			totalGoInSceneDict = new Dictionary<GameObject,SpriteRenderer> ();
			playerSlotsUIDict = new Dictionary<int,SpriteRenderer> ();
			playerSlotGOList = new List<GameObject> ();
			StringToGODict = new Dictionary<string, GameObject>();

			foreach(GameObject GO in GameObject.FindGameObjectsWithTag("Item")){
				SpriteRenderer curSprRend = GO.transform.GetChild (0).GetComponentInChildren<SpriteRenderer> ();
				totalGoInSceneDict.Add (GO, curSprRend);
			
				StringToGODict.Add (GO.name, GO);

			}

			UISlots = GameObject.FindWithTag ("UISlot");


			int itemCount = 0;

			foreach (Transform curTran in UISlots.transform) {


				if (curTran.GetComponent<SpriteRenderer> () == null)
					continue;


				playerSlotsUIDict.Add(itemCount, curTran.GetComponent<SpriteRenderer>());

				itemCount++;

				curTran.gameObject.SetActive (false);

			}
			if (DataManager.Instance.LoadItemList ().Count != 0) {
				playerSlotGOList.Clear ();
				for (int i = 0; i < DataManager.Instance.LoadItemList ().Count; i++) {
					if (!playerSlotGOList.Contains (DataManager.Instance.LoadItemList () [i])) {
						playerSlotGOList.Add (DataManager.Instance.LoadItemList () [i]);
						AddItemToSlot (DataManager.Instance.LoadItemList () [i]);
				//		Debug.Log ("SlotSPace: " + i + playerSlotGOList[i].name);
					}
				//	Debug.Log (DataManager.Instance.LoadItemList () [i].name);
				}
			}

			//	Debug.Log ("SlotSPace: " + playerSlotGOList.Count);

			UISlots.SetActive (false);

		}


	}


	public void AddItemToSlot(GameObject gO){
		curSlot = 0;

		GameObject GO = StringToGODict [gO.name];



		foreach (SpriteRenderer curSR in playerSlotsUIDict.Values) {

			if (curSR.sprite.GetInstanceID () == totalGoInSceneDict [GO].sprite.GetInstanceID ()) {
				return;
			}

			if (playerSlotsUIDict [curSlot].gameObject.activeSelf) {
				++curSlot;
				continue;}

			
		}

		playerSlotsUIDict [curSlot].sprite = totalGoInSceneDict [GO].sprite;
		GameObject mainObject = totalGoInSceneDict [GO].transform.parent.parent.gameObject;

		if(!playerSlotGOList.Contains(StringToGODict[mainObject.name]))
		playerSlotGOList.Add (totalGoInSceneDict [GO].transform.parent.parent.gameObject);
		
		
		playerSlotsUIDict [curSlot].gameObject.SetActive (true);
		StartCoroutine (ShowUISlots ());


	

	}

	public void RemoveItemFromSlot(GameObject gO){

		GameObject GO = StringToGODict [gO.name];
		
		foreach (SpriteRenderer curSR in playerSlotsUIDict.Values) {

			if (curSR.sprite.GetInstanceID () == totalGoInSceneDict [GO].sprite.GetInstanceID ()) {
				curSR.gameObject.SetActive (false);
				playerSlotGOList.Remove (totalGoInSceneDict [GO].transform.parent.parent.gameObject);
			}
			
		}

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
	public IEnumerator ShowUISlots(){

		UISlots.SetActive(true);
		yield return new WaitForSeconds (4f);
		UISlots.SetActive(false);


	}
}
