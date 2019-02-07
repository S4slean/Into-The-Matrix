using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBeam : Skill
{
	public override void Activate(GameObject user)
	{
		if (cooldown > 0)
			return;

		cooldown = coolDownDuration;
	}
}
