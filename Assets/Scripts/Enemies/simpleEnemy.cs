using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleEnemy : MonoBehaviour
{
	[Header ("Stats")]
	[Range(1,1000)] public float difficulty;
	public int moveStep = 8;

	[Header("States")]
	[SerializeField] private bool ismoving = false;
	private GameObject player;

	private void Start()
	{
		player = FindObjectOfType<CharaController>().gameObject;
	}




	IEnumerator Move(Vector3 axe, int step)
	{
		Debug.Log(step);

		for (int i = 0; i < Mathf.Abs(moveStep * step); i++)
		{
			transform.localPosition = transform.localPosition + (axe / moveStep) * 2;

			yield return new WaitForEndOfFrame();
		}
		ismoving = false;
	}
}
