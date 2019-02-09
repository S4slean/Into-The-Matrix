using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBreakableItem : MonoBehaviour
{
    public PlayerMoneyManager PMM;
    public DealDamage DD;

    public int value; // Valeur de l'objet
    public int health; // points de vie de l'objet

    // Start is called before the first frame update
    void Start()
    {
        PMM = GameObject.Find("Player").GetComponent<PlayerMoneyManager>();
        DD = GameObject.Find("AttackCollider").GetComponent<DealDamage>();
        DD.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "AttackCollider")
        {
            Debug.Log("CHest hit !");
            health -= DD.damage;
        }
        if (health < 1)
        {
            PMM.currentMoney += value;
            Destroy(gameObject);
        }
    }
}
