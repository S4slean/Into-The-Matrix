using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IDragHandler, IEndDragHandler
{
	RectTransform rectTransform;
	SkillBar skillBar;
	Button btn;
	public Skill skill;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		skillBar = FindObjectOfType<SkillBar>();
		btn = GetComponent<Button>();
		btn.onClick.AddListener(skill.Activate);
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if(Input.mousePosition.y > 285)
		{
			int index = skillBar.PlayerSkills.IndexOf(skill);
			skillBar.PlayerSkills[index] = null; ;
			Destroy(gameObject);
		}
		else
		{
			rectTransform.anchoredPosition = Vector3.zero;
		}
	}
}
