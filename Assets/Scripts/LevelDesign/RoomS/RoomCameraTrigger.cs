﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class RoomCameraTrigger : MonoBehaviour
{
	public bool isTP = false;
	public GameObject virtualCam;
	public Sprite minimapSprite;
	public List<SpawnEnnemis> enemySpawn;
	public bool alreadyDraw = false;
	GameObject minimap;
	GameObject minimapRoomPrefab;
	RoomTemplates roomTemplate;
	TempsPlongee timeBar;
	GameObject grid;

	

	public List<GameObject> traps;
	public List<GameObject> enemies;

	private void Start()
	{
		minimap = Resources.FindObjectsOfTypeAll<minimap>()[0].gameObject;
		minimapRoomPrefab = Resources.Load("minimapRoom") as GameObject;
		roomTemplate = FindObjectOfType<RoomTemplates>();
		timeBar = FindObjectOfType<TempsPlongee>();

		
	}

	//Si le joueur entre dans le trigger: désactive la virtual cam actuelle et active celle de la salle
	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Spikes>() != null || other.GetComponent<LightningGate>() != null)
		{
			traps.Add(other.gameObject);
		}

		if(other.GetComponent<SimpleEnemy>() != null)
		{
			enemies.Add(other.gameObject);
		}

		if(other.tag == "Player")
		{


			foreach(SpawnEnnemis spawner in enemySpawn)
			{
				spawner.Spawn();
			}

			timeBar.plongee = false;

			if(Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera != null)
				Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
			virtualCam.SetActive(true);

			if (!alreadyDraw) {
				foreach (GameObject room in GameObject.FindGameObjectsWithTag("minimapRoom"))
				{
					if (room.GetComponent<RectTransform>().anchoredPosition == new Vector2(transform.position.x * 21 / 14, transform.position.z * 31.5f / 20))
						alreadyDraw = true;
			} }

			if (!alreadyDraw)
			{
				GameObject instance = Instantiate(minimapRoomPrefab, minimap.transform);
				instance.GetComponent<minimapRoom>().isTP = isTP;
				instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.position.x * 21 / 14, transform.position.z * 31.5f / 20);
				instance.GetComponent<Image>().sprite = minimapSprite;
			}

			minimap.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.position.x * 21 / 14, transform.position.z * 31.5f / 20);
			minimap.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-transform.position.x * 21 / 14, -transform.position.z * 31.5f / 20);
		}
	}

	private void OnTriggerExit(Collider other)
	{

		timeBar.plongee = true;

		if (FindObjectOfType<RoomTemplates>() == null)
			return;

		if (roomTemplate.enemiesRespawn)
		{
			foreach(SpawnEnnemis spawner in enemySpawn)
			{
				if(spawner.spawned == false)
					spawner.Spawn();
			}
		}
	}

	public void OverrideTraps()
	{
		foreach(GameObject trap in traps)
		{
			if(trap.GetComponent<Spikes>() != null)
			{

			}
			else if(trap.GetComponent<LightningGate>()  != null)
			{

			}

		}
	}

	public void OverrideEnemies()
	{
		foreach(GameObject enmy in enemies)
		{
			Destroy(enmy);
		}
	}

	public void OverrideSpawn()
	{
		Instantiate(Resources.Load("Resources/LD/Spawner") as GameObject, transform.position, Quaternion.identity);
		Destroy(transform.parent);
	}
}
