using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
	private void Awake()
	{
		if (GameObject.Find(gameObject.name) != gameObject)
			Destroy(gameObject);
		else
			DontDestroyOnLoad(this.gameObject);
	}
}
