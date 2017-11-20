using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PaintManager : MonoBehaviour {

	public int amountOfPaintObjects;
	public GameObject paintPrefab;
	public GameObject[] paintPrefabList;
	[SerializeField]private float paintSize;
	public Queue <Transform> queue = new Queue<Transform>();
	public static Stack <Transform> stack = new Stack<Transform> ();

	public static PaintManager Instance
	{ get { return instance; } }

	private static PaintManager instance = null;


	void Awake()
	{

		if (instance) {
			Debug.Log ("There are two PaintManagers");
			return;
		}
		instance = this; 

	}


	IEnumerator Start () {
	
		paintPrefabList = new GameObject [amountOfPaintObjects];

		for (int i = 0; i < amountOfPaintObjects; i++) {
			paintPrefabList [i] = Instantiate (paintPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			Transform prefabTrans = paintPrefabList [i].GetComponent<Transform> ();
			//just make the prefab into layer 10 then remove this
		//	prefabTrans.gameObject.layer = 5;
			prefabTrans.localRotation = Quaternion.identity;
			prefabTrans.localScale = new Vector3 (paintSize, paintSize, paintSize);
			prefabTrans.parent = transform;
			queue.Enqueue (prefabTrans);

			paintPrefabList [i].SetActive (false);

			yield return null;
		}
	}

	public static Transform PaintObject(Vector3 pos, Color col){
	
		Transform spawnedPrefab = Instance.queue.Dequeue ();

		stack.Push (spawnedPrefab);

		//spawnedPrefab.GetComponent<Renderer> ().material.color = col;
		spawnedPrefab.GetComponent<SpriteRenderer> ().color = col;
		spawnedPrefab.gameObject.SetActive (true);
		spawnedPrefab.position = pos; 


		Instance.queue.Enqueue (spawnedPrefab);

		return spawnedPrefab;

	
	}
	public static Stack <Transform> GetCurrentStack(){
	
		return stack;
	
	}
	

}
