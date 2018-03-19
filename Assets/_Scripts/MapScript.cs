using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour {

	private SpriteRenderer spriteR;

	// Use this for initialization
	void Start () {
		spriteR = gameObject.GetComponent<SpriteRenderer>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeToBlack() {
		spriteR.color = new Color(0.0f, 0.0f, 0.0f);
	}

	public void changeToWhite() {
		spriteR.color = Color.white;
	}
}
