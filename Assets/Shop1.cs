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

    public float ShopControl;
    public float ShopFinal;

    public GameObject Shop;
    public GameObject ShopSelect1;
    public GameObject ShopSelect2;
    public GameObject ShopSelect3;
    public GameObject ShopSelect4;

    // Start is called before the first frame update
    void Start()
    {

        State = Playerstates.Shop1;
        Shop.SetActive(true);
        ShopSelect1.SetActive(true);
        ShopSelect2.SetActive(false);
        ShopSelect3.SetActive(false);
        ShopSelect4.SetActive(false);

        ShopControl = 0;




    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("w"))
        {
            ShopFinal = ShopControl + 1;

        }

        if (Input.GetKey("s"))
        {
            ShopFinal = ShopControl - 1;

        }


        if (ShopFinal == 0)
        {
            ShopSelect1.SetActive(true);
            ShopSelect2.SetActive(false);
            ShopSelect3.SetActive(false);
            ShopSelect4.SetActive(false);

        }

        if (ShopFinal == -1)
        {
            ShopSelect1.SetActive(false);
            ShopSelect2.SetActive(true);
            ShopSelect3.SetActive(false);
            ShopSelect4.SetActive(false);

        }

        if (ShopFinal == -2)
        {
            ShopSelect1.SetActive(false);
            ShopSelect2.SetActive(false);
            ShopSelect3.SetActive(true);
            ShopSelect4.SetActive(false);

        }

        if (ShopFinal == -3)
        {
            ShopSelect1.SetActive(true);
            ShopSelect2.SetActive(false);
            ShopSelect3.SetActive(false);
            ShopSelect4.SetActive(true);

        }

        if (Input.GetKey("r") && (State == Playerstates.Shop1))
        {
            SceneManager.LoadScene(2);
            State = Playerstates.Overworld;


        }

        if ((State == Playerstates.Shop1) && (Input.GetKey("i")))
        {

        }

    }

}
