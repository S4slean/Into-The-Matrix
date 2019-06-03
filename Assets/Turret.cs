using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	public bool quadDir = false;
	public GameObject projectile;
	public int tickDelay = 5;
	public int tickCount = 0;

    // Update is called once per frame
    void Update()
    {
		if (TickManager.tick > TickManager.tickDuration)
		{
			//tickCount++;


			//if(tickCount > tickDelay)
			//{
				Instantiate(projectile, transform.position + transform.forward + transform.up, transform.rotation);

				if (quadDir)
				{
					Instantiate(projectile, transform.position + transform.up + transform.right, transform.rotation * Quaternion.Euler(0,90,0));
					Instantiate(projectile, transform.position + transform.up + transform.right, transform.rotation * Quaternion.Euler(0, -90, 0));
					Instantiate(projectile, transform.position + transform.up + transform.right, transform.rotation * Quaternion.Euler(0, 180, 0));

				}
				tickCount = 0;
			}
		}

	}
//}
