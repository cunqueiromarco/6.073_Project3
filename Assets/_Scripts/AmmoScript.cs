using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour {

	public int ammoRegen;
    public AudioClip sound;
    private AudioSource source;

	// Use this for initialization
	void Start () {
		ammoRegen = 3;
        source = GetComponent<AudioSource>();
        source.clip = sound;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
            source.Play();
            GetComponent<Renderer>().enabled = false;
            Object.Destroy(GetComponent<BoxCollider2D>());

            DogScript dogScript = collision.gameObject.GetComponent ("DogScript") as DogScript;
			dogScript.gainBarks(3);

            Object.Destroy(this.gameObject, source.clip.length);
		}
	}
}
