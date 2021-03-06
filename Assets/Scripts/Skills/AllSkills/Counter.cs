﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : Skill
{
	public GameObject particles;
    GameObject skillUser;
    private new Collider collider;
	GameObject instance;

    public float counterTimerMaxInTicks;
    public float counterTimerInTicks;

    public PlayerStats playerStats;

    public testson SoundDj;

    [SerializeField] bool isActive = false;

    public void Start()
    {
        SoundDj = GameObject.FindGameObjectWithTag("SoundDj").GetComponent<testson>();
    }
    public override void Activate(GameObject user)
    {
        if (CheckIfInLobby())
            return;

        if (user.tag == "Player" && Input.mousePosition.y > 285)
            return;

        if (cooldown > 0)
        {
            if (user.GetComponent<simpleEnemy>() != null)
                user.GetComponent<simpleEnemy>().StartCoroutine(user.GetComponent<simpleEnemy>().WaitForNewCycle(enemyRecoverTime));
            return;
        }



        skillUser = user;

        //instance = Instantiate(Resources.Load("Buffs/GlitchParticles") as GameObject, user.transform);

        if (skillUser.tag == "Player" && !isActive)
        {
            SoundDj.StartShield.Play();
			collider = skillUser.GetComponent<CapsuleCollider>();
			Debug.Log("Shield : ");
			instance = Instantiate(particles,FindObjectOfType<CharaController>().transform);
			instance.transform.localPosition = Vector3.up;
			TickManager.OnTick += DecreaseTick;
            playerStats = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStats>();
            playerStats.counter = true;
            isActive = true;
            counterTimerInTicks = counterTimerMaxInTicks;
            //skillUser.GetComponent<CharaController>().SetPlayerMovement(false, false);

        }

    }

	public void DecreaseTick()
	{
		if (isActive)
		{


			counterTimerInTicks--;
			if (counterTimerInTicks <= 0)
			{
				isActive = false;
				playerStats.counter = false;
				PowerUsed();
				cooldown = coolDownDuration;
				Destroy(instance);
			}
		}
	}

    private void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

 

    public IEnumerator WaitForAttack()
    {
        SoundDj.Reflect.Play();
        isActive = false;
        simpleEnemy enemy = skillUser.GetComponent<simpleEnemy>();
        yield return new WaitForSeconds(enemyLaunchTime);
        if (Mathf.Abs(enemy.enemyToPlayer.x) > Mathf.Abs(enemy.enemyToPlayer.z))
        {
        }
        else
        {
        }
        yield break;
    }

    public override IEnumerator useSkill(Vector3 pos)
    {
        if (skillUser.tag == "Player")
            yield return new WaitForSeconds(TickManager.tickDuration);
        //Ici on mettra l'animation/FX de réapparition

        if (skillUser.GetComponent<simpleEnemy>() != null)
            skillUser.GetComponent<simpleEnemy>().StartCoroutine(skillUser.GetComponent<simpleEnemy>().WaitForNewCycle(enemyRecoverTime));

        yield break;

    }

    public void Desactivation()
    {
        //if (!isActive)
        //break;
        if (skillUser.tag == "Player")
        {
            skillUser.GetComponent<CharaController>().SetPlayerMovement(true, true);
        }
        collider.enabled = true;
        isActive = false;

    }

    public override void OnDesequip()
    {
        FindObjectOfType<CharaController>().SetPlayerMovement(true, true);


        isActive = false;
    }
}
