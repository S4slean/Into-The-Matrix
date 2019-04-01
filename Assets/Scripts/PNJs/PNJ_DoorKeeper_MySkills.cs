﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PNJ_DoorKeeper_MySkills : MonoBehaviour
{
    // !!! CE SCRIPT EST DUPLIQUE DE PNJ_DoorKeeper_MyStuff  MERCI DE MODIFIER LES DEUX SCRIPTS POUR QU'ILS RESTENT SIMILAIRES !!! 

    public GameObject StuffContent; // Assigner le gameobject du même nom -> UI du doorkeeper
    public MoneyBank money; // La banque
    public skillList skillListReference; // Liste des skills à vendre, assigner "StoreContentList"
    public int HowManySkillsCanITakeInTheHub; // Combien de skills je peux prendre sur moi dans le hub

    [Header("System elements - Ignore please")]

    public Skill EquipmentToUnlock;
    public Transform[] childObjects; // Sert à récupérer la liste des object en enfant
    public List<GameObject> StuffContentList; // Liste des objets de l'inventaire en enfants. Configuré pour contenir chaque bouton du Stuff.
    public GameObject SelectedButton;
    public SkillBar SB;
    public Text equipmentText;
    public int HowManySkillsIHave;


    // Start is called before the first frame update
    void Start()
    {
        UpdateMySkillNumber();
        InitiateLists();
        equipmentText.text = "Skills : " + (HowManySkillsCanITakeInTheHub - SB.PlayerSkills.Count);
    }

    // Update is called once per frame
    void Update()
    {
        SelectedButton = EventSystem.current.currentSelectedGameObject;

        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("test");
            EquipToPlayer();
        }
    }

    public void InitiateLists() // Récupère les boutons de l'inventaire
    {
        childObjects = StuffContent.GetComponentsInChildren<Transform>(true);
        StuffContentList = new List<GameObject>();

        // Récupère les boutons du stuff
        foreach (Transform child in childObjects)
        {
            if (child.name != "Text")
            {
                StuffContentList.Add(child.gameObject);
            }

        }
        StuffContentList.Remove(StuffContent.gameObject);
    }

    public void UnlockItem() // Permet d'acheter des skill dans le store
    {
        for (int i = 0; i < skillListReference.skills.Count; i++) // Trouve le skill à acheter
        {
            if (skillListReference.skills[i].GetType().Name == EventSystem.current.currentSelectedGameObject.name)
            {
                EquipmentToUnlock = skillListReference.skills[i];
            }
        }

        if (EquipmentToUnlock.cost < money.BankMoney) // Procède à l'achat
        {
            money.BankMoney -= EquipmentToUnlock.cost;
            money.ActualizeBankMoney();
            SelectedButton = EventSystem.current.currentSelectedGameObject;
            Debug.Log("object bought !");
            SelectedButton.GetComponent<Button>().interactable = false;

            for (int ii = 0; ii < StuffContentList.Count; ii++)
            {
                if (StuffContentList[ii].name == SelectedButton.name)
                {
                    StuffContentList[ii].GetComponent<Button>().interactable = true;
                }
            }
        }
        else // SI le joueur n'as pas assez d'argent
        {
            Debug.Log("Not enough money !");
        }
    }

    public void EquipToPlayer()
    {
        //Debug.Log(CheckSimilarSkills(EventSystem.current.currentSelectedGameObject.name, SB.PlayerSkills));
        for (int ii = 0; ii < skillListReference.skills.Count; ii++) 
        {
            //Debug.Log(CheckSimilarSkills(EventSystem.current.currentSelectedGameObject.name, SB.PlayerSkills));
            if (HowManySkillsIHave < HowManySkillsCanITakeInTheHub && CheckSimilarSkills(EventSystem.current.currentSelectedGameObject.name, SB.PlayerSkills) == -1) // équipe le skill sélectionné
            {
                if (skillListReference.skills[ii].name == EventSystem.current.currentSelectedGameObject.name)
                {
                    Debug.Log("Skill equipped");
                    EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = new Color(1f, 0.5f, 0);
                    SB.CreateButton(skillListReference.skills[ii]);
                    UpdateMySkillNumber();
                    return;
                }
            }
            else for (int j = 0; j < SB.PlayerSkills.Count; j++)
                {
                    if (CheckSimilarSkills(EventSystem.current.currentSelectedGameObject.name, SB.PlayerSkills) != -1) // déséquipe le skill sélectionné
                    {
                        if (skillListReference.skills[ii].name == EventSystem.current.currentSelectedGameObject.name)
                        {
                            Debug.Log("Skill Unequipped");
                            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = new Color(1f, 1f, 1);

							if (SB.PlayerSkills[j] != null)
							{
								SB.PlayerSkills[j].OnDesequip();
								Destroy(SB.PlayerSkills[j].gameObject);
								SB.PlayerSkills[j] = null;
							}


                            UpdateMySkillNumber();
                            return;
                        }
                    }
            }
        }

        
    }

    void UpdateMySkillNumber()
    {
        HowManySkillsIHave = 0;
        for (int y = 0; y < SB.PlayerSkills.Count; y++)
        {
            if (SB.PlayerSkills[y] != null)
            {
                HowManySkillsIHave += 1;
            }
        }
        equipmentText.text = "Skills : " + (HowManySkillsCanITakeInTheHub - HowManySkillsIHave);
    }

    int CheckSimilarSkills(string skillName, List<Skill> skillList)
    {
        for(int jj = 0; jj < SB.PlayerSkills.Count; jj++)
        {
            if(skillList[jj] != null)
            {
                if (skillList[jj].name == skillName)
                {
					Debug.Log(jj);
					

	                return jj;
                }
                
                //else return -1;
            }
        }
		Debug.Log(-1);
        return -1;
    }
}