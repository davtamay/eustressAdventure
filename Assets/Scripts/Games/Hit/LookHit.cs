using UnityEngine;
using System.Collections;

public class LookHit : MonoBehaviour {

	private Camera cam;
	public float forceStrength;
	public float timerUntilExplode;
	public float ExplodeTime;
	public LayerMask layerMaskRB;

	void Start(){
	
		cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
	
	}
	void FixedUpdate () {
		//float ExplodeTime;

		Ray ray = new Ray (cam.transform.position, cam.transform.rotation * Vector3.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, layerMaskRB)) {

			if (hit.rigidbody == null) {
				ExplodeTime = 0;
				return;
			}
			
		
			ExplodeTime += Time.deltaTime;

			hit.rigidbody.AddRelativeForce (cam.transform.forward * forceStrength);

			if (ExplodeTime >= timerUntilExplode) {
				hit.rigidbody.AddExplosionForce (forceStrength, cam.transform.rotation * Vector3.forward , 5, 2,ForceMode.Impulse);
				ExplodeTime = 0;
			}

		} 



	
	}
}
