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
		if (cooldown > 0)
			return;

		cooldown = coolDownDuration;

		Instantiate(fireballPrefab, user.transform.position + Vector3.up + user.transform.forward, user.transform.rotation);
	}

}
