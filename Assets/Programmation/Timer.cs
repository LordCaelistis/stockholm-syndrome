using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    // Scores :
    float scoreP1 = 0;
    float scoreP2 = 0;

    bool P1HasPrince = false;
    bool P2HasPrince = false;

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
        if (Input.GetKeyDown("&")) {
            P2HasPrince = false;
            P1HasPrince = !P1HasPrince;
        }
        if (Input.GetKeyDown("é")) {
            P1HasPrince = false;
            P2HasPrince = !P2HasPrince;
        }

        scoreP1 += P1HasPrince ? Time.deltaTime : 0;
        scoreP2 += P2HasPrince ? Time.deltaTime : 0;

        // Affichage texte :
        if(textZone != null){
            textZone.text = "P1 : "+scoreP1+" | P2 : "+scoreP2;
        }
    }
}
