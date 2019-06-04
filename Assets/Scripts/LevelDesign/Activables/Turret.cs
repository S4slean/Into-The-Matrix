using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	public bool quadDir = false;
	public GameObject projectile;
	public int tickDelay = 10;
	public int tickCount = 0;


	private void Start()
	{
		TickManager.OnTick += delegate (object sender, TickManager.OnTickEventArgs e)
		{
			tickCount++;

			if (tickCount > tickDelay)
			{
				Instantiate(projectile, transform.position + transform.forward + transform.up, transform.rotation);

				if (quadDir)
				{
					Instantiate(projectile, transform.position + transform.up + transform.right, transform.rotation * Quaternion.Euler(0, 90, 0));
					Instantiate(projectile, transform.position + transform.up - transform.right, transform.rotation * Quaternion.Euler(0, -90, 0));
					Instantiate(projectile, transform.position + transform.up - transform.forward, transform.rotation * Quaternion.Euler(0, 180, 0));

				}
				tickCount = 0;
			}
		};
	}

}
