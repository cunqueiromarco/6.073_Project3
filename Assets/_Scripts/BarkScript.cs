using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
		bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
		if (!onScreen) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			EnemyScript enemyScript = coll.gameObject.GetComponent ("EnemyScript") as EnemyScript;
			enemyScript.loseHealth ();
			Object.Destroy (this.gameObject);
		}
	}
}
