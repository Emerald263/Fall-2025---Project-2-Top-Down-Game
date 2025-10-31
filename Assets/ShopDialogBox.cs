using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void UpdateShopSelection(int Shopactionselect)
    {
        for (int i = 0; i < ShopTexts.Count; i++)
        {
            if (i == Shopactionselect)
                ShopTexts[i].color = highlightedColor;

            else
                ShopTexts[i].color = Color.black;
        }

    }


}

