using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBloc : MonoBehaviour
{
	private int moveStep = 12;

	RaycastHit hit;

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
}
