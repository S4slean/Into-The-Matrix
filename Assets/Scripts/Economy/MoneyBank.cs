using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoneyBank : MonoBehaviour
{
    public int BankMoney; // Argent disponible dans la banque
    public PlayerMoneyManager PMM;
	public Text UIMoneyDisplay; // Indicateur d'argent sur l'UI
    public List<Text> BankMoneyDisplays; // Liste de tous les textes montrant la somme dans la banque;

    // Start is called before the first frame update
    void Start()
    {
        PMM = GameObject.Find("Player").GetComponent<PlayerMoneyManager>();
		UIMoneyDisplay = GameObject.Find("PlayerMoneyText").GetComponent<Text>();

        if (PlayerPrefs.HasKey("BankCurrentMoney"))
        {
            BankMoney = PlayerPrefs.GetInt("BankCurrentMoney");
        }
        else
        {
            BankMoney = 0;
            PlayerPrefs.SetInt("BankCurrentMoney", BankMoney);
        }

		// Met l'argent que le joueur a récupéré dans le donjon dans la banque.

        if(SceneManager.GetActiveScene().name == "Lobby")
        {
            DepositMoney();
            ActualizeBankMoney();
        }
    }


    public void DepositMoney() // Ajoute à la banque l'argent que le player a sur lui
    {
        BankMoney += PMM.currentMoney;
		Debug.Log(PMM.currentMoney + " Déposé à la bank");
		Debug.Log("Total de " + BankMoney);
        PMM.currentMoney = 0;
        PlayerPrefs.SetInt("BankCurrentMoney", BankMoney);
    }

    public void ActualizeBankMoney() // Actualise les display du solde de la banque
    {

        for (int i = 0; i < BankMoneyDisplays.Count; i++)
        {
            BankMoneyDisplays[i].text = "Money: " + BankMoney;
        }

        UIMoneyDisplay.text = BankMoney.ToString();
    }


}