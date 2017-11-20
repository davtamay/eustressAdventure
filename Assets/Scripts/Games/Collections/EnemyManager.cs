using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour {

	public static EnemyManager Instance
	{ get { return instance; } }

	private static EnemyManager instance = null;

	public Dictionary<Enemy,NavMeshAgent> registeredEnemies;
	public Vector3 changeSize;
	//private Transform thisTransform;
	public GameObject enemyPrefab;
	private GameObject tempEnemy;
	public int enemiesToCreate;

	private Transform player;

	public Transform wayPointParent;
	public Vector3[] wayPoints;

	public List <GameObject> enemies;
	//public List <GameObject> activeEnemies;

	[SerializeField]private Transform currentEnemiesUI;


	void Awake()
	{
		//	DontDestroyOnLoad (gameObject);

		if (instance) {
			//		DestroyImmediate (gameObject);
			return;
		}
		instance = this; 

		registeredEnemies = new Dictionary<Enemy, NavMeshAgent> ();
	//	thisTransform = transform;
	

	}

	void Start(){
		player = GameObject.FindWithTag ("Player").transform;

		for (int i = 0; i < currentEnemiesUI.childCount; i++)
			currentEnemiesUI.GetChild (i).gameObject.SetActive (false);

		/*
		enemies = new List<GameObject> ();

		for (int i = 0; i < enemiesToCreate; i++) {
			
			tempEnemy = Instantiate (enemyPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			tempEnemy.transform.parent = transform;
			enemies.Add(tempEnemy);
			enemies[i].SetActive (false);
		
		
		
		}
*/

		wayPoints = new Vector3[wayPointParent.childCount];

		for(int i = 0; i < wayPointParent.childCount; i++){
		
			wayPoints[i] = wayPointParent.GetChild(i).GetComponent<Transform> ().position;
				

		}

	}

public void InitiateEnemy()
{
		SetCurrentEnemies ();

		foreach (Enemy e in registeredEnemies.Keys) {
			if (!e.gameObject.activeInHierarchy)
				return;

			NavMeshHit closestHit;

			if (NavMesh.SamplePosition (e.transform.position, out closestHit, 20f, NavMesh.AllAreas)) {
				transform.position = closestHit.position;
			} else
				Debug.LogError ("Could not find position on NavMesh!");


	}

}
				

	/// <summary>
	/// Call to set the current enemies into PlayerUI.
	/// </summary>
	public void SetCurrentEnemies(){
		int ActiveCount = 0;
		foreach (var e in registeredEnemies.Keys) {
			if (e.gameObject.activeInHierarchy)
				++ActiveCount;
		}


		int CurrentActiveEnemies = ActiveCount;//activeEnemies.Count;
		int EnemyImagesCount = currentEnemiesUI.childCount;

		if (CurrentActiveEnemies == 0) {
			currentEnemiesUI.GetChild (0).gameObject.SetActive (false);
			return;
		}
		

		for (int i = CurrentActiveEnemies - 1; i < EnemyImagesCount; i++)
			currentEnemiesUI.GetChild (i).gameObject.SetActive (false);

		for (int i = 0; i < CurrentActiveEnemies; i++) {

	
			if (currentEnemiesUI.GetChild (i).gameObject.activeInHierarchy)
				continue;

				currentEnemiesUI.GetChild (i).gameObject.SetActive (true);
		}
	
	
	}
	/// <summary>
	/// Get Furthest Waypoint position from player
	/// </summary>
	/// <returns>Furthest waypoint position (Vector3)</returns>
	/// <param name="enemy">Enemy Transform</param>
	/// <param name="radiusPermitance">Allow to include waypoints at a certain distance</param>
	public Vector3 GetFurthestWayPointFromPlayer(Transform enemy, float radiusPermitance = 40f){
	

			Vector3 furtherWayPoint = wayPoints[0];
			float furthestDistance = 0;

		//Vector3 closestWayPoint = wayPoints[0];
		//float closestDistance = 0;

		foreach (Vector3 wp in wayPoints) {


			float playerDistance = Vector3.Distance (player.position, wp);
			float enemyDistance = Vector3.Distance (enemy.position, wp);

			if(Vector3.Distance(enemy.position, wp) > radiusPermitance)
				continue;

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
	public Vector3 ClosestWayPointFromPlayer(Transform enemy, float radiusPermitance){



		Vector3 closestWayPoint = wayPoints[0];
		float closestDistance = Mathf.Infinity;


		foreach (Vector3 wp in wayPoints) {

			float playerDistance = Vector3.Distance (player.position, wp);
			float enemyDistance = Vector3.Distance (enemy.position, wp);

			if(Vector3.Distance(enemy.position, wp) > radiusPermitance)
				continue;

			if (playerDistance >= enemyDistance){

				if (enemyDistance <= closestDistance) {

					closestDistance = enemyDistance;

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

		float originalNavSpeed = 0f;

		foreach(var e in registeredEnemies){
			originalNavSpeed = e.Value.speed;
			e.Value.speed = 2.0f;
			e.Key.speed = 2.0f;

		}
		yield return new WaitForSeconds (8);

		foreach(var e in registeredEnemies){
			e.Value.speed = originalNavSpeed;
			e.Key.speed = 20.0f;

		}

	}

	public void RunAway(){

		StartCoroutine (EnemyRunAway ());

	}
	IEnumerator EnemyRunAway(){
		foreach (var e in registeredEnemies.Keys) {
			e.currentState = EnemyState.RunAway;
			e.EnemyRunAway ();
		
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
