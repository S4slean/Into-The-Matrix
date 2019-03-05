using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempsPlongee : MonoBehaviour
{
    public bool plongee;
    public float timeMax;
    public float timer;
    public Image timeBar;

    void Start()
    {
        timer = timeMax;
    }
    
    void Update()
    {
        if (plongee)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
        }
        timeBar.fillAmount = timer / timeMax;
        timeBar.color = Color.Lerp(Color.red, Color.white, timer / timeMax);
    }
}
