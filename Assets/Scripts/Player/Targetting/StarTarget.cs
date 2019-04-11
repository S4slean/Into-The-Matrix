using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTarget : SelectionArea
{
	public int distance;
	public GameObject tier1;
	public GameObject tier2;
	public GameObject tier3;
    public GameObject tier4;
    public GameObject tier5;

    // Start is called before the first frame update
    void Start()
    {
		if (distance > 0)
			tier1.SetActive(true);
		if (distance > 1)
			tier2.SetActive(true);
		if (distance > 2)
			tier3.SetActive(true);
        if (distance > 3)
            tier4.SetActive(true);
        if (distance > 4)
            tier5.SetActive(true);
    }
}
