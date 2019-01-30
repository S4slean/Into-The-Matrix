using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
	private bool active = true;
	public float delay = 1;


	private void Update()
	{			
		if(delay < 0)
			gameObject.SetActive(false);

		delay -= Time.deltaTime;

	}


}
