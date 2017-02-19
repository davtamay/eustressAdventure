using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemyBoss : MonoBehaviour {

	public UnityEvent OnBossDeath;
	[SerializeField]private int health;

	//[SerializeField] private bool isFirstBoss;
	//[SerializeField] private bool isSecondBoss;
	//private Transform childTransform;
	private Renderer thisRenderer;
	private Color origColor;
	private Color hitColor;



	void Awake(){

		thisRenderer = GetComponent<Renderer> ();
	//	origColor = thisRenderer.material.color;
		origColor = thisRenderer.material.GetColor ("_EmissionColor");
	//	childTransform = transform.GetChild (0);


	
	}

	void Update(){

	
	
	}

	void OnTriggerEnter(Collider other){

		if (other.CompareTag ("Bullet")) {
		
			int BulletID = other.transform.GetComponent<BulletControll> ().bulletID;

			if (BulletID == 0) {
				other.gameObject.SetActive (false);
				StartCoroutine (ChangeBossColor ());
				PlayerManager.Instance.points = 1;
				health--;
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


}
