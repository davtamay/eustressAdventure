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

	[SerializeField]private Transform currentEnemiesUI;


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

		for (int i = 0; i < currentEnemiesUI.childCount; i++)
			currentEnemiesUI.GetChild (i).gameObject.SetActive (false);


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
					if (enemies[i].GetComponent<NavMeshAgent>() == null)
					enemies[i].AddComponent<NavMeshAgent>();

				}
				else
					Debug.LogError("Could not find position on NavMesh!");

				activeEnemies.Add(enemies[i]);

				SetCurrentEnemies();
				//currentEnemiesText.text = activeEnemies.Count.ToString();
			
				break;
			}
		
		}


	}
	/// <summary>
	/// Call to set the current enemies into PlayerUI.
	/// </summary>
	public void SetCurrentEnemies(){

		int CurrentActiveEnemies = activeEnemies.Count;
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

		for (int i = 0; i < enemies.Count; i++) {

			if (enemies [i].activeSelf) {

				Enemy curEnemy = enemies [i].GetComponent<Enemy> ();
				NavMeshAgent curNavAgent = curEnemy.GetComponent<NavMeshAgent> ();
				originalNavSpeed = curNavAgent.speed;
				curNavAgent.speed = 2.0F;
				curEnemy.speed = 2.0f;

				}
		}
		yield return new WaitForSeconds (8);

			for (int e = 0; e < enemies.Count; e++) {

			if (enemies [e].activeSelf) {
				Enemy curEnemy = enemies [e].GetComponent<Enemy> ();
				NavMeshAgent curNavAgent = curEnemy.GetComponent<NavMeshAgent> ();
				curNavAgent.speed = originalNavSpeed;
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
