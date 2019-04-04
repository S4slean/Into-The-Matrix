using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Skill
{
	GameObject fireballPrefab;

	private void Start()
	{
		fireballPrefab = Resources.Load("Projectiles/Fireball") as GameObject;
	}

	public override void Activate(GameObject user)
	{
		if(CheckIfInLobby())
			return;

		if (user.tag == "Player" && Input.mousePosition.y > 285)
			return;

		if (cooldown > 0)
		{
			if (user.GetComponent<SimpleEnemy>() != null)
				user.GetComponent<SimpleEnemy>().StartCoroutine(user.GetComponent<SimpleEnemy>().WaitForNewCycle(enemyRecoverTime));
			return;
		}

		cooldown = coolDownDuration;

		GameObject Instance = Instantiate(fireballPrefab, user.transform.position + Vector3.up + user.transform.forward, user.transform.rotation);
		Instance.GetComponent<DealDamage>().user = user.tag;

		if (user.GetComponent<SimpleEnemy>() != null)
			user.GetComponent<SimpleEnemy>().StartCoroutine(user.GetComponent<SimpleEnemy>().WaitForNewCycle(enemyRecoverTime));
	}

}
