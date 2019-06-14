using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
	public GameObject linkedTP;
	public bool active = true;

	private void OnTriggerStay(Collider other)
	{
		if (active)
		{
			if (other.GetComponent<CharaController>() != null || other.tag == "Projectile")
				StartCoroutine(DelayTP(other.transform));
			if (other.tag == "Enemy")
				StartCoroutine(DelayTP(other.transform.parent));
		}
	}

	IEnumerator DelayTP(Transform other)
	{
		other.gameObject.SetActive(false);
		linkedTP.GetComponent<TP>().active = false;
		yield return new WaitForSeconds(.1f);
		other.position = new Vector3(linkedTP.transform.position.x, other.transform.position.y, linkedTP.transform.position.z);
		other.gameObject.SetActive(true);
		if(other.name == "Player")
		{
			other.GetComponent<CharaController>().isMoving = false;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		active = true;
	}
}
