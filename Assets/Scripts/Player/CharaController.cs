using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
	public float stepDistance;
	public int moveStep = 30;
	public float stepDuration = 1;

	[SerializeField] bool ismoving = false;
	private Vector3 startMousePos;
	private Vector3 hitPosition;


	private void OnMouseDown()
	{
		startMousePos = transform.position;
		print(startMousePos);
	}

	private void OnMouseUp()
	{
		if (Input.GetMouseButtonUp(0) && !ismoving)
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit)) //check if the ray hit something
			{
				hitPosition = hit.point; //use this position for what you want to do
			}
			else
				hitPosition = startMousePos;

			print(hitPosition);

			Vector3 swipe = hitPosition - startMousePos;

			if (swipe.magnitude < 1)
				return;

			if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.z))
			{
				int step = Mathf.RoundToInt(swipe.x / stepDistance);
				ismoving = true;
				StartCoroutine(Move(Vector3.right * Mathf.Sign(step), step));
			}

			if (Mathf.Abs(swipe.x) < Math.Abs(swipe.z))
			{
				int step = Mathf.RoundToInt(swipe.z / stepDistance);
				ismoving = true;
				StartCoroutine(Move(Vector3.forward * Mathf.Sign(step), step));
			}
		}
	}



	IEnumerator Move(Vector3 axe, int step)
	{
		Debug.Log(step);

		for (int i = 0; i < Mathf.Abs(moveStep * step); i++)
		{
			transform.localPosition = transform.localPosition + (axe / moveStep) * 2;

			yield return new WaitForSeconds(stepDuration/moveStep);
		}
		ismoving = false;
	}
}
