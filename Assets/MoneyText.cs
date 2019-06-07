using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
	MoneyBank bank;


	private void OnEnable()
	{
		bank = FindObjectOfType<MoneyBank>();
		bank.BankMoneyDisplays.Add(GetComponent<Text>());
		bank.ActualizeBankMoney();
		GetComponent<Text>().text = bank.BankMoney.ToString();
	}



}
