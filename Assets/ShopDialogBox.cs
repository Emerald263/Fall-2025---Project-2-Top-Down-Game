using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDialogueBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;


    [SerializeField] Text dialogueShop;

    [SerializeField] GameObject actionselectorshop;
    [SerializeField] GameObject actionselectionshop;


    [SerializeField] List<Text> ShopTexts;

    [SerializeField] Text descriptionShop;


    public void SetDialogue(string dialogue)
    {

        dialogueShop.text = dialogue;

    }

    public IEnumerator TypeDialogue(string dialogue)
    {

        dialogueShop.text = "";
        foreach (var letter in dialogue.ToCharArray())
        {
            dialogueShop.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

    }

    public void EnableDialogueText(bool enabled)
    {
        dialogueShop.enabled = enabled;
    }

    public void EnableActionSelectorShop(bool enabled)
    {
        actionselectorshop.SetActive(enabled);
    }



    public void UpdateActionSelection(int selectedshop)
    {
        for (int i = 0; i < Shopaction.Count; i++)
        {
            if (i == selectedAction)
                Shopaction[i].color = highlightedColor;

            else
               Shopaction[i].color = Color.black;
        }

    }


}

