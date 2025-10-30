using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogueBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
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


}




