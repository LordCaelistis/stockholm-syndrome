using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Walljump : MonoBehaviour
{
    //GameSystems Controllers
    [SerializeField]
    public KnightController scriptMouvement;
    public GameObject personnage;
    Rigidbody2D rigid;
    bool againstWall;
    int controllerNumber;

    // Use this for initialization
    void Start()
    {
        rigid = personnage.GetComponent<Rigidbody2D>();
        controllerNumber = scriptMouvement.getSetControllerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (againstWall == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                againstWall = false;
                rigid.velocity += new Vector2(-(Input.GetAxis("L_XAxis_" + controllerNumber))*40, 8f);
                Invoke("WallJump", 0.01f);
            }
        }*/
    }

    void WallJump()
    {
        rigid.velocity += new Vector2(-(Input.GetAxis("L_XAxis_" + controllerNumber)) * 40, 0);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag != "prince") scriptMouvement.againstWall[controllerNumber] = true;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag != "prince") scriptMouvement.againstWall[controllerNumber] = false;
    }
}
