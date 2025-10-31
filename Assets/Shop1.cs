using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Player_Controller;

public class Shop1 : MonoBehaviour
{
    public Playerstates State;

    [SerializeField] ShopDialogueBox dialogueBox;
    int Shopaction;


    // Start is called before the first frame update
    void Start()
    {

        State = Playerstates.Shop1;
        dialogueBox.EnableDialogueText(true);
        dialogueBox.EnableActionSelectorShop(true);

        StartCoroutine(SetupShop());


        


    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKey("r") && (State == Playerstates.Shop1))
        {
            SceneManager.LoadScene(2);
            State = Playerstates.Overworld;


        }

        if ((State == Playerstates.Shop1) && (Input.GetKey("i")))
        {

        }

    }

    void HandleShopSelection()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Shopaction < 1)
                ++Shopaction;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Shopaction > 0)
                --Shopaction;

        }

        dialogueBox.UpdateShopSelection(Shopaction);


        if (Input.GetKeyDown(KeyCode.E))
        {

            if (Shopaction == 0)
            {

                
            }

            if (Shopaction == 1)
            {
                SceneManager.LoadScene(2);
                State = Playerstates.Overworld;

            }



        }


    }

    public IEnumerator SetupShop()
    {


        yield return StartCoroutine(dialogueBox.TypeDialogue($"Entered Shop"));
        yield return new WaitForSeconds(1f);

        HandleShopSelection();
    }

}
