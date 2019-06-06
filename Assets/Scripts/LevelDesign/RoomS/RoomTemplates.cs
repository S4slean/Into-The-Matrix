using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RoomTemplates : MonoBehaviour
{
	[Header("First Area")]				//liste de salles de la zone
	public List<GameObject> LeftEntrances;
	public List<GameObject> upEntrances;
	public List<GameObject> rightEntrances;
	public List<GameObject> downEntrances;

	[Header ("First Area Ends")]		//liste de culs de sac
	public List<GameObject> leftEnds;
	public List<GameObject> upEnds;
	public List<GameObject> downEnds;
	public List<GameObject> rightEnds;

	[Header("Safe Rooms")]
	public List<GameObject> leftSafeRooms;
	public List<GameObject> upSaferooms;
	public List<GameObject> rightSafeRooms;
	public List<GameObject> downSafeRooms;

	[Header("AllRooms")]
	public List<GameObject> allRooms;


	[Header ("Dungeon Properties")]			
	public List<GameObject> spawnedRooms;       //liste des salles du donjons
	public List<Vector3> spawnedPos;
	public List<string> spawnedRoomsName;

	public int SafeRoomFrequency = 5;
	public float currentSafeRoomChances = 0;
	[Range(0, 100)] public float safeRoomIncreaseChance = 15;
	public int nonSaferoomspawned = 0;

	public int dungeonSize = 15;						//lorsque le donjon dépasse ce nombre de salle, les prochaines salles instanciés seront des culs de sac
	public bool seedGenerated = false;					// si le donjon est généré par seed

	public string seed = "";
	public string tempSeed = "";

	public bool enemiesRespawn = false;

	public string savePath;
	



	private void Awake()
	{
		AvailableRunes runeList = FindObjectOfType<AvailableRunes>();

		if (PlayerPrefs.HasKey("LastDay"))
		{
			if (PlayerPrefs.GetInt("LastDay") != System.DateTime.Now.Day)
			{
				Debug.Log("new Dungeon !");
				Debug.Log(System.DateTime.Now.Day);
				seedGenerated = false;
				FindObjectOfType<minimap>().ClearMap();
				FindObjectOfType<PlayerStats>().overrides.Clear();

			}
			else
			{
				seedGenerated = true;
				seed = PlayerPrefs.GetString("Seed");
				tempSeed = seed;
				Debug.Log("same Dungeon !");
				LoadDungeon();

			}
		}
		else
		{
			Debug.Log("new Dungeon !");
			Debug.Log(System.DateTime.Now.Day);
			seedGenerated = false;
			FindObjectOfType<minimap>().ClearMap();
			FindObjectOfType<PlayerStats>().overrides.Clear();
		}

		foreach (Rune rune in runeList.equippedRunes)
		{
			rune.Active();
			
		}

		StartCoroutine(activeSeed());
	}

	Component CopyComponent(Component original, GameObject destination)
	{
		System.Type type = original.GetType();
		Component copy = destination.AddComponent(type);
		// Copied fields can be restricted with BindingFlags
		System.Reflection.FieldInfo[] fields = type.GetFields();
		foreach (System.Reflection.FieldInfo field in fields)
		{
			field.SetValue(copy, field.GetValue(original));
		}
		return copy;
	}

	public IEnumerator activeSeed()
	{
		yield return new WaitForSeconds(20);

		if (seedGenerated)
			yield break;

		int day = System.DateTime.Now.Day;
		PlayerPrefs.SetInt("LastDay", day);
		PlayerPrefs.SetString("Seed", seed);
		PlayerPrefs.Save();
		Debug.Log(PlayerPrefs.GetInt("LastDay"));
		seedGenerated = true;
		SaveDungeon();

		Debug.Log("DungeonSaved");
	}


	public void SaveDungeon()
	{
		Dungeon dj = new Dungeon();
		dj.rooms = spawnedRooms;
		dj.roomNames = spawnedRoomsName;
		dj.roomPos = spawnedPos;

#if UNITY_EDITOR
		savePath = Application.dataPath;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR

		savePath = Application.persistentDataPath;
#endif

		string json = JsonUtility.ToJson(dj);
		File.WriteAllText(savePath + "/dungeon.json", json);
		Debug.Log("dungeon saved !");
		Debug.Log(FindObjectsOfType<RoomTemplates>().Length);
	}

	public void LoadDungeon()
	{
		Dungeon dj = new Dungeon();


#if UNITY_EDITOR

		savePath = Application.dataPath;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR

		savePath = Application.persistentDataPath;
#endif

		string json = File.ReadAllText(savePath + "/dungeon.json");
		JsonUtility.FromJsonOverwrite(json, dj);


		for (int i = 0; i < dj.roomNames.Count; i++)
		{
			for(int j = 0; j < allRooms.Count; j++)
			{
				if(dj.roomNames[i] == allRooms[j].name)
				{
					Instantiate(allRooms[j], dj.roomPos[i], Quaternion.identity);

				}
			}
		}

		Debug.Log("dungeon loaded !");


		FindObjectOfType<PlayerStats>().ExecuteOverride();
		FindObjectOfType<PlayerStats>().SetStartPos();

	}

	public class Dungeon
	{
		public List<GameObject> rooms;
		public List<string> roomNames;
		public List<Vector3> roomPos;
	}
}
