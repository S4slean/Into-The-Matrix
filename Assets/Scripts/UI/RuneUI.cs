using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneUI : MonoBehaviour
{
	public GameObject minimapAnchor;
	private GameObject instance;
	minimapRoom selectedRoom;
	PlayerStats stats;
	public Text trapUse;
	public Text enmyUse;
	public Text spawnerUse;

	public GameObject noOvrd;
	public GameObject rmOvrdd;
	public GameObject noRmSelected;

	private void Start()
	{
		stats = FindObjectOfType<PlayerStats>();
	}

	private void OnEnable()
	{
		stats = FindObjectOfType<PlayerStats>();
		FindObjectOfType<CharaController>().enabled = false;

		GameObject minimap = Resources.FindObjectsOfTypeAll<minimap>()[0].gameObject;
		instance = Instantiate(minimap, minimapAnchor.transform);
		instance.SetActive(true);
		minimapRoom[] rooms = instance.GetComponentsInChildren<minimapRoom>();

		trapUse.text = stats.trapOvrd.ToString();
		enmyUse.text = stats.enmyOvrd.ToString();
		spawnerUse.text = stats.phoneOvrd.ToString();

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
		FindObjectOfType<CharaController>().enabled = true;
		Destroy(instance);
	}

	public void UseOverride(int i)
	{

		if(selectedRoom == null)
		{
			Debug.Log("No room Selected ! ");
			noRmSelected.SetActive(true);
			return;
		}
		DungeonOverride ovrd = new DungeonOverride();
		ovrd.overridesIndex = i;
		ovrd.spritePos = selectedRoom.GetComponent<RectTransform>().anchoredPosition;
		Debug.Log(new Vector3(ovrd.spritePos.x / 21 * 14, 0, ovrd.spritePos.y / 31.5f * 20));
		stats.overrides.Add(ovrd);
		Debug.Log("overrides stacks : " + stats.overrides.Count);
		rmOvrdd.SetActive(true);
	}


	public void OvrTrap()
	{
		if(stats.trapOvrd  <= 0)
		{
			noOvrd.SetActive(true);
		}
		else
		{
			stats.trapOvrd--;
			PlayerPrefs.SetInt("trapOvrd", stats.trapOvrd);
			trapUse.text = stats.trapOvrd.ToString();
			UseOverride(0);
		}
	}

	public void OvrEnmy()
	{
		if(stats.enmyOvrd <= 0)
		{
			noOvrd.SetActive(true);
		}
		else
		{
			stats.enmyOvrd--;
			PlayerPrefs.SetInt("enmyOvrd", stats.enmyOvrd);
			enmyUse.text = stats.enmyOvrd.ToString();
			UseOverride(1);
		}
	}

	public void OvrSpawn()
	{
		if(stats.phoneOvrd <= 0)
		{
			noOvrd.SetActive(true);
		}
		else
		{
			stats.phoneOvrd--;
			PlayerPrefs.SetInt("spawnOvrd", stats.phoneOvrd);
			spawnerUse.text = stats.phoneOvrd.ToString();
			UseOverride(2);
		}
	}

}
