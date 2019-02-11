using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBeam : Skill
{
	public GameObject beam;
	public float duration;
	Transform player;
	GameObject instance;

	public override void Activate(GameObject user)
	{
		player = GameObject.FindObjectOfType<CharaController>().transform;

		if (cooldown > 0)
			return;

		cooldown = coolDownDuration;

		player.GetComponent<CharaController>().SetPlayerMovement(true, false);
		instance = Instantiate(beam, player.position + Vector3.up + player.forward, player.rotation, player);
	}

	IEnumerator DesactiveAfterTime()
	{
		yield return new WaitForSeconds(duration);
		Destroy(instance);
		yield break;
	}


}
