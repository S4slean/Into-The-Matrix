using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentBar : MonoBehaviour
{

    public GameObject EquipmentIconPrefab;
    public List<Equipment> PlayerEquipments = new List<Equipment>();

    public void AddPlayerEquipment(Equipment equipment)
    {
                PlayerEquipments.Add(equipment);
                equipment.OnEquip(gameObject);
        
    }
}
