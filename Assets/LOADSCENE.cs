using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LOADSCENE : MonoBehaviour {

	public void OnEnterScene(string name){

		SceneManager.LoadSceneAsync(name);
	}


}
