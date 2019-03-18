using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionArea : MonoBehaviour
{
	public Skill skill;
	CharaController player;

	private void Update()
	{
		if(player == null)
			player = FindObjectOfType<CharaController>();

		if (player.isMoving)
		{
			Destroy(gameObject);
		}
	}
}
