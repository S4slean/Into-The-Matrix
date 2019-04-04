using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Rune : MonoBehaviour
{
	public new string name;
	public string description;
	AvailableRunes runeSet;
	Rune rune;
	GameObject unequipBtn;

	private void Start()
	{
		runeSet = FindObjectOfType<AvailableRunes>();
		rune = runeSet.GetComponent(this.GetType()) as Rune;
		Debug.Log(rune.name);
		if(GetComponent<Button>() != null)
		GetComponent<Button>().onClick.AddListener(ActiveRune);
	}

	public void ActiveRune()
	{
		rune.enabled = true;
		rune.EquipRune();
	}

	public virtual void Active()
	{

	}


	public void EquipRune()
	{
		if (SceneManager.GetActiveScene().buildIndex == 1)
			return;
		runeSet = FindObjectOfType<AvailableRunes>();

		unequipBtn = Resources.Load("UI/RuneUnequipButton") as GameObject;

		if (runeSet.equippedRunes.Count < runeSet.runeSlot)
		{
			runeSet.equippedRunes.Add(this);
			unequipBtn = Instantiate(unequipBtn, GameObject.Find("RuneSlot1").transform);
			unequipBtn.GetComponentInChildren<Text>().text = name;
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
