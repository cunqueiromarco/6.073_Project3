using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogScript : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		float horizontalAxis = Input.GetAxisRaw ("Horizontal");
		float verticalAxis = Input.GetAxisRaw ("Vertical");
		Vector3 move = new Vector3 (horizontalAxis, verticalAxis, 0);
		move.Normalize ();
		transform.position += move * speed * Time.deltaTime;

		// Rotating to look at cursor
		Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
		worldPos.z = 0;
		Vector3 difference = worldPos - transform.position;
		float angle = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg + 90f;
		transform.localEulerAngles = new Vector3 (0, 0, angle);
	}
}
