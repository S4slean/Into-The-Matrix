using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CabineTéléphonique : MonoBehaviour
{
    private CabineUIScript cabineScript;
    public testson SoundDj;

    private void Start()
    {
        SoundDj = GameObject.FindGameObjectWithTag("SoundDj").GetComponent<testson>();

    cabineScript = GameObject.FindGameObjectWithTag("CabineUI").GetComponent<CabineUIScript>();

    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && other.GetComponent<DealDamage>() != null)
		{
			cabineScript.Activate();
            SoundDj.PhoneRing.Play();
		}
	}
}
