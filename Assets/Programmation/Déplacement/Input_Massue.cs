using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Massue : MonoBehaviour
{
    //GameSystems Controllers
    [SerializeField]
    public int controllerNumber = 1;
    public KnightController scriptMouvement;
    public GameObject personnage;
    public Timer gameMaster;
    private int tempPlayerHit;
    Rigidbody2D rigid;
    public List<int> playersToLeft = new List<int>();
    public List<int> playersToRight = new List<int>();

    // Use this for initialization
    void Start()
    {
        rigid = personnage.GetComponent<Rigidbody2D>();
        controllerNumber = scriptMouvement.getSetControllerNumber;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (coll.transform.position.x > personnage.transform.position.x)
            {
                tempPlayerHit = coll.gameObject.transform.GetChild(2).GetComponent<KnightController>().controllerNumber;
                playersToRight.Add(tempPlayerHit);
            }
            if (coll.transform.position.x <= personnage.transform.position.x)
            {
                tempPlayerHit = coll.gameObject.transform.GetChild(2).GetComponent<KnightController>().controllerNumber;
                playersToLeft.Add(tempPlayerHit);
            }
        }
    }


    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            for (int i = 0; i < playersToLeft.Count; i++)
            {
                if (playersToLeft[i] == coll.gameObject.transform.GetChild(2).GetComponent<KnightController>().controllerNumber)
                {
                    playersToLeft.Remove(playersToLeft[i]);
                }
            }
            for (int i = 0; i < playersToRight.Count; i++)
            {
                if (playersToRight[i] == coll.gameObject.transform.GetChild(2).GetComponent<KnightController>().controllerNumber)
                {
                    playersToRight.Remove(playersToRight[i]);
                }
            }
        }
    }
}
