using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
	Animator anim;

	public bool isActive = true;
	public bool cycling = true;
	public int activeTime = 3;
	public int inactiveTime = 3;

	[SerializeField] int count =0;

	private void Start()
	{ 
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		if (!cycling)
			return;

		if (TickManager.tick > TickManager.tickDuration)
			count += 1;

		if (isActive && count > activeTime*100*TickManager.tickDuration || !isActive && count > inactiveTime*100 * TickManager.tickDuration)
			Activate();
	}

	public void Activate()
	{
		isActive = !isActive;	
		anim.SetBool("isActive", isActive);
		count = 0;
	}

}
