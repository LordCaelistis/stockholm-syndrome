using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Jump : MonoBehaviour {
    
    public GameObject personnage;
    bool onPlatform;

	// Use this for initialization
	void Start () {
<<<<<<< Updated upstream
=======
        rigid = personnage.GetComponent<Rigidbody2D>();
		
>>>>>>> Stashed changes
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
        if (coll.tag != "prince") KnightController.onPlatform = true;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag != "prince") KnightController.onPlatform = false;
    }
}
