using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonChainePlateforme : MonoBehaviour {

    public Rigidbody2D chaineBody;

    // Son
    public AudioClip[] Chaine;
    float intervalchaineMax = 0.5f;
    float intervalchaineNow = 0f;

    AudioSource sourceSon;

    // Use this for initialization
    void Start() {
        sourceSon = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

        if(
            Mathf.Abs(chaineBody.velocity.x) >= 0.2f ||
            Mathf.Abs(chaineBody.velocity.y) >= 0.2f
        ){

            if (intervalchaineNow <= 0)
            {
                SoundStuff.PlayRandomOneShot(sourceSon, Chaine, 0.4f);
                intervalchaineNow += intervalchaineMax;
            }
            else
            {
                intervalchaineNow -= Time.deltaTime;
            }

        }

    }
}
