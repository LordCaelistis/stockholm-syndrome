using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    // Scores :
    float scoreP1 = 0;
    float scoreP2 = 0;

    public GameObject player1;
    public GameObject player2;
    public static bool P1HasPrince = false;
    public static bool P2HasPrince = false;
    public GameObject PrincePrefab;
    Rigidbody2D princeRigid;

    // Zones text :
    [SerializeField]
    public GameObject textObject;
    Text textZone;

    // Use this for initialization
    void Start () {
        if(textObject != null){
            textZone = textObject.GetComponent<Text>();
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1")) {
            print(P1HasPrince + "Fire1");
        }
        if (Input.GetButtonDown("Fire2")) {
            print(P2HasPrince + "Fire2");
        }
        //Fait spawn un prince fonctionnel on-hit
        if (Input.GetKeyDown("t"))
        {
            Vector3 pos0 = player1.transform.position;
            Quaternion rot0 = Quaternion.identity;
            GameObject prince0 = (GameObject)Instantiate(PrincePrefab, pos0, rot0);
            princeRigid = prince0.GetComponent<Rigidbody2D>();
            princeRigid.velocity += new Vector2(0, 8);
        }

        scoreP1 += P1HasPrince ? Time.deltaTime : 0;
        scoreP2 += P2HasPrince ? Time.deltaTime : 0;
        print("P1 : " + scoreP1 + " | P2 : " + scoreP2);

        // Affichage texte :
        if (textZone != null){
            textZone.text = "P1 : "+scoreP1+" | P2 : "+scoreP2;
        }
    }
}
