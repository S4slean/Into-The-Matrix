using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longsword : Equipment
{
    CharaController CC;
    PlayerStats PS;

    // Start is called before the first frame update
    void Start()
    {
        PS = FindObjectOfType<PlayerStats>();
        CC = FindObjectOfType<CharaController>();
    }

    public override void OnEquip(GameObject user)
    {
        CC.attackLength += attackLenghtBonus;
        PS.strength += strenghtBonus;
    }

    public override void OnUnequip(GameObject user)
    {
        CC.attackLength -= attackLenghtBonus;
        PS.strength -= strenghtBonus;
    }
}
