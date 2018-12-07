using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiquesPlateforme : MonoBehaviour {

    // Son
    public GameObject parent;

    AudioSource sourceSon;

    // public AudioClip piques;
    public AudioClip[] clonks;
    public AudioClip[] cris;

    // Use this for initialization
    void Start() {
        sourceSon = parent.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }

	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.tag == "Player"){
			// sourceSon.PlayOneShot(piques, 0.3f);
			SoundStuff.PlayRandomOneShot(sourceSon, clonks ,0.05f);
			SoundStuff.PlayRandomOneShot(sourceSon, cris ,0.1f);
		}
	}
}
