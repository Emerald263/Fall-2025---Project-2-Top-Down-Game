using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading; //importing SceneManagement Library
using static BattleManager;
using System;

public class Player_Controller : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public float Battleorder;
    public float EXPfinal;
    public float GPfinal;


    public float speed;
    public float sprint;
    private SpriteRenderer sr;
    public bool hasKey = false;

    //audio variables
    public AudioSource soundEffects;
    public AudioClip[] sounds; // Public variable to access the Audio Source component

    //sprite variables
    public Sprite upSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite frontSprite;
    public Sprite battleSprite;

    //character variables
    public GameObject inventory; //inventory UI
    public float armor;
    public float SP;
    public float attack;
    public float spell;
    public float HP;
    public float EXP;
    public float GP;
    public float Level;

    //Hollow stats
    public float HollowSP;
    public float Hollowattck;
    public float HollowDef;
    public float HollowHP;
    public float HollowEXP;
    public float HollowSpell;

    //Frost stats
    public float FrostSP;
    public float Frostattck;
    public float FrostDef;
    public float FrostHP;
    public float FrostEXP;
    public float FrostSpell;

    //armor & items
    public float armoradd;
    public float SPadd;
    public float attckadd;
    public float splladd;

    public int inventimer;


    public Playerstates State;
    public enum Playerstates
    {
        Overworld = 1,
        Battle = 2,
        Shop1 = 3,
        Shop2 = 4,
        Inventory = 5,

    }

    //inventory variables
    public float CandyApple;
    public float IceCream;
    public float Armorwear;

    //public Rigidbody2D rb;

    public static Player_Controller instance;
    // Start is called before the first frame update
    void Start()
    {

        soundEffects = GetComponent<AudioSource>();
        State = Playerstates.Overworld;
        inventory.SetActive(false);
        inventimer = 2;

        sr = GetComponent<SpriteRenderer>();
        if (instance != null) //if another instance of the player is in the scene
        {
            Destroy(gameObject); //then destroy it
        }

        instance = this; //reassign the instance to the current player
        GameObject.DontDestroyOnLoad(this.gameObject);

        armor = 5;
        attack = 10;
        spell = 20;
        HP = 20;
        EXP = 0;
        Level = 1;
        GP = 250;

    }

    // Update is called once per frame
    void Update()
    {

        if (State == Playerstates.Overworld)
        {

            Vector3 newPosition = transform.position;
        
        //go up
        if (Input.GetKey("w"))
            {
                newPosition.y += speed;
                //change sprite to up sprite
                sr.sprite = upSprite;

            }

            //go left
            if (Input.GetKey("a"))
            {
                newPosition.x -= speed;
                //change sprite to left sprite
                sr.sprite = leftSprite;

            }

            //go down
            if (Input.GetKey("s"))
            {
                newPosition.y -= speed;
                //change sprite to down sprite
                sr.sprite = frontSprite;

            }

            //go right
            if (Input.GetKey("d"))
            {
                newPosition.x += speed;
                //change sprite to right sprite
                sr.sprite = rightSprite;

            }

            //go up
            if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift))
            {
                newPosition.y += speed + sprint;
                //change sprite to up sprite
                sr.sprite = upSprite;

            }

            //go left
            if (Input.GetKey("a") && Input.GetKey(KeyCode.LeftShift))
            {
                newPosition.x -= speed + sprint;
                //change sprite to left sprite
                sr.sprite = leftSprite;

            }

            //go down
            if (Input.GetKey("s") && Input.GetKey(KeyCode.LeftShift))
            {
                newPosition.y -= speed + sprint;
                //change sprite to down sprite
                sr.sprite = frontSprite;

            }

            //go right
            if (Input.GetKey("d") && Input.GetKey(KeyCode.LeftShift))
            {
                newPosition.x += speed + sprint;
                //change sprite to right sprite
                sr.sprite = rightSprite;

            }


            transform.position = newPosition;


            if (Input.GetKey("e"))
            {
                State = Playerstates.Inventory;


              
            }

            

        }

        if (State == Playerstates.Inventory)
        {
            Debug.Log("Inventory");
            inventory.SetActive(true);

        }

        if (Input.GetKey("x") && (State == Playerstates.Inventory))
        {
            inventory.SetActive(false);
            Debug.Log("Hide Inventory");
            State = Playerstates.Overworld;

        }


       

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if colliding with a game object with specific tag
        if (collision.gameObject.tag.Equals("Shop 1") && (Input.GetKey("w")))
        {
            Debug.Log("change scene");
            State = Playerstates.Shop1;
            SceneManager.LoadScene(4);
            
        }

        if (collision.gameObject.tag.Equals("Shop 2") && (Input.GetKey("w")))
        {
            Debug.Log("change scene");
            State = Playerstates.Shop2;
            SceneManager.LoadScene(3);
        }

        if (collision.gameObject.tag.Equals("Key"))
        {
            Debug.Log("obtained key");
            hasKey = true; //player has the key now
            
        }

        if (collision.gameObject.tag.Equals("Town"))
        {
            Debug.Log("Enter Town");
            SceneManager.LoadScene(2); //take to new scene
        }

        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Debug.Log("change scene");

            SceneManager.LoadScene(1);
            sr.sprite = battleSprite;

            Destroy(this.gameObject); //destroy the enemy

            State = Playerstates.Battle;
            Debug.Log("Battle");

        }

        if (collision.gameObject.tag.Equals("Entry"))
        {
            Debug.Log("change scene");
            SceneManager.LoadScene(0);
        }

    }

}
