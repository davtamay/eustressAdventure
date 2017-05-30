using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowMove : MonoBehaviour {

	[SerializeField] float moveSpeed = 3;

	void OnTriggerStay(Collider other){
	
		if(other.CompareTag("Player")){


			Vector3 playerRelativePos =(transform.position - other.transform.position).normalized;


			playerRelativePos.y = 0;

			transform.position += playerRelativePos * moveSpeed * Time.deltaTime;


		}
	
	
	}
}
