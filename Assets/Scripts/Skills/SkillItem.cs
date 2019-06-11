using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
	Animator anim;
	GameObject itemPrefab;
	GameObject player;
	skillList list;
	SkillBar skillBar;
	public Skill skill;
	public SpriteRenderer sprRendr;

	private void Start()
	{
		player = FindObjectOfType<CharaController>().gameObject;
		itemPrefab = Resources.Load("skillItem") as GameObject;
		list = FindObjectOfType<skillList>();
		anim = GetComponent<Animator>();
		skillBar = FindObjectOfType<SkillBar>();

		if(skill == null)
			skill = list.skills[Random.Range(0, list.skills.Count)];

		sprRendr.sprite = skill.icon;
	}

	//en cas de collision avec le joueur
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && other.GetComponent<CharaController>() != null)
		{
			bool alreadyEquipped = false;
			int index = 0;
			for(int i = 0; i < skillBar.PlayerSkills.Count; i++ )
			{
				if (skillBar.PlayerSkills[i] != null && skillBar.PlayerSkills[i].name == skill.name)
				{
					alreadyEquipped = true;
					index = i;
				}
			}

			if (alreadyEquipped)
			{
				if(skillBar.PlayerSkills[index].nbOfUse > 2)
				{
					StartCoroutine(Drop(8, -player.transform.forward));
				}
				else
				{
					skillBar.PlayerSkills[index].nbOfUse++;
					skillBar.PlayerSkills[index].RefreshUI();
					Destroy(gameObject);
				}
			}
			else
			{
				bool listFull = true;
				foreach(Skill sk in skillBar.PlayerSkills)
				{
					if(sk == null)
					{
						listFull = false;
					}

				}
				if (!listFull)
				{
					FindObjectOfType<SkillBar>().CreateButton(skill);                                                                                                                               //sinon Crée le boutton et détruit l'objet
					Destroy(gameObject);
				}
				else
				{
					StartCoroutine(Drop(8, -player.transform.forward));
				}
			}

			//if(FindObjectOfType<SkillBar>().PlayerSkills[0] != skill && FindObjectOfType<SkillBar>().PlayerSkills[1] != null && FindObjectOfType<SkillBar>().PlayerSkills[2] != null)			//Si le joueur est déjà full skill
			//{
			//	StartCoroutine(Drop(8, -player.transform.forward));																																//Le drop derrière lui
			//}
			//else
			//{
			//	FindObjectOfType<SkillBar>().CreateButton(skill);																																//sinon Crée le boutton et détruit l'objet
			//	Destroy(gameObject);
			//}
		}
	}

	//Drop de l'objet
	public IEnumerator Drop(int step, Vector3 axe)			
	{
		GetComponent<Animator>().Play("Appear");			//Joue l'anim d'apparition

		for (int i = 0; i < step; i++)						//Déplace l'objet une case dans la direction du vecteur "axe" en "step" frames
		{
			transform.position += axe /(step/2);
			yield return new WaitForEndOfFrame();
		}

		yield break;
	}

}
