using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap : MonoBehaviour
{
	public void ClearMap()
	{

		foreach (Transform child in transform)
		{
			if (child.GetSiblingIndex() != 0)
				Destroy(child.gameObject);

		}
	}

}
