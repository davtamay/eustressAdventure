using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsBasedOnImpact : MonoBehaviour {

	[SerializeField]private float pointToVelocityFactor;
	void OnCollisionEnter(Collision collision){


		var magnitude = collision.relativeVelocity.magnitude;
		PlayerManager.Instance.points = Mathf.RoundToInt (magnitude * pointToVelocityFactor);



	}
}
