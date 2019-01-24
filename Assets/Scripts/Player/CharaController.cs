using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
	public enum Direction{ None, Right, Left, Up, Down	}

	public Direction dir;
	private Vector3 startMousePos;

	private void Update()
	{
		GetInput();
		MoveTo();
	}



	private void GetInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			startMousePos = Input.mousePosition;
		}


		if (Input.GetMouseButtonUp(0))
		{
			Vector3 swipe = Input.mousePosition - startMousePos;
			dir = Direction.None;
			if (swipe.x > 10 && Mathf.Abs(swipe.y) < 10)
				dir = Direction.Right;
			if (swipe.x < 10 && Mathf.Abs(swipe.y) < 10)
				dir = Direction.Left;
			if (swipe.y > 10 && Mathf.Abs(swipe.x) < 10)
				dir = Direction.Up;
			if (swipe.y < 10 && Mathf.Abs(swipe.x) < 10)
				dir = Direction.Down;
		}

	}

	private void MoveTo()
	{
		
	}
}
