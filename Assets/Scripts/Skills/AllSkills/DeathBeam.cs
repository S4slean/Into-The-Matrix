using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBeam : Skill
{
	public GameObject beam;
	public float duration;
	Transform player;
	GameObject instance;
	simpleEnemy simpleEnemy;

	private void Start()
	{
		beam = Resources.Load("Ray/Beam") as GameObject;
		player = GameObject.FindObjectOfType<CharaController>().transform;
	}

	public override void Activate(GameObject user)
	{
		if(CheckIfInLobby())
			return;

		if (user.tag == "Player" && Input.mousePosition.y > 285)
			return;

		if (cooldown > 0)
		{
			if (user.GetComponent<simpleEnemy>() != null)
				user.GetComponent<simpleEnemy>().StartCoroutine(user.GetComponent<simpleEnemy>().WaitForNewCycle(enemyRecoverTime));
			return;
		}

		if(user.tag =="Player")
			player.GetComponent<CharaController>().SetPlayerMovement(true, false);

		if (user.GetComponent<simpleEnemy>() != null)
		{
			simpleEnemy = user.GetComponent<simpleEnemy>();
			simpleEnemy.isAttacking = true;
			simpleEnemy.unableToRotate = true;
			simpleEnemy.state = simpleEnemy.State.follow;
		}
		


		instance = Instantiate(beam, user.transform.position + Vector3.up + user.transform.forward, user.transform.rotation, user.transform);
		instance.GetComponent<DealDamage>().user = user.tag;
		instance.GetComponent<DealDamage>().StartCoroutine(instance.GetComponent<DealDamage>().DesactiveAfterTime(duration, user, enemyRecoverTime));

        PowerUsed();
        cooldown = coolDownDuration;
    }

	public override void OnDesequip()
	{
		base.OnDesequip();
	}



}