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

		if (cooldown > 0)
			return;

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
		StartCoroutine(DesactiveAfterTime(user));
	}

	IEnumerator DesactiveAfterTime(GameObject user)
	{
		yield return new WaitForSeconds(duration);
		Destroy(instance);

		if(user.tag == "Player" )
			player.GetComponent<CharaController>().SetPlayerMovement(true, true);

		if (user.GetComponent<SimpleEnemy>() != null)
		{
			simpleEnemy.isAttacking = false;
			simpleEnemy.unableToRotate = false;
			StartCoroutine(simpleEnemy.WaitForNewCycle(enemyRecoverTime));
		}

		yield break;
	}


}