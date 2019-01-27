using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
	public Skill skill;

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			FindObjectOfType<SkillBar>().CreateButton(skill);
			Destroy(gameObject);
		}
	}
}
