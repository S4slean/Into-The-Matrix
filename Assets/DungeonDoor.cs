using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonDoor : MonoBehaviour
{
	Transform player;
	public GameObject lifeBar;
	public GameObject loadingScreen;

	private void Start()
	{
		player = FindObjectOfType<CharaController>().transform;
		loadingScreen = GameObject.FindGameObjectWithTag("Loading");
		lifeBar = GameObject.FindGameObjectWithTag("LifeBar");
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			StartCoroutine(Wait());
		}
	}

	public void LoadDungeon()
	{

		player.position = new Vector3(0, 0, -7);
		player.rotation = Quaternion.Euler(0, 0, 0);
		SceneManager.LoadScene(1);

	}

	IEnumerator Wait()
	{
		loadingScreen.GetComponent<Animator>().Play("Appear");
		yield return new WaitForSeconds(1);
		LoadDungeon();
	}
}
