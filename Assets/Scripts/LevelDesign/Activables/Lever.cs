﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
	public Animator anim;
	public MaterialChange changer;
	public UnityEvent Activate;
	bool isActive = false;
    public testson SoundDj;

    public void Start()
    {
        SoundDj = GameObject.FindGameObjectWithTag("SoundDj").GetComponent<testson>();
    }
    private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && other.GetComponent<DealDamage>())
		{
            ActivateLever();
		}
	}

    public void ActivateLever()
    {
        SoundDj.LevierOn.Play();
        Activate.Invoke();
        anim.SetTrigger("Impulse");
		isActive = !isActive;


    }
}
