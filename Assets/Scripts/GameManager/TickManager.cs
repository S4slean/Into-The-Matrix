using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour
{
	public static float tickDuration = 0.33f;
	CharaController player;
	public static float tick = 0;

    // Start is called before the first frame update
    void Start()
    {
		player = FindObjectOfType<CharaController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
		tick += Time.deltaTime;
        if(tick >= tickDuration+0.03f)
		{
			tick = 0;
		}

    }
}
