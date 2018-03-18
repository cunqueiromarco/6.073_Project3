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
    public Text TimerText;
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
        LevelText.text = "Wave " + level.ToString();
        TimerText.text = "Next Wave: 30"; 
        spawnSpots = new List<Vector3>();
        spawnSpots.Add(new Vector3(-5.5f, -10.0f, 0));
        spawnSpots.Add(new Vector3(5.5f, -10.0f, 0));
        spawnSpots.Add(new Vector3(-5.5f, 10.0f, 0));
        spawnSpots.Add(new Vector3(5.5f, 10.0f, 0));
        spawnSpots.Add(new Vector3(-17.0f, 5.0f, 0));
        spawnSpots.Add(new Vector3(17.0f, -5.0f, 0));
        spawnSpots.Add(new Vector3(-17.0f, 5.0f, 0));
        gameOver = false;
    }

    void spawnKitten()
    {
        Vector3 spawnLocation = spawnSpots[Random.Range(0, 7)];
        GameObject kitten = (GameObject)Instantiate(kittenPrefab, spawnLocation, new Quaternion());
    }

    void spawnCat()
    {
        Vector3 spawnLocation = spawnSpots[Random.Range(0, 7)];
        GameObject cat = (GameObject)Instantiate(catPrefab, spawnLocation, new Quaternion());
    }

    void spawnLion()
    {
        Vector3 spawnLocation = spawnSpots[Random.Range(0, 7)];
        GameObject lion = (GameObject)Instantiate(lionPrefab, spawnLocation, new Quaternion());
    }

    // Update is called once per frame
    void Update () {
        if (!gameOver)
        {
            if (lastLevel == maxLevel)
            {
                TimerText.text = "Time till Win: " + (30 - (int)(Time.time - lastLevel)).ToString();
            }
            else
            {
                TimerText.text = "Next Wave: " + (30 - (int)(Time.time - lastLevel)).ToString();
            }
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
                LevelText.text = "Wave " + level.ToString();
                lastLevel = Time.time;
            }
            if (Time.time - lastLevel > 30 && level == maxLevel)
            {
                makeGameOver(true);
                DogScript player = GameObject.FindGameObjectWithTag("Player").GetComponent("DogScript") as DogScript;
                player.makeGameOver();
            }
        }
	}

    public void makeGameOver(bool win)
    {
        gameOver = true;
        if (win)
        {
            LevelText.text = "You Win!";
        }
        else
        {
            LevelText.text = "You lose!";
        }
        TimerText.text = "";
    }
}
