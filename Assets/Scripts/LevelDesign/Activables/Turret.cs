using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Turret : MonoBehaviour
{
	public bool quadDir = false;
	public GameObject projectile;
	private int tickDelay = 5;
	private int tickCount = 0;


	private void Start()
	{
		TickManager.OnTick += AddCount;
	}

	public void AddCount()
	{
		tickCount++;
	}

	private void Update()
	{

			if (tickCount >= tickDelay)
			{
				Instantiate(projectile, transform.position + transform.forward*1.2f + transform.up, transform.rotation);

				if (quadDir)
				{
					Instantiate(projectile, transform.position + transform.up + transform.right, transform.rotation * Quaternion.Euler(0, 90, 0));
					Instantiate(projectile, transform.position + transform.up - transform.right, transform.rotation * Quaternion.Euler(0, -90, 0));
					Instantiate(projectile, transform.position + transform.up - transform.forward, transform.rotation * Quaternion.Euler(0, 180, 0));

				}
				tickCount = 0;
			}
		
	}
}
