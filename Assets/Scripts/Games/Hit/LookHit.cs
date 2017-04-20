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


	void Start(){
	
		cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

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
				
			
		
			ExplodeTime += Time.deltaTime;

			if(isGame)
			//	hit.rigidbody.AddRelativeForce (cam.transform.forward* amountOfObjectDrift);

			//	hit.rigidbody.AddForceAtPosition(hit.point.normalized * amountOfObjectDrift, hit.point);
				hit.rigidbody.AddForceAtPosition(hit.transform.forward.normalized * amountOfObjectDrift, hit.point);


				//hit.rigidbody.AddRelativeForce ( cam.transform.forward * 10 + Vector3.up * liftUp);
				//hit.rigidbody.AddForce (cam.transform.forward* amountOfObjectDrift);
				//Debug.LogFormat("{0} : this is amountoflift", amountOfObjectLift);
			else
				hit.rigidbody.AddRelativeForce (cam.transform.forward * forceStrength);

			if (ExplodeTime >= timerUntilExplode) {

				if (isGame) {
					hit.rigidbody.AddForce (Vector3.up * amountOfObjectLift);

					//hit.rigidbody.AddForce (cam.transform.forward.normalized* amountOfObjectDrift + Vector3.up * amountOfObjectLift);

					//hit.rigidbody.AddForce(
					//hit.rigidbody.AddForceAtPosition(Vector3.up * 5f,hit.point,ForceMode.Impulse);
					//4
					//hit.rigidbody.AddExplosionForce (20, hit.transform.position, 5, amountOfObjectLift, ForceMode.Impulse);
					ExplodeTime = 0;

				} else {

					hit.rigidbody.AddExplosionForce (forceStrength, cam.transform.rotation * Vector3.forward, 5, 2, ForceMode.Impulse);
					ExplodeTime = 0;
				}
			}

		} 
	
	}
}
