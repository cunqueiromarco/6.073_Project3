using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogScript : MonoBehaviour {

	public float speed;
	public float barkSpeed;

	public GameObject barkPrefab;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		move ();
		rotate ();
		if (Input.GetButtonDown ("Fire1")) {
			shoot ();
		}
	}

	private void move(){
		float horizontalAxis = Input.GetAxisRaw ("Horizontal");
		float verticalAxis = Input.GetAxisRaw ("Vertical");
		Vector3 move = new Vector3 (horizontalAxis, verticalAxis, 0);
		move.Normalize ();
		transform.position += move * speed * Time.deltaTime;
	}

	private void rotate(){
		// Rotating to look at cursor
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		worldPos.z = 0;
		Vector3 difference = worldPos - transform.position;
		float angle = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg + 90f;
		transform.localEulerAngles = new Vector3 (0, 0, angle);
	}

	private void shoot(){
		GameObject bark = (GameObject)Instantiate (barkPrefab, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		worldPos.z = 0;
		Vector3 difference = worldPos - bark.transform.position;
		difference.Normalize ();
		bark.GetComponent<Rigidbody2D> ().AddForce (difference*barkSpeed);
	}
}
