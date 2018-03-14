using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnScript : MonoBehaviour {

    private float spawnRate;
    private float lastSpawn;
    private float lastLevel;
    private int level;
    private int maxLevel;

    public Text LevelText;
    public GameObject kittenPrefab;
    public GameObject catPrefab;
    public GameObject lionPrefab;

    private List<Vector3> spawnSpots;
    private bool gameOver;

    // Use this for initialization
	void Start () {
        spawnRate = 0.5f;
        lastSpawn = Time.time;
        lastLevel = Time.time;
        maxLevel = 5;
        level = 1;
        LevelText.text = "Level " + level.ToString();
        spawnSpots = new List<Vector3>();
        spawnSpots.Add(new Vector3(-7.0f, 0, 0));
        spawnSpots.Add(new Vector3(7.0f, 0, 0));
        spawnSpots.Add(new Vector3(0, 5.0f, 0));
        spawnSpots.Add(new Vector3(0, -5.0f, 0));
        gameOver = false;
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
        if (!gameOver)
        {
            if (Time.time - lastSpawn > spawnRate * (maxLevel - level + 1))
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
            if (Time.time - lastLevel > 30 && level < maxLevel)
            {
                level += 1;
                LevelText.text = "Level " + level.ToString();
                lastLevel = Time.time;
            }
            if (Time.time - lastLevel > 30 && level == maxLevel)
            {
                makeGameOver();
            }
        }
	}

    public void makeGameOver()
    {
        gameOver = true;
        LevelText.text = "Game Over!";
        DogScript player = GameObject.FindGameObjectWithTag("Player").GetComponent("DogScript") as DogScript;
        player.makeGameOver();
    }
}
