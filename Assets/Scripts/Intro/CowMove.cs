using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowMove : MonoBehaviour {

	[SerializeField] float moveSpeed = 3;
	[SerializeField] float timeUntilRestartSearch = 2f;



	private RandomMoveAnimations randMoveAnimScript;

	private Transform thisTransform;
	private Rigidbody thisRigidBody;
	private Animator thisAnimator;

	private int randAnimHash = Animator.StringToHash("Random");
	private int animIdleHash = Animator.StringToHash ("Idle");

	void Awake(){
	
		thisTransform = transform;
		//thisRigidBody = GetComponent<Rigidbody> ();
		thisAnimator = GetComponent<Animator>();
		randMoveAnimScript = GetComponent<RandomMoveAnimations> ();


	}
		
	IEnumerator startRanIEnum;
	void OnTriggerEnter(Collider other){


		if (other.CompareTag ("Player")) {

			//thisAnimator.SetBool (animIdleHash, false);
			//CrossFade("Walk",4f);
			StopAllCoroutines ();
			randMoveAnimScript.isRandomOn = false;
		}
		/*	if (startRanIEnum != null)
			StopCoroutine (startRanIEnum);


		
		}*/

	
	}

	//Coroutine startRandomCoroutine;
	
	IEnumerator StartRandom(){
	
		yield return new WaitForSeconds (timeUntilRestartSearch);
		thisAnimator.SetBool (animIdleHash, false);
		randMoveAnimScript.isRandomOn = true;
	}


	float timer = 0;
	Vector3 oldPosition;

 

    void OnTriggerStay(Collider other)
    {

        //if (!isTriggerMove)
        //    return; 

        //if(randMoveAnimScript.GetMoveMagnitude().magnitude < 2f)
        //    thisAnimator.SetBool(animIdleHash, true);
   

		if(other.CompareTag("Player"))
        {

            timer += Time.deltaTime;

            thisAnimator.SetInteger (randAnimHash, Random.Range (0, randMoveAnimScript.randomAnimationParameterChance));
			thisAnimator.SetBool (animIdleHash, false);

            Vector3 playerRelativePos = (thisTransform.position - other.transform.position).normalized;

            playerRelativePos.y = 0;




            if (timer >= 0.5f)
            {

            
                    timer = 0;
                    randMoveAnimScript.isRandomOn = false;
                    StopAllCoroutines();
                    StartCoroutine(randMoveAnimScript.Turn(2 * thisTransform.position - other.transform.position));
                    

              }

            thisTransform.position += playerRelativePos * moveSpeed * Time.deltaTime;


        }


    }

    //bool isTriggerMove = true;
    void OnCollisionEnter(Collision collision)
    {

        if (!collision.collider.CompareTag("Player")) {
            
            Debug.Log("COW COLLIDED");

            randMoveAnimScript.isRandomOn = false;
            //StopAllCoroutines();
            
           // StartCoroutine(TurnToFaceAwayFromObstacles(collision.contacts[0].normal));
            thisAnimator.SetBool(animIdleHash, false);
            //isTriggerMove = false;
		}

    }

    void OnTriggerExit(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            thisAnimator.SetBool(animIdleHash, true);
            //thisAnimator.CrossFade(animIdleHash, 3f);
            startRanIEnum = StartRandom();
            StartCoroutine(startRanIEnum);


        }


    }

    bool isTurnOnCollisionDone = false;

    private IEnumerator TurnToFaceAwayFromObstacles(Vector3 dir)
    {
        isTurnOnCollisionDone = false;
        yield return StartCoroutine(randMoveAnimScript.TurnUntilNoForwardObstacles(dir));
        isTurnOnCollisionDone = true;

    }

    float timerUntilNewTurn;
    private void OnCollisionStay(Collision collision)
    {
        timerUntilNewTurn += Time.deltaTime;

        //if (!collision.collider.CompareTag("Player"))
        //    isTriggerMove = false;
        //else
        //    return;

        //randMoveAnimScript.isRandomOn = false;

        if (!Physics.Raycast(thisTransform.position, thisTransform.forward, 5))
            thisTransform.position += thisTransform.forward * moveSpeed * Time.deltaTime;

        if (isTurnOnCollisionDone || timerUntilNewTurn > 2f)
        {
            randMoveAnimScript.isRandomOn = false;
            thisAnimator.SetBool(animIdleHash, false);
            timerUntilNewTurn = 0f;
            StartCoroutine(TurnToFaceAwayFromObstacles(collision.contacts[0].normal));
        
        }
    }

    void OnCollisionExit(Collision other)
{

    if (!other.collider.CompareTag("Player"))
    {

        startRanIEnum = StartRandom();
        StartCoroutine(startRanIEnum);

        //isTriggerMove = true;
    }



}
}
