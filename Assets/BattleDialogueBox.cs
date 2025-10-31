using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogueBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;


    [SerializeField] Text FrostHP;
    [SerializeField] Text HollowHP;
    [SerializeField] Text dialogueText;
    [SerializeField] GameObject actionselector;
    [SerializeField] GameObject moveselectorFrost;
    [SerializeField] GameObject moveselectorHollow;
    [SerializeField] GameObject movedetailsFrost;
    [SerializeField] GameObject movedetailsHollow;

    [SerializeField] List<Text> actionTexts;
    [SerializeField] List<Text> moveTextsF;
    [SerializeField] List<Text> moveTextsH;
    [SerializeField] List<Text> moves;

    [SerializeField] Text descriptionF;
    [SerializeField] Text descriptionH;

    public void SetDialogue(string dialogue)
    {

        dialogueText.text = dialogue;

    }

    public IEnumerator TypeDialogue(string dialogue)
    {

        dialogueText.text = "";
        foreach (var letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

    }

    public void EnableDialogueText(bool enabled)
    { 
        dialogueText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        actionselector.SetActive(enabled);
    }

    public void EnableMoveSelectorFrost(bool enabled)
    {
        moveselectorFrost.SetActive(enabled);
        movedetailsFrost.SetActive(enabled);
    }

    public void EnableMoveSelectorHollow(bool enabled)
    {
        moveselectorHollow.SetActive(enabled);
        movedetailsHollow.SetActive(enabled);
    }

    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionTexts.Count; i++) 
        {
            if (i == selectedAction)
                actionTexts[i].color = highlightedColor;

            else
                actionTexts[i].color = Color.black;
        }
        
    }

    public void UpdateMoveSelectionFrost(int CurrentMoveFrost)
    {
        for (int i = 0; i < moveTextsF.Count; i++)
        {
            if (i == CurrentMoveFrost)
                moveTextsF[i].color = highlightedColor;

            else
                moveTextsF[i].color = Color.black;
        }

    }

    public void UpdateMoveSelectionHollow(int CurrentMoveHollow)
    {
        for (int i = 0; i < moveTextsH.Count; i++)
        {
            if (i == CurrentMoveHollow)
                moveTextsH[i].color = highlightedColor;

            else
                moveTextsH[i].color = Color.black;
        }

    }

    public void SetMoveName()
    {
        for(int i = 0; i < moves.Count; ++i)
        {

            if (i < moves.Count)
                moveTextsF[i].text = "-";
            else
                moveTextsF[i].text = "-";

        }

    }

    public void FrostHPUI(float FrostHPBattle)
    {

        FrostHP.text = "";

    }
}




