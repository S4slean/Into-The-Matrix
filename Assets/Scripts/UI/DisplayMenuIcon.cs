using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMenuIcon : MonoBehaviour
{
	public Animator anim;


	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<CharaController>() == null)
			return;

		anim.Play("Appear");
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<CharaController>() == null)
			return;

		anim.Play("Disappear");
	}
}
