using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    private float spawnRate;
    private float lastSpawn;

    public GameObject kittenPrefab;
    public GameObject catPrefab;
    public GameObject lionPrefab;

    private List<Vector3> spawnSpots;

    // Use this for initialization
	void Start () {
        spawnRate = 5.0f;
        lastSpawn = Time.time;
        spawnSpots = new List<Vector3>();
        spawnSpots.Add(new Vector3(-7.0f, 0, 0));
        spawnSpots.Add(new Vector3(7.0f, 0, 0));
        spawnSpots.Add(new Vector3(0, 5.0f, 0));
        spawnSpots.Add(new Vector3(0, -5.0f, 0));
    }

    void spawnKitten()
    {
        Vector3 spawnLocation = spawnSpots[Random.Range(0, 4)];
        GameObject kitten = (GameObject)Instantiate(kittenPrefab, spawnLocation, new Quaternion());
    }

    void spawnCat()
    {
        Vector3 spawnLocation = spawnSpots[Random.Range(0, 4)];
        GameObject cat = (GameObject)Instantiate(catPrefab, spawnLocation, new Quaternion());
    }

    void spawnLion()
    {
        Vector3 spawnLocation = spawnSpots[Random.Range(0, 4)];
        GameObject cat = (GameObject)Instantiate(lionPrefab, spawnLocation, new Quaternion());
    }

    // Update is called once per frame
    void Update () {
		if (Time.time - lastSpawn > spawnRate)
        {
            int val = Random.Range(0, 3);
            if (val == 0)
            {
                spawnKitten();
            }
            else if (val == 1)
            {
                spawnCat();
            }
            else
            {
                spawnLion();
            }
            lastSpawn = Time.time;
        }
        
	}
}
