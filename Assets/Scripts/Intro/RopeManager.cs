using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeManager : MonoBehaviour {

	private ContactPoint FirstCP;
	private float TimeUntilFall = 3f;
	private static bool isFirstCollision;
	private static int collisionCount;


	void OnCollisionEnter(Collision other){
	
		if (other.collider.CompareTag ("Player") && !isFirstCollision) {
		
			isFirstCollision = true;

			//NEW
			foreach (ContactPoint cP in other.contacts) {
				FirstCP = cP;
				if (FirstCP.point != null)
					break;
			
			}
				
		//	FirstCP = (ContactPoint)other.contacts.GetValue(0);

			other.gameObject.GetComponent<CollectorLookWalk> ().enabled = false;

		
			StartCoroutine (RopeHoldOn (other.transform, FirstCP.thisCollider.transform));


		}
	
	}

	IEnumerator RopeHoldOn(Transform player, Transform cPRope){
		
		Vector3 movingOffset = Vector3.zero;
		float timer = 0;

		while (true) {

			
			player.position = cPRope.transform.position + Vector3.left * 1.5f + (movingOffset);
		
			if (Vector3.Dot (Camera.main.transform.forward, Vector3.up) > 0.7f) 
				movingOffset += (Vector3.up * 1.5f) * Time.deltaTime;

			if (Vector3.Dot (Camera.main.transform.forward, Vector3.down) > 0.7f) {
				movingOffset += (Vector3.down * 3.5f) * Time.deltaTime;
				timer += Time.deltaTime; 


				RaycastHit hit;


				if (timer > TimeUntilFall || Physics.Raycast (player.position, Vector3.down, 5f, 1<<8)) {
					player.GetComponent<CollectorLookWalk> ().enabled = true;
					isFirstCollision = false;
					//collisionCount = 0;
					StopAllCoroutines ();
				}

			} else
				timer = 0;
		
			yield return null;
		}



	}


		



}
