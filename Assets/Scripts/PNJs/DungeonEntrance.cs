using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntrance : MonoBehaviour
{
	public GameObject UI;
	GameObject player;

	bool UIvisible = false;

	private void Start()
	{
		player = FindObjectOfType<CharaController>().gameObject;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && !UI.activeSelf)
			DisplayUI();
	}

	public void DisplayUI()
	{
		player.GetComponent<CharaController>().freezing = !player.GetComponent<CharaController>().freezing;
		StartCoroutine(DesactiveUI());

	}

	IEnumerator DesactiveUI()
	{
		yield return new WaitForSeconds(.1f);
		UI.SetActive(!UI.activeSelf);
	}
}
