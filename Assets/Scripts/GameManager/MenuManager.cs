using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [Header("Test")]

    public GameObject SorcierPanel;
    public GameObject PortePanel;



    public void ActiveSorcierPanel()
    {
        SorcierPanel.SetActive(true);
    }

    public void ActivePortePanel()
    {
        PortePanel.SetActive(true);
    }

    public void Menu ()
    {
        SorcierPanel.SetActive(false);
        PortePanel.SetActive(false);
    }

    public void EntertheDungeons ()
    {
        SceneManager.LoadScene("TestController");
    }

    
}
