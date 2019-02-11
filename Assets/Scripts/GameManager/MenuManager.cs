using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [Header("Test")]

    public GameObject MarchandPanel;
    public GameObject PortePanel;

    public void ActiveSorcierPanel()
    {
        MarchandPanel.SetActive(true);
    }

    public void ActivePortePanel()
    {
        PortePanel.SetActive(true);
    }

    public void Menu ()
    {
        MarchandPanel.SetActive(false);
        PortePanel.SetActive(false);
    }

    public void EntertheDungeons ()
    {
        SceneManager.LoadScene("TestController");
    }

    
}
