using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
	[Range (0, 300) ]public int swipeTolerance = 30;
	public float stepDistance;
	public int moveStep = 30;
	public float stepDuration = 1;

	[SerializeField] bool ismoving = false;
	private Vector3 startMousePos;
	private Vector3 hitPosition;



	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			startMousePos = Input.mousePosition;
			print(startMousePos);
		}


		if (Input.GetMouseButtonUp(0) && !ismoving)
		{

			hitPosition = Input.mousePosition;

			print(hitPosition);

			Vector3 swipe = hitPosition - startMousePos;

			if (swipe.magnitude < swipeTolerance)
				return;

			if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
			{
				print("yo");
				int step = Mathf.RoundToInt(swipe.x / stepDistance);
				ismoving = true;
				StartCoroutine(Move(Vector3.right * Mathf.Sign(step), 1));
			}

			if (Mathf.Abs(swipe.x) < Math.Abs(swipe.y))
			{
				int step = Mathf.RoundToInt(swipe.y / stepDistance);
				ismoving = true;
				StartCoroutine(Move(Vector3.forward * Mathf.Sign(step), 1));
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
