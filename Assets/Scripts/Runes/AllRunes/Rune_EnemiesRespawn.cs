using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune_EnemiesRespawn : MonoBehaviour
{
	private void Awake()
	{
		RoomTemplates roomTemplate = FindObjectOfType<RoomTemplates>();

		roomTemplate.enemiesRespawn = true;
	}
}
