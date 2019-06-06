using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneUI : MonoBehaviour
{
	public GameObject minimapAnchor;
	private GameObject instance;
	minimapRoom selectedRoom;
	PlayerStats stats;

	private void Start()
	{
		stats = FindObjectOfType<PlayerStats>();
	}

	private void OnEnable()
	{
		GameObject minimap = Resources.FindObjectsOfTypeAll<minimap>()[0].gameObject;
		instance = Instantiate(minimap, minimapAnchor.transform);
		instance.SetActive(true);
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
		selectedRoom = room; 
	}

	private void OnDisable()
	{
		Destroy(instance);
	}

	public void UseOverride(int i)
	{
		if(selectedRoom == null)
		{
			Debug.Log("No room Selected ! ");
			return;
		}
		DungeonOverride ovrd = new DungeonOverride();
		ovrd.overridesIndex = i;
		ovrd.spritePos = selectedRoom.GetComponent<RectTransform>().anchoredPosition;
		Debug.Log(new Vector3(ovrd.spritePos.x / 21 * 14, 0, ovrd.spritePos.y / 31.5f * 20));
		stats.overrides.Add(ovrd);
		Debug.Log("overrides stacks : " + stats.overrides.Count);
	}


}
