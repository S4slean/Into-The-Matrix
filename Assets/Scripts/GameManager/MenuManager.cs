using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [Header("Test")]

    public GameObject MarchandPanel;
    public GameObject MarchandMenu;
    public GameObject MarchandEquipment;
    public GameObject MarchandSkills;
    public GameObject PortePanel;
    public GameObject PorteMenu;
    public GameObject PorteMyStuff;
    public GameObject PorteMySkills;
    public GameObject PorteMap;
    CharaController PlayerCC;

	private void Start()
	{
		PlayerCC = FindObjectOfType<CharaController>();
	}


	// ::: SCRIPTS INTERFACES GENERAUX :::

	public void ExitAllPanels() //Ferme toutes les interfaces
    {
        MarchandPanel.SetActive(false);
        PortePanel.SetActive(false);
        PlayerCC.enabled = true;
    }

    // ::: SCRIPTS DU MARCHAND :::

    public void OpenMerchant() //Ouvre l'interface du marchand
    {
        MarchandPanel.SetActive(true);
        MarchandMenu.SetActive(true);
    }

    public void ActiveMerchantPanel() //Retour au menu et ferme les stores
    {
        MarchandMenu.SetActive(true);
        MarchandEquipment.SetActive(false);
        MarchandSkills.SetActive(false);
    }

    public void ActiveMerchantEquipment() //Accès aux store d'équipements
    {
        MarchandMenu.SetActive(false);
        MarchandEquipment.SetActive(true);
    }

    public void ActiveMerchantSkills() //Accès aux store de skills
    {
        MarchandMenu.SetActive(false);
        MarchandSkills.SetActive(true);
    }

    // ::: SCRIPTS DU PORTIER :::

    public void ActivePortePanel() //Retour au menu et ferme les inventaires et la carte
    {
        PorteMenu.SetActive(true);
        PorteMyStuff.SetActive(false);
        PorteMySkills.SetActive(false);
        PorteMap.SetActive(false);
    }

    public void ActivePorteMyStuff() //Accès aux équipements
    {
        PorteMenu.SetActive(false);
        PorteMyStuff.SetActive(true);
    }

    public void ActivePorteMySkills() //Accès aux skills
    {
        PorteMenu.SetActive(false);
        PorteMySkills.SetActive(true);
    }

    public void ActivePorteMap() //Accès à la carte
    {
        PorteMenu.SetActive(false);
        PorteMap.SetActive(true);
    }

    public void EntertheDungeons () //Envoie le joueur dans le donjon
    {
        SceneManager.LoadScene("TestController");
    }

    
}
