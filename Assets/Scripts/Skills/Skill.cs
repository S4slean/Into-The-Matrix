using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Skill : MonoBehaviour
{
	public new string name;
	public Sprite icon;
	public string description;
	public int cost;
	public float coolDownDuration;
	public bool requireTarget = false;

	public float cooldown;


	public virtual void Activate()
	{
		if (cooldown > 0)
			return;

		cooldown = coolDownDuration;

	}

	private void Update()
	{
		if (cooldown > 0)
			cooldown -= Time.deltaTime;
	}

}
