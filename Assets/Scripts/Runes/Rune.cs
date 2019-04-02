using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rune : MonoBehaviour
{
	public string title;
	public string description;
	AvailableRunes runeSet;
	GameObject unequipBtn;



	public virtual void Active()
	{

	}


	public void OnEnable()
	{
		runeSet = FindObjectOfType<AvailableRunes>();
		unequipBtn = Resources.Load("UI/RuneUnequipButton") as GameObject;

		if (runeSet.equippedRunes.Count < runeSet.runeSlot)
		{
			runeSet.equippedRunes.Add(this);
			unequipBtn = Instantiate(unequipBtn, GameObject.Find("RuneSlot1").transform);
			unequipBtn.GetComponentInChildren<Text>().text = title;
			unequipBtn.GetComponent<Button>().onClick.AddListener(Unequip);

		}
		else
		{
			this.enabled = false;
		}
		
	}


	public void Unequip()
	{
		runeSet.equippedRunes.Remove(this);
		Destroy(unequipBtn);
		this.enabled = false;
	}


	private void OnMouseOver()
	{
		
	}
}
