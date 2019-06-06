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
	public GameObject startingRoom;

	public List<DungeonOverride> overrides;

    public float MaxHealth = 3;
    public float health = 3;
	public int strength = 1;
	public int defense = 1;

	bool dead = false;

    public bool counter = false;

    public int runePieces = 0;
    public int runePiecesToGet;

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

	public void SetStartPos()
	{
		Debug.Log("StartPosnotSet");
		if (startingRoom != null)
		{

			Debug.Log("PlayerMoved");
			RectTransform rTransform = startingRoom.GetComponent<RectTransform>();
			transform.position = new Vector3(rTransform.anchoredPosition.x / 21 / 14, 0, rTransform.anchoredPosition.y / 31.5f / 20);
			loadingScreen.GetComponent<Animator>().Play("Disappear");
		}
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

    public void Death()
    {
        dead = true;
        money.currentMoney = 0;
        StartCoroutine(BackToLobby());
        GetComponent<CharaController>().enabled = false;
    }

	public void TakeDamage(int dmg)
	{
        if (!counter)
        {
			//SPAWN PARTICLE -5S

            //health -= dmg;
            anim.Play("TakeDamage");
            UpdateLifeBar();
            CheckDeath();
        }
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

		//minimap.GetComponent<minimap>().SaveMap();
		minimap.SetActive(false);

		anim.Play("idle");
		GetComponent<CharaController>().enabled = true;
		lifebar.SetActive(false);
		timebar.timer = timebar.timeMax;
		timebar.plongee = false;
		overrides = new List<DungeonOverride>();
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
		overrides.Clear();
		Debug.Log("Fin des overrides");

	} 


	public void BackToDungeon()
	{
		health = MaxHealth;
		UpdateLifeBar();
		lifebar.SetActive(true);
		GetComponent<PlayerMoneyManager>().GetDungeonMoney();
		minimap.SetActive(true);
	}

    public void UpdateLifeBar()
    {
        LifeBarFilled.fillAmount = health/MaxHealth;
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
}
