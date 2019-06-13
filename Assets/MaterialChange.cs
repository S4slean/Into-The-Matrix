using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{

	public List<GameObject> objects;
	public Material green;
	public Material red;
	Renderer rend;


    public void ChangeMat(Material mat)
	{ 

		foreach (GameObject obj in objects)
		{
			rend = obj.GetComponent<Renderer>();
			rend.material = mat;
		}
	}
}
