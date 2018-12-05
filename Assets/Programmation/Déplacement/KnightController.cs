using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour {

    private Animator animator;

    //GameSystems Controllers
    [SerializeField]
    public int controllerNumber = 1;

    // GameObject:
    public Rigidbody2D player;
    public SpriteRenderer sprite;

    // Valeurs:
    float vitesse = 3.5f;
    public bool[] onPlatform;
    public bool[] againstWall;

    // Cooldowns
    float cooldown_max = 0.2f;
    float cooldown_now = 0f;
    float cooldown_max_airmove = 0.05f;
    float cooldown_now_airmove = 0f;
    float cooldown_max_massue = 0.5f;
    float cooldown_now_massue = 0f;


    public int getSetControllerNumber
    {
        get { return controllerNumber; }
        set { controllerNumber = value; }
    }

    // Use this for initialization
    void Start () {
        onPlatform = new bool[controllerNumber + 1];
        onPlatform[controllerNumber] = true;
        againstWall = new bool[controllerNumber + 1];
        onPlatform[controllerNumber] = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (cooldown_now <= 0)
        {
            if(Input.GetButtonDown("A_"+controllerNumber))
            {
                //Walljump
                if (againstWall[controllerNumber] == true)
                {
                    //Léger cooldown pour forcer le joueur a décoller du mur lors d'un walljump
                    cooldown_now_airmove += cooldown_max_airmove;
                    player.velocity += new Vector2(-(Input.GetAxis("L_XAxis_"+controllerNumber)) * 40, 8f);
                }
                //Saut depuis une plateforme
                if (onPlatform[controllerNumber] == true)
                {
                    //print(getSetOnPlatform + "" + controllerNumber);
                    player.velocity += new Vector2(0, 10);
                    onPlatform[controllerNumber] = false;
                }
                cooldown_now += cooldown_max;
            }
            
        }
        else cooldown_now -= Time.deltaTime;

        //Si le coup de massue est pas en cooldown, on FRAPPE
        if (cooldown_now_massue <= 0) {
            if (Input.GetButtonDown("TriggersR_"+controllerNumber)) {
                MassueStrike();
                cooldown_now_massue += cooldown_max_massue;
            }
        }        
        else cooldown_now_massue -= Time.deltaTime;

        //Réduction du cooldown du mouvement aérien
        if (cooldown_now_airmove > 0)  cooldown_now_airmove -= Time.deltaTime;
        if(cooldown_now_airmove < 0) cooldown_now_airmove = 0;
        
        // Déplacement (seulement si le déplacement n'est pas en cooldown):
        if(cooldown_now_airmove <= 0f) player.velocity += new Vector2(Input.GetAxis("L_XAxis_"+controllerNumber), 0);

        // Orientation:
        UpdateFlip();

        // Saut:
        player.velocity = new Vector2(Mathf.Clamp(player.velocity.x, -vitesse, vitesse), player.velocity.y);
    }

    void MassueStrike()
    {
        //animator.SetInteger("attack", 2);
        //Invoke("ResetAttack", 0.2f);
        float directionRaycast = 1;
        //LayerMask mask = LayerMask.GetMask("BreakableObject");
        float positionStartRaycastX = (player.transform.position.x) + 0.2f;
        Vector3 raycastStart = new Vector3(positionStartRaycastX, player.transform.position.y, player.transform.position.z);
        Vector3 drawLineEnd = new Vector3(positionStartRaycastX + 0.5f, player.transform.position.y, player.transform.position.z);
        RaycastHit2D pointContact2d = Physics2D.Raycast(raycastStart, new Vector2(directionRaycast, 0), 0.5f/*, mask*/);
        if (pointContact2d.collider)
        {
            //Debug.DrawLine(raycastStart, drawLineEnd, Color.white, 2.5f);
            print(pointContact2d.collider.gameObject);
        }
    }

    // Orientation:
    void UpdateFlip()
    {
        if (player.velocity.x > 0)
        {
            sprite.flipX = false;
        }
        else if (player.velocity.x < 0)
        {
            sprite.flipX = true;
        }
    }
}