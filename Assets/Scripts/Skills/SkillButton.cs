using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IDragHandler, IEndDragHandler
{
	GameObject player;
	GameObject itemPrefab;
	RectTransform rectTransform;
	SkillBar skillBar;
	Button btn;
	public Skill skill;

	private void Start()
	{
		itemPrefab = Resources.Load("SkillItem" ) as GameObject;
		player = FindObjectOfType<CharaController>().gameObject;
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

			GameObject instance = Instantiate(itemPrefab, player.transform.position + (2*player.transform.forward), Quaternion.Euler(0,0,0));
			instance.GetComponent<SkillItem>().skill = skill;

			Destroy(gameObject);
		}
		else
		{
			rectTransform.anchoredPosition = Vector3.zero;
		}
	}
}
