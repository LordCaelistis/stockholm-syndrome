using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour {

	Animator animator;
	AudioSource sourceSon;

	public AudioClip clap;
	public AudioClip[] clonks;
	public AudioClip[] cris;

	// Use this for initialization
	void Start () {
		animator  = gameObject.GetComponent<Animator>();
		sourceSon = gameObject.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.tag == "Player"){
			animator.SetTrigger("close");

			sourceSon.PlayOneShot(clap, 0.3f);
			SoundStuff.PlayRandomOneShot(sourceSon, clonks ,0.05f);
			SoundStuff.PlayRandomOneShot(sourceSon, cris ,0.1f);
		}
	}
}
