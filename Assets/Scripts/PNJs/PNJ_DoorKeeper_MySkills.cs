using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PNJ_DoorKeeper_MySkills : MonoBehaviour
{
    public GameObject StuffContent; // Assigner le gameobject du même nom -> UI du doorkeeper
    public MoneyBank money; // La banque
    public skillList skillListReference; // Liste des skills à vendre
    public int HowManySkillsCanITakeInTheHub; // Combien de skills je peux prendre sur moi dans le hub

    [Header("System elements - Ignore please")]

    public Skill EquipmentToUnlock;
    public Transform[] childObjects; // Sert à récupérer la liste des object en enfant
    public List<GameObject> StuffContentList; // Liste des objets de l'inventaire en enfants. Configuré pour contenir chaque bouton du Stuff.
    public GameObject SelectedButton;
    public GameObject InfoPopup;
    public SkillBar SB;
    public CharaController CC;
    public Text equipmentText;
    public int HowManySkillsIHave;


    // Start is called before the first frame update
    void Start()
    {
        UpdateMySkillNumber();
        InitiateLists();

        SB = FindObjectOfType<SkillBar>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("DEBUG How many skills I have: " + HowManySkillsIHave);
        }
    }

    public void InitiateLists() // Récupère les boutons de l'inventaire
    {
        childObjects = StuffContent.GetComponentsInChildren<Transform>(true);
        StuffContentList = new List<GameObject>();

        // Récupère les boutons du stuff
        foreach (Transform child in childObjects)
        {
            if (child.name != "SkillIcon" && child.name != "EquipCheck" && child.name != "CostText")
            {
                StuffContentList.Add(child.gameObject);
            }

        }
        StuffContentList.Remove(StuffContent.gameObject);
    }

    public void UnlockItem() // Trouve le skill à acheter et ouvre le popup d'achat
    {
        for (int i = 0; i < skillListReference.skills.Count; i++) // Trouve le skill à acheter
        {
            if (skillListReference.skills[i].name == EventSystem.current.currentSelectedGameObject.name)
            {
                EquipmentToUnlock = skillListReference.skills[i];
            }
        }

        SelectedButton = EventSystem.current.currentSelectedGameObject;

        // Ouvre le popup d'achat et renseigne les champs de texte
        InfoPopup.transform.GetChild(0).GetComponent<Image>().sprite = EquipmentToUnlock.icon;
        InfoPopup.transform.GetChild(1).GetComponent<Text>().text = EquipmentToUnlock.name;
        InfoPopup.transform.GetChild(2).GetComponent<Text>().text = EquipmentToUnlock.description;
        InfoPopup.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "Buy for \n" + EquipmentToUnlock.cost + "$";
        InfoPopup.SetActive(true);
    }

    public void BuySkill() // Achète le skill sélectionné par UnlockItem
    {
        money = FindObjectOfType<MoneyBank>();

        if (EquipmentToUnlock.cost <= money.BankMoney) // Procède à l'achat
        {
            money.BankMoney -= EquipmentToUnlock.cost;
            PlayerPrefs.SetInt("BankCurrentMoney", money.BankMoney);
            PlayerPrefs.SetInt(EquipmentToUnlock.name, 1);
            money.ActualizeBankMoney();
            Debug.Log("object bought !");

            // Change le bouton en équiper/déséquiper
            SelectedButton.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1);  // dégrise l'icone
            SelectedButton.transform.GetChild(1).gameObject.SetActive(true);                        // Active l'icone équipé/déséquipé
            SelectedButton.transform.GetChild(2).GetComponent<Text>().text = "";                    // Retire le coût

            SelectedButton.GetComponent<Button>().onClick.RemoveAllListeners();
            SelectedButton.GetComponent<Button>().onClick.AddListener(() => EquipToPlayer());

            // Désactive le popup d'achat
            InfoPopup.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
            InfoPopup.SetActive(false);
        }
        else // SI le joueur n'as pas assez d'argent
        {
            InfoPopup.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
            Debug.Log("Not enough money !");
        }
    }

    public void EquipToPlayer() // Equipe le skill au player // Need rework
    {
        Debug.Log("! BUTTON PRESSED: " + EventSystem.current.currentSelectedGameObject.name);

        SelectedButton = EventSystem.current.currentSelectedGameObject;

        for (int ii = 0; ii < skillListReference.skills.Count; ii++) 
        {
            // équipe le skill
            Debug.Log("skillcount: " + (HowManySkillsIHave < HowManySkillsCanITakeInTheHub));
            Debug.Log( "How many skills I have: " + HowManySkillsIHave);
            Debug.Log("How many skills I can take in the hub: " + HowManySkillsCanITakeInTheHub);

            if (HowManySkillsIHave < HowManySkillsCanITakeInTheHub && CheckSimilarSkills(EventSystem.current.currentSelectedGameObject.name, SB.PlayerSkills) == -1) // équipe le skill sélectionné
            {
                Debug.Log("YES 2");

                if (skillListReference.skills[ii].name == EventSystem.current.currentSelectedGameObject.name)
                {
                    Debug.Log("Skill equipped: " + skillListReference.skills[ii].name);
                    SB.CreateButton(skillListReference.skills[ii]);

                    SelectedButton.transform.GetChild(1).GetComponent<Image>().color = new Color(0, 0.8f, 0);

                    UpdateMySkillNumber();
                    return;
                }
            }

            // Stack le nombre d'utilisation du skill
            else for (int j = 0; j < SB.PlayerSkills.Count; j++)
                {
                    if (CheckSimilarSkills(EventSystem.current.currentSelectedGameObject.name, SB.PlayerSkills) != -1) // déséquipe le skill sélectionné
                    {
                        Debug.Log("Skill stacked: " + SB.PlayerSkills[CheckSimilarSkills(EventSystem.current.currentSelectedGameObject.name, SB.PlayerSkills)].name);

                        if(SB.PlayerSkills[CheckSimilarSkills(EventSystem.current.currentSelectedGameObject.name, SB.PlayerSkills)].nbOfUse < 3)
                        {
                            SB.PlayerSkills[CheckSimilarSkills(EventSystem.current.currentSelectedGameObject.name, SB.PlayerSkills)].nbOfUse += 1;
                        }
                    }
                }

                        /* // Déséquipe le skill

                        else for (int j = 0; j < SB.PlayerSkills.Count; j++)
                            {
                                if (CheckSimilarSkills(EventSystem.current.currentSelectedGameObject.name, SB.PlayerSkills) != -1) // déséquipe le skill sélectionné
                                {                            
                                    Debug.Log("Skill unequipped: " + SB.PlayerSkills[CheckSimilarSkills(EventSystem.current.currentSelectedGameObject.name, SB.PlayerSkills)].name);

                                    SelectedButton.transform.GetChild(1).GetComponent<Image>().color = new Color(0.8f, 0, 0);

                                    SB.PlayerSkills[CheckSimilarSkills(EventSystem.current.currentSelectedGameObject.name, SB.PlayerSkills)].OnDesequip();
                                    Destroy(SB.PlayerSkills[CheckSimilarSkills(EventSystem.current.currentSelectedGameObject.name, SB.PlayerSkills)].gameObject);
                                    SB.PlayerSkills[CheckSimilarSkills(EventSystem.current.currentSelectedGameObject.name, SB.PlayerSkills)] = null;

                                    UpdateMySkillNumber();
                                    return;
                                }
                        }*/
                    }     
    }

    public void ClosePopup() // Ferme le popup d'achat
    {
        InfoPopup.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
        InfoPopup.SetActive(false);
    }

    public void UpdateMySkillNumber()
    {
        HowManySkillsIHave = 0;
        for (int y = 0; y < 3; y++)
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
        for(int jj = 0; jj < 3; jj++)
        {
            if(skillList[jj] != null)
            {
                if (skillList[jj].name == skillName)
                {
                    Debug.Log("You already have this skill: " + skillList[jj].name + " | ii = " + jj);

                    return jj;
                }
            }
        }
        return -1;
    }

    public void CloseStore()
    {
        CC.enabled = true;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }
    
}