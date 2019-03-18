using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
	public Animator anim;
	public UnityEvent Activate;

	private void OnTriggerEnter(Collider other)
	{
		Activate.Invoke();
		anim.SetTrigger("Impulse");
	}

	private void OnTriggerExit(Collider other)
	{
		Activate.Invoke();
		anim.SetTrigger("Impulse");
	}
}
