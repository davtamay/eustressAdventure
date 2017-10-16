using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows for the rotation adjustment from raycast normal and cameraYrotation.
public class FeetFollowPlayer : MonoBehaviour {

	private Transform camTransform;
	//[SerializeField]private float rotationToSpeed = 1;
	private Transform thisTransform;

	void Start () {

	
		thisTransform = GetComponent<Transform>();
		camTransform = Camera.main.transform;


	}


	void LateUpdate () {
		


		RaycastHit hit;

		//ACTIVATE THIS TO ENABLE FOLLOWING CAMERA YROTATION
		thisTransform.rotation = Quaternion.AngleAxis (camTransform.eulerAngles.y, Vector3.up);

	
		if (Physics.Raycast (thisTransform.position, -thisTransform.up, out hit, 10f)) {

			//thisTransform.position = hit.point + Vector3.up;
			thisTransform.rotation = (Quaternion.FromToRotation (thisTransform.up, hit.normal)) * thisTransform.rotation;

		}
			

		thisTransform.rotation *= Quaternion.Euler (Vector3.right* 90);
			
	

	}



	public static void AlignTransform(Transform transform)
	{
		Vector3 sample = SampleNormal(transform.position);

		Vector3 proj = transform.forward - (Vector3.Dot(transform.forward, sample)) * sample;
		transform.rotation = Quaternion.LookRotation(proj, sample);
	}

	public static Vector3 SampleNormal(Vector3 position)
	{
		Terrain terrain = Terrain.activeTerrain;
		var terrainLocalPos = position - terrain.transform.position;
		var normalizedPos = new Vector2(
			Mathf.InverseLerp(0f, terrain.terrainData.size.x, terrainLocalPos.x),
			Mathf.InverseLerp(0f, terrain.terrainData.size.z, terrainLocalPos.z)
		);
		var terrainNormal = terrain.terrainData.GetInterpolatedNormal(normalizedPos.x, normalizedPos.y);

		return terrainNormal;
	}
}

