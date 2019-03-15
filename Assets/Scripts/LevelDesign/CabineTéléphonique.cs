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
		Debug.Log(cabineScript.name);
    }

	private void OnTriggerEnter(Collider other)
	{
		print("wo");
		if (other.tag == "Player")
		{
			cabineScript.Activate();
		}
	}
}
