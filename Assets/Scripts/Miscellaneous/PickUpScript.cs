using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public int value; // Valeur de la pièce

    public bool moneyPickUp;
    public PlayerMoneyManager PMM; // Le PlayerMoneyManager

    public bool timePickUp;
    public TempsPlongee timeScript;

    void Start()
    {
        if (moneyPickUp)
        { PMM = GameObject.Find("Player").GetComponent<PlayerMoneyManager>(); }
        if (timePickUp)
        { timeScript = FindObjectOfType<TempsPlongee>(); }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            PickedUp();
        }
    }

    public void PickedUp()
    {
        if (moneyPickUp)
        { PMM.AddMoney(value); }

        if (timePickUp)
        { StartCoroutine(timeScript.TimeGain(value)); }

        Destroy(gameObject);
    }
}
