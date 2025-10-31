using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Player_Controller;

public class BattleManager : MonoBehaviour
{

    //audio variables
    public AudioSource soundEffects;
    public AudioClip[] sounds; // Public variable to access the Audio Source component

    //Animation variables
    Animator anim;
    public bool Frostidle;
    public bool Frostattck;
    public bool Hollowidle;
    public float Hollowattck;
    public float Enemyidle;
    public float Enemyattck;

    [SerializeField] BattleDialogueBox dialogueBox;


    public enum Battlestates
    {
        Start,
        PlayerAction,
        PlayerActionFrost,
        PlayerActionHollow,
        EnemyMove,
        Busy,

    }

    [Header("Scripts Ref:")]
    public Playerstates State;
    public float armor;
    public float SP;
    public float attack;
    public float spell;
    public float HP;
    public float EXP;
    public float EXPfinal;
    public float GP;
    public float GPfinal;
    public float Level;
    public float armoradd;
    public float SPadd;
    public float attckadd;
    public float splladd;

    public float eneSP;
    public float eneattck;
    public float eneDef;
    public float eneHP;
    public float eneEXP;
    public float eneHPFinal;

    public float Battleorder;
    public float Lvlattack;


    //Frost stats
    public float FrostSP;
    public float Frostattckst;
    public float FrostDef;
    public float FrostHP;
    public float FrostEXP;
    public float FrostSpell;
    public float FrostSPfinal;
    public float Frostattckfinal;
    public float FrostDeffinal;
    public float FrostHPfinal;
    public float FrostSpellfinal;

    //Hollow stats
    public float HollowSP;
    public float Hollowattckst;
    public float HollowDef;
    public float HollowHP;
    public float HollowEXP;
    public float HollowSpell;
    public float HollowSPfinal;
    public float Hollowattckfinal;
    public float HollowDeffinal;
    public float HollowHPfinal;
    public float HollowSpellfinal;

    Battlestates state;
    int CurrentActionBattle;
    int CurrentMoveFrost;
    int CurrentMoveHollow;


    // Start is called before the first frame update
    private void Start()
    {

        soundEffects = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        dialogueBox.EnableActionSelector(false);
        dialogueBox.EnableMoveSelectorFrost(false);
        dialogueBox.EnableMoveSelectorHollow(false);
        SetupBattle();

        FrostHP = 15;
        HollowHP = 15;


        armor = 10;
        SP = 10;
        attack = 10;
        spell = 1;
        HP = 50;
        EXP = 0;
        GP = 0;
        Level = 1;

        eneSP = 5;
        eneattck = 10;
        eneDef = 5;
        eneHP = 4;
        eneEXP = 10;


        armor = Level * 10;
        attack = (Level * Lvlattack) + 10;
        HP = (Level * 10) + 40;
        spell = (Level * 15) + 15;


        HollowSPfinal = HollowSP + SP;
        FrostSPfinal = FrostSP + SP;

        Hollowattckfinal = Hollowattckst + attack;
        Frostattckfinal = Frostattckst + attack;

        HollowDeffinal = HollowDef + armor;
        FrostDeffinal = FrostDef + armor;

        HollowHPfinal = HollowHP + HP;
        FrostHPfinal = FrostHP + HP;

        HollowSpellfinal = HollowSpell + spell;
        FrostSpellfinal = FrostSpell + spell;



        StartCoroutine(SetupBattle());

    }


    // Update is called once per frame
    private void Update()
    {

        if (state == Battlestates.PlayerAction)
        {

            HandleActionSelection();

        }
        else if(state == Battlestates.PlayerActionFrost)
        { 
            
            FrostAction(); 
        
        }
        else if(state == Battlestates.PlayerActionHollow)
        {

            HollowAction();

        }

        if (Input.GetKeyDown(KeyCode.L))
        {

            eneHP = -1;
        }

        anim.SetBool("Frostattck", Frostattck);
        anim.SetBool("Frostidl", Frostidle);

    }
    void HandleActionSelection()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CurrentActionBattle < 1)
                ++CurrentActionBattle;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CurrentActionBattle > 0)
                --CurrentActionBattle;

        }

        dialogueBox.UpdateActionSelection(CurrentActionBattle);


        if (Input.GetKeyDown(KeyCode.E))
        {

            if (CurrentActionBattle == 0)
            {

                //Fight
                FrostAction();
            }

            if (CurrentActionBattle == 1)
            {

                //Run
                StartCoroutine(BattleFlee());
            }



        }

    }

        
    

   void FrostAction()
    {
        Debug.Log("Frost Action");
        state = Battlestates.PlayerActionFrost;
        dialogueBox.EnableActionSelector(false);
        dialogueBox.EnableDialogueText(false);
        dialogueBox.EnableMoveSelectorFrost(true);
        HandleMoveSelectionFrost();
        Frostidle = true;

    }

    void HandleMoveSelectionFrost()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CurrentMoveFrost < 1)
                ++CurrentMoveFrost;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CurrentMoveFrost > 0)
                --CurrentMoveFrost;

        }

        dialogueBox.UpdateMoveSelectionFrost(CurrentMoveFrost);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (CurrentMoveFrost == 0)
            {
                Debug.Log("Frost Attacked");
                dialogueBox.EnableDialogueText(true);
                dialogueBox.EnableMoveSelectorFrost(false);
                state = Battlestates.Busy;
                StartCoroutine(FrostAttack());
       

            }

            if (CurrentMoveFrost == 1)
            {

                Debug.Log("Frost Casted Ice Shard");
                dialogueBox.EnableDialogueText(true);
                dialogueBox.EnableMoveSelectorFrost(false);
                state = Battlestates.Busy;
                StartCoroutine(FrostSpecial());

            }



        }

    }

    public IEnumerator FrostAttack()
    {

        yield return StartCoroutine(dialogueBox.TypeDialogue($"Frost Attacked"));
        yield return new WaitForSeconds(5f);
        {
            Frostidle = false;
            Frostattck = true;
            --eneHP;

            if (eneHP < 1)
            {

                enemydeath();

            }

            HollowAction();
        }
    }

    public IEnumerator FrostSpecial()
    {

        yield return StartCoroutine(dialogueBox.TypeDialogue($"Frost Casted Ice Shard"));
        yield return new WaitForSeconds(5f);
        {
            Frostidle = false;
            Frostattck = true;
            --eneHP;

            if (eneHP < 1)
            {

                enemydeath();

            }

            HollowAction();
        }
    }

    void HandleMoveSelectionHollow()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CurrentMoveHollow < 1)
                ++CurrentMoveHollow;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CurrentMoveHollow > 0)
                --CurrentMoveHollow;

        }

        dialogueBox.UpdateMoveSelectionHollow(CurrentMoveHollow);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (CurrentMoveHollow == 0)
            {
                Debug.Log("Hollow Attacked");
                dialogueBox.EnableDialogueText(true);
                dialogueBox.EnableMoveSelectorHollow(false);
                state = Battlestates.Busy;
                StartCoroutine(HollowAttack());


            }

            if (CurrentMoveHollow == 1)
            {

                Debug.Log("Hollow Casted Flame Reaper");
                dialogueBox.EnableDialogueText(true);
                dialogueBox.EnableMoveSelectorHollow(false);
                state = Battlestates.Busy;
                StartCoroutine(HollowSpecial());

            }



        }


    }

    public IEnumerator HollowAttack()
    {

        yield return StartCoroutine(dialogueBox.TypeDialogue($"Hollow Attacked"));
        yield return new WaitForSeconds(5f);
        {
            --eneHP;

            if (eneHP < 1)
            {

                enemydeath();

            }

            EnemyAction();

        }
    }

    public IEnumerator HollowSpecial()
    {

        yield return StartCoroutine(dialogueBox.TypeDialogue($"Hollow Casted Flame Reaper"));
        yield return new WaitForSeconds(5f);
        {
            --eneHP;

            if (eneHP < 1)
            {

                enemydeath();

            }

            EnemyAction();

        }
    }

    void HollowAction()
        {
        Frostidle = false;
        Frostattck = false;
        Debug.Log("Hollow Action");
        state = Battlestates.PlayerActionHollow;
        dialogueBox.EnableActionSelector(false);
        dialogueBox.EnableDialogueText(false);
        dialogueBox.EnableMoveSelectorHollow(true);
        HandleMoveSelectionHollow();
        Hollowidle = true;

        if (eneHP < 0)
        {

            enemydeath();

        }
    }


    void EnemyAction()
    {
        state = Battlestates.EnemyMove;
        dialogueBox.EnableDialogueText(true);

            StartCoroutine(EnemyAttack());

        

    }

    public IEnumerator EnemyAttack()
    {
        for (int i = 0; i < 3; i++)
        {

            yield return StartCoroutine(dialogueBox.TypeDialogue($"The Enemy Attacked"));
            yield return new WaitForSeconds(5f);

            --FrostHP;
            --HollowHP;

        }

        FrostAction();

    }

    public void enemydeath()
    {

        StartCoroutine(BattleEndWin());

    }

    
    public IEnumerator SetupBattle()
    {


        yield return StartCoroutine (dialogueBox.TypeDialogue($"Wild Tree Crawlers appeared"));
        yield return new WaitForSeconds (1f);

        Playeraction();
    }

    void Playeraction()
    {

        state = Battlestates.PlayerAction;
        StartCoroutine(dialogueBox.TypeDialogue("Choose an action"));
        dialogueBox.EnableActionSelector(true);
    }

    public IEnumerator BattleFlee()
    {
        yield return StartCoroutine(dialogueBox.TypeDialogue($"You fled"));
        {
            //yield return new WaitForSeconds(1);
            SceneManager.LoadScene(0);
            State = Playerstates.Overworld;
            EXPfinal = EXP + 43;
            GPfinal = GP + 15;
        }
    }

    public IEnumerator BattleEndWin()
    {
        yield return StartCoroutine(dialogueBox.TypeDialogue($"You Won! Gained 50exp and 15gp!"));
        yield return new WaitForSeconds(1f);
        {
            //yield return new WaitForSeconds(1);
            SceneManager.LoadScene(0);
            State = Playerstates.Overworld;
            EXPfinal = EXP + 50;
            GPfinal = GP + 15;
        }


    }

    public IEnumerator BattleEndLose()
    {
        yield return StartCoroutine(dialogueBox.TypeDialogue($"You Lost"));
        yield return new WaitForSeconds(1f);
        {
            //yield return new WaitForSeconds(1);
            SceneManager.LoadScene(0);
            State = Playerstates.Overworld;
            EXPfinal = EXP + 50;
            GPfinal = GP + 15;
        }


    }


}
