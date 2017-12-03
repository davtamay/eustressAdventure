using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	private Dictionary<GameObject,SpriteRenderer> totalGOToSpriteInSceneDict;
	private Dictionary<int, SpriteRenderer> playerIntToSpriteUISlotsDict;

	public Dictionary<string, GameObject> StringToGODict;

	public List<GameObject> playerItemSlotGOList;

	//public GameObject[] allItemGOInScene;

	private GameObject UISlots;
	public List<SpriteRenderer> slotSpots;

	private int curSlot;

	private string curSceneName;

	private GameObject itemContainer;

	[SerializeField] private InventoryList playerInventory;
	[SerializeField] private DataManager DATA_MANAGER;

	public Dictionary<string,GameObject> allItemsInScene;

	private static PlayerInventory instance;
	public static PlayerInventory Instance {
		get{ return instance; }
	}

	public void Awake(){
	

		if (instance != null) {
			Debug.LogError ("There are two instances off PlayerInventory");
			return;
		} else {
			instance = this;
		}

		if(SceneController.Instance != null)
			curSceneName = SceneController.Instance.GetCurrentSceneName ();
	
		allItemsInScene = new Dictionary<string, GameObject> ();
		UISlots = GameObject.FindWithTag ("UISlot");
	}


	void Start()
	{

		foreach(Transform s in UISlots.transform)
			slotSpots.Add (s.GetComponent<SpriteRenderer> ());

		itemContainer = new GameObject ("Item Container");

		foreach(Item item in playerInventory.Items){

			GameObject temp = Instantiate (item.itemGO);
			temp.name = item.itemName;
			temp.SetActive (true);
			temp.transform.parent = itemContainer.transform;
			allItemsInScene.Add (temp.name, temp);


		}
			

		int loadItemListCount = DATA_MANAGER.LoadItemList ().Count;
	
		if (loadItemListCount != 0)
		{
			playerItemSlotGOList.Clear ();

			for (int i = 0; i < loadItemListCount; i++) 
			{

				foreach(var item in playerInventory.Items)
				{

					if (DATA_MANAGER.slotListItemNames [i] == item.itemName ) 
					{
						
						playerItemSlotGOList.Add (allItemsInScene[item.itemName]);

						foreach(var sSR in slotSpots)
							if(sSR.sprite == null)
							{
								sSR.sprite = item.itemSprite;
								allItemsInScene [item.itemName].SetActive (false);
							//	item.itemGO.SetActive (false);
								break;
							}

					}

				}
				
			}

			
		}




//			for (int i = 0; i < loadItemListCount; i++) {
//		
//				if (!playerItemSlotGOList.Contains (StringToGODict [DataManager.Instance.slotListItemNames [i]])) {
//		
//					playerItemSlotGOList.Add (StringToGODict [DataManager.Instance.slotListItemNames [i]]);
//					AddItemToSlot (StringToGODict [DataManager.Instance.slotListItemNames [i]]);
//		
//		
//					StringToGODict [DataManager.Instance.slotListItemNames [i]].SetActive (false);
//		
//		
//				}
//		
//		}





//
//		//FIXME Games may use Item game tag besided the intro, so check if there are no interuptions/bugs 5/13/17
//		//		if(SceneController != null)
//		if(curSceneName == "Intro")
//		if (GameObject.FindWithTag ("Item") != null) {
//
//			allItemGOInScene = GameObject.FindGameObjectsWithTag ("Item");
//
//			totalGOToSpriteInSceneDict = new Dictionary<GameObject,SpriteRenderer> ();
//			playerIntToSpriteUISlotsDict = new Dictionary<int,SpriteRenderer> ();
//			playerItemSlotGOList = new List<GameObject> ();
//			StringToGODict = new Dictionary<string, GameObject>();
//
//			foreach(GameObject GO in allItemGOInScene){
//
//				SpriteRenderer curSprRend = GO.GetComponentInChildren<SpriteRenderer> (true);//GO.transform.GetChild (0).transform.GetChild (0).GetComponentInChildren<SpriteRenderer> ();
//				//Debug.Log(curSprRend.sprite.name);
//
//				totalGOToSpriteInSceneDict.Add (GO, curSprRend);
//
//				StringToGODict[GO.name] =  GO;
//
//				Debug.Log ("GOInEnvironment: " + GO.name);
//			}
//
//
//			int itemCount = 0;
//
//
//			foreach (Transform curTran in UISlots.transform) {
//
//				playerIntToSpriteUISlotsDict.Add(itemCount, curTran.GetComponentInChildren<SpriteRenderer>(true));
//
//				itemCount++;
//
//				//Debug.Log ("SLOTS:" + curTran.name);
//				curTran.gameObject.SetActive (false);
//
//			}
//
//			//Needs to initiate before using liststringnames
//			int loadItemListCount = DataManager.Instance.LoadItemList ().Count;
//
//			foreach (GameObject gO in DataManager.Instance.LoadItemList ())
//				Debug.Log("LOADED LIST + : " + gO.name);
//
//
//			if (loadItemListCount != 0) {
//				playerItemSlotGOList.Clear ();
//				for (int i = 0; i < loadItemListCount ; i++) {
//
//					if (!playerItemSlotGOList.Contains (StringToGODict[DataManager.Instance.slotListItemNames[i]])) {
//
//						playerItemSlotGOList.Add (StringToGODict[DataManager.Instance.slotListItemNames[i]]);
//						AddItemToSlot (StringToGODict[DataManager.Instance.slotListItemNames[i]]);
//
//
//						StringToGODict[DataManager.Instance.slotListItemNames[i]].SetActive (false);
//
//
//					}
//
//				}
//			}
//
//		}


	}

	public void AddItemToSlot(GameObject itemToAdd)
	{

		foreach(var sSR in slotSpots)
		{

			if(sSR.sprite == null)
			{

				foreach(var item in playerInventory.Items)
				{

					if (item.itemName == itemToAdd.name) 
					{
						sSR.sprite = item.itemSprite;
						playerItemSlotGOList.Add (itemToAdd);
						DATA_MANAGER.SaveItemList (playerItemSlotGOList);
						return;
					}

				}

			}

		}


//		int curSlot = 0;
//
//		GameObject GO = StringToGODict [gO.name];
//
//		//Debug.Log("When Adding playerITEMSLOTGOLIST :" + PlayerManager.Instance.playerItemSlotGOList.Count);
//
//		foreach (SpriteRenderer curSR in playerIntToSpriteUISlotsDict.Values) {
//
//			if(curSR.sprite != null)
//			if (curSR.sprite == totalGOToSpriteInSceneDict [GO].sprite) {
//				return;
//			}
//
//			if (playerIntToSpriteUISlotsDict [curSlot].gameObject.activeSelf) {
//				++curSlot;
//				continue;}
//
//
//		}
//
//		if(playerIntToSpriteUISlotsDict.ContainsValue(playerIntToSpriteUISlotsDict [curSlot]))
//			playerIntToSpriteUISlotsDict [curSlot].sprite = totalGOToSpriteInSceneDict [GO].sprite;
//		else
//			playerIntToSpriteUISlotsDict.Add(curSlot, totalGOToSpriteInSceneDict [GO]);
//
//
//		GameObject mainObject = totalGOToSpriteInSceneDict [GO].transform.parent.parent.parent.gameObject;
//
//		if(!playerItemSlotGOList.Contains(StringToGODict[mainObject.name]))
//			playerItemSlotGOList.Add (totalGOToSpriteInSceneDict [GO].transform.parent.parent.parent.gameObject);
//
//
//		playerIntToSpriteUISlotsDict [curSlot].gameObject.SetActive (true);
//		//	StartCoroutine (ShowUISlots ());
//		//	Debug.Log("When Adding playerITEMSLOTGOLIST :" + PlayerManager.Instance.playerItemSlotGOList.Count);
//		DataManager.Instance.SaveItemList (PlayerInventory.Instance.playerItemSlotGOList);
//

	}

	public void RemoveItemFromSlot(GameObject itemToRemove)
	{
		foreach (var sSR in slotSpots) 
		{

			if (sSR.sprite != null) 
			{
				
				foreach (var item in playerInventory.Items) 
				{

					if (item.itemName == itemToRemove.name) 
					{
						sSR.sprite = null;
						sSR.gameObject.SetActive (false);
		
//						GameObject mainObject = totalGOToSpriteInSceneDict [GO].transform.parent.parent.parent.gameObject;
						playerItemSlotGOList.Remove (itemToRemove);
						DATA_MANAGER.SaveItemList (playerItemSlotGOList);
					//	StringToGODict.Remove (Item.name);
//						sSR.sprite = item.itemSprite;
//						playerItemSlotGOList.Add (itemToRemove);
//						DATA_MANAGER.SaveItemList (playerItemSlotGOList);
//						return;
					}

				}
			}
		}
//		GameObject GO = StringToGODict [gO.name];
//
//
//		foreach (SpriteRenderer curSR in playerIntToSpriteUISlotsDict.Values) {
//
//			if (curSR.sprite == totalGOToSpriteInSceneDict [GO].sprite){
//
//				curSR.sprite = null;
//				curSR.gameObject.SetActive (false);
//
//				GameObject mainObject = totalGOToSpriteInSceneDict [GO].transform.parent.parent.parent.gameObject;
//				playerItemSlotGOList.Remove (mainObject);
//				StringToGODict.Remove (mainObject.name);
//
//			}
//
//		}
//		DataManager.Instance.SaveItemList (PlayerInventory.Instance.playerItemSlotGOList);
	}

	public void OnApplicationQuit(){

		if(curSceneName.Contains("Intro"))
			DATA_MANAGER.SaveItemList (playerItemSlotGOList);
	
	}


}
