using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
	[Header("First Area")]
	public List<GameObject> LeftEntrances;
	public List<GameObject> upEntrances;
	public List<GameObject> rightEntrances;
	public List<GameObject> downEntrances;

	[Header ("First Area Ends")]
	public List<GameObject> leftEnds;
	public List<GameObject> upEnds;
	public List<GameObject> downEnds;
	public List<GameObject> rightEnds;
	public GameObject filler;

	[Header ("Dungeon Properties")]
	public List<GameObject> spawnedRooms;

	public int dungeonSize = 15;
	public bool seedGenerated = false;

	public string seed = "";
}
