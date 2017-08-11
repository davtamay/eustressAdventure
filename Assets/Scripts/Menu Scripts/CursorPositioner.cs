using UnityEngine;
using System.Collections;


public class CursorPositioner : MonoBehaviour {
	private float defaultPosZ;
	//private Camera cam;
	Transform camTrans;

	[SerializeField] private float zOffset = 0.2f;

	void Start () { 
		defaultPosZ = transform.position.z;
		camTrans = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		

		Ray ray = new Ray (camTrans.position, camTrans.rotation * Vector3.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 10f ,-1, QueryTriggerInteraction.Ignore)){
		if (hit.distance <= defaultPosZ) {
				transform.localPosition = new Vector3 (0, 0, hit.distance - zOffset);
		} else {
				transform.localPosition = new Vector3 (0,0, defaultPosZ + zOffset);
		
		}
		
		}else {
			transform.localPosition = new Vector3 (0,0, defaultPosZ + zOffset);
		}
}
}