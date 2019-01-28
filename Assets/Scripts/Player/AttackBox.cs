using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
	private bool active = true;

	private void Start()
	{
		active = true;
	}

	public void OnTriggerEnter(Collider other)
	{
		Destroy(other.gameObject);
	}

	private void Update()
	{
		if (!active)
			Destroy(gameObject);
		active = false;
	}


}
