using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Player_Controller;

public class BattleManager : MonoBehaviour
{

    [SerializeField] BattleDialogueBox dialogueBox;


    public enum Battlestates
    {
        Start,
        PlayerAction,
        PlayerActionFrost,
        PlayerActionHollow,
        EnemyMove1,
        EnemyMove2,
        EnemyMove3,
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
    public float Frostattck;
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
    public float Hollowattck;
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

        dialogueBox.EnableActionSelector(false);
        dialogueBox.EnableMoveSelectorFrost(false);
        dialogueBox.EnableMoveSelectorHollow(false);
        SetupBattle();


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
        eneHP = 100;
        eneEXP = 10;


        armor = Level * 10;
        attack = (Level * Lvlattack) + 10;
        SP = Level + 10;
        HP = (Level * 10) + 40;
        spell = (Level * 15) + 15;

        HollowSP = 14;
        FrostSP = 13;

        HollowSPfinal = HollowSP + SP;
        FrostSPfinal = FrostSP + SP;

        Hollowattckfinal = Hollowattck + attack;
        Frostattckfinal = Frostattck + attack;

        HollowDeffinal = HollowDef + armor;
        FrostDeffinal = FrostDef + armor;

        HollowHPfinal = HollowHP + HP;
        FrostHPfinal = FrostHP + HP;

        HollowSpellfinal = HollowSpell + spell;
        FrostSpellfinal = FrostSpell + spell;

        List<float> unsortedNumbers = new List<float> { FrostSPfinal, HollowSPfinal, eneSP };
        List<float> sortedDescending = unsortedNumbers.OrderByDescending(x => x).ToList();
        Debug.Log("Action Order: " + string.Join(", ", sortedDescending));

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
                FrostAttack();
       

            }

            if (CurrentMoveFrost == 1)
            {

                Debug.Log("Frost Casted Ice Shard");
                dialogueBox.EnableDialogueText(true);
                dialogueBox.EnableMoveSelectorFrost(false);
                state = Battlestates.Busy;
                FrostSpecial();

            }



        }

    }

    public IEnumerator FrostAttack()
    {

        yield return StartCoroutine(dialogueBox.TypeDialogue($"Frost Attacked"));
        {
            //yield return new WaitForSeconds(3f);
            HollowAction();
        }
    }

    public IEnumerator FrostSpecial()
    {

        yield return StartCoroutine(dialogueBox.TypeDialogue($"Frost Casted Ice Shard"));
        {
            //yield return new WaitForSeconds(3f);
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

    }

    void HollowAction()
        {
        Debug.Log("Hollow Action");
        state = Battlestates.PlayerActionHollow;
        dialogueBox.EnableActionSelector(false);
        dialogueBox.EnableDialogueText(false);
        dialogueBox.EnableMoveSelectorHollow(true);
        HandleMoveSelectionHollow();
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

 
}
