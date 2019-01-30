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
				CopyComponent(skill, Instance);
				skillButton.index = i;
				Instance.transform.GetChild(0).GetComponent<Text>().text = skill.name;
				break;
			}
		}

	}

	Component CopyComponent(Component original, GameObject destination)
	{
		System.Type type = original.GetType();
		Component copy = destination.AddComponent(type);
		// Copied fields can be restricted with BindingFlags
		System.Reflection.FieldInfo[] fields = type.GetFields();
		foreach (System.Reflection.FieldInfo field in fields)
		{
			field.SetValue(copy, field.GetValue(original));
		}
		return copy;
	}
}
