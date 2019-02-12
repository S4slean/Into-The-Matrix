using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNJ_Merchant_StoreContent : MonoBehaviour
{
    private Transform[] childObjects; // Sert à récupérer la liste des object en enfant
    public List<GameObject> ShopContentList ; // Liste des objets en enfants. Configuré pour contenir chaque bouton du shop.

    public EquipmentList EList; // Liste générale des équipements

    // Start is called before the first frame update
    void Start()
    {
        childObjects = GetComponentsInChildren<Transform>();
        ShopContentList = new List<GameObject>();

        foreach (Transform child in childObjects)
        {
            if(child.name != "Text")
            {
                ShopContentList.Add(child.gameObject);
            }
            
        }
        ShopContentList.Remove(gameObject);
    }

 public void AddItemToShopContent()
    {

    }
}
