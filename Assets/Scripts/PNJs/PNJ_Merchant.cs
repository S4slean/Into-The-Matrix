using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNJ_Merchant : MonoBehaviour
{
    public GameObject MerchantInterface; // Interface du marchand sur l'UI, qui doit s'activer quand le joueur interragit avec le PNJ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "AttackCollider")
        {
            MerchantInterface.SetActive(true);
        }
    }
}
