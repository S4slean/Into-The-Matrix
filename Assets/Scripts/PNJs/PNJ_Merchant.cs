﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNJ_Merchant : MonoBehaviour
{
    public GameObject MerchantInterface; // Interface du marchand sur l'UI, qui doit s'activer quand le joueur interragit avec le PNJ
    CharaController PlayerCC;

    // Start is called before the first frame update
    void Start()
    {
		PlayerCC = FindObjectOfType<CharaController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "AttackCollider")
        {
            MerchantInterface.SetActive(true);
            PlayerCC.enabled = false;
        }
    }
}
