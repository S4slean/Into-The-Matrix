using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation: Skill
{
	public int distance = 4;
	public GameObject selectionArea;
	GameObject skillUser;
	GameObject instance;
	Vector3 tpDir;
	private new Collider collider;

	[SerializeField]bool isActive = false;

	public override void Activate(GameObject user)
	{

		if (CheckIfInLobby())
			return;

		if (user.tag == "Player" && Input.mousePosition.y > 285)
			return;

		if (cooldown > 0)
		{
			if (user.GetComponent<SimpleEnemy>() != null)
				user.GetComponent<SimpleEnemy>().StartCoroutine(user.GetComponent<SimpleEnemy>().WaitForNewCycle(enemyRecoverTime));
			return;
		}
        
		skillUser = user;

		collider = skillUser.GetComponent<CapsuleCollider>();
		//collider.enabled = false;

		//instance = Instantiate(Resources.Load("Buffs/GlitchParticles") as GameObject, user.transform);
		/*isActive = true;*/

		if (skillUser.tag == "Player")
		{
			//skillUser.GetComponent<CharaController>().SetPlayerMovement(false, false);

			WaitForTarget();
		}

		if (skillUser.tag == "Enemy")
		{
			StartCoroutine(WaitForAttack());
		}
	}


	private void Update()
	{
		if (cooldown > 0)
			cooldown -= Time.deltaTime;

		if (!isActive)
			return;
	}

	public void WaitForTarget()
	{
        if (!isActive)
        {
            CharaController player = skillUser.GetComponent<CharaController>();
            instance = Instantiate(selectionArea, player.transform.position + Vector3.up * .05f, Quaternion.identity, player.transform);
            instance.GetComponent<StarTarget>().distance = distance;
            instance.GetComponent<SelectionArea>().skill = this;
            isActive = true;
        }
        else if (isActive)
        {
            Desactivation();
        }
	}

	public IEnumerator WaitForAttack()
	{
		isActive = false;
		SimpleEnemy enemy = skillUser.GetComponent<SimpleEnemy>();
		yield return new WaitForSeconds(enemyLaunchTime);
		if(Mathf.Abs(enemy.enemyToPlayer.x) > Mathf.Abs(enemy.enemyToPlayer.z))
		{
			tpDir = Vector3.right * Mathf.Sign(enemy.enemyToPlayer.x);
			distance = Mathf.Abs(Mathf.RoundToInt(enemy.enemyToPlayer.x));
		}
		else
		{
			tpDir = Vector3.forward * Mathf.Sign(enemy.enemyToPlayer.z);
			distance = Mathf.Abs(Mathf.RoundToInt(enemy.enemyToPlayer.z));
		}

		Vector3 dodgePos = skillUser.transform.position + tpDir * distance;
		StartCoroutine(useSkill(dodgePos));
		yield break;
	}

	public override IEnumerator useSkill(Vector3 tpPos)
	{
		while(TickManager.tick < TickManager.tickDuration)
        {
            yield return new WaitForEndOfFrame();
        }
        //Ici on mettra l'animation/FX de disparition
        skillUser.SetActive(false);
        if (skillUser.tag == "Player")
        { skillUser.GetComponent<CharaController>().lastMove = Vector3.zero; }
        yield return new WaitForSeconds(TickManager.tickDuration);
        //Ici on mettra l'animation/FX de réapparition
        skillUser.transform.position = tpPos;
        skillUser.SetActive(true);

        if (skillUser.tag == "Player")
		{
			this.skillUser.GetComponent<CharaController>().SetPlayerMovement(true, true);
		}
		Destroy(instance);
		collider.enabled = true;

		if (skillUser.GetComponent<SimpleEnemy>() != null)
			skillUser.GetComponent<SimpleEnemy>().StartCoroutine(skillUser.GetComponent<SimpleEnemy>().WaitForNewCycle(enemyRecoverTime));

        PowerUsed();
        cooldown = coolDownDuration;

        yield break;

    }

	public void Desactivation()
	{
		//if (!isActive)
			//break;

		Destroy(instance);
		if (skillUser.tag == "Player")
		{
			skillUser.GetComponent<CharaController>().SetPlayerMovement(true, true);
		}
		collider.enabled = true;
		isActive = false;

	}

	public override void OnDesequip()
	{
		if (instance == null)
			return;

		Destroy(instance);


		FindObjectOfType<CharaController>().SetPlayerMovement(true, true);

		
		isActive = false;
	}
}
