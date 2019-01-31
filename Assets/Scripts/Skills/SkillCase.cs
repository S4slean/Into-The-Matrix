using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCase : MonoBehaviour
{
	RectTransform rectTransform;


	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2(Screen.width / 3 - 65, 135);
	}

}

