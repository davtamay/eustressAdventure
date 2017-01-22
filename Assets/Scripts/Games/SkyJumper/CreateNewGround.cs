using UnityEngine;
using System.Collections;
public class CreateNewGround : MonoBehaviour {


	public GameObject ground;
	public GameObject eraseTrigger;
	public float eraseTrigOffset = 5f;

	public Transform cameraPos;
	public Vector3 charOffsetPos = new Vector3 (0,1,0);
	//private Transform eTriggerTran;

	private Vector3 eraseZOffset;
	private GameObject tempGround;
	private GameObject tempErase;


	void Awake(){
	

		ground = GameObject.FindWithTag ("Ground");

		eraseTrigger = this.gameObject;
	//	eTriggerTran = GameObject.FindWithTag ("GroundInit").transform;
	//	eraseTrigger.transform.position = new Vector3 (ground.transform.position.x, ground.transform.position.y,
	//		ground.transform.position.z - eraseTrigOffset);
		
	


	}
		


	void OnTriggerEnter(Collider other){






		AddNewGround ();

		Destroy (other.gameObject);
	


	
	}

	void AddNewGround(){



			cameraPos = Camera.main.transform;

			tempGround = (GameObject)Instantiate (ground, cameraPos.position - charOffsetPos, Quaternion.identity);



			//eTriggerTran = GameObject.FindWithTag ("GroundInit").transform;

			
			
			eraseZOffset = new Vector3 (ground.transform.position.x, ground.transform.position.y,
			tempGround.transform.position.z - eraseTrigOffset);


			
		

			tempErase = (GameObject)Instantiate (eraseTrigger, eraseZOffset, Quaternion.identity);

			CreateNewGround tempEraseScript = tempErase.GetComponent<CreateNewGround> ();
			tempEraseScript.ground = tempGround;
			tempEraseScript.eraseTrigger = tempErase;

		Destroy (this.gameObject);
	}

	
}


