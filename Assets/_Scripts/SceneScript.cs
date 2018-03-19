using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadScene(string scene)
    {
		MapScript map = GameObject.FindGameObjectWithTag ("Map").GetComponent ("MapScript") as MapScript;
		map.changeToWhite ();
        SceneManager.LoadScene(scene);
    }
}
