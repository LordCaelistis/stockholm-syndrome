using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour {

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
    public AudioClip[] ClonkArmureMarche;
    public AudioClip[] CriDeCoup;
    public AudioClip[] cris;

    // GameObject:
    public GameObject playerGameObject;
    public Rigidbody2D player;
    Rigidbody2D tempRigidBodyPlayer;
    public GameObject PrincePrefab;
    public Rigidbody2D princeRigid;
    public SpriteRenderer sprite;
    public GameObject collisionMassue;
    public Animator animator;

    // Valeurs:
    float vitesse = 6f;
    public bool[] onPlatform;
    public bool[] againstWall;
    public bool facingRight;

    //Valeurs déplacement
    float jumpHeight = 12f;
    float vectorDash = 500f;

    // Cooldowns
    float cooldown_max = 0.1f;
    float cooldown_now = 0f;
    float cooldown_max_airmove = 0.2f;
    float cooldown_now_airmove = 0f;
    float cooldown_max_massue = 0.4f;
    float cooldown_now_massue = 0f;
    float cooldown_max_dash = 0.5f;
    float cooldown_now_dash = 0f;


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


        if (intervalTauntNow <= 0)
        {
            if(Input.GetButtonDown("B_" + controllerNumber))
            {
                SoundStuff.PlayRandomOneShot(sourceSon, sonTaunt, 0.4f);
                intervalTauntNow += intervalTauntMax;
            }

        }
        else
        {
            intervalTauntNow -= Time.deltaTime;
        }

        /*if(Input.GetButtonDown("Y_" + controllerNumber)){
            animator.SetBool("isParrying", true);
            SoundStuff.PlayRandomOneShot(sourceSon, ClonkArmureMarche, 0.4f);
        }
        if(Input.GetButtonUp("Y_" + controllerNumber)){
            animator.SetBool("isParrying", false);
            SoundStuff.PlayRandomOneShot(sourceSon, ClonkArmureMarche, 0.4f);
        }*/

        if(Input.GetButtonDown("Y_" + controllerNumber) && cooldown_now_dash <= 0 && Timer.playerArray[controllerNumber] == false)
        {
            vitesse = 15f;
            cooldown_now_airmove += cooldown_max_airmove;
            cooldown_now_dash += cooldown_max_dash;
            player.velocity = new Vector2(vectorDash, 0);
            Invoke("resetSpeed", 0.3f);
        }


        if (cooldown_now <= 0)
        {
            if(Input.GetButtonDown("A_"+controllerNumber))
            {
                //Saut depuis une plateforme
                print(playerGameObject);
                if (playerGameObject.GetComponent<Input_Jump>().plateformCount > 0)
                {
                    //print(getSetOnPlatform + "" + controllerNumber);
                    player.velocity += new Vector2(0, jumpHeight);
                    onPlatform[controllerNumber] = false;

                    SoundStuff.PlayRandomOneShot(sourceSon, sonsSaut, 0.3f);

                    cooldown_now += cooldown_max;
                    animator.SetBool("isJumping", true);
                }
                //Walljump
                else if (againstWall[controllerNumber] == true)
                {
                    //Léger cooldown pour forcer le joueur a décoller du mur lors d'un walljump
                    cooldown_now_airmove += cooldown_max_airmove;
                    player.velocity += new Vector2((-(Input.GetAxis("L_XAxis_"+controllerNumber)) * 400), jumpHeight-2);

                    SoundStuff.PlayRandomOneShot(sourceSon, sonsSaut, 0.3f);

                    cooldown_now += cooldown_max;
                    animator.SetBool("isJumping", true);
                }
            }

        }
        else cooldown_now -= Time.deltaTime;

        //Si le coup de massue est pas en cooldown, on FRAPPE
        if (cooldown_now_massue <= 0) {
            if (Input.GetButtonDown("X_"+controllerNumber)) {
                MassueStrike();
                cooldown_now_massue += cooldown_max_massue;

                animator.SetTrigger("strike");

                Invoke("playAudio", 0.3f);
                SoundStuff.PlayRandomOneShot(sourceSon, ClonkArmureMarche, 0.1f);
            }
        }
        else cooldown_now_massue -= Time.deltaTime;

        //Réduction du cooldown du mouvement aérien, et du dash
        if (cooldown_now_airmove > 0)  cooldown_now_airmove -= Time.deltaTime;
        if(cooldown_now_airmove < 0) cooldown_now_airmove = 0;
        if (cooldown_now_dash > 0) cooldown_now_dash -= Time.deltaTime;
        if (cooldown_now_dash < 0) cooldown_now_dash = 0;

        // Déplacement (seulement si le déplacement n'est pas en cooldown):
        if (cooldown_now_airmove <= 0f && Input.GetAxis("L_XAxis_" + controllerNumber)!=0)
        {
            player.velocity += new Vector2(Input.GetAxis("L_XAxis_"+controllerNumber), 0);

            animator.SetBool("isRunning", true);

            if(intervalFootstepsNow <= 0 && onPlatform[controllerNumber] == true)
            {
                SoundStuff.PlayRandomOneShot(sourceSon, ClonkArmureMarche, 0.1f);
                if(solType == "Herbe")  SoundStuff.PlayRandomOneShot(sourceSon, footstepsHerbe, 0.25f);
                if(solType == "Bois")   SoundStuff.PlayRandomOneShot(sourceSon, footstepBois,   0.25f);
                if(solType == "Glace")  SoundStuff.PlayRandomOneShot(sourceSon, footstepGlace,  0.25f);
                if(solType == "Pierre") SoundStuff.PlayRandomOneShot(sourceSon, footstepPierre, 0.25f);

                intervalFootstepsNow += intervalFootstepsMax;
            }
            else
            {
                intervalFootstepsNow -= Time.deltaTime;
            }

        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // Orientation:
        UpdateFlip();

        // Saut:
        player.velocity = new Vector2(Mathf.Clamp(player.velocity.x, -vitesse, vitesse), player.velocity.y);
    }

    void resetSpeed()
    {
        vitesse = 6f;
    }

    void MassueStrike()
    {
        if (facingRight == true && collisionMassue.GetComponent<Input_Massue>().playersToRight.Count > 0)
        {

            Invoke("PlayAudioHit", 0.3f);

            for (int i = 0; i < collisionMassue.GetComponent<Input_Massue>().playersToRight.Count; i++)
            {
                foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if (gameObject.transform.GetChild(2).GetComponent<KnightController>().controllerNumber == collisionMassue.GetComponent<Input_Massue>().playersToRight[i])
                    {
                        GetBonked(gameObject);
                    }
                }
                //Si un des ennemis frappés possède la princesse...
                if (Timer.playerArray[collisionMassue.GetComponent<Input_Massue>().playersToRight[i]] == true)
                {
                    foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        if(gameObject.transform.GetChild(2).GetComponent<KnightController>().controllerNumber == collisionMassue.GetComponent<Input_Massue>().playersToRight[i])
                        {
                            PrincePrefab.AddComponent<Rigidbody2D>();
                            PrincePrefab.GetComponent<Animator>().SetBool("isCarried", false);
                            PrincePrefab.GetComponent<BoxCollider2D>().enabled = true;
                            PrincePrefab.GetComponent<PrinceBehavior>().cooldown_now += PrincePrefab.GetComponent<PrinceBehavior>().cooldown_max;
                            Vector3 pos0 = gameObject.transform.position;
                            Quaternion rot0 = Quaternion.identity;
                            //GameObject prince0 = (GameObject)Instantiate(PrincePrefab, pos0, rot0);
                            princeRigid = PrincePrefab.GetComponent<Rigidbody2D>();
                            princeRigid.velocity += new Vector2(0, 8);
                            Timer.playerArray[collisionMassue.GetComponent<Input_Massue>().playersToRight[i]] = false;
                        }
                    }
                }
            }
        }

        else if (facingRight == false && collisionMassue.GetComponent<Input_Massue>().playersToLeft.Count > 0)
        {


            Invoke("PlayAudioHit", 0.3f);


            for (int i = 0; i < collisionMassue.GetComponent<Input_Massue>().playersToLeft.Count; i++)
            {
                foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if (gameObject.transform.GetChild(2).GetComponent<KnightController>().controllerNumber == collisionMassue.GetComponent<Input_Massue>().playersToLeft[i])
                    {
                        GetBonked(gameObject);
                    }
                }
                //Si un des ennemis frappés possède la princesse...
                if (Timer.playerArray[collisionMassue.GetComponent<Input_Massue>().playersToLeft[i]] == true)
                {
                    foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        if (gameObject.transform.GetChild(2).GetComponent<KnightController>().controllerNumber == collisionMassue.GetComponent<Input_Massue>().playersToLeft[i])
                        {
                            PrincePrefab.AddComponent<Rigidbody2D>();
                            PrincePrefab.GetComponent<BoxCollider2D>().enabled = true;
                            PrincePrefab.GetComponent<PrinceBehavior>().cooldown_now += PrincePrefab.GetComponent<PrinceBehavior>().cooldown_max;
                            Vector3 pos0 = gameObject.transform.position;
                            Quaternion rot0 = Quaternion.identity;
                            //GameObject prince0 = (GameObject)Instantiate(PrincePrefab, pos0, rot0);
                            princeRigid = PrincePrefab.GetComponent<Rigidbody2D>();
                            princeRigid.velocity += new Vector2(0, 8);
                            Timer.playerArray[collisionMassue.GetComponent<Input_Massue>().playersToLeft[i]] = false;
                        }
                    }
                }
            }
        }
    }

    void GetBonked(GameObject gameObject)
    {
        gameObject.GetComponent<Animator>().SetTrigger("hit");
        tempRigidBodyPlayer = gameObject.GetComponent<Rigidbody2D>();
        if(sprite.flipX == false) tempRigidBodyPlayer.velocity = new Vector2(25f, 10f);
        if (sprite.flipX == true) tempRigidBodyPlayer.velocity = new Vector2(-25f, 10f);
        gameObject.transform.GetChild(2).GetComponent<KnightController>().cooldown_now_airmove = 1f;
        print(gameObject.transform.GetChild(2).GetComponent<KnightController>().cooldown_now_airmove);
    }

    // Orientation:
    void UpdateFlip()
    {
        if (player.velocity.x > 0.01)
        {
            sprite.flipX = false;
            facingRight = true;
            vectorDash = 500;
            //collisionMassue.GetComponent<Input_Massue>.playersHit.Clear();
        }
        else if (player.velocity.x < -0.01)
        {
            sprite.flipX = true;
            facingRight = false;
            vectorDash = -500;
        }
    }

    void playAudio()
    {
        SoundStuff.PlayRandomOneShot(sourceSon, CriDeCoup, 0.4f);
        SoundStuff.PlayRandomOneShot(sourceSon, ClonkArmureMarche, 0.1f);
        sourceSon.PlayOneShot(MassueVent, 0.7f);
    }

    void PlayAudioHit()
    {
        SoundStuff.PlayRandomOneShot(sourceSon, MassueTouche, 0.3f);
        SoundStuff.PlayRandomOneShot(sourceSon, cris, 0.3f);
    }

}
