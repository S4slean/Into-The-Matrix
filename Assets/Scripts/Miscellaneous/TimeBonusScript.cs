using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBonusScript : MonoBehaviour
{
    public int timeBonus;
    public TempsPlongee timeScript;

    private void Start()
    {
        timeScript = FindObjectOfType<TempsPlongee>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(timeScript.TimeGain(timeBonus));
            Destroy(gameObject);
        }
    }

}
