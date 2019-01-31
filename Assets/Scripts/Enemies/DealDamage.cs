using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
	public int damage;

	private void OnEnable()
	{
		//Detect Damage initiator
		if (gameObject.tag == "Player")
			damage =FindObjectOfType<PlayerStats>().damage;
		if (gameObject.tag == "Enemy")
			damage = GetComponent<simpleEnemy>().difficulty;
	}

	private void OnTriggerEnter(Collider other)
	{
		//Deal Damage to player
		if(other.tag == "Player")
		{
			other.GetComponent<PlayerStats>().health -= damage;
		}
		
		//Deal damage to ennemies
		if(other.tag == "Enemy")
		{
			other.GetComponent<simpleEnemy>().health -= damage;
		}

		//Trigger interaction with NPC
		if(other.tag == "NPC")
		{

		}
	}
}
