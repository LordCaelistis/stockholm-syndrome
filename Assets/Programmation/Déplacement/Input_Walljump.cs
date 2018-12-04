﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Walljump : MonoBehaviour
{

    public GameObject personnage;
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    bool againstWall;

    // Use this for initialization
    void Start()
    {
        rigid = personnage.GetComponent<Rigidbody2D>();
        sprite = personnage.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (againstWall == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                againstWall = false;
                rigid.velocity += new Vector2(-(Input.GetAxis("Horizontal"))*40, 8f);
                Invoke("WallJump", 0.01f);
            }
        }*/
    }

    void WallJump()
    {
        rigid.velocity += new Vector2(-(Input.GetAxis("Horizontal")) * 40, 0);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Input_Move.againstWall = true;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        Input_Move.againstWall = false;
    }
}
