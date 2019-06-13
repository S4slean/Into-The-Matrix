using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;


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

	public bool isTimeSafe = false;

	

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


		if(other.tag == "Player" )
		{
			if (other.GetComponent<DealDamage>() != null || other.GetComponent<Projectile>() != null)
				return;

            other.GetComponent<PlayerStats>().squareRoomEntered = new Vector3(Mathf.Ceil(other.transform.position.x / 2) * 2, 0, (Mathf.Ceil((other.transform.position.z) / 2) * 2) - 1) ;

			foreach(SpawnEnnemis spawner in enemySpawn)
			{
				spawner.Spawn();
			}


			if(Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera != null)
				Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
			virtualCam.SetActive(true);

			if (!alreadyDraw) {
				foreach (GameObject room in GameObject.FindGameObjectsWithTag("minimapRoom"))
				{
					if (room.GetComponent<RectTransform>().anchoredPosition == new Vector2(transform.position.x * 21 / 14, transform.position.z * 31.5f / 20))
						alreadyDraw = true;
			} }

			if (!alreadyDraw && SceneManager.GetActiveScene().name.Contains("Donjon"))	
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

		if (FindObjectOfType<RoomTemplates>() == null)
			return;

		//if (roomTemplate.enemiesRespawn)
		//{
		//	foreach(SpawnEnnemis spawner in enemySpawn)
		//	{
		//		if(spawner.spawned == false)
		//			spawner.Spawn();
		//	}
		//}
	}


	public void ApplyOverride(int i)
	{
		if (i == 0)
			OverrideTraps();
		else if (i == 1)
			OverrideEnemies();
		else if (i == 2)
			OverrideSpawn();


	}

	public void OverrideTraps()
	{
		foreach(GameObject trap in traps)
		{
			if(trap.GetComponent<Spikes>() != null)
			{
				trap.GetComponent<Spikes>().Override();
				Debug.Log("spikes overided");
			}
			else if(trap.GetComponent<LightningGate>()  != null)
			{
				trap.GetComponent<LightningGate>().Override();
				Debug.Log("gate overided");
			}

		}

		Debug.Log("traps overrided");
	}

	public void OverrideEnemies()
	{
		foreach(GameObject enmy in enemies)
		{
			Destroy(enmy);
		}

		Debug.Log("enemies overrided ! ");
	}

	public void OverrideSpawn()
	{
		Instantiate(Resources.Load("LD/Spawner") as GameObject, transform.position, Quaternion.identity);
		Destroy(transform.parent.gameObject);
	}

    public IEnumerator GetSquareEntered()
    {
        yield return new WaitForSecondsRealtime(TickManager.tickDuration/2);

    }
}
