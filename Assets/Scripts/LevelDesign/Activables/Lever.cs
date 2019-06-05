﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
	public Animator anim;

	public UnityEvent Activate;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && other.GetComponent<DealDamage>())
		{
            ActivateLever();
		}
	}

    public void ActivateLever()
    {
        Activate.Invoke();
        anim.SetTrigger("Impulse");
    }
}
