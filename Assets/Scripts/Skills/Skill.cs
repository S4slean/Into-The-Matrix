using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Skill : MonoBehaviour
{
	public new string name;
	public Image image;
	public string description;
	public int cost;
	public int coolDown;
	public bool requireTarget = false;


	public virtual void Activate()
	{

	}
	
}
