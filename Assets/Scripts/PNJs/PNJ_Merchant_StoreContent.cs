using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* !!! POUR AJOUTER DE NOUVEAUX OBJETS !!!  
 * Ajouter le script de l'objet (ex: LongSword) au gameobject "EquipmentList" pour les objets du dongeon et "StoreContentList" pour les objets du store
 * puis ajouter un bouton avec le nom du script en enfant aux gameobjects "StoreContent" et "StuffContent" dans "HubUI".
 * Pour rendre disponible ou non un item, activer ou désactiver son script sur le gameobject "StoreContentList"*/
 
    // ! ! ! Pour voir l'opération d'achat, voir le script "PNJ_DoorKeeper_MyStuff", fonction "UnlockItem"

public class PNJ_Merchant_StoreContent : MonoBehaviour
{
    public GameObject StoreButtonsParentObject; // Assigner le gameobject du même nom -> UI du marchand
    public GameObject StoreContentList; // Assigner le gameobject du même nom

    [Header("System elements - Ignore please")]

    public Transform[] childObjects; // Sert à récupérer la liste des object en enfant
    public List<GameObject> ShopButtonsContentList; // Liste des objets en enfants. Configuré pour contenir chaque bouton du shop.
    public List<Equipment> equipmentsForSale; // Liste des équipements à vendre

    // Start is called before the first frame update
    void Start()
    {
        InitiateLists();
        AddItemToShopContent();
    }

    private void Update()
    {

    }

    public void InitiateLists() // Récupère les listes de boutons et d'équipements à vendre
    {
        childObjects = StoreButtonsParentObject.GetComponentsInChildren<Transform>(true);
        ShopButtonsContentList = new List<GameObject>();

        // Récupère les boutons du store
        foreach (Transform child in childObjects)
        {
            if (child.name != "Text")
            {
                ShopButtonsContentList.Add(child.gameObject);
            }

        }
        ShopButtonsContentList.Remove(StoreButtonsParentObject.gameObject);

        // Récupère les équipements et les ajoute à la liste 
        //equipmentsForSale = new List<Equipment>(StoreContentList.GetComponents<Equipment>());
    }

    public void AddItemToShopContent() // Active les boutons du Store selon que leur script est actif ou non
    {
        for (int i = 0; i < equipmentsForSale.Count; i++)
        {
            if (equipmentsForSale[i].enabled == true)
            {
                for (int ii = 0; ii < ShopButtonsContentList.Count; ii++)
                {
                    if (ShopButtonsContentList[ii].name == equipmentsForSale[i].GetType().Name)
                    {
                        ShopButtonsContentList[ii].GetComponent<Button>().interactable = true;
                    }
                }
            }
        }
    }
}