using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PNJ_DoorKeeper_MyStuff : MonoBehaviour
{
    public GameObject StuffContent; // Assigner le gameobject du même nom -> UI du doorkeeper
    public MoneyBank money; // La banque
    public EquipmentList equipmentListReference; // Liste des équipements à vendre, assigner "StoreContentList"

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

    public void UnlockItem() // Permet d'acheter des objets dasn le store
    {
        for (int i = 0; i < equipmentListReference.equipments.Count; i++) // Désactive les boutons des objets déjà utilisés
        {
            if (equipmentListReference.equipments[i].GetType().Name == EventSystem.current.currentSelectedGameObject.name)
            {
                EquipmentToUnlock = equipmentListReference.equipments[i];   
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
}
