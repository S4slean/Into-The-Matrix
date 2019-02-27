using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Contient les boutons des objets en vente dans le magasin

/* !!! POUR AJOUTER DE NOUVEAUX OBJETS !!!  
 * Ajouter le script de l'objet (ex: LongSword) au gameobject auquel est attaché ce script
 * puis ajouter un bouton avec le nom du script en enfant. 
 * !!! POUR AJOUTER DES OBJETS AU STORE !!! 
 * Activer le script de l'objet contenu sur ce gameobject puis activer la fonction AdditemToShopContent*/
 
public class PNJ_Merchant_StoreContent : MonoBehaviour
{
    private Transform[] childObjects; // Sert à récupérer la liste des object en enfant
    public List<GameObject> ShopContentList; // Liste des objets en enfants. Configuré pour contenir chaque bouton du shop.

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

    public void InitiateLists()
    {
        childObjects = GetComponentsInChildren<Transform>(true);
        ShopContentList = new List<GameObject>();

        // Récupère les boutons du store
        foreach (Transform child in childObjects)
        {
            if (child.name != "Text")
            {
                ShopContentList.Add(child.gameObject);
            }

        }
        ShopContentList.Remove(gameObject);

        // Récupère les équipements et les ajoute à la liste 
        equipmentsForSale = new List<Equipment>(GetComponents<Equipment>());
    }

    public void AddItemToShopContent()
    {
        for (int i = 0; i < equipmentsForSale.Count; i++)
        {
            if (equipmentsForSale[i].enabled == true)
            {
                for (int ii = 0; ii < ShopContentList.Count; ii++)
                {
                    if (ShopContentList[ii].name == equipmentsForSale[i].GetType().Name)
                    {
                        ShopContentList[ii].SetActive(true);
                    }
                }
            }
        }
    }
}