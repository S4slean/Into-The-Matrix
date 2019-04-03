using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


	[Header ("Dungeon Properties")]			
	public List<GameObject> spawnedRooms;       //liste des salles du donjons

	public int SafeRoomFrequency = 5;
	public float currentSafeRoomChances = 0;
	[Range(0, 100)] public float safeRoomIncreaseChance = 15;
	public int nonSaferoomspawned = 0;

	public int dungeonSize = 15;						//lorsque le donjon dépasse ce nombre de salle, les prochaines salles instanciés seront des culs de sac
	public bool seedGenerated = false;					// si le donjon est généré par seed

	public string seed = "";

	public bool enemiesRespawn = false;


	private void Awake()
	{
		AvailableRunes runeList = FindObjectOfType<AvailableRunes>();
		
		foreach(Rune rune in runeList.equippedRunes)
		{
			rune.Active();
		}


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
}
