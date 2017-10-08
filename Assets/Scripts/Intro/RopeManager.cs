using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeManager : MonoBehaviour {

//	private ContactPoint FirstCP;
	private Transform thisRopeTransform;
//	private float TimeUntilFall = 3f;
	private static bool isFirstCollision;
	private static int collisionCount;

	//private Rigidbody thisRigidbody;


	void OnCollisionEnter(Collision other){
		
		Debug.Log (other.transform.name);

		if (other.collider.CompareTag ("Player") && !isFirstCollision) {

			isFirstCollision = true;
			Debug.Log ("InsideCollisonEnter");
		
			thisRopeTransform = this.transform;
			other.transform.position = Vector3.Lerp (other.transform.position, thisRopeTransform.position, Time.deltaTime * 1.5f);

			StartCoroutine (RopeHoldOn (other.transform, thisRopeTransform));//FirstCP.thisCollider.transform));


		}
	
	}


	IEnumerator RopeHoldOn(Transform player, Transform ropeSegmentTrans){
		var PlayerLook = player.gameObject.GetComponent<PlayerLookMove> ();
		PlayerLook.SetFeetDisplay (false);
		PlayerLook.enabled = false;
		player.gameObject.GetComponent<CharacterController> ().enabled = false;
		player.gameObject.GetComponent<CapsuleCollider> ().enabled = false;


		Vector3 movingOffset = Vector3.zero;

		float swingingVelocity = 7f;

		Vector3 dir = Camera.main.transform.forward.normalized;
		dir.y = 0;

		Vector3 origRopePos = new Vector3 (ropeSegmentTrans.parent.position.x,  player.position.y , ropeSegmentTrans.parent.position.z);


	
		float parentYPos = transform.parent.position.y;

		while (Vector3.Distance (player.position, ropeSegmentTrans.position) > 1.5f) {
			player.position = Vector3.Lerp (player.position, ropeSegmentTrans.position, Time.deltaTime * 1.5f);
			ropeSegmentTrans.position = Vector3.Lerp (ropeSegmentTrans.position, player.position, Time.deltaTime * 1.5f);
			yield return null;
			continue;
		}




		while (true) {
			if((Vector3.Distance (player.position, ropeSegmentTrans.position) > 3f))
				ropeSegmentTrans.position = Vector3.Lerp (ropeSegmentTrans.position, player.position  , Time.deltaTime * 8f);

			yield return new WaitForEndOfFrame();

				player.position = Vector3.Lerp (player.position, origRopePos  + Vector3.left * 1f + (movingOffset), Time.deltaTime * 7f);
				
		
			if (player.position.y > parentYPos)
				player.position -= Vector3.up * 0.2f;//Vector3.up * parentYPos;


			movingOffset = Vector3.zero;


			if (swingingVelocity > 0.01f) {

			//	thisRigidbody.AddRelativeForce(dir * (swingingVelocity), ForceMode.Acceleration);

				swingingVelocity -= Time.deltaTime * 0.4f;


				movingOffset = dir * Mathf.Sin(Time.time * 1.5f) * swingingVelocity;
			
				
				if (Vector3.Dot (Camera.main.transform.forward, Vector3.down) > 0.7f) {
					PlayerLook.SetFeetDisplay (true);
					PlayerLook.enabled = true;
					player.GetComponent<CharacterController> ().enabled = true;
					player.gameObject.GetComponent<CapsuleCollider> ().enabled = true;
			
					isFirstCollision = false;
					StopAllCoroutines ();
				}

				if (swingingVelocity < 0.1f) {
					
					//thisRigidbody.Sleep ();
					player.position = Vector3.Lerp (player.position, origRopePos + Vector3.left * 1f , Time.deltaTime * 1f);
					swingingVelocity = 0f;
				}


				continue;
			}

	


			//10f reflects maxUpPos
			if (Vector3.Dot (Camera.main.transform.forward, Vector3.up) > 0.7f)// && player.transform.position.y < 10f)//transform.parent.position.y) 
				movingOffset.y += (1f) * Time.deltaTime;

			if (Vector3.Dot (Camera.main.transform.forward, Vector3.down) > 0.7f) {
				player.GetComponent<PlayerLookMove> ().enabled = true;

				player.GetComponent<CharacterController> ().enabled = true;
				player.gameObject.GetComponent<CapsuleCollider> ().enabled = true;


				isFirstCollision = false;
				StopAllCoroutines ();


			}

			yield return null;
		}



	}


		



}
