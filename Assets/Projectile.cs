﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

	public enum Behaviour { oneHit, goThrough, bounce, split}

	public float projectileSpeed = 1;
	public float range = 10;
	public Behaviour behaviour = Behaviour.oneHit;

	private Vector3 startPos;

	private void Start()
	{
		startPos = transform.position;
	}

	void Update()
    {
		transform.position += transform.forward * projectileSpeed * Time.deltaTime;

		CheckRange();
    }

	private void OnTriggerEnter(Collider other)
	{
		switch (behaviour)
		{
			case Behaviour.oneHit:
				{
					Destroy();
					break;
				}
		}
	}

	private void CheckRange()
	{
		if (Vector3.Magnitude(transform.position - startPos) > range)
			Destroy();
			
	}
	public void Destroy()
	{
		Destroy(gameObject);

	}
}
