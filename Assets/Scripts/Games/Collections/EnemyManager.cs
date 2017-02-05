using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour {

	public static EnemyManager Instance
	{ get { return instance; } }

	private static EnemyManager instance = null;


	public Vector3 changeSize;

	public GameObject enemyPrefab;
	private GameObject tempEnemy;
	public int enemiesToCreate;

	public List <GameObject> enemies;
	public List <GameObject> activeEnemies;

	void Awake()
	{
		//	DontDestroyOnLoad (gameObject);

		if (instance) {
			//		DestroyImmediate (gameObject);
			return;
		}
		instance = this; 
	}

	void Start(){
		
		enemies = new List<GameObject> ();

		for (int i = 0; i < enemiesToCreate; i++) {
			
			tempEnemy = Instantiate (enemyPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			tempEnemy.transform.parent = transform;
			enemies.Add(tempEnemy);
			enemies[i].SetActive (false);
		
		
		
		}
		//enemies.Add(GameObject.FindWithTag("Enemy"));

	}


	public void InitiateEnemy(Vector3 pos){

		for (int i = 0; i < enemies.Count; i++) {
		
			if (!enemies [i].activeInHierarchy) {

				enemies [i].SetActive (true);
				enemies [i].transform.position = pos;
				activeEnemies.Add(enemies[i]);

			
				break;
			}
		
		}


	}



	public void ReduceSpeed(){
	
		StartCoroutine (ReduceEnemySpeed ());
	
	}
	IEnumerator ReduceEnemySpeed(){



		for (int i = 0; i < enemies.Count; i++) {

			if (enemies [i].activeSelf) {

				Enemy curEnemy = enemies [i].GetComponent<Enemy> ();
				curEnemy.speed = 2.0f;

				}
		}
		yield return new WaitForSeconds (8);

			for (int e = 0; e < enemies.Count; e++) {

			if (enemies [e].activeSelf) {
				Enemy curEnemy = enemies [e].GetComponent<Enemy> ();
				curEnemy.speed = 20.0f;
			}
			}


	}

	public void RunAway(){

		StartCoroutine (EnemyRunAway ());

	}
	IEnumerator EnemyRunAway(){

		for (int i = 0; i < activeEnemies.Count; i++) {

				Enemy curEnemy = activeEnemies [i].GetComponent<Enemy> ();
				curEnemy.currentState = EnemyState.RunAway;
				curEnemy.EnemyRunAway ();
			
		}
		yield return null;
	}
	public void ReduceSize(){

		StartCoroutine (ReduceEnemySize ());

	}
	IEnumerator ReduceEnemySize(){

		Vector3 realSize = Vector3.zero;

		for (int i = 0; i < enemies.Count; i++) {
			if (enemies [i].activeSelf) {
				Transform curEnemy = enemies [i].GetComponent<Transform> ();
				realSize = curEnemy.localScale;

				curEnemy.localScale = changeSize;
			}
		}
		yield return new WaitForSeconds (8);

		for (int e = 0; e < enemies.Count; e++) {
			if (enemies [e].activeSelf) {

				Transform curEnemy = enemies [e].GetComponent<Transform> ();

				curEnemy.localScale = realSize;

			}

		}

	}



}
