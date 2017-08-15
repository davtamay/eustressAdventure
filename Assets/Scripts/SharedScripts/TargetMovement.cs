using UnityEngine;
using System.Collections;

public class TargetMovement : MonoBehaviour 
{
	// Move target around circle with tangential speed 
    public Vector3 bound;
    public float speed = 100.0f;

	[SerializeField]private Vector3 initialPosition;
    [SerializeField]private Vector3 nextMovementPoint;
	private Transform thisTransform;
		
	// Use this for initialization
	void Start () 
    {	
		thisTransform = transform;
		initialPosition = thisTransform.position;

        CalculateNextMovementPoint();


	}

    void CalculateNextMovementPoint()
    {
        float posX = Random.Range(initialPosition.x - bound.x, initialPosition.x + bound.x);
        float posY = Random.Range(initialPosition.y - bound.y, initialPosition.y + bound.y);
        float posZ = Random.Range(initialPosition.z - bound.z, initialPosition.z + bound.z);

		nextMovementPoint = new Vector3(posX, posY, posZ); //initialPosition + new Vector3(posX, posY, posZ); 
    }

//	void OnDrawGizmos(){
	
//		Gizmos.DrawRay (thisTransform.position, thisTransform.forward *10);
//	}
	// Update is called once per frame
	void Update () 
    {
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nextMovementPoint - transform.position), 2.0f * Time.deltaTime);

	
		if(Physics.Raycast(thisTransform.position, thisTransform.forward, 5f))
			CalculateNextMovementPoint();

        if(Vector3.Distance(nextMovementPoint, transform.position) <= 10.0f)
            CalculateNextMovementPoint();
	}
}
