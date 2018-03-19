using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToGameScene() {
		SceneManager.LoadScene ("GameScene");
	}

	public void ToInstructionsScene() {
		SceneManager.LoadScene ("InstructionsScene");
	}

}
