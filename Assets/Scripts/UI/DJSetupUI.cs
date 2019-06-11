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
	MoneyBank money;

	private void Start()
	{
		DJdoor = FindObjectOfType<DungeonDoor>();
		money = FindObjectOfType<MoneyBank>();
	}

	private void OnEnable()
	{
		Debug.Log("Ah");
		GenerateMap();
	}

	private void GenerateMap()
	{
		instance = Instantiate(minimap, mapAnchor.transform);
		instance.SetActive(true);
		instance.GetComponent<minimap>().enabled = false;
		if (instance.GetComponentsInChildren<minimapRoom>().Length > 0)
			instance.GetComponentsInChildren<minimapRoom>()[0].selected = true;
	}

	private void OnDisable()
	{
		Destroy(instance);
	}

	public void Teleport()
	{
		DJdoor.StartCoroutine(DJdoor.Wait());
		CharaController player = FindObjectOfType<CharaController>();
		player.GetComponent<PlayerStats>().BackToDungeon();
		player.StartCoroutine(player.FreezePlayer(2));
		gameObject.SetActive(false);
	}

	public void RerollDJ()
	{
		if(money.BankMoney > 200)
		{
			money.BankMoney -= 200;
			money.ActualizeBankMoney();
			minimap.GetComponent<minimap>().ClearMap();
			Destroy(instance);
			GenerateMap();

			PlayerPrefs.DeleteKey("LastDay");
			Debug.Log("Donjon rerolled !");
		}
		else
		{
			//Play anim : not enough money
			Debug.Log("not enough money ! ");
		}
	}

	public void Return()
	{
		CharaController player = FindObjectOfType<CharaController>();
		player.StartCoroutine(player.FreezePlayer(1));
		gameObject.SetActive(false);
	}
}
