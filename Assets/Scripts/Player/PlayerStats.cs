using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public int health = 3;
	public int damage = 1;

	public void Update()
	{
		CheckDeath();
	}

	private void CheckDeath()
	{
		if(health < 1)
		{
			gameObject.SetActive(false);
		}
	}
}
