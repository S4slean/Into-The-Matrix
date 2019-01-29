using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
	skillList list;
	public Skill skill;

	private void Start()
	{
		list = FindObjectOfType<skillList>();
		skill = list.skills[Random.Range(0, list.skills.Count)];
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			FindObjectOfType<SkillBar>().CreateButton(skill);
			Destroy(gameObject);
		}
	}

}
