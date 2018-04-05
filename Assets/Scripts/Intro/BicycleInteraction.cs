using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BicycleInteraction : InteractionBehaviour {


	[SerializeField]private Transform positionToMoveTo;
	[SerializeField]private Transform positionToGetOut;

	[SerializeField]private float velocity = 0.7f;//private float speedOfBike;
	[SerializeField]private float rotSpeed = 5f;
	[SerializeField] private float minMoveAngleFromUp = 89.0f;
	[SerializeField] private float maxMoveAngleFromUp = 180.0f;
	[SerializeField] private CharacterController charController;

	[SerializeField]private GameObject feetInteractionUI_GO;
    [SerializeField] private GameObject getOutBikeUI_GO;

    private bool isOnBike;



	private Vector3 moveDirection;
	private Coroutine Drive;

	private AudioSource bikeAS;

	public void MoveToPosition(){

	
		if (!isOnBike) {
		//	TriggerInfo ();
			player.position = positionToMoveTo.position;
			player.GetComponent<PlayerLookMove> ().enabled = false;

			bikeAS = AudioManager.Instance.GetAudioSourceReferance (AudioManager.AudioReferanceType._DIRECT, "BikeRiding");

			Drive = StartCoroutine (DriveBike ());

            if (feetInteractionUI_GO && getOutBikeUI_GO)
            {
                feetInteractionUI_GO.SetActive(false);
                getOutBikeUI_GO.SetActive(true);
            }

			isOnBike = true;


		} else {
		
			player.position = positionToGetOut.position;
			player.GetComponent<PlayerLookMove> ().enabled = true;
			StopCoroutine (Drive);
			isOnBike = false;

            if (feetInteractionUI_GO && getOutBikeUI_GO)
            {
                feetInteractionUI_GO.SetActive(true);
                getOutBikeUI_GO.SetActive(false);
            }
		}

		
	}






	public float minPitch  = 1.0f;
	public float maxPitch = 2.0f;
	private float pitchModifier;
	public float maxSpeed = 20.0f; 
	IEnumerator DriveBike(){
	
	
		while (true) {
			player.position = positionToMoveTo.position;
			yield return new WaitForEndOfFrame();
			player.position = positionToMoveTo.position;
			moveDirection = thisTransform.TransformDirection (Vector3.forward);//thisTransform.forward.normalized;
			player.position = Vector3.Lerp (player.position, positionToMoveTo.position, Time.deltaTime * 3);


			thisTransform.localRotation = Quaternion.RotateTowards(thisTransform.rotation, Quaternion.Euler (new Vector3(0, Camera.main.transform.eulerAngles.y,0)), Time.deltaTime * 20 * rotSpeed); //Quaternion.AngleAxis (plare.position, Vector3.up);//Quaternion.RotateTowards(thisTransform.rotation, Quaternion.LookRotation(player.position), Time.deltaTime * rotSpeed);
		

			if (minMoveAngleFromUp < CameraAngleFromUp() && CameraAngleFromUp() < maxMoveAngleFromUp) {

				moveDirection.x = 0;
				moveDirection.z = 0;

			} 

		//	moveDirection.x *= velocity ;
		//	moveDirection.z *= velocity ;
			//moveSpeed based On Tilt
			moveDirection *= (90 - CameraAngleFromUp ()) * Time.deltaTime * velocity;
			moveDirection.y += Physics.gravity.y * Time.deltaTime;


				
			if (charController.isGrounded && !AudioManager.Instance.CheckIfAudioPlaying (AudioManager.AudioReferanceType._DIRECT, "BikeRiding")) {

				//avgMag = (pastVelMagnitude + charController.velocity.magnitude) / 2;




				if (charController.velocity.magnitude > 1f) {
					//minMag = Mathf.Min (avgMag, minMag) ;
				
					//maxMag = Mathf.Max (avgMag, maxMag);

					float currentSpeed = charController.velocity.magnitude;
						pitchModifier = maxPitch - minPitch;
					bikeAS.pitch = minPitch + (currentSpeed/maxSpeed)*pitchModifier;
					//bikeAS.pitch =  CameraAngleFromUp ()  ;
					//bikeAS.pitch = (avgMag / pastVelMagnitude) *1;
				//	bikeAS.pitch = (avgMag - minMag) / (maxMag - minMag);//15 - charController.velocity.magnitude / velocity;
					//bikeAS.pitch = Mathf.Clamp (bikeAS.pitch, 0.5f, 1f);
					
					AudioManager.Instance.PlayDirectSound ("BikeRiding");
				}

		
			}
			if (charController.velocity.magnitude < 0.5f)
				AudioManager.Instance.StopAudioPlaying (AudioManager.AudioReferanceType._DIRECT, "BikeRiding");

			//pastVelMagnitude = charController.velocity.magnitude;
				
			charController.Move(moveDirection);
		//	thisTransform.rotation = Quaternion.RotateTowards(thisTransform.rotation, Quaternion.LookRotation(Camera.main.transform.position), Time.deltaTime *20);
			//RaycastHit hit;
			//if (Physics.Raycast (transform.position, -Vector3.up, out hit, LayerMask.NameToLayer("Ground"))) 
			//	thisTransform.up = Vector3.Slerp (thisTransform.up, hit.normal, 5 * Time.deltaTime);
			Debug.Log(charController.velocity.magnitude);


			yield return null;


		}
	
	}

	private float CameraAngleFromUp(){
		return Vector3.Angle (Vector3.up, Camera.main.transform.rotation * Vector3.forward);}
	

}
