using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour {

    private Animator animator;

    //GameSystems Controllers
    [SerializeField]
    public int controllerNumber = 1;

    public string solType;
    // Son
    public AudioSource sourceSon;
    public AudioClip[] sonsSaut;

    public AudioClip[] footstepsHerbe;
    float intervalFootstepsMax = 0.3f;
    float intervalFootstepsNow = 0f;

    public AudioClip[] footstepBois;
    public AudioClip[] footstepGlace;
    public AudioClip[] footstepPierre;

    public AudioClip[] sonTaunt;
    float intervalTauntMax = 3f;
    float intervalTauntNow = 0f;

    public AudioClip MassueVent;
    public AudioClip[] MassueTouche;
    public AudioClip[] CriDeCoup;
    public AudioClip[] CriTouche;

    public AudioClip[] ClonkArmureMarche;

    // GameObject:
    public Rigidbody2D player;
    public GameObject PrincePrefab;
    public Rigidbody2D princeRigid;
    public SpriteRenderer sprite;

    // Valeurs:
    float vitesse = 3.5f;
    float directionRaycast;
    float positionStartRaycastX;
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


        if (Input.GetButtonDown("B_" + controllerNumber) && (intervalTauntNow <= 0))
        {

            SoundStuff.PlayRandomOneShot(sourceSon, sonTaunt, 0.4f);
            intervalTauntNow += intervalTauntMax;
        }
        else
        {
            intervalTauntNow -= Time.deltaTime;
        }
        


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

                    SoundStuff.PlayRandomOneShot(sourceSon, sonsSaut, 0.4f);
                }
                //Saut depuis une plateforme
                if (onPlatform[controllerNumber] == true)
                {
                    //print(getSetOnPlatform + "" + controllerNumber);
                    player.velocity += new Vector2(0, 10);
                    onPlatform[controllerNumber] = false;

                    SoundStuff.PlayRandomOneShot(sourceSon, sonsSaut, 0.4f);
                }
                cooldown_now += cooldown_max;
            }

        }
        else cooldown_now -= Time.deltaTime;

        //Si le coup de massue est pas en cooldown, on FRAPPE
        if (cooldown_now_massue <= 0) {
            if (Input.GetButtonDown("X_"+controllerNumber)) {
                MassueStrike();
                cooldown_now_massue += cooldown_max_massue;

                sourceSon.PlayOneShot(MassueVent, 0.7f);
                SoundStuff.PlayRandomOneShot(sourceSon, CriDeCoup, 0.4f);
                SoundStuff.PlayRandomOneShot(sourceSon, ClonkArmureMarche, 0.3f);
            }
        }
        else cooldown_now_massue -= Time.deltaTime;

        //Réduction du cooldown du mouvement aérien
        if (cooldown_now_airmove > 0)  cooldown_now_airmove -= Time.deltaTime;
        if(cooldown_now_airmove < 0) cooldown_now_airmove = 0;

        // Déplacement (seulement si le déplacement n'est pas en cooldown):
        if (cooldown_now_airmove <= 0f && Input.GetAxis("L_XAxis_" + controllerNumber)!=0) {
            player.velocity += new Vector2(Input.GetAxis("L_XAxis_" + controllerNumber), 0);

            if(intervalFootstepsNow <= 0 && onPlatform[controllerNumber] == true)
            {
                print(solType);

                if (solType=="Herbe")
                {
                    SoundStuff.PlayRandomOneShot(sourceSon, footstepsHerbe, 0.3f);
                }
                else if (solType=="Bois")
                {
                   SoundStuff.PlayRandomOneShot(sourceSon, footstepBois, 0.3f);
                }
                else if (solType == "Glace")
                {
                    SoundStuff.PlayRandomOneShot(sourceSon, footstepGlace, 0.3f);
                }
                else if (solType == "Pierre")
                {
                    SoundStuff.PlayRandomOneShot(sourceSon, footstepPierre, 0.3f);
                }

                intervalFootstepsNow += intervalFootstepsMax;
            }
            else
            {
                intervalFootstepsNow -= Time.deltaTime;
            }
            
        };

        // Orientation:
        UpdateFlip();

        // Saut:
        player.velocity = new Vector2(Mathf.Clamp(player.velocity.x, -vitesse, vitesse), player.velocity.y);
    }

    void MassueStrike()
    {
        //animator.SetInteger("attack", 2);
        //Invoke("ResetAttack", 0.2f);
        LayerMask mask = LayerMask.GetMask("Default");
        Vector3 raycastStart = new Vector3(positionStartRaycastX, player.transform.position.y, player.transform.position.z);
        //Vector3 drawLineEnd = new Vector3(positionStartRaycastX + 0.5f, player.transform.position.y, player.transform.position.z);
        RaycastHit2D pointContact2d = Physics2D.Raycast(raycastStart, new Vector2(directionRaycast, 0), 0.5f, mask);
        if (pointContact2d.collider && pointContact2d.collider.tag == "Player")
        {

            
            SoundStuff.PlayRandomOneShot(sourceSon, MassueTouche, 0.3f);           
            SoundStuff.PlayRandomOneShot(sourceSon, CriDeCoup, 0.5f);
            SoundStuff.PlayRandomOneShot(sourceSon, CriTouche, 0.5f);


            //Debug.DrawLine(raycastStart, drawLineEnd, Color.white, 2.5f);
            print(pointContact2d.collider.transform.GetChild(2).GetComponent<KnightController>().controllerNumber);
            if(Timer.playerArray[pointContact2d.collider.transform.GetChild(2).GetComponent<KnightController>().controllerNumber] == true)
            {
                print("Il avait la princesse !");
                Vector3 pos0 = pointContact2d.collider.transform.position;
                Quaternion rot0 = Quaternion.identity;
                GameObject prince0 = (GameObject)Instantiate(PrincePrefab, pos0, rot0);
                princeRigid = prince0.GetComponent<Rigidbody2D>();
                princeRigid.velocity += new Vector2(0, 8);
                Timer.playerArray[pointContact2d.collider.transform.GetChild(2).GetComponent<KnightController>().controllerNumber] = false;
            }
        }
        /*if (Input.GetKeyDown("t") && P1HasPrince)
        {
            Vector3 pos0 = player1.transform.position;
            Quaternion rot0 = Quaternion.identity;
            GameObject prince0 = (GameObject)Instantiate(PrincePrefab, pos0, rot0);
            princeRigid = prince0.GetComponent<Rigidbody2D>();
            princeRigid.velocity += new Vector2(0, 8);
            P1HasPrince = !P1HasPrince;
        }*/
    }

    // Orientation:
    void UpdateFlip()
    {
        if (player.velocity.x > 0.01)
        {
            sprite.flipX = false;
            directionRaycast = 1;
            positionStartRaycastX = (player.transform.position.x) + 0.2f;
        }
        else if (player.velocity.x < -0.01)
        {
            sprite.flipX = true;
            directionRaycast = -1;
            positionStartRaycastX = (player.transform.position.x) - 0.2f;
        }
    }
}
