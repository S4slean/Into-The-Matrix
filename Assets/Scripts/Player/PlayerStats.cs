using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
	Animator anim;
	PlayerMoneyManager money;
    public Image LifeBarFilled;

    public float MaxHealth = 3;
    public float health = 3;
	public int strength = 1;
	public int defense = 1;

	bool dead = false;

	private void Start()
	{
		anim = GetComponent<Animator>();
		money = GetComponent<PlayerMoneyManager>();
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
			money.currentMoney = 0;
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
		transform.position = new Vector3(0, 0, 1);
		transform.rotation = Quaternion.Euler(0, 180, 0);
		health = MaxHealth;
	}

    public void UpdateLifeBar()
    {
        LifeBarFilled.fillAmount = health/MaxHealth;
    }
}
