using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IDragHandler, IEndDragHandler
{
	public int index;
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
			skillBar.PlayerSkills[index] = null; ;

			GameObject instance = Instantiate(itemPrefab, player.transform.position + Vector3.up, Quaternion.Euler(0,0,0));
			instance.GetComponent<SkillItem>().skill = skill;
			if(!Physics.Raycast(player.transform.position + Vector3.up, player.transform.forward, 2))
				instance.GetComponent<SkillItem>().StartCoroutine(instance.GetComponent<SkillItem>().Drop(8, player.transform.forward));
			else if (!Physics.Raycast(player.transform.position + Vector3.up, player.transform.right, 2))
				instance.GetComponent<SkillItem>().StartCoroutine(instance.GetComponent<SkillItem>().Drop(8, player.transform.right));
			else if (!Physics.Raycast(player.transform.position + Vector3.up, -player.transform.right, 2))
				instance.GetComponent<SkillItem>().StartCoroutine(instance.GetComponent<SkillItem>().Drop(8, -player.transform.right));
			else if (!Physics.Raycast(player.transform.position + Vector3.up, -player.transform.forward, 2))
				instance.GetComponent<SkillItem>().StartCoroutine(instance.GetComponent<SkillItem>().Drop(8, -player.transform.forward));

			Destroy(gameObject);
		}
		else
		{
			rectTransform.anchoredPosition = Vector3.zero;
		}
	}
}
