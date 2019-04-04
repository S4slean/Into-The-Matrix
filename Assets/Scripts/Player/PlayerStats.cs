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
	SkillBar skillBar;
	//CabineUIScript cabineUI;
    public Image LifeBarFilled;
	public GameObject lifebar;
	public GameObject minimap;
	public GameObject loadingScreen;
	public TempsPlongee timebar;

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
		timebar = FindObjectOfType<TempsPlongee>();
		skillBar = FindObjectOfType<SkillBar>();
		//cabineUI = GameObject.FindGameObjectWithTag("CabineUI").GetComponent<CabineUIScript>();
        UpdateLifeBar();

    }

	public void Update()
	{
		
	}

	public void CheckDeath()
	{
		if(health < 1 && !dead)
		{
			dead = true;
			money.currentMoney = 0;
			StartCoroutine(BackToLobby());
			GetComponent<CharaController>().enabled = false;
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

	public IEnumerator BackToLobby()
	{


		loadingScreen.GetComponent<Animator>().Play("Appear");
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(0);
		transform.position = new Vector3(0, 0, 1);
		transform.rotation = Quaternion.Euler(0, 180, 0);
		health = MaxHealth;
		UpdateLifeBar();
		skillBar.DesequipAll();
		yield return new WaitForSeconds(2);
		if (dead == true)
			dead = false;
		foreach (Transform child in minimap.transform)
		{
			if (child.GetSiblingIndex() != 0)
				Destroy(child.gameObject);

		}
		anim.Play("idle");
		GetComponent<CharaController>().enabled = true;
		lifebar.SetActive(false);
		timebar.timer = timebar.timeMax;
		timebar.plongee = false;
		loadingScreen.GetComponent<Animator>().Play("Disappear");

	}

	public void BackToDungeon()
	{
		health = MaxHealth;
		UpdateLifeBar();
		lifebar.SetActive(true);
	}

    public void UpdateLifeBar()
    {
        LifeBarFilled.fillAmount = health/MaxHealth;
    }
}
