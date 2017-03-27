using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour {

	public static EnemyManager Instance
	{ get { return instance; } }

	private static EnemyManager instance = null;


	public Vector3 changeSize;
	private Transform thisTransform;
	public GameObject enemyPrefab;
	private GameObject tempEnemy;
	public int enemiesToCreate;

	private Transform player;

	public Transform wayPointParent;
	public Vector3[] wayPoints;

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


		thisTransform = transform;
	
		player = GameObject.FindWithTag ("Player").transform;
	}

	void Start(){


		enemies = new List<GameObject> ();

		for (int i = 0; i < enemiesToCreate; i++) {
			
			tempEnemy = Instantiate (enemyPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			tempEnemy.transform.parent = transform;
			enemies.Add(tempEnemy);
			enemies[i].SetActive (false);
		
		
		
		}


		wayPoints = new Vector3[wayPointParent.childCount];

		for(int i = 0; i < wayPointParent.childCount; i++){
		
			wayPoints[i] = wayPointParent.GetChild(i).GetComponent<Transform> ().position;
				

		}
		//enemies.Add(GameObject.FindWithTag("Enemy"));

	}


	public void InitiateEnemy(Vector3 pos){

		for (int i = 0; i < enemies.Count; i++) {
		
			if (!enemies [i].activeInHierarchy) {

				enemies [i].SetActive (true);
				//enemies [i].transform.position = pos;
				Vector3 sourcePostion = pos;//thisTransform.position;//The position you want to place your agent
				NavMeshHit closestHit;

				if(NavMesh.SamplePosition(  sourcePostion, out closestHit, 500, 1 ) ){
					transform.position = closestHit.position;
					enemies[i].AddComponent<NavMeshAgent>();

				}
				else
					Debug.LogError("Could not find position on NavMesh!");






				activeEnemies.Add(enemies[i]);

			
				break;
			}
		
		}


	}


	public Vector3 furthestWayPointFromPlayer(Transform enemy){
	

			Vector3 furtherWayPoint = wayPoints[0];
			float furthestDistance = 0;

		Vector3 closestWayPoint = wayPoints[0];
		//float closestDistance = 0;

		foreach (Vector3 wp in wayPoints) {

			float playerDistance = Vector3.Distance (player.position, wp);
			float enemyDistance = Vector3.Distance (enemy.position, wp);


			if (playerDistance > enemyDistance){

				if (playerDistance > furthestDistance) {

					furthestDistance = playerDistance;
				
					furtherWayPoint = wp;
					//closestWayPoint = wayPoints[0];
				
				}
			}

		}
			
		return furtherWayPoint;
	
	}
	public Vector3 closestWayPointFromPlayer(Transform enemy){



		Vector3 closestWayPoint = wayPoints[0];
		float closestDistance = Mathf.Infinity;


		foreach (Vector3 wp in wayPoints) {

			if (Vector3.Distance (player.position, wp) >= Vector3.Distance (enemy.position, wp)){

				if (Vector3.Distance (enemy.position, wp) <= closestDistance) {

					closestDistance = Vector3.Distance (enemy.position, wp);

					closestWayPoint = wp;
					//closestWayPoint = wayPoints[0];

				}
			}

		}

		return closestWayPoint;





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
