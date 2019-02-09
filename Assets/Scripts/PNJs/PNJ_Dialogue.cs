using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ_Dialogue : MonoBehaviour
{
	public dialogueBox dialBox;

	private void Start()
	{
		dialBox = transform.GetComponentInChildren<dialogueBox>();
	}

	public void DisplayText()
	{
		print("interaction");
		dialBox.DisplayDialogue();
	}
}
