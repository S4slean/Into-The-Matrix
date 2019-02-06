using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [Header("Test")]

    public GameObject SorcierPanel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveSorcierPanel()
    {
        SorcierPanel.SetActive(true);
    }

    public void Menu ()
    {
        SorcierPanel.SetActive(false);
    }
}
