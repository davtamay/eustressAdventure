using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHtSmaller : MonoBehaviour {

	private ParticleSystem pS;
	[SerializeField] private Transform rock;
	[SerializeField]Vector3 rockReduction;

	//[SerializeField] string ppName;


	// Use this for initialization
	void Start () {
		
		pS = GetComponentInChildren<ParticleSystem> ();
		pS.Stop ();
		/*
		if (PlayerPrefs.GetInt (ppName) == 1)
			Destroy (gameObject);
		else {
			pS = GetComponentInChildren<ParticleSystem> ();
			pS.Stop ();

		}
		*/
	}
	//public void OnStart(){

	//	StartCoroutine (MakeRockSmaller());
	//}

	//IEnumerator
	public void MakeRockSmaller(){
		//float timer = 0;
		pS.Play ();
		//while (true) {
		
			//timer += Time.deltaTime;
			//if(pS.main.startLifetime.constant <= timer){
				rock.localScale -= rockReduction;

				//timer = 0;

				if (Mathf.Sign (rock.localScale.x) == -1 || Mathf.Sign (rock.localScale.y) == -1 || Mathf.Sign (rock.localScale.z) == -1) {
					
					//StopAllCoroutines ();
					
					rock.localScale = Vector3.zero;
				//	pS.Stop ();
					Destroy (rock.parent.gameObject,3);

				}

		//	}
		//	yield return null;
		
		//}

	
	
	}
	

}
