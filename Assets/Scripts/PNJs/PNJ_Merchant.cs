using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNJ_Merchant : MonoBehaviour
{
    public GameObject MerchantInterface; // Interface du marchand sur l'UI, qui doit s'activer quand le joueur interragit avec le PNJ
    public GameObject MerchantInterface2; // Background du marchand
    CharaController PlayerCC;

    // Start is called before the first frame update
    void Start()
    {
		PlayerCC = FindObjectOfType<CharaController>();
        MerchantInterface = GameObject.Find("HubUIv2").transform.GetChild(0).gameObject;
        MerchantInterface2 = GameObject.Find("HubUIv2").transform.GetChild(1).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "AttackCollider")
        {
            MerchantInterface.SetActive(true);
            MerchantInterface2.SetActive(true);
            PlayerCC.enabled = false;
        }
    }

    public void CloseInterface()
    {
        MerchantInterface.SetActive(false);
        PlayerCC.enabled = true;
    }
}
