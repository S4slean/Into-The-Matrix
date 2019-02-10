﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
	public enum OpeningDirection
	{
		Left,
		Top,
		Right,
		Bottom
	}

	public OpeningDirection openingdir;
	bool spawned = false;
	RoomTemplates roomTemplates;
	GameObject filler;
	float delay;

	private void Start()
	{
		roomTemplates = FindObjectOfType<RoomTemplates>();
		filler = roomTemplates.filler;

		if (openingdir == OpeningDirection.Left)
			delay = .02f;
		if (openingdir == OpeningDirection.Top)
			delay = .03f;
		if (openingdir == OpeningDirection.Right)
			delay = .05f;
		if (openingdir == OpeningDirection.Bottom)
			delay = .07f;

		Invoke("DefineNeededRoom",.1f);

	}

	private void DefineNeededRoom()
	{

		if (roomTemplates.spawnedRooms.Count > roomTemplates.dungeonSize)
		{
			if (openingdir == OpeningDirection.Left)
				SpawnRoom(roomTemplates.leftEnds);
			if (openingdir == OpeningDirection.Top)
				SpawnRoom(roomTemplates.upEnds);
			if (openingdir == OpeningDirection.Right)
				SpawnRoom(roomTemplates.rightEnds);
			if (openingdir == OpeningDirection.Bottom)
				SpawnRoom(roomTemplates.downEnds);
		}
		else
		{
			if (openingdir == OpeningDirection.Left)
				SpawnRoom(roomTemplates.LeftEntrances);
			if (openingdir == OpeningDirection.Top)
				SpawnRoom(roomTemplates.upEntrances);
			if (openingdir == OpeningDirection.Right)
				SpawnRoom(roomTemplates.rightEntrances);
			if (openingdir == OpeningDirection.Bottom)
				SpawnRoom(roomTemplates.downEntrances);
		}
	}

	private void SpawnRoom(List<GameObject> roomList)
	{
		int roomID;
		if (roomTemplates.seedGenerated == false)
		{
			roomID = UnityEngine.Random.Range(0, roomList.Count);
			roomTemplates.seed += roomID;
		}
		else
		{
			roomID = int.Parse(roomTemplates.seed.Substring(0, 1));
			roomTemplates.seed = roomTemplates.seed.Remove(0, 1);
			print(roomID);
		}

		roomTemplates.spawnedRooms.Add( Instantiate(roomList[roomID], transform.position, Quaternion.identity));
		spawned = true;
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != "RoomSpawner")
		{
			//Instantiate(filler, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
