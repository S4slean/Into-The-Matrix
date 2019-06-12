using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyButton : MonoBehaviour
{
	public PlayerStats stats;

	private void Start()
	{
		stats = FindObjectOfType<PlayerStats>();
	}

	private void OnEnable()
	{
		
	}

	public void ActivateKey()
	{

	}
}
