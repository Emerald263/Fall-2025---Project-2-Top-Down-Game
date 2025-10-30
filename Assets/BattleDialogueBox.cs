using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogueBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;
    

    [SerializeField] Text dialogueText;
    [SerializeField] GameObject actionselector;
    [SerializeField] GameObject moveselector;
    [SerializeField] GameObject movedetails;

    [SerializeField] List<Text> actionTexts;
    [SerializeField] List<Text> moveTexts;

    [SerializeField] Text description;

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

    public void EnableMoveSelector(bool enabled)
    {
        moveselector.SetActive(enabled);
        movedetails.SetActive(enabled);
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

    public void SetMoveName(List<Move> moves)
    {
        for(int i = 0; i < moves.Count; ++i)
        {

            if (i < moves.Count)
                moveTexts[i].text = moves[i].Base.Name;
            else
                moveTexts[i].text = "-";

        }


    }
}




