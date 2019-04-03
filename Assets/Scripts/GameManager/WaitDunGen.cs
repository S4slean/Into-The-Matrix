using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitDunGen: MonoBehaviour
{
	TempsPlongee diveTime;
	GameObject player;

	private void Start()
	{
		player = FindObjectOfType<CharaController>().gameObject;
		diveTime = FindObjectOfType<TempsPlongee>();
	}

	void Update()
    {
        if(FindObjectOfType<RoomSpawner>() == null)
		{
			if(player == null)
				player = FindObjectOfType<CharaController>().gameObject;

			player.SetActive(true);
			diveTime.plongee = true;

			Destroy(this);
		}
    }
}
