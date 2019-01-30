using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
	public int damage;

	private void OnEnable()
	{
		if (gameObject.tag == "Player")
			damage = GetComponent<PlayerStats>().damage;
		if (gameObject.tag == "Enemy")
			damage = GetComponent<simpleEnemy>().difficulty;
	}

	private void OnTriggerEnter(Collider other)
	{


		if(other.tag == "Player")
		{
		}
	}
}
