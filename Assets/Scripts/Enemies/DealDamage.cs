using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
	public int damage = 1;
	public bool DoT = false;

	public float dmgTick = 1;
	float tick;

	private void OnTriggerEnter(Collider other)
	{
		tick = dmgTick;
		ApplyDamage(other);

	}


	private void OnTriggerStay(Collider other)
	{
		if (!DoT)
			return;

		tick -= Time.deltaTime;

		if(tick <= 0)
		{
			ApplyDamage(other);
			tick = dmgTick;
		}
	}

	private void ApplyDamage(Collider other)
	{
		//Deal Damage to player
		if (other.tag == "Player")
		{
			FindObjectOfType<PlayerStats>().health -= damage;
			//Ajouter Feedbacks visuels !!								<=============
		}

		//Deal damage to ennemies
		if (other.tag == "Enemy")
		{
			other.GetComponent<simpleEnemy>().health -= damage;
		}

		//Trigger interaction with NPC
		if (other.tag == "PNJ" && gameObject.tag == "Player")
		{
			other.GetComponent<PNJ_Dialogue>().DisplayText();
		}
	}


}
