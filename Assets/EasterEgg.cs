using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasterEgg : MonoBehaviour
{

    public Image EasterEggImage; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<DealDamage>() != null)
        {
            EasterEggImage.color = new Color(255, 255, 255, 255);

        }
    }
}
