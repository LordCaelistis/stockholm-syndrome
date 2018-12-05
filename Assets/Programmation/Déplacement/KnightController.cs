using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour {


    // GameSystem Déplacement Joueur:
    [SerializeField]
    public int controllerNumber = 1;

    // GameObject:
    public Rigidbody2D player;
    public SpriteRenderer sprite;

    // Valeurs:
    float vitesse = 3.5f;
    public static bool onPlatform = true;
    public static bool againstWall = false;

    // Cooldown exemple
    float cooldown_max = 0.2f;
    float cooldown_now = 0f;
    float cooldown_max_airmove = 0.05f;
    float cooldown_now_airmove = 0f;

    // Use this for initialization
    void Start () {
    }

	// Update is called once per frame
	void Update () {
        if (cooldown_now <= 0)
        {
            if(Input.GetButtonDown("A_"+controllerNumber))
            {
                if (againstWall == true)
                {
                    cooldown_now_airmove += cooldown_max_airmove;
                    player.velocity += new Vector2(-(Input.GetAxis("L_XAxis_"+controllerNumber)) * 40, 8f);
                }
                if (onPlatform == true)
                {
                    player.velocity += new Vector2(0, 10);
                    onPlatform = false;
                }
                cooldown_now += cooldown_max;
            }
        }
        else
        {
            cooldown_now -= Time.deltaTime;
        }

        if(cooldown_now_airmove > 0)
        {
            cooldown_now_airmove -= Time.deltaTime;
        }
        if (cooldown_now_airmove < 0) cooldown_now_airmove = 0;

        // Déplacement:
        if(cooldown_now_airmove <= 0f) player.velocity += new Vector2(Input.GetAxis("L_XAxis_"+controllerNumber), 0);

        // Orientation:
        UpdateFlip();

        // Saut:
        player.velocity = new Vector2(Mathf.Clamp(player.velocity.x, -vitesse, vitesse), player.velocity.y);
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
