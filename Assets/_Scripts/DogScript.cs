
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DogScript : MonoBehaviour {

	public float speed;
	public float barkSpeed;
	private int barks;
	public float barkRecoverSpeed; // public so we can modify on the fly in game
	private float barkTimeout;
	public Slider ammoSlider;

	public int health;
	public Slider healthSlider;

	public GameObject barkPrefab;

    public AudioClip barkClip;
    private AudioSource source;

	private int maxHealth;
	private int maxBarks;

    private int score;
    public Text scoreText;

    private bool gameOver;

	// Use this for initialization
	void Start () {
		maxHealth = 10;
		maxBarks = 20;
		health = 10;
		barks = 20;
        score = 0;
        GetComponent<Animator>().speed = 1;
        scoreText.text = score.ToString();
		barkRecoverSpeed = 1.0F;
        gameOver = false;

        source = GetComponent<AudioSource>();
        source.clip = barkClip;
	}

	// Update is called once per frame
	void Update () {
        if (!gameOver)
        {
            if (health <= 0)
            {
                makeGameOver();
                SpawnScript spawn = GameObject.Find("Spawn").GetComponent("SpawnScript") as SpawnScript;
                spawn.makeGameOver(false);
            }
            move();
            rotate();
            if (Input.GetButtonDown("Fire1"))
            {
                if (barks > 0) { shoot(); }
            }
            updateAmmoSlider();
            updateHealthSlider();
			Vector3 camPosition = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, -10);
			if (transform.position.x > -9.66f && transform.position.x < 9.66f) {
				camPosition.x = transform.position.x;
			}
			if (transform.position.y > -5.8f && transform.position.y < 5.8f) {
				camPosition.y = transform.position.y;
			}
			Camera.main.transform.position = camPosition;
        }
	}

	private void move() {
		float horizontalAxis = Input.GetAxisRaw ("Horizontal");
		float verticalAxis = Input.GetAxisRaw ("Vertical");
		Vector2 move = new Vector2 (horizontalAxis, verticalAxis);
		move.Normalize ();
		Vector2 newPosition = new Vector2 (transform.position.x, transform.position.y);
		newPosition += move * speed * Time.deltaTime;
		if (newPosition.x <= -16.9f) {
			newPosition.x = -16.9f;
		}else if(newPosition.x >= 16.9f) {
			newPosition.x = 16.9f;
		}

		if (newPosition.y <= -9.8) {
			newPosition.y = -9.8f;
		}else if (newPosition.y >= 9.8) {
			newPosition.y = 9.8f;
		}
		GetComponent<Rigidbody2D> ().MovePosition (newPosition);
		transform.position = new Vector3 (newPosition.x, newPosition.y, 2);
	}

	private void rotate() {
		// Rotating to look at cursor
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		worldPos.z = 0;
		Vector3 difference = worldPos - transform.position;
		float angle = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg - 90f;
		transform.localEulerAngles = new Vector3 (0, 0, angle);
	}

	private void shoot() {
		barks--;
		GetComponent<Animator> ().SetTrigger ("TriggerBark");

		GameObject bark = (GameObject)Instantiate (barkPrefab, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
		bark.transform.localEulerAngles = new Vector3 (0, 0, transform.localEulerAngles.z);
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		worldPos.z = 1;
		Vector3 difference = worldPos - bark.transform.position;
		difference.Normalize ();
		bark.GetComponent<Rigidbody2D> ().AddForce (difference*barkSpeed);

        source.Play();
	}

	private void updateAmmoSlider() {
		if (Time.time > barkTimeout) {
			gainBarks (1);
			barkTimeout = Time.time + barkRecoverSpeed;
		}
		ammoSlider.value = getBarks ();
	}

	private void updateHealthSlider() {
		healthSlider.value = getHealth ();
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

    public void addScore(int inc)
    {
        score += inc;
        scoreText.text = score.ToString();
    }

    public void makeGameOver()
    {
        gameOver = true;
        GetComponent<Animator>().speed = 0;
        // Remove all cats from field
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Object.Destroy(enemy);
        }
    }
}
