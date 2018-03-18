using UnityEngine;
using System.Collections;


public class CursorPositioner : MonoBehaviour {
	private float defaultPosZ;
	Transform camTrans;
	Transform thisTransform;
	private Vector3 crosshairOriginalScale;
	private float originalDistance;
	[SerializeField] private float zOffset = 0.2f;
	[SerializeField] private float minDistanceForDefaultScale;

	void Start () { 
		thisTransform = transform;
		defaultPosZ = thisTransform.localPosition.z;
		camTrans = Camera.main.transform;
		crosshairOriginalScale = thisTransform.localScale;
		originalDistance = Vector3.Distance (camTrans.position, thisTransform.position);
	}
	
	// Update is called once per frame

	void Update () {
		

		Ray ray = new Ray (camTrans.position, camTrans.rotation * Vector3.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 8 ,-1, QueryTriggerInteraction.Ignore)){

			//thisTransform.localScale = crosshairOriginalScale * ( (originalDistance - 0.6f)/ hit.distance);
				 

		if (hit.distance <= defaultPosZ) {
				thisTransform.localPosition = new Vector3 (0, 0, hit.distance - zOffset);
		} else {
				thisTransform.localPosition = new Vector3 (0,0, defaultPosZ + zOffset);
		
		}

//			if(hit.distance < minDistanceForDefaultScale){
//
//				thisTransform.localScale = crosshairOriginalScale *
//					(hit.distance/ originalDistance);
//			}
		

		}else {
			transform.localPosition = new Vector3 (0,0, defaultPosZ + zOffset);
			thisTransform.localScale = crosshairOriginalScale ;
		}


}
}