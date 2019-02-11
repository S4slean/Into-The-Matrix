using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentBar : MonoBehaviour
{

    public GameObject EquipmentIconPrefab;
    public List<Equipment> PlayerEquipments = new List<Equipment>();

    public void AddPlayerEquipment(Equipment equipment)
    {
        for (int i = 0; i < PlayerEquipments.Count; i++)                                                    //Vérifie chaque slot en commençant par le premier
        {
            if (PlayerEquipments[i] == null)                                                                    //si le slot est libre
            {
                PlayerEquipments[i] = equipment;
                equipment.OnEquip(gameObject);
                break;
            }
        }
    }

}
