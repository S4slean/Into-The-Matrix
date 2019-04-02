using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillList : MonoBehaviour
{
	public List<Skill> skills;

	private void Start()
	{
		DontDestroyOnLoad(this.gameObject);
	}
}
