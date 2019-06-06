using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueBox : MonoBehaviour
{
	Vector3 anchor;
    public bool loop;
	public List<string> dialogueLines;
	RectTransform rt;
	typewritterScript TypeWritter;
	Text text;
	public int index = 0;
	float displayTime;

	private void Awake()
	{
		rt = transform.parent.GetComponent<RectTransform>();
		TypeWritter = GetComponent<typewritterScript>();
		text = GetComponent<Text>();
		anchor = transform.parent.parent.parent.position + Vector3.up * 3;
		rt.anchoredPosition = Camera.main.WorldToScreenPoint(anchor);
	}

	public void DisplayDialogue()
	{
		print(dialogueLines.Count);
		displayTime = 0;
		TypeWritter.NextText(dialogueLines[index]);

		index++;
		if (index > dialogueLines.Count - 1 && loop == true)
			index = 0;
	}

	private void Update()
	{

		rt.sizeDelta = new Vector2(Mathf.Ceil(text.text.Length * 10 ), Mathf.Ceil(text.text.Length / 40)+1 * 50);

		if (displayTime > 5 && text.text.Length > 0)
			TypeWritter.NextText("");

	

		if (text.text.Length == 0) 
			rt.sizeDelta = Vector2.zero;
		else
			displayTime += Time.deltaTime;
	}
}
