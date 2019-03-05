using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeRoll : Skill
{
	public int distance = 3;
	GameObject skillUser;
	GameObject instance;
	Vector3 dodgeDir;
	private new Collider collider;

	[SerializeField]bool isActive = false;

	public override void Activate(GameObject user)
	{
		if (cooldown > 0)
		{
			if (user.GetComponent<SimpleEnemy>() != null)
				user.GetComponent<SimpleEnemy>().StartCoroutine(user.GetComponent<SimpleEnemy>().WaitForNewCycle(enemyRecoverTime));
			return;
		}

		distance = 3;
		skillUser = user;

		collider = skillUser.GetComponent<CapsuleCollider>();
		collider.enabled = false;


		instance = Instantiate(Resources.Load("Buffs/GlitchParticles") as GameObject, user.transform);
		isActive = true;
		cooldown = coolDownDuration;

		StartCoroutine(WaitForDesactivation());
	}


	private void Update()
	{
		if (cooldown > 0)
			cooldown -= Time.deltaTime;

		if (!isActive)
			return;

		if (skillUser.tag == "Player")
		{
			skillUser.GetComponent<CharaController>().SetPlayerMovement(false, false);

			WaitForSwipe();
		}

		if(skillUser.tag == "Enemy")
		{
			StartCoroutine(WaitForAttack());
		}


	}

	public void WaitForSwipe()
	{
		CharaController player = skillUser.GetComponent<CharaController>();
		if(player.swipe.magnitude > player.swipeTolerance && Input.GetMouseButtonUp(0))
		{

			isActive = false;
			if (Mathf.Abs(player.swipe.x) > Mathf.Abs(player.swipe.y))
			{
				dodgeDir = Vector3.right * Mathf.Sign(player.swipe.x);
			}
			else
			{
				dodgeDir = Vector3.forward * Mathf.Sign(player.swipe.y);
			}
			StartCoroutine(Dodge(skillUser));
		}
	}

	public IEnumerator WaitForAttack()
	{
		isActive = false;
		SimpleEnemy enemy = skillUser.GetComponent<SimpleEnemy>();
		yield return new WaitForSeconds(.6f);
		if(Mathf.Abs(enemy.enemyToPlayer.x) > Mathf.Abs(enemy.enemyToPlayer.z))
		{
			dodgeDir = Vector3.right * Mathf.Sign(enemy.enemyToPlayer.x);
			distance = Mathf.Abs(Mathf.RoundToInt(enemy.enemyToPlayer.x/2));
		}
		else
		{
			dodgeDir = Vector3.forward * Mathf.Sign(enemy.enemyToPlayer.z);
			distance = Mathf.Abs(Mathf.RoundToInt(enemy.enemyToPlayer.z / 2));
		}
		
		StartCoroutine(Dodge(skillUser));
		yield break;
	}

	public IEnumerator Dodge(GameObject user)
	{
		if(Physics.Raycast(user.transform.position,dodgeDir, distance * 2, 9))
		{
			distance -= 1;
			StartCoroutine(Dodge(user));
			yield break;
		}
		else
		{


			for (int i = 0; i < 5; i++)
			{
				user.transform.position += dodgeDir*distance*2/5 ;
				yield return new WaitForEndOfFrame();
			}

			if(user.tag == "Player")
			{
				skillUser.GetComponent<CharaController>().SetPlayerMovement(true, true);
			}
			Destroy(instance);
			collider.enabled = true;

			if (user.GetComponent<SimpleEnemy>() != null)
				user.GetComponent<SimpleEnemy>().StartCoroutine(user.GetComponent<SimpleEnemy>().WaitForNewCycle(enemyRecoverTime));
		}


	}

	IEnumerator WaitForDesactivation()
	{

		yield return new WaitForSeconds(1f);

		if (!isActive)
			yield break;

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
		Destroy(instance);
		if (skillUser.tag == "Player")
		{
			skillUser.GetComponent<CharaController>().SetPlayerMovement(true, true);
		}
		collider.enabled = true;
		isActive = false;
	}
}
