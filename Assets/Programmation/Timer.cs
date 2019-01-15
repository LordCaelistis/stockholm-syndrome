using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public GameObject PrincePrefab;
    public static Dictionary<int, bool> playerArray = new Dictionary<int, bool>();
    public float[] playerScore;
    public float winningScore = 3f;
    public bool gameOver = false;
    private int winningPlayer = 0;
    Rigidbody2D princeRigid;

    public Animator VictoryPlayer1;
    public Animator VictoryPlayer2;

    private int tempControllerNumber;

    // Zones text :
    [SerializeField]
    public GameObject textObject;
    Text textZone;

    // Use this for initialization
    void Start () {
        //VictoryPlayer1.enabled = false;
        //VictoryPlayer2.enabled = false;
        if (textObject != null){
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

        foreach (KeyValuePair<int, bool> item in playerArray)
        {
            if (item.Value == true)
            {
                playerScore[(item.Key) - 1] += Time.deltaTime;
            }
            // print(playerScore[(item.Key) - 1]);
            if (playerScore[(item.Key) - 1] >= winningScore)
            {
                winningPlayer = item.Key;
                gameOver = true;
                foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
                {
                    Destroy(gameObject);
                }
            }
        }

        /*scoreP1 += P1HasPrince ? Time.deltaTime : 0;
        scoreP2 += P2HasPrince ? Time.deltaTime : 0;*/
        //print("P1 : " + playerScore[0] + " | P2 : " + playerScore[1]);

        // Affichage texte :
        if (textZone != null && gameOver == false){
            textZone.text = "P1 : " + playerScore[0] + " | P2 : " + playerScore[1];
        } else if (textZone != null && gameOver == true)
        {
            if(winningPlayer == 1) VictoryPlayer1.SetFloat("VictoryTrigger", 1f);
            if(winningPlayer == 2) VictoryPlayer2.SetFloat("VictoryTrigger2", 1f);
        }
    }
}
