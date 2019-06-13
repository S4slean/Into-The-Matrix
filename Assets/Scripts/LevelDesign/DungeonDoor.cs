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

	public GameObject LoadingNewDj;
	public GameObject LoadingClassic;

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
		Debug.Log(dJSetupUI.name);
			//StartCoroutine(Wait());
		}
	}

	public void LoadDungeon()
	{

		player.position = new Vector3(0, 0, -7);
		player.rotation = Quaternion.Euler(0, 0, 0);
		if (FindObjectOfType<PlayerStats>().byPass)
		{
			SceneManager.LoadScene(3);
			return;
		}

		if(PlayerPrefs.HasKey("LastDay") && PlayerPrefs.GetInt("LastDay") != System.DateTime.Now.Day || !PlayerPrefs.HasKey("LastDay"))
		{
			Debug.Log("changeLoadScreen");
			loadingScreen.transform.GetChild(0).gameObject.SetActive(true);
			loadingScreen.transform.GetChild(1).gameObject.SetActive(false);

		}
		else
		{
			loadingScreen.transform.GetChild(0).gameObject.SetActive(false);
			loadingScreen.transform.GetChild(1).gameObject.SetActive(true);
		}
		SceneManager.LoadSceneAsync(2);

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
