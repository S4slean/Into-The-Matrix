using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Skill
{
	GameObject fireballPrefab;

	private new void Start()
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
			if (user.GetComponent<simpleEnemy>() != null)
				user.GetComponent<simpleEnemy>().StartCoroutine(user.GetComponent<simpleEnemy>().WaitForNewCycle(enemyRecoverTime));
			return;
		}

		GameObject Instance = Instantiate(fireballPrefab, user.transform.position + Vector3.up + user.transform.forward, user.transform.rotation);
		Instance.GetComponent<Projectile>().DestroyOnHit = true;
		Instance.tag = "Player";


		if (user.GetComponent<simpleEnemy>() != null)
			user.GetComponent<simpleEnemy>().StartCoroutine(user.GetComponent<simpleEnemy>().WaitForNewCycle(enemyRecoverTime));

        PowerUsed();
        cooldown = coolDownDuration;
    }

}
