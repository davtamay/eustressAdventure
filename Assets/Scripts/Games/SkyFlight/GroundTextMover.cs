using UnityEngine;
using System.Collections;

public class GroundTextMover : MonoBehaviour {

	public float horzSpeed;
	public float vertSpeed;

	public float horzUVMin = 1.0f;
	public float horzUVMax = 2.0f;

	public float vertUVMin = 1.0f;
	public float vertUVMax = 2.0f;

	private MeshRenderer meshRend;


	void Awake () {
		meshRend = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = new Vector2 ((meshRend.material.mainTextureOffset.x > horzUVMax) ? horzUVMin : 
			meshRend.material.mainTextureOffset.x + Time.deltaTime * horzSpeed, 

		(meshRend.material.mainTextureOffset.y > vertUVMax ) ? vertUVMin : meshRend.material.mainTextureOffset.y
		+ Time.deltaTime * vertSpeed);

		meshRend.material.mainTextureOffset = offset;
	}
}
