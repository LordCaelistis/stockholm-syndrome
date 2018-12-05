using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Jump : MonoBehaviour {
    
    // public GameObject personnage;
    public KnightController scriptMouvement;
    bool onPlatform;
    int controllerNumber;

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
        if (coll.tag != "prince") scriptMouvement.onPlatform[controllerNumber] = true;
        //print(scriptMouvement.player);
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag != "prince") scriptMouvement.onPlatform[controllerNumber] = false;
    }
}
