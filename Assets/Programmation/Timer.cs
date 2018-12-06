using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public static string hasPrince = null;
    public static bool P1HasPrince = false;
    public static bool P2HasPrince = false;
    public GameObject PrincePrefab;
    public static Dictionary<int, bool> playerArray = new Dictionary<int, bool>();
    public float[] playerScore;
    public bool gameOver = false;
    private int winningPlayer;
    Rigidbody2D princeRigid;

    private int tempControllerNumber;

    // Zones text :
    [SerializeField]
    public GameObject textObject;
    Text textZone;

    // Use this for initialization
    void Start () {
        if(textObject != null){
            textZone = textObject.GetComponent<Text>();
        }
        playerScore = new float[GameObject.FindGameObjectsWithTag("Player").Length];
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            tempControllerNumber = gameObject.transform.GetChild(2).GetComponent<KnightController>().controllerNumber;
            playerArray.Add(tempControllerNumber, false);
            playerScore[tempControllerNumber-1] = 0;
        }
        print(playerArray);
    }

	// Update is called once per frame
	void Update () {
        /*if (Input.GetKeyDown("e")) {
            print(playerArray[1] + "Fire1");
        }
        if (Input.GetKeyDown("r")) {
            print(playerArray[2] + "Fire2");
        }
        //Quand le Player1 se prend un gnon
        if (Input.GetKeyDown("t") && P1HasPrince)
        {
            Vector3 pos0 = player1.transform.position;
            Quaternion rot0 = Quaternion.identity;
            GameObject prince0 = (GameObject)Instantiate(PrincePrefab, pos0, rot0);
            princeRigid = prince0.GetComponent<Rigidbody2D>();
            princeRigid.velocity += new Vector2(0, 8);
            P1HasPrince = !P1HasPrince;
        }*/

        
        foreach (KeyValuePair<int, bool> item in playerArray)
        {
            if (item.Value == true) playerScore[(item.Key) - 1] += Time.deltaTime;
            print(playerScore[(item.Key) - 1]);
            if (playerScore[(item.Key) - 1] >= 3)
            {
                winningPlayer = item.Key;
                gameOver = true;
            }
            //print("Key: {0}, Value: {1}" + item.Key + "" + item.Value);
        }
        //print(playerArray.Count);

        /*scoreP1 += P1HasPrince ? Time.deltaTime : 0;
        scoreP2 += P2HasPrince ? Time.deltaTime : 0;*/
        //print("P1 : " + playerScore[0] + " | P2 : " + playerScore[1]);

        // Affichage texte :
        if (textZone != null && gameOver == false){
            textZone.text = "P1 : " + playerScore[0] + " | P2 : " + playerScore[1];
        } else if (textZone != null && gameOver == true)
        {
            textZone.text = "Player" + winningPlayer + " wins!";
        }
    }
}
