using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
	public GameObject linkedTP;
	public bool active = true;

	private void OnTriggerEnter(Collider other)
	{
		if (active)
		{
			if (other.tag == "Player" || other.tag == "Enemy" || other.tag == "Projectile")
				StartCoroutine(DelayTP(other));
		}
	}

	IEnumerator DelayTP(Collider other)
	{
		yield return new WaitForSeconds(.1f);
		other.transform.position = new Vector3(linkedTP.transform.position.x, other.transform.position.y, linkedTP.transform.position.z);
		linkedTP.GetComponent<TP>().active = false;
	}

	private void OnTriggerExit(Collider other)
	{
		active = true;
	}
}
