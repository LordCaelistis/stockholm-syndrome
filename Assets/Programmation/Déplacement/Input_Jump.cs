using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Jump : MonoBehaviour {
    
    public GameObject personnage;
    Rigidbody2D rigid;
    bool onPlatform;

	// Use this for initialization
	void Start () {
        rigid = personnage.GetComponent<Rigidbody2D>();
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
        Input_Move.onPlatform = true;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        Input_Move.onPlatform = false;
    }
}
