using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour
{
	public static float tickDuration = 0.15f;
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
        if(tick > tickDuration)
		{
			tick = 0;
		}

		tick += Time.deltaTime;
    }
}
