using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [Header("Equipment Infos")]

    public new string name;
    public Sprite icon;
    public string description;

    [Header("Equipment Stats")]
    [Tooltip("The cost at the shop")]
    public int cost;
    [Tooltip("Attack bonus")]
    public int attackBonus;
    [Tooltip("Strenght bonus")]
    public int strenghtBonus;
    [Tooltip("Defence bonus")]
    public int defenceBonus;
    [Tooltip("Speed bonus")]
    public int speedBonus;
    [Tooltip("Attack lenght bonus")]
    public int attackLenghtBonus;
    [Tooltip("Attack width bonus")]
    public int attackWidthBonus;

    [Header("System")]
    public PlayerStats PS;
    public CharaController CC;
    public DealDamage DD;

    public void Start()
    {
        PS = FindObjectOfType<PlayerStats>();
        CC = FindObjectOfType<CharaController>();
        DD = FindObjectOfType<DealDamage>();
    }

    public virtual void OnEquip(GameObject user)
    {
        Debug.Log(name + " equipped");
        Debug.Log(DD);
        DD.damage += attackBonus;
        PS.strength += strenghtBonus;
        PS.defense += defenceBonus;
        //CC.moveStep -= speedBonus;
        CC.attackLength += attackLenghtBonus;
        CC.attackWidth += attackWidthBonus;

    }

    public virtual void OnUnequip(GameObject user)
    {
        Debug.Log(name + " unequipped");
        DD.damage -= attackBonus;
        PS.strength -= strenghtBonus;
        PS.defense -= defenceBonus;
        //CC.moveStep += speedBonus;
        CC.attackLength -= attackLenghtBonus;
        CC.attackWidth -= attackWidthBonus;
    }
}
