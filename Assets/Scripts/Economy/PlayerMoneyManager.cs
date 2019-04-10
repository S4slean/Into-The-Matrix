using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        //Line where to set the UI element for money
		if(SceneManager.GetActiveScene().buildIndex !=0)
			CoinsText.text = currentMoney.ToString();
    }

    void Update()
    {
        
    }

	public void GetDungeonMoney()
	{
		CoinsText.text = currentMoney.ToString();
	}


    // Permet aux autres éléments d'ajouter/retirer de l'argent au joueur
    public void AddMoney(int MoneyToAdd)
    {
        currentMoney += MoneyToAdd;
        CoinsText.text = currentMoney.ToString();
        PlayerPrefs.SetInt("PlayerCurrentMoney", currentMoney);
        //Line where to update the UI element for money
        //CoinsText.text = "Coins:"+currentMoney;

    }
}
