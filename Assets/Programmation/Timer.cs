using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public GameObject PrincePrefab;
    public static Dictionary<int, bool> playerDictionary = new Dictionary<int, bool>();

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
        if (textObject != null){
            textZone = textObject.GetComponent<Text>();
        }
        playerScore = new float[GameObject.FindGameObjectsWithTag("Player").Length];
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            tempControllerNumber = gameObject.transform.GetChild(2).GetComponent<KnightController>().controllerNumber;
            playerDictionary.Add(tempControllerNumber, false);
            playerScore[tempControllerNumber-1] = 0;
        }
        print(playerDictionary[1]);
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Return) && gameOver)
        {
            Array.Clear(playerScore, 0, playerScore.Length);
            playerDictionary.Clear();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        foreach (KeyValuePair<int, bool> item in playerDictionary)
        {
            if (item.Value == true)
            {
                playerScore[(item.Key) - 1] += Time.deltaTime;
            }
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
