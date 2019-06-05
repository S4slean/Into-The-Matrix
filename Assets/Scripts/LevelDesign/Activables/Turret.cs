using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Turret : MonoBehaviour
{
	public bool quadDir = false;
	public GameObject projectile;
	private int tickDelay = 5;
	public int tickCount = 0;


	private void Start()
	{

		TickManager.OnTick += Fire;
	}

	private void Fire()
	{
			tickCount++;
	}

	private void Update()
	{
		if (tickCount >= tickDelay)
		{
			Instantiate(projectile, transform.position + transform.forward*1.2f + transform.up, transform.rotation);
			tickCount = 0;
		}
	}

}
