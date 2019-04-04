using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CabineTéléphonique : MonoBehaviour
{
    private CabineUIScript cabineScript;

    private void Start()
    {
        cabineScript = GameObject.FindGameObjectWithTag("CabineUI").GetComponent<CabineUIScript>();
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && other.GetComponent<DealDamage>() != null)
		{
			cabineScript.Activate();
		}
	}
}
