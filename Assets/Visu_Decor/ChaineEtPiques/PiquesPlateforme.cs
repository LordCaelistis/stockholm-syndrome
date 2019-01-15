using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiquesPlateforme : MonoBehaviour
{

    // Son
    public GameObject parent;

    AudioSource sourceSon;

    // public AudioClip piques;
    public AudioClip[] clonks;
    public AudioClip[] cris;

    private Rigidbody2D tempRigidBodyPlayer;
    public GameObject PrincePrefab;
    public Rigidbody2D princeRigid;

    // Use this for initialization
    void Start()
    {
        sourceSon = parent.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            //Equivalent de GetBonked() dans KnightController
            coll.GetComponent<Animator>().SetTrigger("hit");
            tempRigidBodyPlayer = coll.GetComponent<Rigidbody2D>();
            if (coll.GetComponent<SpriteRenderer>().flipX == false) tempRigidBodyPlayer.velocity = new Vector2(25f, -5f);
            if (coll.GetComponent<SpriteRenderer>().flipX == true) tempRigidBodyPlayer.velocity = new Vector2(-25f, -5f);
            coll.transform.GetChild(2).GetComponent<KnightController>().cooldown_now_airmove = 0.5f;

            print(Timer.playerDictionary[coll.transform.GetChild(2).GetComponent<KnightController>().controllerNumber]);

            if (Timer.playerDictionary[coll.transform.GetChild(2).GetComponent<KnightController>().controllerNumber] == true)
            {
                PrincePrefab.AddComponent<Rigidbody2D>();
                PrincePrefab.GetComponent<Animator>().SetBool("isCarried", false);
                PrincePrefab.GetComponent<BoxCollider2D>().enabled = true;
                PrincePrefab.GetComponent<PrinceBehavior>().cooldown_now += PrincePrefab.GetComponent<PrinceBehavior>().cooldown_max;
                Vector3 pos0 = gameObject.transform.position;
                Quaternion rot0 = Quaternion.identity;
                //GameObject prince0 = (GameObject)Instantiate(PrincePrefab, pos0, rot0);
                princeRigid = PrincePrefab.GetComponent<Rigidbody2D>();
                princeRigid.velocity += new Vector2(0, 8);
            }

            // sourceSon.PlayOneShot(piques, 0.3f);            
            SoundStuff.PlayRandomOneShot(sourceSon, clonks, 0.1f);
            SoundStuff.PlayRandomOneShot(sourceSon, cris, 0.2f);
        }
    }
}
