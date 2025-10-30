using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player/Create new Character")]
public class Player_Base : ScriptableObject
{


    [SerializeField] string Charactername;
    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite battle;

    //Enemy stats
    [SerializeField] int SP;
    [SerializeField] int attck;
    [SerializeField] int spell;
    [SerializeField] int Def;
    [SerializeField] int HP;





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}