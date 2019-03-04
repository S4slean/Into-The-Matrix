using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [Header("Test")]

    public GameObject MarchandPanel;
    public GameObject PortePanel;
    public GameObject PorteMyStuff;
    public GameObject PorteMap;
    public CharaController PlayerCC; 

    public void ActiveMerchantPanel()
    {
        MarchandPanel.SetActive(true);
    }

    public void ActivePortePanel()
    {
        PortePanel.SetActive(true);
        PorteMyStuff.SetActive(false);
        PorteMap.SetActive(false);
    }

    public void ActivePorteMyStuff()
    {
        PorteMyStuff.SetActive(true);
    }

    public void ActivePorteMap()
    {
        PorteMap.SetActive(true);
    }

    public void Menu ()
    {
        MarchandPanel.SetActive(false);
        PortePanel.SetActive(false);
        PlayerCC.enabled = true;
    }

    public void EntertheDungeons ()
    {
        SceneManager.LoadScene("TestController");
    }

    
}
