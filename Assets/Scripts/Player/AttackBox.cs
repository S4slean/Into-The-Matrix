using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
	public float delay = 1;
	private float timer;

	private void OnEnable()
	{
		timer = delay;
	}

	private void Update()
	{			
		if(timer < 0)
			gameObject.SetActive(false);

		timer -= Time.deltaTime;

	}


}
