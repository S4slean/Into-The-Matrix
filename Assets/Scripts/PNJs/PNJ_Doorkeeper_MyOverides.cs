using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ_Doorkeeper_MyOverides : MonoBehaviour
{
    public PlayerStats ps;
    public MoneyBank mb;

    public int PriceEnemy;
    public int PriceTraps;
    public int PriceSpawn;


    // Start is called before the first frame update
    void Start()
    {
        ps = FindObjectOfType<PlayerStats>();
    }

    void BuyOvrdEnemy() // Achète un usage pour l'overide enemy
    {
        if(mb.BankMoney >= PriceEnemy)
        {
            ps.enmyOvrd += 1;
        }
    }

    void BuyOvrdTraps() // Achète un usage pour l'overide traps
    {
        if (mb.BankMoney >= PriceTraps)
        {
            ps.trapOvrd += 1;
        }
    }
     
    void BuyOvrdSpawn() // Achète un usage pour l'overide spawn
    {
        if (mb.BankMoney >= PriceSpawn)
        {
            ps.phoneOvrd += 1;
        }
    }
}
