using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentBar : MonoBehaviour
{

    public GameObject EquipmentIconPrefab;
    public List<Equipment> PlayerEquipments = new List<Equipment>();

    public void AddPlayerEquipment(Equipment equipment) // Ajoute un équipement au joueur
    {
        Debug.Log("Equipment added");
        if (PlayerEquipments.IndexOf(equipment) < 0)
        {
            PlayerEquipments.Add(equipment);
            equipment.OnEquip(gameObject);
            
        }
        else
        {
            Debug.Log("You already have this equipment");
        }
    }

    public void RemovePlayerEquipment(Equipment equipment) // Retire un équipement au joueur
    {
        Debug.Log("Equipment removed");
        PlayerEquipments.Remove(equipment);
        equipment.OnUnequip(gameObject);
    }
}
