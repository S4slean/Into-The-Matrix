using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caseTarget : MonoBehaviour
{
	public void ActivateTarget()
	{
			transform.GetComponentInParent<SelectionArea>().skill.StartCoroutine(transform.GetComponentInParent<SelectionArea>().skill.useSkill(transform.position -  new Vector3(0,0.01f,0)));
			Destroy(transform.parent.gameObject);
	}
}
