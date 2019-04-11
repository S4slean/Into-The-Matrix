using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
	public int damage = 1;
	public string user = "Player";
	public bool DamageOverTime = false;

	public float dmgTick = 1;
	float tick;



	private void OnTriggerEnter(Collider other)
	{
		tick = dmgTick;
		ApplyDamage( other);

	}


	private void OnTriggerStay(Collider other)
	{
		if (!DamageOverTime)
			return;

		if(TickManager.tick > TickManager.tickDuration)
		{
			ApplyDamage(other);
		}
	}

	private void ApplyDamage(Collider other)
	{
		Debug.Log(user);
		if (other.tag == null)
			return;

		//Deal Damage to player
		if (other.tag == "Player" && other.GetComponent<CharaController>() != null)
		{
			if (other.tag == gameObject.tag)
				return;

			FindObjectOfType<PlayerStats>().TakeDamage(damage);
			//Ajouter Feedbacks visuels !!								<=============
		}

		//Deal damage to ennemies
		if (other.tag == "Enemy" && !other.isTrigger)
		{
			if (other.tag == gameObject.tag)
				return;

			other.GetComponent<SimpleEnemy>().health -= damage;
		}

		//Trigger interaction with NPC
		if (other.tag == "PNJ" && gameObject.tag == "Player")
		{
			other.GetComponent<PNJ_Dialogue>().DisplayText();
		}
	}

	public void DestroySelf()
	{
		Destroy(gameObject);
	}

	public IEnumerator DesactiveAfterTime(float duration,GameObject user, int enemyRecoverTime)
	{
		yield return new WaitForSeconds(duration);
		Destroy(gameObject);

		if (user.tag == "Player")
			user.gameObject.GetComponent<CharaController>().SetPlayerMovement(true, true);

		if (user.GetComponent<SimpleEnemy>() != null)
		{
			SimpleEnemy simpleEnemy = user.GetComponent<SimpleEnemy>();
			simpleEnemy.isAttacking = false;
			simpleEnemy.unableToRotate = false;
			simpleEnemy.StartCoroutine(simpleEnemy.WaitForNewCycle(enemyRecoverTime));
		}

		yield break;
	}


}
