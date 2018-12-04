using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinceBehavior : MonoBehaviour {

    public GameObject PrincePrefab;

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
            if (coll.name == "Player1")
            {
                print(coll.name);
                Timer.P1HasPrince = true;
                Timer.P2HasPrince = false;
                Destroy(gameObject);
            }

            if (coll.name == "Player2")
            {
                Timer.P1HasPrince = false;
                Timer.P2HasPrince = true;
                Destroy(gameObject);
            }
        }
    }
}
