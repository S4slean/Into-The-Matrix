using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DJSetupUI : MonoBehaviour
{
	public DungeonDoor DJdoor;
	public GameObject minimap;
	public GameObject mapAnchor;
	GameObject instance;

	private void Start()
	{
		DJdoor = FindObjectOfType<DungeonDoor>();
	}

	private void OnEnable()
	{
		instance = Instantiate(minimap, mapAnchor.transform);
		instance.SetActive(true);
	}

	private void OnDisable()
	{
		Destroy(instance);
	}

	public void Teleport()
	{
		DJdoor.StartCoroutine(DJdoor.Wait());
		CharaController player = FindObjectOfType<CharaController>();
		player.StartCoroutine(player.FreezePlayer(1));
		gameObject.SetActive(false);
	}

	public void Return()
	{
		CharaController player = FindObjectOfType<CharaController>();
		player.StartCoroutine(player.FreezePlayer(1));
		gameObject.SetActive(false);
	}
}
