using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public int health;
    private Transform target;
    private float speed;

    private float lastAttacked;
    private float attackSpeed;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        speed = 0.5f;
        attackSpeed = 1.0f;
        lastAttacked = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        rotateToPlayer();
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < 1.0)
        {
            attack();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void rotateToPlayer()
    {
        Vector3 difference = target.position - transform.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + 90f;
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }

    private void attack()
    {
        // Attack once per second
        if (Time.time - lastAttacked > attackSpeed)
        {
            //Attack logic
            DogScript player = GameObject.FindGameObjectWithTag("Player").GetComponent("DogScript") as DogScript;
            if (player.health > 0)
            {
                player.health -= 1;
            }
            lastAttacked = Time.time;
        }
        return;
    }

    public void loseHealth()
    {
        health -= 1;
        // When enemy dies and updates player score
        if (health <= 0)
        {
            Object.Destroy(this.gameObject);
            DogScript player = GameObject.FindGameObjectWithTag("Player").GetComponent("DogScript") as DogScript;
            player.addScore(1);
        }
    }

}

