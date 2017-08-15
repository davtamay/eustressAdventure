using UnityEngine;
using System.Collections;


public class CursorPositioner : MonoBehaviour {
	private float defaultPosZ;
	Transform camTrans;
	Transform thisTransform;

	[SerializeField] private float zOffset = 0.2f;

	void Start () { 
		thisTransform = transform;
		defaultPosZ = thisTransform.localPosition.z;
		camTrans = Camera.main.transform;

	}
	
	// Update is called once per frame
	void Update () {
		

		Ray ray = new Ray (camTrans.position, camTrans.rotation * Vector3.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 8f ,-1, QueryTriggerInteraction.Ignore)){
		if (hit.distance <= defaultPosZ) {
				thisTransform.localPosition = new Vector3 (0, 0, hit.distance - zOffset);
		} else {
				thisTransform.localPosition = new Vector3 (0,0, defaultPosZ + zOffset);
		
		}
		
		}else {
			transform.localPosition = new Vector3 (0,0, defaultPosZ + zOffset);
		}
}
}