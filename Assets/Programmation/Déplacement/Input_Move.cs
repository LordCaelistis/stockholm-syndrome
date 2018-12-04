using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Move : MonoBehaviour {


    // GameSystem Déplacement Joueur:
    [SerializeField] Rigidbody2D rigidbody2D;

    // GameObject:
    Rigidbody2D rigid;
    SpriteRenderer sprite;

    // Valeurs:
    float vitesse = 3.5f;
    bool platform;
   
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
