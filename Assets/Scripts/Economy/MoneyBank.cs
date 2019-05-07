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
            PlayerPrefs.SetInt("BankCurrentMoney", 0);
        }

		// Met l'argent que le joueur a récupéré dans le donjon dans la banque.

        if(SceneManager.GetActiveScene().name == "Lobby")
        {
            DepositMoney();
            GetAllMoneyDisplays();
            ActualizeBankMoney();
        }
    }

    // Update is called once per frame
    void Update()
    {

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

        UIMoneyDisplay.text = BankMoney.ToString();
    }

    public void GetAllMoneyDisplays()
    {
        BankMoneyDisplays.Add(GameObject.Find("HubUIv2").transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>() ) ;
    }
}