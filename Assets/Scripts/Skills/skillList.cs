using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillList : MonoBehaviour
{
	public List<Skill> skills;

	private void Start()
	{
		if (FindObjectsOfType<skillList>().Length > 1)
			Destroy(this.gameObject);

		DontDestroyOnLoad(this.gameObject);
	}
}
