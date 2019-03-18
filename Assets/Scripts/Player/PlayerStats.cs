using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	Animator anim;

	public int health = 3;
	public int strength = 1;
	public int defense = 1;

	private void Start()
	{
		anim = GetComponent<Animator>();
	}

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

	public void TakeDamage(int dmg)
	{
		health -= dmg;
		anim.Play("TakeDamage");
	}
}
