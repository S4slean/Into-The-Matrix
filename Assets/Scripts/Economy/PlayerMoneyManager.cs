using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoneyManager : MonoBehaviour
{
    public int currentMoney; // Argent que le joueur possède
    public Text CoinsText;

    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerCurrentMoney"))
        {
            currentMoney = PlayerPrefs.GetInt("PlayerCurrentMoney");
        }
        else
        {
            currentMoney = 0;
            PlayerPrefs.SetInt("PlayerCurrentMoney", 0);
        }

        CoinsText.text = "Coins:" + currentMoney;
    }

    void Update()
    {
        
    }

    // Permet aux autres éléments d'ajouter/retirer de l'argent au joueur
    public void AddMoney(int MoneyToAdd)
    {
        currentMoney += MoneyToAdd;
        PlayerPrefs.SetInt("PlayerCurrentMoney", currentMoney);
        
        CoinsText.text = "Coins:"+currentMoney;

    }
}
