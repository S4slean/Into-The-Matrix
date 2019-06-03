using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonDoor : MonoBehaviour
{
	Transform player;
	public GameObject lifeBar;
	public GameObject loadingScreen;
	public GameObject dJSetupUI;
	public GameObject minimapBck;

	public List<GameObject> availableTP;

	private void Start()
	{
		player = FindObjectOfType<CharaController>().transform;
		loadingScreen = GameObject.FindGameObjectWithTag("Loading");
		lifeBar = GameObject.FindGameObjectWithTag("LifeBar");
		dJSetupUI = Resources.FindObjectsOfTypeAll<DJSetupUI>()[0].gameObject;
		dJSetupUI.GetComponent<DJSetupUI>().DJdoor = this;
		minimapBck = GameObject.FindGameObjectWithTag("minimap");
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && other.GetComponent<DealDamage>() != null)
		{
			player.GetComponent<CharaController>().StartCoroutine(player.GetComponent<CharaController>().FreezePlayer(1));
			dJSetupUI.SetActive(true);
			//StartCoroutine(Wait());
		}
	}

	public void LoadDungeon()
	{

		player.position = new Vector3(0, 0, -7);
		player.rotation = Quaternion.Euler(0, 0, 0);
		SceneManager.LoadScene(1);

	}

	public IEnumerator Wait()
	{
		loadingScreen.GetComponent<Animator>().Play("Appear");
		yield return new WaitForSeconds(1);
		player.GetComponent<PlayerStats>().BackToDungeon();
		LoadDungeon();
	}

	void DisplayDJSetup()
	{
		dJSetupUI.SetActive(true);
		minimapBck.SetActive(true);

	}

	public void SelectRoom(minimapRoom room)
	{
		foreach (minimapRoom rm in FindObjectsOfType<minimapRoom>())
		{
			rm.selected = false;
		}
		room.selected = true;
	}
}
