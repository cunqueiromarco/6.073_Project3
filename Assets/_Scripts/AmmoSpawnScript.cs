using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawnScript : MonoBehaviour {

	public float ammoSpawnSpeed;
	public float ammoSpawnTimeout;

	public GameObject ammoPrefab;

	private int xRightBound = 16;
	private int xLeftBound = -16;

	private int yTopBound = 9;
	private int yBottomBound = -9;

	public static bool gameOver;

	// Use this for initialization
	void Start () {
		ammoSpawnSpeed = 5.0F;
		ammoSpawnTimeout = Time.time + ammoSpawnSpeed;
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (gameOver) {
			GameObject[] ammos = GameObject.FindGameObjectsWithTag("Ammo");
			foreach (GameObject ammo in ammos)
			{
				Object.Destroy(ammo);
			}
			return;
		}

		if (Time.time > ammoSpawnTimeout) {
			SpawnAmmo ();
			ammoSpawnTimeout = Time.time + ammoSpawnSpeed;
		}
	}

	void SpawnAmmo() { 
		int xSpawn = Random.Range (xLeftBound, xRightBound);
		int ySpawn = Random.Range (yBottomBound, yTopBound);
		Vector3 spawnLocation = new Vector3 (xSpawn, ySpawn, 0);
		Instantiate (ammoPrefab, spawnLocation, new Quaternion ());
	}
}
