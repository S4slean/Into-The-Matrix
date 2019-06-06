using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TickManager : MonoBehaviour
{
	public delegate void Tick();
	public static event Tick OnTick;

	public static float tickDuration = 0.33f;
	CharaController player;
	public static float tick = 0;

    // Start is called before the first frame update
    void Start()
    {
		player = FindObjectOfType<CharaController>();
    }

    // Update is called once per frame
    void Update()
    {
		tick += Time.deltaTime;
        if(tick >= tickDuration+0.03f)
		{
			tick -= tickDuration;
			OnTick();
		}

    }

	public static void ClearDelegate()
	{
		OnTick = null;
		FindObjectOfType<CharaController>().GetPlayerInTick(); 
	}
}
