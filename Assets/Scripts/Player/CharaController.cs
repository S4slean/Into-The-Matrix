using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
	[Header("References")]
	public GameObject AttackBox;

	[Header ("Move Stats")]
	[Range (0, 300) ]public int swipeTolerance = 30;
	public float stepDistance;
	public int moveStep = 30;
	public float stepDuration = 1;
	public float delayBeforeRun = .7f;

	[Header ("Attack Stats")]
	public float attackLength = 1;
	public float attackWidth = 1;
	public static Collider[] targets;

	[Header ("States")]
	[SerializeField] bool ismoving = false;

	private Vector3 startMousePos;
	private Vector3 hitPosition;
	private Vector3 swipe;
	private float holdedTime;

	private void Start()
	{
		if (AttackBox == null)
			AttackBox = transform.GetChild(1).gameObject;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			startMousePos = Input.mousePosition;
			holdedTime = 0;
		}

		hitPosition = Input.mousePosition;
		swipe = hitPosition - startMousePos;

		if (Input.GetMouseButtonUp(0) && !ismoving)
		{
			if (swipe.magnitude < swipeTolerance)
			{
				Attack();
				return;
			}
			HandleMove();			
		}

		if (Input.GetMouseButton(0) && holdedTime > delayBeforeRun && !ismoving)
		{
			HandleMove();
		}

		holdedTime += Time.deltaTime;
	}

	public void HandleMove()
	{
		//Mouvement Horizontal
		if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
		{
			int step = Mathf.RoundToInt(swipe.x / stepDistance);
			ismoving = true;
			StartCoroutine(Move(Vector3.right * Mathf.Sign(step)));


		}

		//mouvement Vertical
		if (Mathf.Abs(swipe.x) < Math.Abs(swipe.y))
		{
			int step = Mathf.RoundToInt(swipe.y / stepDistance);
			ismoving = true;
			StartCoroutine(Move(Vector3.forward * Mathf.Sign(step)));
		}
	}

	IEnumerator Move(Vector3 axe)
	{
		//Déplacement du perso sur chaque frame pendant "moveStep" frame
		for (int i = 0; i < Mathf.Abs(moveStep); i++)
		{
			transform.localPosition = transform.localPosition + (axe / moveStep) * 2;

			yield return new WaitForSeconds(stepDuration/moveStep);
		}
		ismoving = false;
	}

	void Attack()
	{
		print("Attack");
		AttackBox.SetActive(true);
	}
}
