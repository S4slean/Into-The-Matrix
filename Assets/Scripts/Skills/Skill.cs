using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Skill : MonoBehaviour
{
	[Header ("Skill Infos")]
	public new string name;
	public Sprite icon;
	public string description;

	[Header ("Skill Stats")]
	[Tooltip("The cost at the shop")]
	public int cost;
	[Tooltip("The time between two successive activations by the player")]
	public float coolDownDuration;
	[Tooltip("The player needs to select a specific target ?")]
	public bool requireTarget = false;
	[Tooltip("")]
	public float enemyActivationRange = 5;
	[Tooltip("The anticipation animation duration")]
	public float enemyLaunchTime = .75f;
	[Tooltip("The Recover animation duration (Vulnerability Frame)")]
	public float enemyRecoverTime = 1;

	[Header ("Debug Status (NE PAS MODIFIER)")]
	public float cooldown;

	//Active the skill
	public virtual void Activate(GameObject user)
	{
		if (cooldown > 0)
			return;

		cooldown = coolDownDuration;
	}

	//Activation by enemy, contains placement functions if needed
	public virtual IEnumerator EnemyUse(SimpleEnemy enemy)
	{
		yield return new WaitForSeconds(enemyLaunchTime);
		Activate(enemy.gameObject);
		yield break;
	}

	public virtual IEnumerator useSkill(Vector3 pos)
	{
		yield break;
	}

	public virtual void OnDesequip()
	{

	}

	private void Update()
	{
		if (cooldown > 0)
			cooldown -= Time.deltaTime;
	}

}
