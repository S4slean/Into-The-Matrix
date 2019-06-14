using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
	Animator anim;
	public Animator meshAnim;

	public bool isActive = true;
	public bool cycling = true;
	private int activeTime = 5;
	private int inactiveTime = 5;
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


		if (isActive && count >= activeTime || !isActive && count >= inactiveTime)
			Activate();
	}

	public void Activate()
	{
		isActive = !isActive;	
		anim.SetBool("isActive", isActive);
		count = 0;
	}

	public void Anticipation()
	{
		meshAnim.Play("Play");
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

	public void Override()
	{
		overrided = true;
		anim = GetComponent<Animator>();
		anim.SetBool("isActive", false);
	}
}
