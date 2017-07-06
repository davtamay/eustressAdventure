using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHtSmaller : MonoBehaviour {

	ParticleSystem pS;
	[SerializeField] private Transform rock;
	[SerializeField]Vector3 rockReduction;

	[SerializeField] string ppName;


	// Use this for initialization
	void Awake () {
		
		if (PlayerPrefs.GetInt (ppName) == 1)
			Destroy (gameObject);
		else {
			pS = GetComponent<ParticleSystem> ();
			pS.Stop ();

		}
		
	}
	public void OnStart(){

		StartCoroutine (MakeRockSmaller());
	}

	IEnumerator MakeRockSmaller(){
		float timer = 0;
		pS.Play ();
		while (true) {
		
			timer += Time.deltaTime;
			if(pS.main.startLifetime.constant <= timer){
				rock.localScale -= rockReduction;

				timer = 0;

				if (Mathf.Sign (rock.localScale.x) == -1 || Mathf.Sign (rock.localScale.y) == -1 || Mathf.Sign (rock.localScale.z) == -1) {
					pS.Stop ();
					StopAllCoroutines ();
					PlayerPrefs.SetInt (ppName, 1);
					PlayerPrefs.Save ();
					QuestAssess.Instance.OnUpdate ();
					Destroy (rock.gameObject);

				}

			}
			yield return null;
		
		}

	
	
	}
	

}
