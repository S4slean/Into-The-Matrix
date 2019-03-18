using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossTarget : SelectionArea
{
	public int distance;
	public GameObject targetCase;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < distance; i++)
		{
			Instantiate(targetCase, transform.position + Vector3.right * (i + 1) * 2, Quaternion.identity, transform);
			Instantiate(targetCase, transform.position + Vector3.back * (i + 1) * 2, Quaternion.identity, transform);
			Instantiate(targetCase, transform.position + Vector3.left * (i + 1) * 2, Quaternion.identity, transform);
			Instantiate(targetCase, transform.position + Vector3.forward * (i + 1) * 2, Quaternion.identity, transform);
		}
    }



}
