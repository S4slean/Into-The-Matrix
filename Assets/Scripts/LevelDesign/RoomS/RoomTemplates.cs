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


	[Header ("Dungeon Properties")]			
	public List<GameObject> spawnedRooms;		//liste des salles du donjons

	public int dungeonSize = 15;						//lorsque le donjon dépasse ce nombre de salle, les prochaines salles instanciés seront des culs de sac
	public bool seedGenerated = false;					// si le donjon est généré par seed

	public string seed = "";

	public bool enemiesRespawn = false;



	private void Awake()
	{
		AvailableRunes runes = FindObjectOfType<AvailableRunes>();

		for(int i = 0; i < runes.equippedRunes.Count; i++)
		{
			gameObject.AddComponent(runes.equippedRunes[i].GetType());
				
		}

	}
}
