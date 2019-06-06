using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

	public enum Behaviour { oneHit, goThrough, bounce, split}

	public float projectileSpeed = .5f;
	private float range = 20;
	public bool explode = false;
	public bool DestroyOnHit = false;
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
		if (other.isTrigger)
			return;

		switch (behaviour)
		{
			case Behaviour.oneHit:
				{
					if (explode)
					{
						GameObject instance = Instantiate(Resources.Load("Projectiles/Explosion") as GameObject, other.transform.position, transform.rotation);
						instance.tag = gameObject.tag;
					}
					if (DestroyOnHit)
						Destroy(gameObject);

					gameObject.SetActive(false);
					break;
				}
		}
	}

	private void CheckRange()
	{
		if (Vector3.Magnitude(transform.position - startPos) > range)
			gameObject.SetActive(false);

	}

}
