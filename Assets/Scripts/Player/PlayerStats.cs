using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
	Animator anim;
    public Image LifeBarFilled;

    public float MaxHealth = 3;
    public float health = 3;
	public int strength = 1;
	public int defense = 1;

	bool dead = false;

	private void Start()
	{
		anim = GetComponent<Animator>();
        health = MaxHealth;
        LifeBarFilled = GameObject.Find("LifeBarFilled").GetComponent<Image>();
        UpdateLifeBar();

    }

	public void Update()
	{
		
	}

	private void CheckDeath()
	{
		if(health < 1 && !dead)
		{
			dead = true;
			StartCoroutine(BackToLobby());
			gameObject.GetComponent<CharaController>().enabled = false;
		}
	}

	public void TakeDamage(int dmg)
	{
		health -= dmg;
		anim.Play("TakeDamage");
        UpdateLifeBar();
        CheckDeath();
    }

	public void KillPlayer()
	{
		health = 0;
	}

	IEnumerator BackToLobby()
	{
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(0);
	}

    public void UpdateLifeBar()
    {
        LifeBarFilled.fillAmount = health/MaxHealth;
    }
}
