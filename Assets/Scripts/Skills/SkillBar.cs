using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBar : MonoBehaviour
{

	public GameObject SkillButtonPrefab;
	public List<Skill> PlayerSkills;

   public void CreateButton(Skill skill)
	{
		if (PlayerSkills.Count > 3)
			return;

		PlayerSkills.Add(skill);
		Instantiate(SkillButtonPrefab, transform.GetChild(PlayerSkills.Count - 1));
	}
}
