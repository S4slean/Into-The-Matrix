using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneUI : MonoBehaviour
{
	public GameObject minimapAnchor;
	private GameObject instance;


	private void OnEnable()
	{
		GameObject minimap = Resources.FindObjectsOfTypeAll<DJSetupUI>()[0].gameObject;
		instance = Instantiate(minimap, minimapAnchor.transform);
		minimapRoom[] rooms = instance.GetComponentsInChildren<minimapRoom>();

		foreach(minimapRoom rm in rooms)
		{
			rm.isSelectable = true;
		}
	}

	public void SelectRoom(minimapRoom room)
	{
		foreach (minimapRoom rm in FindObjectsOfType<minimapRoom>())
		{
			rm.selected = false;
		}
		room.selected = true;
	}

	private void OnDisable()
	{
		Destroy(instance);
	}



}
