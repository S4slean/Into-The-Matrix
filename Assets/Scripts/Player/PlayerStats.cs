﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerStats : MonoBehaviour
{
	public Animator anim;
	PlayerMoneyManager money;
	SkillBar skillBar;
	//CabineUIScript cabineUI;
	public GameObject minimap;
	public GameObject loadingScreen;
	public TempsPlongee timebar;
	public Vector2 startingRoom;
	public GameObject dmgUI;


	public int trapOvrd = 0;
	public int enmyOvrd = 0;
	public int phoneOvrd = 0;

	public int key = 0;
	public bool byPass = false;

	public List<DungeonOverride> overrides = new List<DungeonOverride>();

    public float MaxHealth = 3;
    public float health = 3;
	public int strength = 1;
	public int defense = 1;

	bool dead = false;

    public bool counter = false;

    public int runePieces = 0;
    public int runePiecesToGet;

    public Vector3 squareRoomEntered;

    public testson SoundDj;

    private void Start()
	{
        SoundDj = GameObject.FindGameObjectWithTag("SoundDj").GetComponent<testson>();
        money = GetComponent<PlayerMoneyManager>();
        health = MaxHealth;
        
		timebar = FindObjectOfType<TempsPlongee>();
		skillBar = FindObjectOfType<SkillBar>();
		//cabineUI = GameObject.FindGameObjectWithTag("CabineUI").GetComponent<CabineUIScript>();

    }

	bool positionned = false;

	public void SetStartPos()
	{
		if (positionned)
			return;

		positionned = true;

		anim.Play("idle");
		Debug.Log("StartPosnotSet");
		if (startingRoom != null)
		{

			Debug.Log("PlayerMoved");
			
			transform.position = new Vector3(startingRoom.x / 21 * 14, 0, (startingRoom.y / 31.5f * 20) -5);
		}
		startingRoom = Vector3.zero;
		loadingScreen.GetComponent<Animator>().Play("Disappear");
		minimap.SetActive(true);
		StartCoroutine(PosSetup());
	}

	IEnumerator PosSetup()
	{
		yield return new WaitForSeconds(2f);
		positionned = false;
	}

	public void CheckDeath()
	{
		if(health < 1 && !dead)
		{
			Death();
		}
	}

    IEnumerator TutoDelayBeforeTeleport()
    {
        loadingScreen.GetComponent<Animator>().Play("Appear");
        yield return new WaitForSeconds(0.5f);
        anim.Play("idle");
        transform.position = squareRoomEntered;
        transform.LookAt(transform.position + Vector3.forward);
        yield return new WaitForSeconds(0.7f);
        loadingScreen.GetComponent<Animator>().Play("Disappear");
        yield break;
    }

    public void Death()
    {
        if (dead)
			return;
        if (SceneManager.GetActiveScene().name == "LobbyTutorial")
        {
            dead = true;
            money.currentMoney = 0;
            money.UpdateDJMoneyUI();
            skillBar.DesequipAll();
            GetComponent<CharaController>().enabled = false;
            StartCoroutine(TutoDelayBeforeTeleport());
            Debug.Log(transform.position);
            GetComponent<CharaController>().enabled = true;
            dead = false;
        }
        else
        {
            dead = true;
            money.currentMoney = 0;
            money.UpdateDJMoneyUI();
            skillBar.DesequipAll();
            StartCoroutine(BackToLobby());
            GetComponent<CharaController>().enabled = false;
        }

    }

	public void TakeDamage(int dmg)
	{
        if (!counter)
        {
			if(SoundDj == null)
			{
				SoundDj = FindObjectOfType<testson>();
			}
            SoundDj.HeroDamage.Play();
			//SPAWN PARTICLE -5S

            //health -= dmg;
            anim.CrossFade("TakeDamage", .1f);
			if(timebar == null)
			{
				timebar = FindObjectOfType<TempsPlongee>();
			}
			timebar.LoseTime(dmg);
			GetComponent<CinemachineImpulseSource>().GenerateImpulse();
			dmgUI.SetActive(true);

			CheckDeath();
        }
    }

	public void KillPlayer()
	{
		health = 0;
	}

	public IEnumerator BackToLobby()
	{
		PlayerPrefs.SetInt("trapOvrd", trapOvrd);
		PlayerPrefs.SetInt("enmyOvrd", enmyOvrd);
		PlayerPrefs.SetInt("spawnOvrd", phoneOvrd);
		PlayerPrefs.SetInt("Keys", key);

		loadingScreen.GetComponent<Animator>().Play("Appear");
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(1);
		transform.position = new Vector3(0, 0, 1);
		transform.rotation = Quaternion.Euler(0, 180, 0);
		health = MaxHealth;

		yield return new WaitForSeconds(2);
		if (dead == true)
			dead = false;

		//minimap.GetComponent<minimap>().SaveMap();
		minimap.SetActive(false);

		anim.Play("idle");
		GetComponent<CharaController>().enabled = true;
		timebar = FindObjectOfType<TempsPlongee>();
		timebar.timer = timebar.timeMax;
		timebar.plongee = false;
		//overrides = new List<DungeonOverride>();
		TickManager.ClearDelegate();
		loadingScreen.GetComponent<Animator>().Play("Disappear");

	}

	public void ExecuteOverride()
	{
		if (overrides == null)
			return;  

		Debug.Log("Début de l'override");
		RoomCameraTrigger[] rooms = FindObjectsOfType<RoomCameraTrigger>();

		foreach(DungeonOverride ovr in overrides)
		{
			foreach(RoomCameraTrigger room in rooms)
			{
				if(room.transform.position == new Vector3(ovr.spritePos.x/21 * 14 , 0, ovr.spritePos.y/ 31.5f * 20))
				{
					room.ApplyOverride(ovr.overridesIndex);
					Debug.Log("Salles Overridée");
				}
			}
		}
		Debug.Log("Fin des overrides");

	} 


	public void BackToDungeon()
	{
		health = MaxHealth;
		money.currentMoney = 0;
		money.UpdateDJMoneyUI();
		minimap.SetActive(true);
	}


    public void AddRunePiece()
    {
        runePieces++;
        //mettre un startAnimation avec un runePieces en paramètre, pour montrer la progression de la récupération des runes
        if(runePieces == runePiecesToGet)
        {
            Debug.Log("Toutes les runes ont été collectées, la porte s'ouvre ");
        }
    }

    public IEnumerator FallInHole()
    {
        //GameObject.FindGameObjectWithTag("Loading").GetComponent<Animator>().Play("Appear");
        StartCoroutine(GetComponent<CharaController>().FreezePlayer(0.25f));
        yield return new WaitForSeconds(0.1f);
        TakeDamage(10);
        transform.position = squareRoomEntered;
        transform.LookAt(transform.position + Vector3.forward);
        GetComponent<CharaController>().fell = false;
        //GameObject.FindGameObjectWithTag("Loading").GetComponent<Animator>().Play("Disappear");
    }
}
