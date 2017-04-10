using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemyBoss : MonoBehaviour {

	public UnityEvent OnBossDeath;
	[SerializeField]private int health;

	[SerializeField] private bool isSecondBoss;
	[SerializeField]private GameObject zombie;
	[SerializeField]private Transform[] zombieRespawnPoints;
	[SerializeField] private Transform player;
	private GameObject zombieParent;

	[SerializeField] private bool isThirdBoss;
	[SerializeField]private GameObject rock;
	[SerializeField]private Transform rockTransform;




	private Transform thisTransform;
	private Renderer thisRenderer;
	private Animator thisAnimator;
	private Vector3 originalPos;
	private Color origColor;




	void Awake(){

		thisTransform = transform;
		originalPos = thisTransform.position;
		thisRenderer = GetComponent<Renderer> ();
		origColor = Color.black;
		player = GameObject.FindWithTag ("Player").transform;
	

		if (isSecondBoss) {
			thisAnimator = GetComponent <Animator> ();

			zombieParent = new GameObject ("Zombie Manager");
			zombieParent.transform.parent = transform.parent;


			StartCoroutine (CloudBossActions ());

		}

		if (isThirdBoss) {
		
		
		
		}
	
	}

	IEnumerator CloudBossActions(){
	

		//yield return new WaitForSeconds (3f);

		while (true) {


			yield return new WaitForSeconds (4f);

		//	StartCoroutine(	MoveCloudToNewPos ());

			GameObject zomGO = Instantiate (zombie, zombieRespawnPoints [Random.Range (0, zombieRespawnPoints.Length)].position,Quaternion.LookRotation (player.forward));//player.forward));//Quaternion.identity );
			zomGO.transform.parent = zombieParent.transform;
		
		
		}
	
	
	
	}
	public void MoveCloudToNewPos(){
		Vector3 newRandomLoc = originalPos +  Random.insideUnitSphere * 30;
		thisTransform.position = newRandomLoc;
	}

	void OnTriggerEnter(Collider other){

		if (other.CompareTag ("Bullet")) {
		
			int BulletID = other.transform.GetComponent<BulletControll> ().bulletID;

			if (BulletID == 0) {
				other.gameObject.SetActive (false);
				StartCoroutine (ChangeBossColor ());
				PlayerManager.Instance.points = 10;
				health--;

				if (isSecondBoss)
					thisAnimator.SetTrigger ("isHit");
				if (health == 0) {
					OnBossDeath.Invoke();
					Destroy (transform.parent.gameObject);

				}

			}
		
		
		
		}

	}
	IEnumerator ChangeBossColor(){

		//thisRenderer.material.color = Color.red;
		thisRenderer.material.SetColor ("_EmissionColor", Color.red);

		yield return new WaitForSeconds (2);

		//thisRenderer.material.color = origColor;
		thisRenderer.material.SetColor ("_EmissionColor", origColor);
	
	}

	public void SetRockToThrow(){

		Instantiate (rock, rockTransform.position, Quaternion.identity);

	}


}
