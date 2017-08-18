using UnityEngine;
using System.Collections;


public class AddRandomPiece : MonoBehaviour {

	public GameObject[] obstacles;
	public GameObject [] items;
	public Transform[] objStartLocations;

	private int obstacleNumb;
	//private int itemNumb;



	void Awake(){
	
		obstacleNumb = obstacles.Length;
		//itemNumb = items.Length;
	}

	public void AddNewObstacle(int obstacleLength){
	
		if (obstacleNumb > 0) {

			if (obstacleLength > obstacles.Length)
				obstacleLength = obstacles.Length;
				
				
			int tempRandom = Random.Range (0, obstacleLength);

		
				
			int tempTranRandom = Random.Range (0, objStartLocations.Length);

			Instantiate (obstacles [tempRandom], objStartLocations [tempTranRandom].position, obstacles [tempRandom].transform.rotation);
		} else {
			return;
	
		}
	}

	public void AddNewItem(){

			if (obstacleNumb > 0){


				int tempRandom = Random.Range (0, items.Length);
				int tempTranRandom = Random.Range (0, objStartLocations.Length);

			Instantiate (items [tempRandom], objStartLocations[tempTranRandom].position, items[tempRandom].transform.rotation) ;
			}else{
				return;

			}



	
	
	}


}

