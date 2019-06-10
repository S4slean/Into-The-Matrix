using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
	public List<GameObject> bullets;
	int i;
	Transform parent;

	public void Start()
	{

	}

	public GameObject GetBullet()
	{

		i++;
		if (i > bullets.Count-1)
			i = 0;

		bullets[i].SetActive(true);
		return bullets[i];
	}
}
