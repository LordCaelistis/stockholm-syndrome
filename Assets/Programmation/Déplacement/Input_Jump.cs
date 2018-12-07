using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Jump : MonoBehaviour {

    // public GameObject personnage;
    public KnightController scriptMouvement;
    bool onPlatform;
    public int plateformCount;
    int controllerNumber;

    public AudioClip clongAtterissage;
    public AudioClip atterissageHerbe;
    public AudioClip atterissageBois;
    public AudioClip atterissagePierre;
    public AudioClip atterissageGlace;

	// Use this for initialization
	void Start () {
        controllerNumber = scriptMouvement.getSetControllerNumber;
    }

	// Update is called once per frame
	void Update () {
        /*if (onPlatform == true)
        {
            if (Input.GetButtonDown("Jump"))
            {

                onPlatform = false;
                rigid.velocity += new Vector2(0, 10);
            }
        }*/
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        scriptMouvement.solType = coll.tag;
        if (plateformCount <= 0) {
            scriptMouvement.sourceSon.PlayOneShot(clongAtterissage,  0.1f);
            // SoundStuff.PlayRandomOneShot(scriptMouvement.sourceSon, scriptMouvement.ClonkArmureMarche, 0.2f);

            if(coll.tag == "Herbe")  scriptMouvement.sourceSon.PlayOneShot(atterissageHerbe,  0.3f);
            if(coll.tag == "Bois")   scriptMouvement.sourceSon.PlayOneShot(atterissageBois,   0.3f);
            if(coll.tag == "Glace")  scriptMouvement.sourceSon.PlayOneShot(atterissageGlace,  0.3f);
            if(coll.tag == "Pierre") scriptMouvement.sourceSon.PlayOneShot(atterissagePierre, 0.3f);
        }

        if (coll.tag != "prince") {
            scriptMouvement.onPlatform[controllerNumber] = true;
            plateformCount ++;
        }
        scriptMouvement.animator.SetBool("isJumping", false);
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag != "prince") {
            scriptMouvement.onPlatform[controllerNumber] = false;
            plateformCount --;
        }
        scriptMouvement.solType = "";

    }
}
