using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class RoomCameraTrigger : MonoBehaviour
{

	public GameObject virtualCam;
	public Sprite minimapSprite;
	public List<SpawnEnnemis> enemySpawn;
	GameObject minimap;
	GameObject minimapRoomPrefab;
	RoomTemplates roomTemplate;
	TempsPlongee timeBar;
	GameObject grid;

	private void Start()
	{
		minimap = GameObject.FindObjectOfType<minimap>().gameObject;
		minimapRoomPrefab = Resources.Load("minimapRoom") as GameObject;
		roomTemplate = FindObjectOfType<RoomTemplates>();
		timeBar = FindObjectOfType<TempsPlongee>();

		
	}

	//Si le joueur entre dans le trigger: désactive la virtual cam actuelle et active celle de la salle
	private void OnTriggerEnter(Collider other)
	{


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

			GameObject instance = Instantiate(minimapRoomPrefab, minimap.transform);
			instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.position.x * 21 / 14, transform.position.z * 31.5f / 20);
			instance.GetComponent<Image>().sprite = minimapSprite;

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
}
