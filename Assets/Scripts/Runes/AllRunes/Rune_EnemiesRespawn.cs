using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune_EnemiesRespawn : Rune
{
	public override void Active()
	{
		RoomTemplates roomTemplate = FindObjectOfType<RoomTemplates>();

		roomTemplate.enemiesRespawn = true;
		Debug.Log(name);
	}
}
