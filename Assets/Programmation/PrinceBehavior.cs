﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinceBehavior : MonoBehaviour {

    public GameObject PrincePrefab;
    private int tempControllerNumber;

    float cooldown_max = 1f;
    float cooldown_now = 0f;

    // Use this for initialization
    void Start () {
        cooldown_now += cooldown_max;
        print("Coucou");
    }
	
	// Update is called once per frame
	void Update () {
        //frames d'invincibilité lorsque le Prince spawn
		if (cooldown_now > 0) cooldown_now -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (cooldown_now <= 0)
        {
            Timer.hasPrince = coll.name;
            if (coll.tag == "Player")
            {
                print(coll.gameObject.transform.GetChild(2).GetComponent<KnightController>().controllerNumber);
                tempControllerNumber = coll.gameObject.transform.GetChild(2).GetComponent<KnightController>().controllerNumber;
                Timer.playerArray[tempControllerNumber] = true;
                Destroy(gameObject);
            }

            /*if (coll.name == "Player2")
            {
                Timer.P1HasPrince = false;
                Timer.P2HasPrince = true;
                Destroy(gameObject);
            }*/
        }
    }
}
