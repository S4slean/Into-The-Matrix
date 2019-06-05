﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
	Animator anim;

	private bool isActive = false;
	public bool cycling = true;
	int activeTime = 5;
	int inactiveTime = 5;
	public int delay = 0;

	[SerializeField] int count =0;

	private void Start()
	{ 
		anim = GetComponent<Animator>();
		StartCoroutine(Delay());
	}

	private void Update()
	{
		if (!cycling)
			return;


		if (isActive && count >= activeTime || !isActive && count >= inactiveTime)
			Activate();
	}

	public void Activate()
	{
		isActive = !isActive;	
		anim.SetBool("isActive", isActive);
		count = 0;
	}

	IEnumerator Delay()
	{
		yield return new WaitForSeconds(TickManager.tickDuration * delay);
		TickManager.OnTick += AddCount;
	}

	void AddCount()
	{
		count++;
	}
}
