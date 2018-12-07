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
    public float winningScore = 3f;
    public bool gameOver = false;
    private int winningPlayer;
    Rigidbody2D princeRigid;

    public Image victoryPlayer1;
    public Image victoryPlayer2;

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

        victoryPlayer1.enabled = false;
        victoryPlayer2.enabled = false;

        foreach (KeyValuePair<int, bool> item in playerArray)
        {
            if (item.Value == true) playerScore[(item.Key) - 1] += Time.deltaTime;
            // print(playerScore[(item.Key) - 1]);
            if (playerScore[(item.Key) - 1] >= winningScore)
            {
                winningPlayer = item.Key;
                gameOver = true;
            }
        }

        // Affichage texte :
        if (textZone != null && gameOver == false){
            textZone.text = "P1 : " + playerScore[0] + " | P2 : " + playerScore[1];
        } else if (textZone != null && gameOver == true)
        {
            if (winningPlayer == 1) victoryPlayer1.enabled = true;
            if (winningPlayer == 2) victoryPlayer2.enabled = true;
        }
    }
}
