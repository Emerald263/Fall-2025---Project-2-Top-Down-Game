using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Create new enemy")]
public class Enemy_Base : ScriptableObject
{


    [SerializeField] string Enemyname;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite battle;

    //Enemy stats
    [SerializeField] int SP;
    [SerializeField] int attck;
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
