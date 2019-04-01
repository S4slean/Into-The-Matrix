using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBeam : Skill
{
	public GameObject beam;
	public float duration;
	Transform player;
	GameObject instance;
	SimpleEnemy simpleEnemy;

	private void Start()
	{
		beam = Resources.Load("Ray/Beam") as GameObject;
		player = GameObject.FindObjectOfType<CharaController>().transform;
	}

	public override void Activate(GameObject user)
	{
		if (user.tag == "Player" && Input.mousePosition.y > 285)
			return;

		if (cooldown > 0)
		{
			if (user.GetComponent<SimpleEnemy>() != null)
				user.GetComponent<SimpleEnemy>().StartCoroutine(user.GetComponent<SimpleEnemy>().WaitForNewCycle(enemyRecoverTime));
			return;
		}

		cooldown = coolDownDuration;

		if(user.tag =="Player")
			player.GetComponent<CharaController>().SetPlayerMovement(true, false);

		if (user.GetComponent<SimpleEnemy>() != null)
		{
			simpleEnemy = user.GetComponent<SimpleEnemy>();
			simpleEnemy.isAttacking = true;
			simpleEnemy.unableToRotate = true;
			simpleEnemy.state = SimpleEnemy.State.follow;
		}
		


		instance = Instantiate(beam, user.transform.position + Vector3.up + user.transform.forward, user.transform.rotation, user.transform);
		instance.GetComponent<DealDamage>().StartCoroutine(instance.GetComponent<DealDamage>().DesactiveAfterTime(duration, user, enemyRecoverTime));
	}

	public override void OnDesequip()
	{
		base.OnDesequip();
	}



}