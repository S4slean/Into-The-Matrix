using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCoins : MonoBehaviour
{
    public int value; // Valeur de la pièce
    public PlayerMoneyManager PMM; // Le PlayerMoneyManager
    public testson SoundDj;

    void Start()
    {
        SoundDj = GameObject.FindGameObjectWithTag("SoundDj").GetComponent<testson>();
        PMM = GameObject.Find("Player").GetComponent<PlayerMoneyManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            SoundDj.Coins.Play();
            Debug.Log("Coin picked up !");
            PMM.AddMoney(value);
            Destroy(gameObject);
        }
    }
}
