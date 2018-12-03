using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Move : MonoBehaviour {


    // GameSystem Déplacement Joueur:

    // GameObject:
    Rigidbody2D rigid;
    SpriteRenderer sprite;

    // Valeurs:
    float vitesse = 3f;
    bool platform;


    void OnCollisionEnter2D(Collision2D collision)
    {
        platform = true;
    }
   
    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        
        // Déplacement:
        rigid.velocity += new Vector2(Input.GetAxis("Horizontal"), 0);

        // Orientation:
        UpdateFlip();

        // Saut:
        rigid.velocity = new Vector2(Mathf.Clamp(rigid.velocity.x, -vitesse, vitesse), rigid.velocity.y);

        if (platform == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                platform = false;
                rigid.velocity += new Vector2(0, 6);
            }
        }
    }

    // Orientation:
    void UpdateFlip()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (rigid.velocity.x > 0)
        {
            sprite.flipX = false;
        }
        else if (rigid.velocity.x < 0)
        {
            sprite.flipX = true;
        }
    }
}
