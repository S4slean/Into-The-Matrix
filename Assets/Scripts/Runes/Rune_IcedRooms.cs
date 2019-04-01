using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune_IcedRooms : Rune
{

	public List<GameObject> LeftIcedRooms;
	public List<GameObject> RightIcedRooms;
	public List<GameObject> UpIcedRooms;
	public List<GameObject> DownIcedRooms;


	// Start is called before the first frame update
	void Awake()
    {
		roomTemplate.LeftEntrances.AddRange(LeftIcedRooms);
		roomTemplate.rightEntrances.AddRange(RightIcedRooms);
		roomTemplate.upEntrances.AddRange(UpIcedRooms);
		roomTemplate.downEntrances.AddRange(DownIcedRooms);
    }
}
