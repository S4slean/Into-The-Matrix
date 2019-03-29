using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
	Animator anim;

	public bool isActive = true;
	public bool cycling = true;
	public float activeTime = 3;
	public float inactiveTime = 3;

	[SerializeField] float count;

	private void Start()
	{ 
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		if (!cycling)
			return;

		if (isActive && count > activeTime || !isActive && count > inactiveTime)
			Activate();

		count += Time.deltaTime;
	}

	public void Activate()
	{
		isActive = !isActive;	
		anim.SetBool("isActive", isActive);
		count = 0;
	}

}
