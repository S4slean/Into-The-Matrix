using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  PushableBloc : MonoBehaviour
{
	private int moveStep = 12;

	RaycastHit hit;
	Animator anim;
	BoxCollider boxCollider;

	bool felt = false;
	public Vector3 lastMove;

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
			if (felt)
				yield break;
			transform.localPosition = transform.localPosition + (axe / moveStep) * 2;
			yield return new WaitForSeconds(TickManager.tickDuration / 2 / moveStep);
		}

		lastMove = axe;
	}

	private void Update()
	{
		if(!Physics.Raycast(transform.position+Vector3.up, Vector3.down, 2)&& !felt)
		{
			Debug.Log("Fall at " + transform.position);
			StartCoroutine(DelayBeforeFall());
		}
	}

	IEnumerator DelayBeforeFall()
	{
		Vector3 tempPos = transform.position;
		felt = true;
		yield return new WaitForSeconds(.5f);
		transform.parent = null;
		transform.position = new Vector3(Mathf.Round(tempPos.x / 2) * 2, 0, (Mathf.Round((tempPos.z) / 2) * 2) - 1);
		Debug.Log(transform.position);
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
