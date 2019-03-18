using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caseTarget : MonoBehaviour
{
	public void ActivateTarget()
	{
			print("target");

			transform.GetComponentInParent<SelectionArea>().skill.StartCoroutine(transform.GetComponentInParent<SelectionArea>().skill.useSkill(transform.position));
			Destroy(transform.parent.gameObject);
	}
}
