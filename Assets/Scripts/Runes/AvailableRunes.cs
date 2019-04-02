using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableRunes : MonoBehaviour
{
	public int runeSlot = 1;

	public List<Rune> unlockedRunes;

	public List<Rune> equippedRunes;


	private void Start()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	public void EquipRune(Rune rune)
	{
		rune.enabled = true;
		equippedRunes.Add(rune);
	}
}
