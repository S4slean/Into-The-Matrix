using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longsword : Equipment
{
    PlayerStats PS;

    private void Start()
    {
        PS = FindObjectOfType<PlayerStats>();
    }

    public override void OnEquip(GameObject user)
    {
        PS.defense += defenceBonus;
        PS.strength += strenghtBonus;
    }

    public override void OnUnequip(GameObject user)
    {
        PS.defense -= defenceBonus;
        PS.strength -= strenghtBonus;
    }
}
