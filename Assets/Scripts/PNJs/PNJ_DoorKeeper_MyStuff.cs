using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PNJ_DoorKeeper_MyStuff : MonoBehaviour
{
    public GameObject StuffContent; // Assigner le gameobject du même nom -> UI du doorkeeper
    public MoneyBank money; // La banque
    public EquipmentList equipmentListReference;

    [Header("System elements - Ignore please")]

    public Equipment EquipmentToUnlock;
    public Transform[] childObjects; // Sert à récupérer la liste des object en enfant
    public List<GameObject> StuffContentList; // Liste des objets en enfants. Configuré pour contenir chaque bouton du Stuff.
    public GameObject SelectedButton;


    // Start is called before the first frame update
    void Start()
    {
        InitiateLists();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitiateLists()
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

    public void UnlockItem()
    {
        for (int i = 0; i < equipmentListReference.equipments.Count; i++)
        {
            if (equipmentListReference.equipments[i].GetType().Name == EventSystem.current.currentSelectedGameObject.name)
            {
                EquipmentToUnlock = equipmentListReference.equipments[i];   
            }
        }
    
        if (EquipmentToUnlock.cost < money.BankMoney)
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
        else
        {
            Debug.Log("Not enough money !");
        }
    }
}
