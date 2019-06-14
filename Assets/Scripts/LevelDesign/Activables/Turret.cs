using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Turret : MonoBehaviour
{

	public BulletPool pool;
	public GameObject projectile;
	private int tickDelay = 5;
	private int tickCount = 0;
    public testson SoundDj;


    private void Start()
	{
        SoundDj = GameObject.FindGameObjectWithTag("SoundDj").GetComponent<testson>();
        TickManager.OnTick += AddCount;
		pool = GetComponentInChildren<BulletPool>();
	}

	public void AddCount()
	{
		tickCount++;
	}

	private void Update()
	{

			if (tickCount >= tickDelay)
			{
           // SoundDj.FireEnemie.Play();
				projectile = pool.GetBullet();
				projectile.transform.position = transform.position + transform.forward * 1.2f + transform.up;
			projectile.transform.rotation = transform.rotation;
				tickCount = 0;
			}
		
	}

	public void Kill()
	{
        SoundDj.Deathrobot.Play();
		Destroy(gameObject);
	}
}
