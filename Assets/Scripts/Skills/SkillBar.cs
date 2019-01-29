using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{

	public GameObject SkillButtonPrefab;
	public List<Skill> PlayerSkills = new List<Skill>(3);

   public void CreateButton(Skill skill)
	{
		if (PlayerSkills.Count > 3)
			return;

		for (int i =0; i < PlayerSkills.Count ; i++)
		{
			if(PlayerSkills[i] == null)
			{
				PlayerSkills[i] = skill;
				GameObject Instance = Instantiate(SkillButtonPrefab, transform.GetChild(i));
				SkillButton skillButton = Instance.GetComponent<SkillButton>();
				skillButton.skill = skill;
				Instance.transform.GetChild(0).GetComponent<Text>().text = skill.name;
				break;
			}
		}

	}
}
