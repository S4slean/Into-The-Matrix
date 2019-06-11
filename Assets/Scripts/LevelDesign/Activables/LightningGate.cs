using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningGate : MonoBehaviour
{
	public Transform target;
	public LineRenderer lineR;
	public CapsuleCollider capsule;

	public bool cycling;
	public float activeDelay;
	public float inactiveDelay;

	public bool isActive = true;
	private float count;

	public bool overrided = false;
	public Animator anim;

	public void Activate()
	{
		if (overrided)
			return;

		lineR.enabled = !lineR.enabled;
		capsule.enabled = !capsule.enabled;
		isActive = !isActive;
		anim.SetBool("isActive", isActive);
	}

	private void Start()
	{
		Vector3 pos;

		if(target == null)
		{
			pos = new Vector3(0, 1.5f,0);
			isActive = false;
		}
		else
			pos = new Vector3(target.position.x - transform.position.x, 1.5f, target.position.z -transform.position.z);

		lineR.SetPosition(1, pos);

		capsule.direction = 2;
		capsule.transform.localPosition = ((lineR.GetPosition(1) - lineR.GetPosition(0)) / 2) + new Vector3(0,1.5f,0);
		capsule.transform.LookAt(transform.position + new Vector3(0,1.5f,0));
		capsule.height = (lineR.GetPosition(1) - lineR.GetPosition(0)).magnitude;


		lineR.enabled = isActive;
		capsule.enabled = isActive;



	}

	public void Override()
	{
		isActive = false;
		overrided = true;
		lineR.enabled = false;
		capsule.enabled = false;
	}

	private void Update()
	{

		if (cycling)
		{
			if (isActive)
			{
				count += Time.deltaTime;
				if(count > activeDelay)
				{
					isActive = !isActive;
					lineR.enabled = !lineR.enabled;
					capsule.enabled = !capsule.enabled;
					count = 0;
				}

			}
			else
			{
				count += Time.deltaTime;
				if(count > inactiveDelay)
				{
					isActive = !isActive;
					lineR.enabled = !lineR.enabled;
					capsule.enabled = !capsule.enabled;
					count = 0;
				}

			}
		}
	}
}
