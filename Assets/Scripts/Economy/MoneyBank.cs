using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyBank : MonoBehaviour
{
    //DEBUG
    //Appuyer sur A pour déposer l'argent du joueur dans la banque
    //Appuyer sur Z pour reset la banque 

    public int BankMoney; // Argent disponible dans la banque
    public PlayerMoneyManager PMM;
    public List<Text> BankMoneyDisplays; // Liste de tous les textes montrant la somme dans la banque;

    // Start is called before the first frame update
    void Start()
    {
        PMM = GameObject.Find("Player").GetComponent<PlayerMoneyManager>();

        if (PlayerPrefs.HasKey("BankCurrentMoney"))
        {
            BankMoney = PlayerPrefs.GetInt("BankCurrentMoney");
        }
        else
        {
            BankMoney = 0;
            PlayerPrefs.SetInt("BankCurrentMoney", 0);
        }

        ActualizeBankMoney();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) // DEBUG !!! A SUPPRIMER DES QUE NECESSAIRE
        {
            DepositMoney();
            ActualizeBankMoney();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            BankMoney = 0;
            ActualizeBankMoney();
            PlayerPrefs.SetInt("BankCurrentMoney", 0);
        }
    }

    public void DepositMoney() // Ajoute à la banque l'argent que le player a sur lui
    {
        BankMoney += PMM.currentMoney;
        PMM.currentMoney = 0;
        PlayerPrefs.SetInt("BankCurrentMoney", BankMoney);
        PlayerPrefs.SetInt("PlayerCurrentMoney", 0);
    }

    public void ActualizeBankMoney() // Actualise les display du solde de la banque
    {
        for (int i = 0; i < BankMoneyDisplays.Count; i++)
        {
            BankMoneyDisplays[i].text = "Money: " + BankMoney;
        }
    }
}