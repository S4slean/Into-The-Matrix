using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonDoor : MonoBehaviour
{
	Transform player;

	private void Start()
	{
		player = FindObjectOfType<CharaController>().transform;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			LoadDungeon();
		}
	}

	public void LoadDungeon()
	{
		player.position = new Vector3(0, 0, -7);
		player.rotation = Quaternion.Euler(0, 0, 0);
		SceneManager.LoadScene(1);

	}
}
