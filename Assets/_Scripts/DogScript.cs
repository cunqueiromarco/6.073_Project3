﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DogScript : MonoBehaviour {

	public float speed;
	public float barkSpeed;
	public int barks;
	public Slider ammoSlider;
	public float barkRecoverSpeed;
	public float barkTimeout;

	public GameObject barkPrefab;

	private int maxHealth;
	private int maxBarks;
	private int health;

	// Use this for initialization
	void Start () {
		maxHealth = 10;
		maxBarks = 20;
		health = 10;
		barks = 20;
		barkRecoverSpeed = 3.0F;
		barkTimeout = Time.time + barkRecoverSpeed;
	}

	// Update is called once per frame
	void Update () {
		move ();
		rotate ();
		if (Input.GetButtonDown ("Fire1")) {
			if (barks > 0) { shoot (); }
		}
		updateAmmoSlider ();
	}

	private void move() {
		float horizontalAxis = Input.GetAxisRaw ("Horizontal");
		float verticalAxis = Input.GetAxisRaw ("Vertical");
		Vector3 move = new Vector3 (horizontalAxis, verticalAxis, 0);
		move.Normalize ();
		transform.position += move * speed * Time.deltaTime;
	}

	private void rotate() {
		// Rotating to look at cursor
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		worldPos.z = 0;
		Vector3 difference = worldPos - transform.position;
		float angle = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg + 90f;
		transform.localEulerAngles = new Vector3 (0, 0, angle);
	}

	private void shoot() {
		barks--;
		GameObject bark = (GameObject)Instantiate (barkPrefab, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		worldPos.z = 1;
		Vector3 difference = worldPos - bark.transform.position;
		difference.Normalize ();
		bark.GetComponent<Rigidbody2D> ().AddForce (difference*barkSpeed);
	}

	private void updateAmmoSlider() {
		if (Time.time > barkTimeout) {
			barks++;
			barkTimeout = Time.time + barkRecoverSpeed;
		}
		ammoSlider.value = getBarks ();
	}

	public void gainBarks(int amount) {
		barks += amount;
		if (barks > maxBarks) {
			barks = maxBarks;
		}
	}

	public void gainHealth(int amount) {
		health += amount;
		if (health > maxHealth) {
			health = maxHealth;
		}
	}

	public int getBarks() {
		return barks;
	}

	public int getMaxBarks() {
		return maxBarks;
	}

	public int getHealth() {
		return health;
	}

	public int getMaxHealth(){
		return maxHealth;
	}
}
