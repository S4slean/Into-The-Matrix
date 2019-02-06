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

	//en cas de collision avec le joueur
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if(FindObjectOfType<SkillBar>().PlayerSkills[0] != null && FindObjectOfType<SkillBar>().PlayerSkills[1] != null && FindObjectOfType<SkillBar>().PlayerSkills[2] != null)			//Si le joueur est déjà full skill
			{
				StartCoroutine(Drop(8, -player.transform.forward));																																//Le drop derrière lui
			}
			else
			{
				FindObjectOfType<SkillBar>().CreateButton(skill);																																//sinon Crée le boutton et détruit l'objet
				Destroy(gameObject);
			}
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
