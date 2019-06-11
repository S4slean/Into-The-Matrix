using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popUp : MonoBehaviour
{

	Animator anim;
	private void Start()
	{
		anim = GetComponent<Animator>();
	}

	// Start is called before the first frame update
	private void OnEnable()
	{
		anim = GetComponent<Animator>();
		anim.Play("Appear");
	}

	public void Desactivate()
	{
		gameObject.SetActive(false);
	}
}
