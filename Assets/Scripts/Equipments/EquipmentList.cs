using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentList : MonoBehaviour
{
    public List<Equipment> equipments;

    private void Start()
    {
        EnableItemIntheList();
    }

    public void EnableItemIntheList()
    {
        equipments = new List<Equipment>(GetComponents<Equipment>());

        for (int i = 0; i < equipments.Count; i++)
        {
            if (equipments[i].enabled != true)
            {
                equipments.Remove(equipments[i]);
            }
        }
    }
}