using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
	Animator anim;
	GameObject itemPrefab;
	GameObject player;
	skillList list;
	public Skill skill;

	private void Start()
	{
		player = FindObjectOfType<CharaController>().gameObject;
		itemPrefab = Resources.Load("skillItem") as GameObject;
		list = FindObjectOfType<skillList>();
		anim = GetComponent<Animator>();

		if(skill == null)
			skill = list.skills[Random.Range(0, list.skills.Count)];
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if(FindObjectOfType<SkillBar>().PlayerSkills[0] != null && FindObjectOfType<SkillBar>().PlayerSkills[1] != null && FindObjectOfType<SkillBar>().PlayerSkills[2] != null)
			{
				StartCoroutine(Drop(8, -player.transform.forward));
			}
			else
			{
				FindObjectOfType<SkillBar>().CreateButton(skill);
				Destroy(gameObject);
			}
		}
	}

	public IEnumerator Drop(int step, Vector3 axe)
	{
		GetComponent<Animator>().Play("Appear");

		for (int i = 0; i < step; i++)
		{
			transform.position += axe /(step/2);
			yield return new WaitForEndOfFrame();
		}

		yield break;
	}

}
