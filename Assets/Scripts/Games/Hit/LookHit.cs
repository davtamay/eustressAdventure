using UnityEngine;
using System.Collections;

public class LookHit : MonoBehaviour {


	[SerializeField]private bool isGame = false;

	//Variables That Could Change Difficulty
	[SerializeField] private float amountOfObjectDrift;
	[SerializeField] private float amountOfObjectLift;
	//FIXME - connect gravity modifier to system?
	[SerializeField] private Vector3 gravityModifier;

	[SerializeField]private float timerUntilExplode;

	public float forceStrength;

	private float ExplodeTime;
	public LayerMask layerMaskRB;

	private Camera cam;

	private AudioSource ballHitAS;


	void Start(){
	
		cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

		ballHitAS = AudioManager.Instance.GetAudioSourceReferance (AudioManager.AudioReferanceType._AMBIENT, "BallHit");

		if (isGame)
			timerUntilExplode = 0.0f;
	
	}
	void FixedUpdate () {
		//float ExplodeTime;

	//	Collider[] PointZoneObjects = Physics.OverlapBox(pointZoneCenter,

		Physics.gravity = gravityModifier;
			
		Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);
		RaycastHit hit;

	//	if (Physics.Raycast (ray, out hit,layerMaskRB)) {
		if (Physics.Raycast (ray, out hit, 1000f,layerMaskRB, QueryTriggerInteraction.Ignore)){

			if (hit.rigidbody == null) {
				ExplodeTime = 0;
				return;
			}
			//AudioManager.Instance.GetAudioSourceReferance(AudioManager.AudioReferanceType._AMBIENT,"BallHit")
			//AudioSource.PlayClipAtPoint (AudioManager.Instance.GetAudioSourceReferance(AudioManager.AudioReferanceType._AMBIENT,"BallHit").clip, hit.point);
			ballHitAS.transform.position = hit.point;
			ballHitAS.Play ();

			ExplodeTime += Time.deltaTime;

			if(isGame)//hit.normal.back try it!!!
				hit.rigidbody.AddForceAtPosition((hit.normal * -1) * amountOfObjectDrift, hit.point);
				//hit.rigidbody.AddForceAtPosition(hit.transform.forward.normalized * amountOfObjectDrift, hit.point);
			else
				hit.rigidbody.AddRelativeForce (cam.transform.forward * forceStrength);

			if (ExplodeTime >= timerUntilExplode) {

				if (isGame) {
					hit.rigidbody.AddForce (Vector3.up * amountOfObjectLift);
					ExplodeTime = 0;

				} else {

					hit.rigidbody.AddExplosionForce (forceStrength, cam.transform.rotation * Vector3.forward, 5, 2, ForceMode.Impulse);
					ExplodeTime = 0;
				}
			}

		} 
	
	}
}
