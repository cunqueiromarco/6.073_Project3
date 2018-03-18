using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour {

	public int ammoRegen;

	// Use this for initialization
	void Start () {
		ammoRegen = 3;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			DogScript dogScript = collision.gameObject.GetComponent ("DogScript") as DogScript;
			dogScript.gainBarks (3);
			Object.Destroy (this.gameObject);
		}
	}
}
