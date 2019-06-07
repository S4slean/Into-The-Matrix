using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  PushableBloc : MonoBehaviour
{
	private int moveStep = 12;

	RaycastHit hit;
	Animator anim;
	BoxCollider boxCollider;

	private void Start()
	{
		anim = GetComponent<Animator>();
		boxCollider = GetComponent<BoxCollider>();
	}

	public IEnumerator MoveBloc(Vector3 axe)
	{
		if (Physics.Raycast(transform.position + Vector3.up, axe, out hit, 2, 9))
			yield break;

			//Déplacement du perso sur chaque frame pendant "moveStep" frame
		for (int i = 0; i < Mathf.Abs(moveStep); i++)
		{
			transform.localPosition = transform.localPosition + (axe / moveStep) * 2;
			yield return new WaitForSeconds(TickManager.tickDuration / 2 / moveStep);
		}
	}

	private void Update()
	{
		if(!Physics.Raycast(transform.position+Vector3.up, Vector3.down, 2))
		{
			StartCoroutine(DelayBeforeFall());
		}
	}

	IEnumerator DelayBeforeFall()
	{
		yield return new WaitForSeconds(.1f);
		anim.Play("Fall");
	}

	public void DesactivateCollision()
	{
		boxCollider.enabled = false;
	}

	private void OnDestroy()
	{
		Debug.Log("bye");
	}
}
