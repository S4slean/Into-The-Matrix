﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Liste des équipements trouvables dans le dongeon

public class EquipmentList : MonoBehaviour
{
    public List<Equipment> Allequipments;
    public List<Equipment> equipments;

    private void Awake()
    {
        EnableItemIntheList();
        if (FindObjectsOfType<EquipmentList>().Length > 2)
			Destroy(this.gameObject);

		DontDestroyOnLoad(this.gameObject);

    }

    public void EnableItemIntheList()
    {
        Allequipments = new List<Equipment>(GetComponents<Equipment>());


        for (int i = 0; i < Allequipments.Count; i++)
        {
            if(Allequipments[i].enabled != false)
            {
                equipments.Add(Allequipments[i]);
            }

        }
    }
}