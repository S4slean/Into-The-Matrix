﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTile : MonoBehaviour
{
	CharaController player;

    // Start is called before the first frame update
    void Start()
    {
		player = FindObjectOfType<CharaController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	private void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
		{
			player.freezing = true;
			player.StartCoroutine(player.Move(player.lastMove));
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			player.freezing = false;
		}
	}
}