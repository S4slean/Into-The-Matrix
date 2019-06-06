using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
	Animator anim;

	public bool isActive = true;
	public bool cycling = true;
	int activeTime = 4;
	int inactiveTime = 6;
	public int delay = 0;

	public bool overrided = false;

	[SerializeField] int count =0;

	private void Start()
	{ 
		anim = GetComponent<Animator>();
		StartCoroutine(Delay());
	}

	private void Update()
	{
		if (!cycling || overrided)
			return;


		if (isActive && count > activeTime || !isActive && count > inactiveTime)
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
		TickManager.OnTick += delegate (object sender, TickManager.OnTickEventArgs e)
		{
			count++;
		};
	}

	void AddCount()
	{
		count++;
	}

	public void Override()
	{
		overrided = true;
		anim.SetBool("isActive", false);
	}
}
