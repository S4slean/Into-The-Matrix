using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMoneyManager : MonoBehaviour
{
    public int currentMoney; // Argent que le joueur possède
    public Text CoinsText;



	public void UpdateDJMoneyUI()
	{
		CoinsText.text = currentMoney.ToString();
	}


    // Permet aux autres éléments d'ajouter/retirer de l'argent au joueur
    public void AddMoney(int MoneyToAdd)
    {
        currentMoney += MoneyToAdd;
		UpdateDJMoneyUI();

    }
}
